using frugalcafe_test_openlist.Model;
using System.Text.RegularExpressions;

namespace br.com.logica
{
    public class ReadFileIBGE : IDisposable
    {
        private string filePathPessoa = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "File"), "ibge-fem-10000.csv");
        private string filePathEndereco = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "File"), "tabela_integrada.csv");

        string[] estados = { "SP", "RJ", "MG", "RS", "BA", "PR", "PE", "CE", "SC", "GO" };
        char[] sexos = { 'M', 'F' };
        decimal[] salario = { 1000, 2000, 3200, 4440, 5000, 6750, 7150, 8000, 9500, 10000, 11725, 12300, 1400, 15000, 17000, 20000 };
        DateTime[] dataInicioTrabalho = { new DateTime(2019,01,05), new DateTime(2020, 05, 03), new DateTime(2018, 02, 12), new DateTime(2022, 05, 17) };

        public IEnumerable<Pessoa> ReadFile()
        {
            StreamReader? pessoaFile = null;
            Random random = new Random();
            try
            {
                List<(string Estado, int Cep, string Logradouro, string Complemento)> resultEndereco = ListaEndereco().ToList();
                pessoaFile = new StreamReader(filePathPessoa);
                pessoaFile?.ReadLine()?.Skip(0);
                string line;
                while ((line = pessoaFile?.ReadLine()) != null)
                {

                    string[] columns = line.Split(",");
                    string estado = estados[random.Next(estados.Count())];
                    char sexo = sexos[random.Next(sexos.Length)];

                    yield
                        return new Pessoa(Regex.Replace(columns[0], "\"", ""), sexo,
                        new Endereco
                        {
                            Cep = resultEndereco[random.Next(0, resultEndereco.Count)].Cep,
                            Estado = resultEndereco[random.Next(0, resultEndereco.Count)].Estado,
                            Complemento = resultEndereco[random.Next(0, resultEndereco.Count)].Complemento,
                            Logradouro = resultEndereco[random.Next(0, resultEndereco.Count)].Logradouro,
                        }, new Profissao
                        {
                            TipoProfissao = (TipoProfissao) random.Next(0, Enum.GetValues(typeof(TipoProfissao)).Length),
                            TempoTrabalho = dataInicioTrabalho[random.Next(dataInicioTrabalho.Length)],
                            Salario = salario[random.Next(salario.Length)]
                    });
                }
            }
            finally
            {
                pessoaFile?.Dispose();
            }
        }

        public IEnumerable<(string Estado, int Cep, string Logradouro, string Complemento)> ListaEndereco()
        {
            StreamReader? enderecoFile = null;
            try
            {
                enderecoFile = new StreamReader(filePathEndereco);
                enderecoFile?.ReadLine()?.Skip(0);
                string line;
                while ((line = enderecoFile?.ReadLine()) != null)
                {
                    string[] columns = line.Split("|");
                    yield return (columns[8], int.Parse(columns[0]), columns[2], columns[3]);
                }
            }
            finally
            {
                enderecoFile?.Dispose();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}

