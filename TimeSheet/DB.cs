///     Alexander Mochalin (c) 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
using System.ComponentModel;

namespace SwarthyComponents.FireBird
{
    public class DBBindingSource<T> : BindingSource where T:Domain, new()
    {
        public DBBindingSource()
            : base()
        {
        }
        public void Remove(T item, bool delete_on_remove = false)
        {
            if (delete_on_remove)
                item.Delete();
            base.Remove(item);
        }        
    }
    public static class Extensions
    {
        public static DBList<T> ToDBList<T>(this IEnumerable<T> ien) where T : Domain, new()
        {
            DBList<T> res = new DBList<T>();
            foreach (T item in ien)
                res.Add(item);
            return res;
        }
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> ien) where T : Domain, new()
        {
            BindingList<T> res = new BindingList<T>();
            foreach (T item in ien)
                res.Add(item);
            return res;
        }
    }
    public static class DB
    {
        private static FbConnection connection = GetNewConnection;
        public static string ConnectionString = "";        
        internal static FbConnection Connection
        {
            get
            {   
                if (connection.State != System.Data.ConnectionState.Open)
                    try
                    {
                        connection.Open();
                        DBHelper.Log("Подключение к БД...");
                    }
                    catch
                    {
                        DBHelper.Log("Создаю новый экземпляр подключения");
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
                FbConnection c = new FbConnection(ConnectionString);                
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
            DBHelper.Log("Query запрос к БД: {0}", query);
            return command.ExecuteScalar();
        }
    }
    public static class DBHelper
    {
        public static IDictionary<string, object> AnonymousObjectToDictionary(object propertyBag)
        {
            var result = new Dictionary<string, object>();
            if (propertyBag != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(propertyBag))
                {
                    result.Add(property.Name, property.GetValue(propertyBag));
                }
            }
            return result;
        }
        public static formatAndArgsFunction Log;
    }
    public delegate void formatAndArgsFunction(string parameter, params object[] args);
    public class DBEventArgs<T> : EventArgs
    {
        private readonly T data;
        private readonly string FieldSource, FieldDestination;
        private readonly bool needSaveThis;
        public DBEventArgs(T data, string FieldSource, string FieldDestination, bool needSaveThis = true)
        {
            this.data = data;
            this.FieldSource = FieldSource;
            this.FieldDestination = FieldDestination;
            this.needSaveThis = needSaveThis;
        }
        public T GetData
        {
            get
            {
                return data;
            }
        }
        public string GetFieldSource
        {
            get
            {
                return FieldSource;
            }
        }
        public string GetFieldDestination
        {
            get
            {
                return FieldDestination;
            }
        }
        public bool NeedSave
        {
            get
            {
                return needSaveThis;
            }
        }
    }
    public class DBList<T> : List<T> where T : Domain, new()
    {
        public bool DeleteFromServerOnRemove = false;
        public DBList(string FieldSource = "", string FieldDestination = "")
            : base()
        {            
            this.FieldSource = FieldSource;
            this.FieldDestination = FieldDestination;
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
                    OnChange(this, new DBEventArgs<T>(base[i], FieldSource, FieldDestination));
            }
        }        
        public DBList<T> Except(DBList<T> ex)
        {
            DBList<T> res = new DBList<T>();            
            foreach (T item in this)
            {
                if (ex.Contains(item))
                    continue;
                res.Add(item);
            }
            return res;
        }
        public void SaveList(bool hardSave = false)
        {
            foreach (T item in this)
                item.Save(hardSave);
        }
        public T FindOrCreate(object match)
        {
            var rest = DBHelper.AnonymousObjectToDictionary(match);
            var temp = this.Find((item) => { bool result = true; foreach (var k in rest.Keys) if (!rest[k].Equals(item[k])) { result = false; break; } return result; });
            if (temp == null)
            {
                temp = new T();
                temp.SetValues(match);
                this.Add(temp);
            }
            return temp;
        }
        new public void RemoveAt(int i)
        {
            Remove(this[i]);
        }
        public string FieldSource = "";
        public string FieldDestination = "";
        public event EventHandler<DBEventArgs<T>> OnAdd, OnRemove, OnChange;
        new public void Add(T item)
        {
            if (OnAdd != null)
                OnAdd(this, new DBEventArgs<T>(item, FieldSource, FieldDestination));
            base.Add(item);
        }
        public void Clear(bool delete_from_db = false)
        {
            T temp = null;
            while (Count > 0)
            {
                temp = this.First();
                if (temp != null)
                    Remove(temp, delete_from_db);
            }
        }
        public void Remove(T item, bool delete_removable_object = false)
        {
            if (OnRemove != null)
                OnRemove(this, new DBEventArgs<T>(item, FieldSource, FieldDestination));//был true для удалени персонала из списка favorite 
            base.Remove(item);
            if (delete_removable_object || DeleteFromServerOnRemove)
                item.Delete();
        }
    }
    public struct Link
    {
        /// <summary>
        /// Связь таблиц
        /// </summary>
        /// <param name="FieldSoruce">Поле в исходной таблице, в которой записывается значение, принадлежащее записи в ссылаемой таблице (Personal_Table_Number)</param>
        /// <param name="FieldDestination">Имя поля в связываемой таблице, являющееся естественным ключем (Table_Number) [по умолчанию это ID]</param>
        /// <param name="DomainType">Класс записи</param>
        public Link(string FieldSoruce, Type DomainType, string FieldDestination = "ID")
        {
            Type = DomainType;
            Field_Source = FieldSoruce;
            Field_Destination = FieldDestination;
        }
        public string Field_Source;
        public string Field_Destination;
        public Type Type;
    }
    public delegate object VirtualDeletionDeletedValue();
    public class Domain
    {
        #region settings
        public static string VirtualDeletionField = "";//isdeleted например для bool
        public static string VirtualDeletionNotDeletedRecord = "";//"isdeleted = 0"
        public static VirtualDeletionDeletedValue VirtualDeletedValue { get; set; } 
        #endregion
        public static EventHandler OnFindBegin = null, OnFindEnd = null;
        /// <summary>
        /// Словарь, содержащий пары [Поле|Значение]
        /// </summary>
        protected Dictionary<string, object> Fields = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Список имен полей
        /// </summary>
        public static List<string> FieldNames = new List<string>();        
        /// <summary>
        /// Словари внешних связей
        /// </summary>
        public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>();
        public static Dictionary<string, Link> has_one = new Dictionary<string, Link>();
        public static Dictionary<string, Link> has_many = new Dictionary<string, Link>();
        /// <summary>
        /// Поле сортировки
        /// </summary>
        public static string OrderBy = "";
        /// <summary>
        /// Загружена ли модель из БД?
        /// </summary>
        protected Dictionary<string, bool> BTfetched = new Dictionary<string, bool>();
        protected Dictionary<string, bool> H1fetched = new Dictionary<string, bool>();
        protected Dictionary<string, bool> HMfetched = new Dictionary<string, bool>();        
        /// <summary>
        /// Имя таблицы
        /// </summary>
        protected static string tableName = "";
        /// <summary>
        /// Указывает, необходимо ли заменять физическое удаление вирутальным.
        /// </summary>
        protected static bool virtualDeletion = false;
        /// <summary>
        /// Указывает, необходимо ли рекурсивно виртуально удалять объекты в связях hasone, hasmany
        /// </summary>
        protected static bool virtualSubDeletion = false;
        /// <summary>
        /// Сохранена ли на сервере?
        /// </summary>
        public bool _IsNew
        {
            get
            {
                return Fields["ID"] == null;
            }
        }
        bool changed = true;
        public bool _Changed { get { return changed; } private set { changed = value; } }

        public object this[string key]
        {
            get
            {   
                return Fields.ContainsKey(key) ? Fields[key] : null;
            }
            set
            {
                Fields[key] = value;
                _Changed = true;
                if (value!=null && value.GetType().IsSubclassOf(typeof(Domain)) && BTfetched.Keys.Contains(key, StringComparer.OrdinalIgnoreCase))
                    BTfetched[key] = true;                    
            }
        }
        public T BT<T>(string key, bool getFromServer = false) where T : Domain, new()
        {
            if (GetBelongsTo(GetType()).ContainsKey(key))
                return FetchBelongsTo<T>(key, getFromServer);
            else
                throw new Exception("You can't get field, which is not a Domain object from here");
        }
        public T H1<T>(string key, bool getFromServer = false) where T : Domain, new()
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
                return Fields["ID"]==null?-1:(int)Fields["ID"];    //чтобы не вызывать лишних проверок
            }
            set
            {
                this["ID"] = value;          //чтобы сменился changed      
            }
        }
        public Domain()
        {
        }
        public void SetValues(object values)
        {
            var val = DBHelper.AnonymousObjectToDictionary(values);
            foreach (var k in val.Keys)
                this[k] = val[k];
        }
        /// <summary>
        /// Констурктор с инициализацией ключей в словаре Fields
        /// </summary>
        /// <param name="type"></param>
        public Domain(Type type)
        {
            List<string> field_names = GetFieldNames(type);
            field_names.ForEach(fn => Fields[fn] = null);//инициализация ключей в словаре
            GetBelongsTo(type).Keys.ToList().ForEach(k => {  BTfetched[k] = false; });
            GetHasMany(type).Keys.ToList().ForEach(k => {  HMfetched[k] = false; });
            GetHasOne(type).Keys.ToList().ForEach(k => { H1fetched[k] = false; });
        }        
        /// <summary>
        /// Инициализация класса перед началом работы. Заполнение списка, содержащего имена полей в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Initialize<T>() where T: Domain, new()
        {
            Type type = typeof(T);            
            List<string> field_names = GetFieldNames(type);
            type.GetProperties().ToList().Where(pi =>
                pi.Name != "Item" &&
                pi.Name[0]!='_' &&
                !pi.PropertyType.IsGenericType &&
                !pi.PropertyType.IsSubclassOf(typeof(Domain))).ToList().ForEach(pi => field_names.Add(pi.Name));
            GetBelongsTo(type).Values.ToList().ForEach(k => { field_names.Add(k.Field_Source); });
        }
        protected T FetchBelongsTo<T>(string key, bool refetch = false) where T: Domain, new()
        {
            Dictionary<string, Link> belongsto = GetBelongsTo(GetType());
            if (typeof(T) != belongsto[key].Type)
                throw new Exception("Fetch error: Type mismatch");//на случай, если в коде будет ошибка
            if (belongsto.Count > 0 && (!BTfetched[key] || refetch))
            {
                if (this[belongsto[key].Field_Source]==null || this[belongsto[key].Field_Source].ToString() == "")
                    return null;
                var temp = Domain.F_All<T>(string.Format("{0} = \'{1}\'", belongsto[key].Field_Destination, this[belongsto[key].Field_Source]), true, belongsto[key].Type)[0]; //раньше было Domain.F_ALL<DOmain>(...)[0].ParseTo<T>();
                Fields[key] = temp;
                BTfetched[key] = true;
                return temp;
            }
            else
                if (BTfetched[key])
                    return Fields[key] as T;
                else
                    //throw new Exception("Can't get \"belongs to\" field");
                    return null;
        }
        protected T FetchHasOne<T>(string key, bool refetch = false) where T : Domain, new()
        {
            Dictionary<string, Link> hasOne = GetHasOne(GetType());
            if (typeof(T) != hasOne[key].Type)
                throw new Exception("Fetch error: Type mismatch");
            if (hasOne.Count > 0 && Fields[hasOne[key].Field_Destination] != null && (!H1fetched[key] || refetch))
            {
                var temp = Domain.F_All<T>(string.Format("{0} = \'{1}\'", hasOne[key].Field_Source, Fields[hasOne[key].Field_Destination]), true);
                T result;
                if (temp.Count == 0)
                    return null;
                else
                    result = temp[0];
                H1fetched[key] = true;
                Fields[key] = result;
                return result;
            }
            else
                if (H1fetched[key] || (Fields.ContainsKey(key) && Fields[key] != null))
                    return Fields[key] as T;
                else
                    //throw new Exception("Can't get \"has one\" field. \"this\" is not saved or static \"has_one\" field is not defined correctly");
                    return null;
        }
        protected DBList<T> FetchHasMany<T>(string key, bool refetch = false) where T : Domain, new()
        {
            Dictionary<string, Link> hasmany = GetHasMany(GetType());
            if (typeof(T) != hasmany[key].Type)
                throw new Exception("Fetch error: Type mismatch");
            if (hasmany.Count > 0 && Fields[hasmany[key].Field_Destination] != null && (!HMfetched[key] || refetch))
            {
                var temp = Domain.F_All<T>(string.Format("{0} = \'{1}\'", hasmany[key].Field_Source, Fields[hasmany[key].Field_Destination]));
                HMfetched[key] = true;
                temp.FieldSource = hasmany[key].Field_Source;
                temp.OnAdd += new EventHandler<DBEventArgs<T>>(ListOnAdd);
                temp.OnRemove += new EventHandler<DBEventArgs<T>>(ListOnRemove);
                Fields[key] = temp;
                return temp;
            }
            else
                if (HMfetched[key] || (Fields.ContainsKey(key) && Fields[key] != null))
                    return Fields[key] as DBList<T>;
                else
                    //throw new Exception("Can't get \"has many\" field. \"this\" is not saved or static \"has_many\" field is not defined correctly");
                    return new DBList<T>();
        }
        public static T FindOrCreate<T>(object restrictions) where T : Domain, new()
        {
            var rest = DBHelper.AnonymousObjectToDictionary(restrictions);
            var result = Domain.Find<T>(restrictions);
            if (result == null)
            {
                result = new T();
                rest.Keys.ToList().ForEach(k => result.Fields[k] = rest[k]);
            }
            else
                result.changed = false;            
            return result;
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
        static bool AllowVirtualDeletion(Type type)
        {
            var f = type.GetField("virtualDeletion");
            if (f == null)
                return false;            
            return Convert.ToBoolean(f.GetValue(null));            
        }
        static bool AllowVirtualSubDeletion(Type type)
        {
            var f = type.GetField("virtualSubDeletion");
            if (f == null)
                return false;
            return Convert.ToBoolean(f.GetValue(null));
        }        
        static string GetOrderBy(Type type)
        {
            var f = type.GetField("OrderBy");
            if (f == null)
                return "";
            return f.GetValue(null) as string;
        }
        static Dictionary<string, Link> GetHasMany(Type type)
        {
            var f = type.GetField("has_many");
            if (f == null)
                return new Dictionary<string,Link>();
            return f.GetValue(null) as Dictionary<string, Link>;
        }
        static Dictionary<string, Link> GetHasOne(Type type)
        {
            var f = type.GetField("has_one");
            if (f == null)
                return new Dictionary<string, Link>();
            return f.GetValue(null) as Dictionary<string, Link>;
        }
        static Dictionary<string, Link> GetBelongsTo(Type type)
        {
            var f = type.GetField("belongs_to");
            if (f == null)
                return new Dictionary<string,Link>();
            return f.GetValue(null) as Dictionary<string, Link>;
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
        public override string ToString()
        {
            return GetTextData();
        }
        public string GetTextData()
        {
            return "[" + Fields.Aggregate(new StringBuilder(), (sb, kvp) => sb.AppendFormat("{0}{1}={2}", sb.Length > 0 ? ", " : "", kvp.Key, kvp.Value), sb => sb.ToString()) + "]";
        }
        /// <summary>
        /// Поиск записей в БД
        /// </summary>
        /// <typeparam name="T">seft_ref</typeparam>
        /// <param name="where_str">Условия выборки</param>
        /// <param name="single">Необходим один элемент?</param>
        /// <returns>Список объектов</returns>
        private static DBList<T> F_All<T>(string where_str = "", bool single = false, Type customType = null) where T : Domain, new()
        {
            if (OnFindBegin != null)
                OnFindBegin(null, EventArgs.Empty);
            DBList<T> result = new DBList<T>();
            Type type = customType == null ? typeof(T) : customType;
            string tableName = GetTableName(type);
            bool virtualMode = AllowVirtualDeletion(type);            
            List<string> fieldNames = GetFieldNames(type);
            // Получение строки "ПОЛЕ1, ПОЛЕ2, ПОЛЕ3 ..."
            string fields = fieldNames.Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str; });
            string wherePart = "";
            //where_str.Length > 0 ? string.Format("where {0}", where_str) : ""            
            if (where_str.Length > 0)
                wherePart += string.Format("where {0}",where_str);
            if (virtualMode)
            {
                wherePart += where_str.Length > 0 ? " and" : "where";                
                wherePart += string.Format(" {0}", VirtualDeletionNotDeletedRecord);
            }
            FbCommand command = new FbCommand(string.Format("select {0} from {1} {2}{3}", fields, tableName, wherePart, GetOrderBy(type).Length > 0 ? " order by " + GetOrderBy(type) : ""), DB.Connection);
            try
            {
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (OnFindEnd != null)
                OnFindEnd(null, EventArgs.Empty);
            return result;
        }
        private static DBList<T> F_All<T>(object restrictions, bool single = false, Type customType = null, bool Distinct = false) where T : Domain, new()
        {
            if (OnFindBegin != null)
                OnFindBegin(null, EventArgs.Empty);
            var rest = DBHelper.AnonymousObjectToDictionary(restrictions);
            if (rest.Keys.Count == 0)
                return All<T>();
            DBList<T> result = new DBList<T>();
            Type type = customType == null ? typeof(T) : customType;
            string tableName = GetTableName(type);
            List<string> fieldNames = GetFieldNames(type);
            // Получение строки "ПОЛЕ1, ПОЛЕ2, ПОЛЕ3 ..."
            string fields = fieldNames.Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str; });
            string wherestr = rest.Aggregate(new StringBuilder(),
              (sb, kvp) => sb.AppendFormat("{0}{1} = @{1}",
                           sb.Length > 0 ? " AND " : "", kvp.Key, kvp.Value),
              sb => sb.ToString());
            FbCommand command = new FbCommand(string.Format("select{4} {0} from {1} where {2}{3}", fields, tableName, wherestr, GetOrderBy(type).Length > 0 ? " order by "+GetOrderBy(type) : "",Distinct?" DISTINCT":""), DB.Connection);
            rest.Keys.ToList().ForEach(k => command.Parameters.AddWithValue("@" + k, rest[k]));            
            try
            {
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (OnFindEnd != null)
                OnFindEnd(null, EventArgs.Empty);
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
        public bool Save(bool hard_save = false)
        {
            if (!_Changed && !hard_save)
                return true;
            GetBelongsTo(GetType()).Keys.ToList().ForEach(k =>
            {
                if (Fields.ContainsKey(k))
                {
                    var itm = Fields[k] as Domain;
                    if (itm != null)
                    {
                        itm.Save();
                        Fields[GetBelongsTo(GetType())[k].Field_Source] = itm[GetBelongsTo(GetType())[k].Field_Destination];
                    }
                    else
                        Fields[GetBelongsTo(GetType())[k].Field_Source] = null;
                    changed = true;
                }
            });
            
            try
            {
                if (Fields["ID"] != null)
                    Update();
                else
                    Create();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message, "DataBase: Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _Changed = false;
            GetHasOne(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as Domain).Save(); });
            GetHasMany(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as IEnumerable<Domain>).ToList().ForEach(item => item.Save()); });
            return true;
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
            if (AllowVirtualDeletion(GetType()))
                VirtualDelete();
            else
                PhysicalDelete();
        }
        /// <summary>
        /// Физическое удаление
        /// </summary>
        void PhysicalDelete()
        {
            if (Fields["ID"] == null)
                return;
            GetHasOne(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as Domain).Delete(); });
            GetHasMany(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as IEnumerable<Domain>).ToList().ForEach(item => item.Delete()); });
            try
            {
                string tableName = GetTableName(GetType());
                FbCommand command = new FbCommand(string.Format("delete from {0} where ID = {1}", tableName, Fields["ID"]), DB.Connection);
                command.ExecuteNonQuery();
                Fields["ID"] = null;
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message, "DataBase: Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Виртуальное удаление
        /// </summary>
        void VirtualDelete()
        {
            if (Fields["ID"] == null)
                return;
            if (AllowVirtualDeletion(GetType()))
            {
                string tableName = GetTableName(GetType());
                FbCommand command = new FbCommand(string.Format("update {0} set {1} = @DELVAL where ID = {2}", tableName, VirtualDeletionField, Fields["ID"]), DB.Connection);
                command.Parameters.AddWithValue("@DELVAL", VirtualDeletedValue());
                command.ExecuteNonQuery();
                if (AllowVirtualSubDeletion(GetType()))
                {
                    GetHasOne(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as Domain).VirtualDelete(); });
                    GetHasMany(GetType()).Keys.ToList().ForEach(k => { if (Fields.ContainsKey(k)) (Fields[k] as IEnumerable<Domain>).ToList().ForEach(item => item.VirtualDelete()); });
                }
            }
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
        public static DBList<T> FindAll<T>(object restrictions) where T : Domain, new()
        {
            return F_All<T>(restrictions);
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
        public static T Find<T>(string where_str = "", params object[] args) where T : Domain, new()
        {
            var item = F_All<T>(args.Length > 0 ? string.Format(where_str, args) : where_str, true);
            return item.Count > 0 ? item[0] : null;
        }
        public static T Find<T>(object restrictions) where T : Domain, new()
        {
            var item = F_All<T>(restrictions, true);
            return item.Count > 0 ? item[0] : null;
        }
        /// <summary>
        /// Поиск элемента по ID
        /// </summary>
        /// <typeparam name="T">self_ref</typeparam>
        /// <param name="id">ID</param>
        /// <returns>Элемент с ID = id</returns>
        public static T Get<T>(int id) where T: Domain, new()
        {
            return Find<T>("id = '{0}'", id);
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
        public void ListOnAdd<T>(object sender, DBEventArgs<T> args) where T: Domain, new()
        {
            args.GetData.Fields[args.GetFieldSource] = this[args.GetFieldDestination];
            args.GetData.changed = true;
            changed = true;
        }
        public void ListOnRemove<T>(object sender, DBEventArgs<T> args) where T: Domain, new()
        {
            args.GetData.Fields[args.GetFieldSource] = null;            
            if (args.NeedSave)
                args.GetData.Save(true);
            changed = true;
        }
        public static bool operator ==(Domain A, Domain B)
        {
            return Domain.Equals(A, B);            
        }
        public static bool operator !=(Domain A, Domain B)
        {
            return !Domain.Equals(A, B);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if ((obj as Domain) == null)
                return false;
            //return (obj as Domain).Fields.All(kv => Fields.ContainsKey(kv.Key) && Fields[kv.Key] == kv.Value);
            return (obj as Domain).ID == ID;
        }
        public override int GetHashCode()
        {
            return ID.GetHashCode();            
        }
    }
}