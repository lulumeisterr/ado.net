using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frugalcafe_test_openlist.Model
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
        public char Sexo { get; set; }

        public Pessoa(string nome, string estado, char sexo)
        {
            Nome = nome;
            this.Estado = estado;
            this.Sexo = sexo;
        }

        public Pessoa()
        {
        }
    }
}
