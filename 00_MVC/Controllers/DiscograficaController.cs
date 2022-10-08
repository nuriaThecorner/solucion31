using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_Services.DiscograficaServices;
using _04_Data.Datos;

namespace _00_MVC.Controllers
{
    public class DiscograficaController : Controller
    {
        //private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();

        // GET: Discografica
        public ActionResult Index(int? id)
        {
            //Necesitamos un IList<Categoria> para pasárselo a la View
            IList<Discografica> discograficas = null;
            //Creamos un objeto de la Clase CategoriasService
            DiscograficasService service = null;
            service = new DiscograficasService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<Categoria> autores
            discograficas = service.List(id);

            return View(discograficas);
        }

        // GET: Discograficas/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Discografica discografica = null;

            DiscograficasService service = null;
            service = new DiscograficasService();

            discografica = service.Detail(id.Value);

            if (discografica == null)
            {
                return HttpNotFound();
            }
            return View(discografica);
            //hasta aquí
        }

        // GET: Discograficas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Discograficas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Discografica discografica)
        {
            if (ModelState.IsValid)
            {
                DiscograficasService service = new DiscograficasService();
                bool ok = false;
                ok = service.Create(discografica);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(discografica);
        }

        // GET: Discograficas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Discografica discografica = null;

            DiscograficasService service = null;
            service = new DiscograficasService();

            discografica = service.Detail(id.Value);

            if (discografica == null)
            {
                return HttpNotFound();
            }

            return View(discografica);
        }

        // POST: Discograficas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Discografica discografica)
        {
            if (ModelState.IsValid)
            {
                DiscograficasService service = new DiscograficasService();
                bool ok = false;

                Discografica buscada = service.Detail(discografica.id);


                ok = service.Edit(discografica);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(discografica);
        }

        // GET: Discograficas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Discografica discografica = null;

            DiscograficasService service = null;
            service = new DiscograficasService();

            discografica = service.Detail(id.Value);
            //Fin Nuevo
            if (discografica == null)
            {
                return HttpNotFound();
            }
            return View(discografica);
        }

        // POST: Discograficas/Delete/5
        //A pesar de que el método se llama "DeleteConfirmed"
        //Nosotros podremosacceder a él como "Delete"
        //Gracias a esta línea:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Discografica discografica = null;

            DiscograficasService service = null;
            service = new DiscograficasService();

            discografica = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(discografica);

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
