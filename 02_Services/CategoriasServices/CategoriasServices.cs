using _04_Data.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Services.CategoriasServices
{
    public class CategoriasService
    {
        private static ProyectoMusicaDbContext _db = null;
        public CategoriasService()
        {
            if (_db == null)
            {
                _db = new ProyectoMusicaDbContext();
            }
        }

        //Index
        public IList<Categoria> List(int? id)
        {
            IList<Categoria> categorias = null;
            if (id == null || id < 1)
            {
                categorias = _db.Categoria.ToList();
            }
            else
            {
                categorias = _db.Categoria
                                .Where(x => x.id == id)
                                .ToList();
            }

            return categorias;
        }
        //Details
        public Categoria Detail(int id)
        {
            Categoria categoria = null;
            categoria = _db.Categoria
                                .Where(x => x.id == id)
                                .FirstOrDefault();
            return categoria;
        }
        //Create
        public bool Create(Categoria categoria)
        {
            bool ok = false;
            try
            {
                _db.Categoria.Add(categoria);
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
        public bool Edit(Categoria categoria)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Categoria que tiene el mismo id
                //que el objeto que ha creado la view
                Categoria buscada = _db.Categoria
                                    .Where(x => x.id == categoria.id)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                ////buscada.CategoryID = categoria.CategoryID;
                //buscada.id = categoria.id;
                buscada.nombre = categoria.nombre;

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
        public bool Delete(Categoria categoria)
        {
            bool ok = false;
            try
            {
                _db.Categoria.Remove(categoria);
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
