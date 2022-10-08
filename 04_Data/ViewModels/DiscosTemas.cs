using _04_Data.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Data.ViewModels
{
    public class DiscosTemas
    {
        public IList<Disco> discos { get; set; }

        public IList<Autor> autores { get; set; }

        public IList<Discografica> discograficas { get; set; }

        public IList<Categoria> categorias { get; set; }



        public Tema temas { get; set; }

    }
}
