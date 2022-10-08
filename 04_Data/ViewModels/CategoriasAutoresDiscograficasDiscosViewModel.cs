using _04_Data.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Data.ViewModels
{

    public class CategoriasAutoresDiscograficasDiscosViewModel
    {

        public IList<Autor> autores { get; set; }

        public IList<Discografica> discograficas { get; set; }

        public IList<Categoria> categorias { get; set; }


        public Disco discos { get; set; }

    }
}
