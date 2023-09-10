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
        public char Sexo { get; set; }
        public Profissao Profissao { get; set; }
        public Endereco endereco { get; set; }

        public Pessoa(string nome, char sexo, Endereco endereco, Profissao profissao)
        {
            Nome = nome;
            this.Sexo = sexo;
            this.endereco = endereco;
            Profissao = profissao;
        }

        public Pessoa()
        {
        }
    }
}
