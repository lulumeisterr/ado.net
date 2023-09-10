using br.com.logica;
using frugalcafe_test_openlist.DatabaseConnection;
using frugalcafe_test_openlist.Model;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace frugalcafe_test_openlist.ResourceAcess
{
    public class InsertPessoas
    {
        public void Insert()
        {
            ReadFileIBGE dados = new ReadFileIBGE();
            Stopwatch stopwatch = new Stopwatch();
            Random random = new Random();
            stopwatch.Start();
            using (OracleConnection connection = OracleConnectionSingleton.GetInstance())
            {
                try
                {
                    using (OracleCommand insertPessoa = connection.CreateCommand())
                    {
                        foreach (Pessoa? pessoa in dados.ReadFile())
                        {
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();

                            insertPessoa.CommandText = "INSERT INTO T_PESSOA (nome,sexo) VALUES (:Valor1,:Valor2)";
                            OracleParameter[] parameters = new OracleParameter[]
                            {
                              new OracleParameter(":Valor1", pessoa?.Nome),
                              new OracleParameter(":Valor2", pessoa?.Sexo),
                            };
                            insertPessoa.Parameters.AddRange(parameters);
                            insertPessoa.ExecuteNonQuery();

                            using (OracleCommand insertEnderecoProfissao = connection.CreateCommand())
                            {
                                Endereco? endereco = pessoa?.endereco;
                                if (endereco != null)
                                {
                                    insertEnderecoProfissao.CommandText = "INSERT INTO T_ENDERECO (ENDERECOID, CEP, ESTADO, LOGRADOURO, COMPLEMENTO) VALUES (T_PESSOA_SQ.currval,:Valor2,:Valor3,:Valor4,:Valor5)";
                                    OracleParameter[] parametersEndereco = new OracleParameter[]
                                    {
                                        new OracleParameter(":Valor2", endereco?.Cep),
                                        new OracleParameter(":Valor3", endereco?.Estado),
                                        new OracleParameter(":Valor4", endereco?.Logradouro),
                                        new OracleParameter(":Valor5", endereco?.Complemento)
                                    };
                                    insertEnderecoProfissao.Parameters.AddRange(parametersEndereco);
                                    insertEnderecoProfissao.ExecuteNonQuery();
                                    insertEnderecoProfissao.Parameters.Clear();
                                }
                                Profissao? profissao = pessoa?.Profissao;
                                if (profissao != null)
                                {
                                    insertEnderecoProfissao.CommandText = "INSERT INTO T_PROFISSAO(tp_profissao, salario, temp_trabalho) VALUES(:Valor1,:Valor2,:Valor3)";

                                    OracleParameter param1 = new OracleParameter(":Valor1", OracleDbType.Varchar2);
                                    param1.Value = profissao.TipoProfissao;

                                    OracleParameter param2 = new OracleParameter(":Valor2", OracleDbType.Decimal);
                                    param2.Value = profissao.Salario;

                                    OracleParameter param3 = new OracleParameter(":Valor3", OracleDbType.Date);
                                    param3.Value = profissao.TempoTrabalho.Date;

                                    insertEnderecoProfissao.Parameters.Add(param1);
                                    insertEnderecoProfissao.Parameters.Add(param2);
                                    insertEnderecoProfissao.Parameters.Add(param3);
                                    insertEnderecoProfissao.ExecuteNonQuery();
                                    insertEnderecoProfissao.Parameters.Clear();
                                }

                                if(profissao != null && endereco != null) //TB Associativa entre endereco e profissao
                                {
                                    insertEnderecoProfissao.CommandText = "INSERT INTO T_ENDERECO_PROFISSAO(ID_ENDERECO,ID_PROFISSAO) VALUES(T_PESSOA_SQ.currval,T_PROFISSAO_SQ.currval)";
                                    insertEnderecoProfissao.ExecuteNonQuery();
                                    insertEnderecoProfissao.Parameters.Clear();
                                }
                            }
                            insertPessoa.Parameters.Clear();
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
        }
    }
}
