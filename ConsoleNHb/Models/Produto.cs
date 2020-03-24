using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNHb.Models
{
    //TODOS OS MEMBROS DEVEM SER VIRTUAL
    public class Produto
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual ProdutoGrupo Grupo { get; set; }
        public virtual decimal PrecoVenda { get; set; }
    }
}
