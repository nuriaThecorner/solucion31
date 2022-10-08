using _04_Data.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Services.DiscograficaServices
{
    public class DiscograficasService
    {

        private static ProyectoMusicaDbContext _db = null;
        public DiscograficasService()
        {
            if (_db == null)
            {
                _db = new ProyectoMusicaDbContext();
            }
        }

        //Index
        public IList<Discografica> List(int? id)
        {
            IList<Discografica> discograficas = null;
            if (id == null || id < 1)
            {
                discograficas = _db.Discografica.ToList();
            }
            else
            {
                discograficas = _db.Discografica
                                .Where(x => x.id == id)
                                .ToList();
            }

            return discograficas;
        }
        //Details
        public Discografica Detail(int id)
        {
            Discografica discografica = null;
            discografica = _db.Discografica
                                .Where(x => x.id == id)
                                .FirstOrDefault();
            return discografica;
        }
        //Create
        public bool Create(Discografica discografica)
        {
            bool ok = false;
            try
            {
                _db.Discografica.Add(discografica);
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
        public bool Edit(Discografica discografica)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Categoria que tiene el mismo id
                //que el objeto que ha creado la view
                Discografica buscada = _db.Discografica
                                    .Where(x => x.id == discografica.id)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:
                //buscada.CategoryID = categoria.CategoryID;
                //buscada.id = discografica.id;
                buscada.nombre = discografica.nombre;
                buscada.imagen = discografica.imagen;

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
        public bool Delete(Discografica discografica)
        {
            bool ok = false;
            try
            {
                _db.Discografica.Remove(discografica);
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
