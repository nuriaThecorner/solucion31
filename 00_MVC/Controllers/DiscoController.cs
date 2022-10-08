using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_Services.AutoresService;
using _02_Services.CategoriasServices;
using _02_Services.DiscograficaServices;
using _02_Services.DiscosServices;
using _04_Data.Datos;
using _04_Data.ViewModels;

namespace _00_MVC.Controllers
{
    public class DiscoController : Controller
    {
        //private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();



        // GET: Producto
        public ActionResult Index(int? id, bool? categoria)
        {

            ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();
            var discos = db.Disco.Include(p => p.Categoria).Include(p => p.Autor);
            //Si el método Index recibe un parámetro id != null y > 0
            if (id != null && id > 0)
            {
                if (categoria != null)
                {
                    if (categoria == true)
                    {
                        discos = discos.Where(x => x.id_categoria == id);
                        if (discos != null && discos.Count() > 0)
                        {
                            ViewBag.Message = "Discos de la Categoría: " + discos.FirstOrDefault().Categoria.nombre;
                        }
                    }
                    else
                    {
                        discos = discos.Where(x => x.id_autor == id);
                        if (discos != null && discos.Count() > 0)
                        {
                            ViewBag.Message = "Discos del Autor: " + discos.FirstOrDefault().Autor.nombre
                                            //+ "Descripción: " + discos.FirstOrDefault().Autor.decripcion + " "
                                            //+ discos.FirstOrDefault().Autor.imagen + " "
                                            //+ discos.FirstOrDefault().Autor.spotify + " "
                                            //+ discos.FirstOrDefault().Autor.twitter + " ";
                                            ;
                        }
                    }
                }
            }
            return View(discos.ToList());
        }






        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult _DiscoPartialView(Disco disco)
        {
            return View("_DiscoOtraPartialView", disco);
        }


        

        //// GET: Producto
        //public ActionResult Index(int? id, bool? disco)
        //{

        //    ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();
        //    var discos = db.Disco.Include(p => p.Categoria).Include(p => p.Autor);
        //    //Si el método Index recibe un parámetro id != null y > 0
        //    if (id != null && id > 0)
        //    {
        //        if (disco != null)
        //        {
        //            if (disco == true)
        //            {
        //                discos = discos.Where(x => x.id_categoria == id);
        //                if (discos != null && discos.Count() > 0)
        //                {
        //                    ViewBag.Message = "Discos de la Categoría: " + discos.FirstOrDefault().Categoria.nombre;
        //                }
        //            }
        //            else
        //            {
        //                discos = discos.Where(x => x.id_autor == id);
        //                if (discos != null && discos.Count() > 0)
        //                {
        //                    ViewBag.Message = "Discos del Autor: " + discos.FirstOrDefault().Autor.nombre
        //                                    + "Descripción: " + discos.FirstOrDefault().Autor.decripcion + " "
        //                                    + discos.FirstOrDefault().Autor.imagen + " "
        //                                    + discos.FirstOrDefault().Autor.spotify + " "
        //                                    + discos.FirstOrDefault().Autor.twitter + " ";
        //                }
        //            }
        //        }
        //    }
        //    return View(discos.ToList());
        //}


        //// GET: Discos
        //public ActionResult Index(int? id, string madre, string nombreMadre)
        //{
        //    //Necesitamos un IList<Disco> para pasárselo a la View
        //    IList<Disco> discos = null;
        //    //Creamos un objeto de la Clase DiscosService
        //    DiscosService service = null;
        //    service = new DiscosService();
        //    //Lo utilizamos para llegar a su método List 
        //    //Y, así rellenar nuestro IList<Disco> discos
        //    discos = service.List(id, madre); //madre

        //    ViewBag.Message = "";
        //    if (madre != null && madre != "")
        //    {
        //        ViewBag.Message = "Discos de " + madre + ": " + nombreMadre;

        //    }
        //    return View(discos);
        //}

        // GET: Discos/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //hasta aquí
            //Nuevo
            //Necesitamos un objeto Disco para pasárselo a la View
            Disco disco = null;
            //Creamos un objeto de la Clase DiscosService
            DiscosService service = null;
            service = new DiscosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Disco disco
            disco = service.Detail(id.Value);
            //Fin Nuevo
            //Esto como estaba:
            if (disco == null)
            {
                return HttpNotFound();
            }
            return View(disco);
            //hasta aquí
        }

        // GET: Discos/Create
        // GET: Discos/Create
        public ActionResult Create()
        {
            CategoriasAutoresDiscograficasDiscosViewModel viewModel = null;

            DiscosService service = null;
            service = new DiscosService();

            viewModel = service.RellenaViewModel();
            //ViewBag.CustomerID = SelectListsClienteRellenator(null);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(null);
            //ViewBag.shipperID = SelectListsNavieraRellenator(null);
            return View(viewModel);
        }

        // POST: Discos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriasAutoresDiscograficasDiscosViewModel viewModel)
        {

            DiscosService service = null;
            if (ModelState.IsValid)
            {
                service = new DiscosService();
                bool ok = false;
                ok = service.Create(viewModel.discos);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Error";

            //ViewBag.CustomerID = SelectListsClienteRellenator(viewModel.disco.CustomerID);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(viewModel.disco.EmployeeID);
            //ViewBag.shipperID = SelectListsNavieraRellenator(viewModel.disco.shipperID);

            //ClientesEmpleadosNavierasDiscoViewModel1 viewModel = null;
            viewModel = service.RellenaViewModel(viewModel);
            //viewModel.discos = viewModel.discos;

            return View(viewModel);
        }

        // GET: Discos/Edit/5
        public ActionResult Edit(int? id)
        {


            CategoriasAutoresDiscograficasDiscosViewModel viewModel = null;

            DiscosService service = null;
            service = new DiscosService();

            viewModel = service.RellenaViewModel();

            Disco disco = null;
            disco = service.Detail(id.Value);
            viewModel.discos = disco;

            //ViewBag.CustomerID = SelectListsClienteRellenator(disco.CustomerID);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(disco.EmployeeID);
            //ViewBag.shipperID = SelectListsNavieraRellenator(disco.shipperID);

            return View(viewModel);
        }

        // POST: Discos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriasAutoresDiscograficasDiscosViewModel viewModel)
        {
            //ESTE OBJETO disco que ha entrado es NUEVO
            //para comprobarlo, buscamos el que está en la Tabla Disco
            if (ModelState.IsValid)
            {
                DiscosService service = new DiscosService();
                bool ok = false;
                //Vamos a testear el registro que hay en la tabla:
                Disco buscada = service.Detail(viewModel.discos.id);
                //Vemos los valores de el objeto Disco buscada
                //id = 9
                //name = Bicho
                //description = Cambiamos la descripción
                //El registro de dentro de la Tabla Disco NO HA CAMBIADO. PORQUE ES OTRO

                ok = service.Edit(viewModel.discos);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            //ViewBag.CustomerID = SelectListsClienteRellenator(disco.CustomerID);
            //ViewBag.EmployeeID = SelectListsEmpleadoRellenator(disco.EmployeeID);
            //ViewBag.shipperID = SelectListsNavieraRellenator(disco.shipperID);

            return View(viewModel);
        }

        // GET: Discos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto Disco para pasárselo a la View
            Disco disco = null;
            //Creamos un objeto de la Clase DiscosService
            DiscosService service = null;
            service = new DiscosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Disco disco
            disco = service.Detail(id.Value);
            //Fin Nuevo
            if (disco == null)
            {
                return HttpNotFound();
            }
            return View(disco);
        }

        // POST: Discos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            //Nuevo
                     //Necesitamos un objeto Disco para pasárselo a la View
            Disco disco = null;
            //Creamos un objeto de la Clase DiscosService
            DiscosService service = null;
            service = new DiscosService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Disco disco
            disco = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(disco);

            return RedirectToAction("Index");
        }
        //SelectListsRellenators
        private SelectList SelectListsClienteRellenator(int? id)
        {
            CategoriasService service = null;
            service = new CategoriasService();
            IList<Categoria> categorias = null;
            categorias = service.List(null);
            SelectList selectList = null;
            if (id != null && id > 0)
            {
                selectList = new SelectList(categorias, "id", "nombre", id);
            }
            else
            {
                selectList = new SelectList(categorias, "id", "nombre");
            }

            return selectList;
        }
        private SelectList SelectListsEmpleadoRellenator(int? id)
        {
            AutoresService service = null;
            service = new AutoresService();
            IList<Autor> autores = null;
            autores = service.List(null);
            SelectList selectList = null;
            if (id != null && id > 0)
            {
                selectList = new SelectList(autores, "id", "nombre", id);
            }
            else
            {
                selectList = new SelectList(autores, "id", "nombre");
            }

            return selectList;
        }
        private SelectList SelectListsNavieraRellenator(int? id)
        {
            DiscograficasService service = null;
            service = new DiscograficasService();
            IList<Discografica> discograficas = null;
            discograficas = service.List(null);
            SelectList selectList = null;
            if (id != null && id > 0)
            {
                selectList = new SelectList(discograficas, "id", "nombre", id);
            }
            else
            {
                selectList = new SelectList(discograficas, "id", "nombre");
            }

            return selectList;
        }




        protected override void Dispose(bool disposing)
        {
            //bool ok = false;
            ////Creamos un objeto de la Clase DiscosService
            //DiscosService service = null;
            //service = new DiscosService();
            ////Lo utilizamos para llegar a su método Dispose 
            //ok = service.Dispose(disposing);

            //base.Dispose(disposing);
        }
    }
}
