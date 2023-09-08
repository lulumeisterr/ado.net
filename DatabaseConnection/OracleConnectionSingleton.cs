using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frugalcafe_test_openlist.DatabaseConnection
{
    /// <summary>
    /// 
    /// </summary>
    public class OracleConnectionSingleton
    {
        private static OracleConnection _connection;
        private static readonly object _lock = new object();

        private OracleConnectionSingleton() { }

        public static OracleConnection GetInstance()
        {
            // Verifique se a conexão já foi criada
            if (_connection == null)
            {
                lock (_lock)
                {
                    // Verifique novamente dentro do bloqueio, pois pode ter sido criado por outra thread
                    if (_connection == null)
                    {
                        // Substitua "SuaStringDeConexao" pela sua string de conexão real
                        string connectionString = "User Id=lucas;Password=123;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=XE)));";
                        _connection = new OracleConnection(connectionString);
                        _connection.Open();
                    }
                }
            }
            return _connection;
        }
    }
}
