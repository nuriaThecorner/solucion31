using _04_Data.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Services.AutoresService
{

    public class AutoresService2
    {
        private static ProyectoMusicaDbContext _db = null;
        public AutoresService2()
        {
            if (_db == null)
            {
                _db = new ProyectoMusicaDbContext();
            }
        }

        //Index
        public IList<Autor> List(int? id)
        {
            IList<Autor> autores = null;
            if (id == null || id < 1)
            {
                autores = _db.Autor.ToList();
            }
            else
            {
                autores = _db.Autor
                                .Where(x => x.id == id)
                                .ToList();
            }

            return autores;
        }
        //Details
        public Autor Detail(int id)
        {
            Autor autor = null;
            autor = _db.Autor
                                .Where(x => x.id == id)
                                .FirstOrDefault();
            return autor;
        }
        //Create
        public bool Create(Autor autor)
        {
            bool ok = false;
            try
            {
                _db.Autor.Add(autor);
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Edit
        public bool Edit(Autor autor)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Categoria que tiene el mismo id
                //que el objeto que ha creado la view
                Autor buscada = _db.Autor
                                    .Where(x => x.id == autor.id)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                //buscada.CategoryID = categoria.CategoryID;
                //buscada.id = autor.id;
                buscada.nombre = autor.nombre;
                buscada.decripcion = autor.decripcion;
                buscada.imagen = autor.imagen;
                buscada.spotify = autor.spotify;
                buscada.twitter = autor.twitter;

                //Guardamos cambios:
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Delete
        public bool Delete(Autor autor)
        {
            bool ok = false;
            try
            {
                _db.Autor.Remove(autor);
                //Guardamos cambios:
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //SaveChanges
        public bool SaveChanges()
        {
            bool ok = false;
            try
            {
                int retorno = 0;
                retorno = _db.SaveChanges();
                if (retorno > 0)
                {
                    ok = true;
                }
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Dispose
        public bool Dispose(bool ok)
        {
            if (ok == true)
            {
                _db.Dispose();
            }

            return ok;
        }
    }
}
