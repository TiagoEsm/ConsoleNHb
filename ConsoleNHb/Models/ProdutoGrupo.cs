using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNHb.Models
{
    //TODOS OS MEMBROS DEVEM SER VIRTUAL
    public class ProdutoGrupo
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
    }
}
