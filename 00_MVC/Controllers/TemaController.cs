using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_Services.TemasServices;
using _04_Data.Datos;
using _04_Data.ViewModels;

namespace _00_MVC.Controllers
{
    public class TemaController : Controller
    {
        private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult _TemaPartialView(Tema tema)
        {
            return View("_TemaPartialView", tema);
        }






        // GET: Temas
        public ActionResult Index(int? id, string madre, string nombreMadre)
        {
            //Necesitamos un IList<Pedido> para pasárselo a la View
            IList<Tema> temas = null;
            //Creamos un objeto de la Clase PedidosService
            TemasService service = null;
            service = new TemasService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<DetallePedido> pedidos
            temas = service.List(id, madre);

            ViewBag.Message = "";
            if (madre != null && madre != "")
            {
                ViewBag.Message = "Temas del " + madre + ": " + nombreMadre;

            }
            return View(temas);
        }




        //// GET: Tema
        //public ActionResult Index()
        //{
        //    var tema = db.Tema.Include(t => t.Disco);
        //    return View(tema.ToList());
        //}

        // GET: Tema/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            return View(tema);
        }

        // GET: Tema/Create
        public ActionResult Create()
        {
            ViewBag.id_disco = new SelectList(db.Disco, "id", "nombre");
            return View();
        }

        // POST: Tema/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_disco,nombre,link")] Tema tema)
        {
            if (ModelState.IsValid)
            {
                db.Tema.Add(tema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_disco = new SelectList(db.Disco, "id", "nombre", tema.id_disco);
            return View(tema);
        }

        // GET: Tema/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_disco = new SelectList(db.Disco, "id", "nombre", tema.id_disco);
            return View(tema);
        }

        // POST: Tema/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_disco,nombre,link")] Tema tema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_disco = new SelectList(db.Disco, "id", "nombre", tema.id_disco);
            return View(tema);
        }

        // GET: Tema/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return HttpNotFound();
            }
            return View(tema);
        }

        // POST: Tema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tema tema = db.Tema.Find(id);
            db.Tema.Remove(tema);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
