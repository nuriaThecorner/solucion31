using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_Services.AutoresService;
using _04_Data.Datos;

namespace _00_MVC.Controllers
{
    public class AutorController : Controller
    {
        private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();


        // GET: Autor
        public ActionResult Index(int? id)
        {
            //Necesitamos un IList<Categoria> para pasárselo a la View
            IList<Autor> autores = null;
            //Creamos un objeto de la Clase CategoriasService
            AutoresService service = null;
            service = new AutoresService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<Categoria> autores
            autores = service.List(id);

            return View(autores);
        }

        // GET: Autores/Details/5
        //public ActionResult Details(int? id)
        //{
        //    //Esto como estaba:
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Autor autor = null;

        //    AutoresService service = null;
        //    service = new AutoresService();

        //    autor = service.Detail(id.Value);

        //    if (autor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(autor);
        //    //hasta aquí
        //}


        public ActionResult Details(int? id, bool? siguiente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = null;
            if (siguiente == null)
            {
                autor = db.Autor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == true)
                {
                    autor = db.Autor.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<Autor> empleados = db.Autor.Where(x => x.id < id.Value).ToList();
                    if (empleados != null && empleados.Count() > 0)
                    {
                        int? idEmpleado = empleados.Max(x => x.id);
                        autor = db.Autor.Where(x => x.id == idEmpleado.Value).FirstOrDefault();
                    }
                }
            }
            if (autor == null)
            {
                autor = db.Autor.Where(x => x.id == id.Value).FirstOrDefault();
            }
            return View(autor);
        }







        // GET: Autores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                AutoresService service = new AutoresService();
                bool ok = false;
                ok = service.Create(autor);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(autor);
        }

        // GET: Autores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Autor autor = null;

            AutoresService service = null;
            service = new AutoresService();

            autor = service.Detail(id.Value);

            if (autor == null)
            {
                return HttpNotFound();
            }

            return View(autor);
        }

        // POST: Autores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Autor autor)
        {
            if (ModelState.IsValid)
            {
                AutoresService service = new AutoresService();
                bool ok = false;

                Autor buscada = service.Detail(autor.id);


                ok = service.Edit(autor);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(autor);
        }

        // GET: Autores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Autor autor = null;

            AutoresService service = null;
            service = new AutoresService();

            autor = service.Detail(id.Value);
            //Fin Nuevo
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        // POST: Autores/Delete/5
        //A pesar de que el método se llama "DeleteConfirmed"
        //Nosotros podremosacceder a él como "Delete"
        //Gracias a esta línea:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Autor autor = null;

            AutoresService service = null;
            service = new AutoresService();

            autor = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(autor);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //bool ok = false;
            ////Creamos un objeto de la Clase CategoriasService
            //CategoriasService service = null;
            //service = new CategoriasService();
            ////Lo utilizamos para llegar a su método Dispose 
            //ok = service.Dispose(disposing);

            //base.Dispose(disposing);
        }
    }
}
