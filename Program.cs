using br.com.logica;
using frugalcafe_test_openlist.DatabaseConnection;
using frugalcafe_test_openlist.Model;
using frugalcafe_test_openlist.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;
using System.Xml.Linq;

Stopwatch stopwatch = new Stopwatch();


stopwatch.Start();

using (OracleConnection connection = OracleConnectionSingleton.GetInstance())
{
    ReadFileIBGE dados = new ReadFileIBGE();
    Random random = new Random();
    //int count = 0;
    try
    {
        using (OracleCommand command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO T_PESSOA (nome, estado, sexo) VALUES (:Valor1, :Valor2, :Valor3)";

            IEnumerable<Pessoa> pessoas = dados.ReadFile();
            Pessoa[] pessoasArray = pessoas.ToArray(); //Forçando o Ienumerable atualizar os registros.
            foreach (Pessoa? pessoa in pessoasArray)
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                OracleParameter[] parameters = new OracleParameter[]
                {
                    new OracleParameter(":Valor1", pessoa?.Nome),
                    new OracleParameter(":Valor2", pessoa?.Estado),
                    new OracleParameter(":Valor3", pessoa?.Sexo)
                };
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                //count++;
                //Console.WriteLine($"Esta processando ? {(result == 1 ? true : false)} - Total registros {count}");
            }
            Console.WriteLine("Inserção em lote concluída.");
        }
    }
    catch (OracleException ex)
    {
        Console.WriteLine("Erro do Oracle: " + ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
    }
}
stopwatch.Stop();
TimeSpan tempoDecorrido = stopwatch.Elapsed;
Console.WriteLine($"Tempo decorrido: {tempoDecorrido.TotalMilliseconds} ms");
Console.WriteLine($"Tempo decorrido: {tempoDecorrido.TotalSeconds} segs");
Console.WriteLine($"Tempo decorrido: {tempoDecorrido.Minutes} minuts");

