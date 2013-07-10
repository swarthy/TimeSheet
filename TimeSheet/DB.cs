using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Reflection;

namespace TimeSheet
{
    public static class DB
    {
        private static FbConnection connection = GetNewConnection;
        internal static FbConnection Connection
        {
            get
            {   
                if (connection.State != System.Data.ConnectionState.Open)
                    try
                    {
                        connection.Open();
                    }
                    catch
                    {
                        connection = GetNewConnection;
                        connection.Open();
                    }
                return connection;
            }
        }
        public static FbConnection GetNewConnection
        {
            get
            {
                FbConnection c = new FbConnection("UserID=SYSDBA;Password=masterkey;" +
                                  "Database=c:/FBDB.FDB;" +
                                  "DataSource=localhost;Charset=NONE;");
                c.StateChange += new System.Data.StateChangeEventHandler((sender, args) => {
                    switch (args.CurrentState)
                    {
                        case System.Data.ConnectionState.Broken:
                            if (args.OriginalState == System.Data.ConnectionState.Open)
                            {
                                c.Close();
                                c.Open();
                            }
                            break;                        
                    }

                });
                return c;
            }
        }        
        public static object Query(string query)
        {
            FbCommand command = new FbCommand(query, DB.Connection);
            return command.ExecuteScalar();
        }
    }
    public class DBEventArgs<T> : EventArgs
    {
        private readonly T data;
        private readonly string fieldInDB;
        public DBEventArgs(T data, string fieldInDB)
        {
            this.data = data;
            this.fieldInDB = fieldInDB;
        }
        public T GetData
        {
            get
            {
                return data;
            }
        }
        public string GetFieldInDBName
        {
            get
            {
                return fieldInDB;
            }
        }
    }
    public class DBList<T> : List<T> where T: Domain, new()
    {
        public DBList(string FieldInDB = "")
            : base()
        {
            this.FieldInDB = FieldInDB;
        }
        new public T this[int i]
        {
            get
            {
                return base[i];
            }
            set
            {
                base[i] = value;
                if (OnChange != null)
                    OnChange(this, new DBEventArgs<T>(base[i], FieldInDB));
            }
        }
        new public void RemoveAt(int i)
        {
            Remove(this[i]);
            base.RemoveAt(i);
        }
        public string FieldInDB = "";
        public event EventHandler<DBEventArgs<T>> OnAdd, OnRemove, OnChange;        
        new public void Add(T item)
        {
            if (OnAdd != null)
                OnAdd(this, new DBEventArgs<T>(item, FieldInDB));
            base.Add(item);
        }
        public void Clear(bool delete_from_db = false)
        {
            for (int i = 0; i < Count; i++)
                Remove(this[i], delete_from_db);
            base.Clear();
        }
        public void Remove(T item, bool delete_removable_object = false)
        {
            if (OnRemove != null)
                OnRemove(this, new DBEventArgs<T>(item, FieldInDB));
            base.Remove(item);
            if (delete_removable_object)
                item.Delete();
        }     
    }
    public struct Link
    {
        public Link(string FieldInDB, Type DomainType)
        {
            Type = DomainType;
            this.FieldInDB = FieldInDB;
        }
        public string FieldInDB;
        public Type Type;
    }
    public class Domain
    {        
        /// <summary>
        /// Словарь, содержащий пары [Поле|Значение]
        /// </summary>
        protected Dictionary<string, object> Fields = new Dictionary<string, object>();
        /// <summary>
        /// Список имен полей
        /// </summary>
        public static List<string> FieldNames = new List<string>();
        public static Dictionary<string, Link> has_one = new Dictionary<string, Link>();
        public static Dictionary<string, Link> has_many = new Dictionary<string, Link>();
        protected Dictionary<string, bool> H1fetched = new Dictionary<string, bool>();
        protected Dictionary<string, bool> HMfetched = new Dictionary<string, bool>();        
        /// <summary>
        /// Имя таблицы
        /// </summary>
        public static string tableName = "";

        bool changed = true;
        public bool _Changed { get { return changed; } private set { changed = value; } }
        public object this[string key]
        {
            get
            {
                if (GetHasOne(GetType()).ContainsKey(key) || GetHasMany(GetType()).ContainsKey(key))
                    throw new Exception("You can't get \"Domain\" type field from here. Use H1/HM(\"domain_field_name\") method for this type of fields");
                return Fields[key.ToUpper()];
            }
            set
            {
                Fields[key.ToUpper()] = value;
                _Changed = true;
            }
        }
        public T H1<T>(string key, bool getFromServer = false) where T: Domain, new()
        {
            if (GetHasOne(GetType()).ContainsKey(key))
                return FetchHasOne<T>(key, getFromServer);
            else
                throw new Exception("You can't get field, which is not a Domain object from here");
        }
        public DBList<T> HM<T>(string key, bool getFromServer = false) where T: Domain, new()
        {
            if (GetHasMany(GetType()).ContainsKey(key))
                return FetchHasMany<T>(key, getFromServer);
            else
                throw new Exception("You can't get field, which is not a Domain object from here");
        }
        /// <summary>
        /// Поле ID (В БД поле должно называться аналогично)
        /// </summary>
        public int ID
        {
            get
            {
                return (int)this["ID"];
            }
            set
            {
                this["ID"] = value;                
            }
        }
        public Domain()
        {
        }
        /// <summary>
        /// Констурктор с инициализацией ключей в словаре Fields
        /// </summary>
        /// <param name="type"></param>
        public Domain(Type type)
        {
            List<string> field_names = GetFieldNames(type);
            field_names.ForEach(fn => Fields[fn] = null);            
            GetHasOne(type).Keys.ToList().ForEach(k => { H1fetched[k] = false; });
            GetHasMany(type).Keys.ToList().ForEach(k => HMfetched[k] = false);
        }        
        /// <summary>
        /// Инициализация класса перед началом работы. Заполнение списка, содержащего имена полей в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Initialize<T>() where T: Domain, new()
        {
            Type type = typeof(T);            
            List<string> field_names = GetFieldNames(type);
            type.GetProperties().ToList().Where(pi => pi.Name != "Item" &&pi.Name[0]!='_' && !pi.PropertyType.IsGenericType && !pi.PropertyType.IsSubclassOf(typeof(Domain))).ToList().ForEach(pi => field_names.Add(pi.Name.ToUpper()));
            GetHasOne(type).Values.ToList().ForEach(k => { field_names.Add(k.FieldInDB); });
        }        
        protected T FetchHasOne<T>(string key, bool refetch = false) where T: Domain, new()
        {
            Dictionary<string, Link> belongsto = GetHasOne(GetType());
            if (typeof(T) != belongsto[key].Type)
                throw new Exception("Fetch error: Type mismatch");//на случай, если в коде будет ошибка
            if (belongsto.Count > 0 && Fields["ID"] != null && (!H1fetched[key] || refetch))
            {
                if (this[belongsto[key].FieldInDB].ToString() == "")
                    return null;
                var temp = Domain.F_All<Domain>("ID = " + this[belongsto[key].FieldInDB], true, belongsto[key].Type)[0].ParseTo<T>();
                Fields[key] = temp;
                H1fetched[key] = true;
                return temp;
            }
            else
                if (H1fetched[key])
                    return Fields[key] as T;
                else
                    throw new Exception("Can't get \"has one\" field");
        }
        protected DBList<T> FetchHasMany<T>(string key, bool refetch = false)where T: Domain, new()
        {
            Dictionary<string, Link> hasmany = GetHasMany(GetType());
            if (typeof(T) != hasmany[key].Type)
                throw new Exception("Fetch error: Type mismatch");
            if (hasmany.Count > 0 && Fields["ID"] != null && (!HMfetched[key] || refetch))
            {
                var temp = Domain.F_All<T>(hasmany[key].FieldInDB + " = " + ID, false);
                HMfetched[key] = true;
                temp.FieldInDB = hasmany[key].FieldInDB;
                temp.OnAdd += new EventHandler<DBEventArgs<T>>(ListOnAdd);
                temp.OnRemove += new EventHandler<DBEventArgs<T>>(ListOnRemove);
                Fields[key] = temp;                
                return temp;
            }
            else
                if (HMfetched[key] || Fields[key]!=null)
                    return Fields[key] as DBList<T>;
                else
                    throw new Exception("Can't get \"has many\" field. \"this\" is not saved or static \"has_many\" field is not defined correctly");
        }
        /// <summary>
        /// Получить список полей для типа type
        /// </summary>
        /// <param name="type">Класс объекта</param>
        /// <returns>Список имен полей</returns>
        static List<string> GetFieldNames(Type type)
        {
            return type.GetField("FieldNames").GetValue(null) as List<string>;
        }
        /// <summary>
        /// Получить имя таблицы
        /// </summary>
        /// <param name="type">Класс объекта</param>
        /// <returns>Название таблицы</returns>
        static string GetTableName(Type type)
        {
            return type.GetField("tableName").GetValue(null) as string;
        }
        static Dictionary<string, Link> GetHasMany(Type type)
        {
            return type.GetField("has_many").GetValue(null) as Dictionary<string, Link>;
        }
        static Dictionary<string, Link> GetHasOne(Type type)
        {
            return type.GetField("has_one").GetValue(null) as Dictionary<string, Link>;
        }
        /// <summary>
        /// Преобразование из Domain в производные классы
        /// </summary>
        /// <typeparam name="T">Производный класс</typeparam>
        /// <returns>Объект типа T</returns>
        public T ParseTo<T>() where T: Domain, new()
        {
            if (!typeof(T).IsSubclassOf(typeof(Domain)))
                throw new Exception("Parsing class must be a derived for Domain");
            T newinst = new T();
            GetFieldNames(typeof(T)).ForEach(fn => newinst[fn] = this[fn]);            
            return newinst;
        }
        public object ParseTo(Type t)
        {
            if (!t.IsSubclassOf(typeof(Domain)))
                throw new Exception("Parsing class must be a derived for Domain");            
            var newinst = Activator.CreateInstance(t);
            GetFieldNames(t).ForEach(fn => (newinst as Domain)[fn] = this[fn]);
            return newinst;
        }
        public override string ToString()
        {
            return "["+Fields.Aggregate(new StringBuilder(), (sb, kvp) => sb.AppendFormat("{0}{1} = \'{2}\'", sb.Length > 0 ? ", " : "", kvp.Key, kvp.Value), sb => sb.ToString())+"]";
        }   
        /// <summary>
        /// Поиск записей в БД
        /// </summary>
        /// <typeparam name="T">seft_ref</typeparam>
        /// <param name="where_str">Условия выборки</param>
        /// <param name="single">Необходим один элемент?</param>
        /// <returns>Список объектов</returns>
        private static DBList<T> F_All<T>(string where_str = "", bool single = false, Type customType = null) where T: Domain, new()
        {   
            DBList<T> result = new DBList<T>();
            Type type = customType == null ? typeof(T) : customType;            
            string tableName = GetTableName(type);
            List<string> fieldNames = GetFieldNames(type);
            // Получение строки "ПОЛЕ1, ПОЛЕ2, ПОЛЕ3 ..."
            string fields = fieldNames.Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str; });
            FbCommand command = new FbCommand(string.Format("select {0} from {1} {2}", fields, tableName, where_str.Length > 0 ? string.Format("where {0}", where_str) : ""), DB.Connection);
            //try
            //{            
            FbDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                T temp = new T();                
                fieldNames.ForEach(fn => temp[fn] = data[fn]);
                temp._Changed = false;
                result.Add(temp);
                if (single)
                    break;
            }
            //}
            //catch (Exception e)
            //{
                //MessageBox.Show(e.Message,"SQL Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}                 
            return result;
        }
        /// <summary>
        /// Обновить существующую запись в БД
        /// </summary>
        private void Update()
        {
            string tableName = GetTableName(GetType());   
            List<string> fieldNames = GetFieldNames(GetType());
            string fields = fieldNames.Where(k=>k!="ID").Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str + " = @"+str; });                        
            FbCommand command = new FbCommand(string.Format("update {0} set {1} where ID = @ID", tableName, fields), DB.Connection);
            fieldNames.ForEach(key => command.Parameters.AddWithValue("@" + key, Fields[key]));
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Создать новую запись в БД
        /// </summary>
        private void Create()
        {
            string tableName = GetTableName(GetType());
            List<string> fieldNames = GetFieldNames(GetType());
            string fields = fieldNames.Where(k => k != "ID").Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str; });
            string values = fieldNames.Where(k => k != "ID").Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + "@" + str; });
            FbCommand command = new FbCommand(string.Format("insert into {0}({1}) values ({2}) returning ID;", tableName, fields, values), DB.Connection);
            fieldNames.Where(k => k != "ID").ToList().ForEach(key => command.Parameters.AddWithValue("@" + key, Fields[key]));
            Fields["ID"] = (int)command.ExecuteScalar();
        }
        /// <summary>
        /// Сохроняет запись в БД. В зависимости от того, задан ID или нет, будет вызван запрос UPDATE или INSERT
        /// </summary>
        public void Save(bool hard_save = false)
        {
            GetHasOne(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) { (Fields[k] as Domain).Save(); Fields[GetHasOne(GetType())[k].FieldInDB] = (Fields[k] as Domain).ID; changed = true; } });
            if (!_Changed && !hard_save)
                return;
            if (Fields["ID"]!=null)
                Update();
            else
                Create();
            _Changed = false;
            GetHasMany(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as IEnumerable<Domain>).ToList().ForEach(item => item.Save()); });
        }
        /// <summary>
        /// Создаёт новую запись
        /// </summary>
        public void SaveNew()
        {
            Create();
        }
        /// <summary>
        /// Удаляет запись из базы
        /// </summary>
        public void Delete()
        {
            if (Fields["ID"]==null)
                throw new Exception("You can't delete object without ID");
            string tableName = GetTableName(GetType());
            FbCommand command = new FbCommand(string.Format("delete from {0} where ID = {1}", tableName, Fields["ID"]), DB.Connection);            
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// Найти все записи, удовлетворяющие условию where_str
        /// </summary>
        /// <typeparam name="T">seft_ref</typeparam>
        /// <param name="where_str">where</param>
        /// <param name="args">Необязательные аргументы, которые могут быть использованы в where_str</param>
        /// <returns>Список объектов</returns>
        public static DBList<T> FindAll<T>(string where_str = "", params object[] args) where T : Domain, new()
        {
            return F_All<T>(args.Length > 0 ? string.Format(where_str, args) : where_str);
        }
        /// <summary>
        /// Получить все записи таблицы
        /// </summary>
        /// <typeparam name="T">self_ref</typeparam>
        /// <returns>Список всех объектов таблицы</returns>
        public static DBList<T> All<T>() where T : Domain, new()
        {
            return F_All<T>("1=1");
        }
        /// <summary>
        /// Поиск одного элемента
        /// </summary>
        /// <typeparam name="T">seft_ref</typeparam>
        /// <param name="where_str">where</param>
        /// <param name="args">Необязательные аргументы, которые могут быть использованы в where_str</param>
        /// <returns>Объект, удовлетворяющий условию where_str</returns>
        public static T Find<T>(string where_str = "", params object[] args) where T: Domain, new()
        {
            return F_All<T>(args.Length > 0 ? string.Format(where_str, args) : where_str, true)[0];
        }        
        /// <summary>
        /// Поиск элемента по ID
        /// </summary>
        /// <typeparam name="T">self_ref</typeparam>
        /// <param name="id">ID</param>
        /// <returns>Элемент с ID = id</returns>
        public static T Get<T>(int id) where T: Domain, new()
        {
            return F_All<T>(string.Format("ID = {0}",id), true)[0];
        }
        /// <summary>
        /// Количество элементов в базе
        /// </summary>
        /// <typeparam name="T">seft_ref</typeparam>
        /// <typeparam name="where_str">Условия выборки (по умолчанию выбираются все элементы таблицы)</typeparam>
        /// <returns>Число элементов в БД, удовлетворяющих </returns>
        public static int Count<T>(string where_str = "1=1") where T: Domain, new()
        {
            string tableName = GetTableName(typeof(T));
            FbCommand command = new FbCommand(string.Format("select count(*) from {0} where {1};", tableName, where_str), DB.Connection);
            return (int)command.ExecuteScalar();            
        }
        void ListOnAdd<T>(object sender, DBEventArgs<T> args) where T: Domain, new()
        {
            args.GetData.Fields[args.GetFieldInDBName] = ID;
            args.GetData.changed = true;
            changed = true;
        }
        void ListOnRemove<T>(object sender, DBEventArgs<T> args) where T: Domain, new()
        {
            args.GetData.Fields[args.GetFieldInDBName] = null;            
            args.GetData.Save(true);
            changed = true;
        }
    }    
}
