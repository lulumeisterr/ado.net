using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frugalcafe_test_openlist.Utils
{
    public class RetryOperationQuery
    {
        public int ExecuteNonQueryWithRetry(OracleCommand command, int maxRetries = 3, int retryDelayMilliseconds = 1000)
        {
            int retries = 0;

            while (retries < maxRetries)
            {
                try
                {
                    if (command.Connection.State == ConnectionState.Closed)
                        command.Connection.Open();

                    int result = command.ExecuteNonQuery();
                    return result; // Se tiver sucesso, retorna o resultado

                }
                catch (OracleException ex)
                {
                    retries++;
                    if (retries < maxRetries)
                    {
                        System.Threading.Thread.Sleep(retryDelayMilliseconds);
                    }
                    else
                    {
                        throw; // Se esgotar as tentativas, relança a exceção
                    }
                }
                finally
                {
                    command.Parameters.Clear(); // Limpa os parâmetros após cada tentativa
                }
            }
            throw new Exception("Número máximo de tentativas atingido.");
        }
    }
}
