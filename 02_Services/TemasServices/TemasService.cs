using _04_Data.Datos;
using _04_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using _02_Services.DiscosServices;

namespace _02_Services.TemasServices
{
    public class TemasService
    {

        private static ProyectoMusicaDbContext _db = null;
        public TemasService()
        {
            if (_db == null)
            {
                _db = new ProyectoMusicaDbContext();
            }
        }

        //Index
        public IList<Tema> List(int? id, string madre)
        {
            IList<Tema> temas = null;
            if (id == null || id < 1)
            {
                temas = _db.Tema
                            .Include(p => p.Disco)
                            .ToList();
            }
            else
            {
                if (madre != null && madre != "")
                {
                    if (madre == "Disco")
                    {
                        temas = _db.Tema
                            .Include(p => p.Disco)
                                .Where(x => x.id_disco == id)
                                .ToList();
                    }


                }


            }

            return temas;
        }
        //Details
        public Tema Detail(int id)
        {
            Tema tema = null;
            tema = _db.Tema
                                .Where(x => x.id == id)
                                .FirstOrDefault();
            return tema;
        }
        //Create
        public bool Create(Tema tema)
        {
            bool ok = false;
            try
            {
                _db.Tema.Add(tema);
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
        public bool Edit(Tema tema)
        {
            bool ok = false;
            try
            {
                //Buscamos el registro de la Tabla Pedido que tiene el mismo id
                //que el objeto que ha creado la view
                Tema buscada = _db.Tema
                                    .Where(x => x.id == tema.id)
                                    .FirstOrDefault();
                //Le pasamos los valores del objeto que ha creado la vista:

                //buscada.OrderDetailID = tema.OrderDetailID;

                //buscada.id = tema.id;
                buscada.id = tema.id;
                buscada.id = tema.id;


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
        public bool Delete(Tema tema)
        {
            bool ok = false;
            try
            {
                _db.Tema.Remove(tema);
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


        //Creamos y Rellenamos el ViewModel
        public DiscosTemas RellenaViewModel()
        {
            //Creamos el viewModel
            DiscosTemas viewModel = null;
            viewModel = new DiscosTemas();
            //Rellenamos solamente los Clientes, Empleados y Navieras del ViewModel
            viewModel = RellenaViewModel(viewModel);
            //Creamos un nuevo Pedido
            Tema tema = new Tema();
            //Rellenamos los 3 campos IMPRESCINDIBLES de el nuevo objeto Pedido pedido 
            //Con el id del primer elemento de cada una de las tres listas
            tema.id = viewModel.discos.FirstOrDefault().id;

            viewModel.temas = tema;

            return viewModel;
        }
        //Rellenamos solamente los Clientes, Empleados y Navieras del ViewModel
        public DiscosTemas RellenaViewModel(DiscosTemas viewModel)
        {
            DiscosService discosService = null;
            discosService = new DiscosService();
            viewModel.discos = discosService.List(null, null);

            return viewModel;

        }

    }
}
