using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_DataInfo
{
    public class Tarefa
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; } // ENUMERATOR ??
        public DateTime DataCriacao { get; set; }
    }
}
