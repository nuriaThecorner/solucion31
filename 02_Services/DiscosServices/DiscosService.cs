using _02_Services.CategoriasServices;
using _02_Services.DiscograficaServices;
using _04_Data.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using _02_Services.AutoresService;
using System.Data.Entity;

using System.Text;
using System.Threading.Tasks;
using _04_Data.ViewModels;

namespace _02_Services.DiscosServices
{

    public class DiscosService
    {
        private static ProyectoMusicaDbContext _db = null;
        public DiscosService()
        {
            if (_db == null)
            {
                _db = new ProyectoMusicaDbContext();
            }
        }


        //Index
        public IList<Disco> List(int? id, string madre)
        {
            IList<Disco> discos = null;
            if (id == null || id < 1)
            {
                discos = _db.Disco
                            .Include(p => p.Categoria)
                            .Include(p => p.Autor)
                            .Include(p => p.Discografica)
                            .ToList();
            }
            else
            {
                if (madre != null && madre != "")
                {
                    if (madre == "Autores")
                    {
                        discos = _db.Disco
                                .Include(p => p.Autor)
                                .Include(p => p.Discografica)
                                .Include(p => p.Categoria)
                                .Where(x => x.id_autor == id)
                                .ToList();
                    }
                    if (madre == "Discograficas")
                    {
                        discos = _db.Disco
                                .Include(p => p.Autor)
                                .Include(p => p.Discografica)
                                .Include(p => p.Categoria)
                                .Where(x => x.id_discografia == id)
                                .ToList();
                    }
                    if (madre == "Categorias")
                    {
                        discos = _db.Disco
                                .Include(p => p.Autor)
                                .Include(p => p.Discografica)
                                .Include(p => p.Categoria)
                                .Where(x => x.id_categoria == id)
                                .ToList();
                    }
                }
            }




            return discos;
        }
        //Details
        public Disco Detail(int id)
        {
            Disco disco = null;
            disco = _db.Disco
                                .Where(x => x.id == id)
                                .FirstOrDefault();
            return disco;
        }
        //Create
        public bool Create(Disco disco)
        {
            bool ok = false;
            try
            {
                _db.Disco.Add(disco);
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
        public bool Edit(Disco disco)
        {
            bool ok = false;
            try
            {
                Disco buscada = _db.Disco
                                    .Where(x => x.id == disco.id)
                                    .FirstOrDefault();

                //buscada.id = disco.id;
                buscada.id_discografia = disco.id_discografia;
                buscada.id_autor = disco.id_autor;
                buscada.id_categoria = disco.id_categoria;
                buscada.nombre = disco.nombre;
                buscada.imagen = disco.imagen;

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
        public bool Delete(Disco disco)
        {
            bool ok = false;
            try
            {
                _db.Disco.Remove(disco);
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
        public CategoriasAutoresDiscograficasDiscosViewModel RellenaViewModel()
        {
            //Creamos el viewModel
            CategoriasAutoresDiscograficasDiscosViewModel viewModel = null;
            viewModel = new CategoriasAutoresDiscograficasDiscosViewModel();

            viewModel = RellenaViewModel(viewModel);

            Disco disco = new Disco();

            disco.id_categoria = viewModel.categorias.FirstOrDefault().id;
            disco.id_autor = viewModel.autores.FirstOrDefault().id;
            disco.id_discografia = viewModel.discograficas.FirstOrDefault().id;

            viewModel.discos = disco;

            return viewModel;
        }
        //Rellenamos solamente los Clientes, Empleados y Navieras del ViewModel
        public CategoriasAutoresDiscograficasDiscosViewModel RellenaViewModel(CategoriasAutoresDiscograficasDiscosViewModel viewModel)
        {
            CategoriasService categoriasService = null;
            categoriasService = new CategoriasService();
            viewModel.categorias = categoriasService.List(null);

            AutoresService2 autoresService = null;
            autoresService = new AutoresService2();
            viewModel.autores = autoresService.List(null);

            DiscograficasService discograficaService = null;
            discograficaService = new DiscograficasService();
            viewModel.discograficas = discograficaService.List(null);

            return viewModel;

        }


    }
}
