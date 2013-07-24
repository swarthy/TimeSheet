using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace TimeSheetManger
{
    static public class DBFmanager
    {
        public static void Test()
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;DataSource=c:\\dbf\\;Extended Properties=dBASE IV;User ID=Admin;Password=;");
            conn.Open();
            OleDbCommand create = conn.CreateCommand();
            create.CommandText = "CREATE TABLE Clients.dbf (Id numeric (8), Name char(254), Feature numeric(1), Address)law char(254));";
            create.ExecuteNonQuery();
        }
    }
}
