using frugalcafe_test_openlist.Model;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace br.com.logica
{
    public class ReadFileIBGE
    {
        private string filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "File"),"ibge-fem-10000.csv");

        string[] estados = { "SP", "RJ", "MG", "RS", "BA", "PR", "PE", "CE", "SC", "GO" };
        char[] sexos = { 'M', 'F' };
        public IEnumerable<Pessoa> ReadFile()
        {
            StreamReader? sr = null;
            Random random = new Random();
            try
            {
                sr = new StreamReader(filePath);
                sr?.ReadLine()?.Skip(0);
                string line;
                while ((line = sr?.ReadLine()) != null)
                {
                    string[] columns = line.Split(",");
                    string estado = estados[random.Next(estados.Count())];
                    char sexo = sexos[random.Next(sexos.Length)];
                    yield 
                        return new Pessoa(Regex.Replace(columns[0], "\"", ""), estado, sexo);
                }
            }
            finally 
            {
                sr?.Dispose();
            }
        }
    }
}

