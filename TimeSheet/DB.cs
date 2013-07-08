using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
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
        public static Dictionary<string, Type> belongs_to = new Dictionary<string, Type>();
        public static Dictionary<string, Type> has_many = new Dictionary<string, Type>();
        public static Dictionary<string, bool> H1fetched = new Dictionary<string, bool>();
        public static Dictionary<string, bool> HMfetched = new Dictionary<string, bool>();        
        /// <summary>
        /// Имя таблицы
        /// </summary>
        public static string tableName = "";
        public object this[string key]
        {
            get
            {
                if (GetBelongsTo(GetType()).Keys.Any(k => k.ToUpper() == key.ToUpper()) || GetHasMany(GetType()).Keys.Any(k => k.ToUpper() == key.ToUpper()))
                    throw new Exception("You can't get \"Domain\" type field from here. Use D(\"domain_field_name\") method for this type of fields");
                return Fields[key.ToUpper()];
            }
            set
            {
                Fields[key.ToUpper()] = value;
            }
        }
        public T H1<T>(string key, bool getFromServer = false) where T: Domain, new()
        {
            if (GetBelongsTo(GetType()).Keys.Any(k => k.ToUpper() == key.ToUpper()))
                return FetchBelongsTo<T>(key, getFromServer);
            else
                throw new Exception("You can't get field, which is not a Domain object from here");            
        }
        public List<T> HM<T>(string key, bool getFromServer = false) where T: Domain, new()
        {
            if (GetHasMany(GetType()).Keys.Any(k => k.ToUpper() == key.ToUpper()))
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
            (type.GetField("FieldNames").GetValue(null) as List<string>).ForEach(fn => Fields[fn] = null);            
        }        
        /// <summary>
        /// Инициализация класса перед началом работы. Заполнение списка, содержащего имена полей в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Initialize<T>() where T :Domain, new()
        {
            Type type = typeof(T);            
            List<string> field_names = GetFieldNames(type);
            type.GetProperties().ToList().Where(pi => pi.Name != "Item" && !pi.PropertyType.IsGenericType && !pi.PropertyType.IsSubclassOf(typeof(Domain))).ToList().ForEach(pi => field_names.Add(pi.Name.ToUpper()));
            GetBelongsTo(type).Keys.ToList().ForEach(k => { field_names.Add(k.ToUpper() + "_ID"); H1fetched[k] = false; });
            GetHasMany(type).Keys.ToList().ForEach(k => HMfetched[k] = false);
        }        
        protected T FetchBelongsTo<T>(string key, bool refetch = false) where T: Domain, new()
        {
            Dictionary<string, Type> belongsto = GetBelongsTo(GetType());
            if (typeof(T) != belongsto[key])
                throw new Exception("Fetch error: Type mismatch");//на случай, если в коде будет ошибка - отловить! иначе заполнит полученный объект не своими полями
            if (belongsto.Count > 0 && Fields["ID"] != null && (!H1fetched[key] || refetch))
            {
                var temp = Domain.F_All<Domain>("ID = " + this[belongsto[key].Name + "_ID"], true, belongsto[key])[0].ParseTo<T>();
                Fields[key] = temp;
                H1fetched[key] = true;
                return temp;
            }
            else
                throw new Exception("Cant get \"belongs to\" field");
        }
        protected List<T> FetchHasMany<T>(string key, bool refetch = false)where T: Domain, new()
        {
            Dictionary<string, Type> hasmany = GetHasMany(GetType());
            if (typeof(T) != hasmany[key])
                throw new Exception("Fetch error: Type mismatch");
            if (hasmany.Count > 0 && Fields["ID"] != null && (!HMfetched[key] || refetch))
            {
                var temp = Domain.F_All<T>(GetType().Name + "_ID = " + ID, false);
                HMfetched[key] = true;
                Fields[key] = temp;
                return temp;
            }
            else
                throw new Exception("Cant get \"has many\" field");
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
        static Dictionary<string, Type> GetHasMany(Type type)
        {
            return type.GetField("has_many").GetValue(null) as Dictionary<string, Type>;
        }
        static Dictionary<string, Type> GetBelongsTo(Type type)
        {
            return type.GetField("belongs_to").GetValue(null) as Dictionary<string, Type>;
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
            return Fields.Aggregate(new StringBuilder(), (sb, kvp) => sb.AppendFormat("{0}{1} = \'{2}\'", sb.Length > 0 ? ", " : "", kvp.Key, kvp.Value), sb => sb.ToString());
        }   
        /// <summary>
        /// Поиск записей в БД
        /// </summary>
        /// <typeparam name="T">seft_ref</typeparam>
        /// <param name="where_str">Условия выборки</param>
        /// <param name="single">Необходим один элемент?</param>
        /// <returns>Список объектов</returns>
        private static List<T> F_All<T>(string where_str = "", bool single = false, Type customType = null) where T: Domain, new()
        {   
            List<T> result = new List<T>();
            Type type = customType == null ? typeof(T) : customType;
            if (!type.IsSubclassOf(typeof(Domain)) && type != typeof(Domain))
                throw new Exception("Wrong class name. Class T must be subclass of Domain");
            string tableName = GetTableName(type);
            List<string> fieldNames = GetFieldNames(type);
            // Получение строки "ПОЛЕ1, ПОЛЕ2, ПОЛЕ3 ..."
            string fields = fieldNames.Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str; });
            FbCommand command = new FbCommand(string.Format("select {0} from {1} {2}", fields, tableName, where_str.Length > 0 ? string.Format("where {0}", where_str) : ""), DB.Connection);
            try
            {
            FbDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                T temp = new T();                
                fieldNames.ForEach(fn => temp[fn] = data[fn]);                
                result.Add(temp);
                if (single)
                    break;
            }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"SQL Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }            
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
        public void Save()
        {
            if (Fields["ID"]!=null)
                Update();
            else
                Create();
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
        public static List<T> FindAll<T>(string where_str = "", params object[] args)where T:Domain, new()
        {            
            return F_All<T>(args.Length > 0 ? string.Format(where_str, args) : where_str);
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
        public static int Count<T>(string where_str = "1=1")
        {
            string tableName = GetTableName(typeof(T));
            FbCommand command = new FbCommand(string.Format("select count(*) from {0} where {1};", tableName, where_str), DB.Connection);
            return (int)command.ExecuteScalar();            
        }        
    }
    
    public class User:Domain
    {   
        new public static string tableName = "USERS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Type> has_many = new Dictionary<string, Type>() { { "personal", typeof(Personal) } };
        new public static Dictionary<string, Type> belongs_to = new Dictionary<string, Type>();
        #region Properties        
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        public string Login
        {
            get
            {
                return this["login"].ToString();
            }
            set
            {
                this["login"] = value;
            }
        }
        public string Pass
        {
            get
            {
                return this["pass"].ToString();
            }
            set
            {
                this["pass"] = value;
            }
        }
        public DateTime time
        {
            get
            {
                return DateTime.Now;
            }
        }
        public Personal FirstPerosn
        {
            get
            {
                return PersonalList[0];
            }
        }
        public List<Personal> PersonalList
        {
            get
            {
                return this.HM<Personal>("personal");
            }
        }
        #endregion
        public User():base(typeof(User))
        {            
        }
        public User(string login, string password, string name)
            : base(typeof(User))
        {
            Login = login;
            Pass = Helper.getMD5(password);
            //Name = name;
        }        
    }
    public class Personal:Domain
    {
        new public static string tableName = "PERSONAL";
        new public static List<string> FieldNames = new List<string>();
        new public static Dictionary<string, Type> belongs_to = new Dictionary<string, Type>() { { "User", typeof(User) } };
        new public static Dictionary<string, Type> has_many = new Dictionary<string, Type>();
        #region Properties
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        public string Pos
        {
            get
            {
                return this["pos"].ToString();
            }
            set
            {
                this["pos"] = value;
            }
        }
        public string Department
        {
            get
            {
                return this["department"].ToString();
            }
            set
            {
                this["department"] = value;
            }
        }
        #endregion
        public Personal():base(typeof(Personal))
        {            
        }
        public Personal(string name, string position, string department)
            : base(typeof(Personal))
        {
            Name = name;
            Pos = position;
            Department = department;            
        }     
    }    
}
