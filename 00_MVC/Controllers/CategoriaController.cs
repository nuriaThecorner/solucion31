using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _02_Services.CategoriasServices;
using _04_Data.Datos;

namespace _00_MVC.Controllers
{
    public class CategoriaController : Controller
    {
        //private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();


        // GET: Categorias
        public ActionResult Index(int? id)
        {
            //Necesitamos un IList<Categoria> para pasárselo a la View
            IList<Categoria> categorias = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método List 
            //Y, así rellenar nuestro IList<Categoria> categorias
            categorias = service.List(id);

            return View(categorias);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            //Esto como estaba:
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //hasta aquí
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Categoria categoria = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoria = service.Detail(id.Value);
            //Fin Nuevo
            //Esto como estaba:
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
            //hasta aquí
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                CategoriasService service = new CategoriasService();
                bool ok = false;
                ok = service.Create(categoria);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Categoria categoria = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoria = service.Detail(id.Value);
            //Fin Nuevo
            if (categoria == null)
            {
                return HttpNotFound();
            }
            //Cogemos el objeto y se lo enviamos a la View
            //LEAMOS LO QUE PONE EN LA VISTA
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                CategoriasService service = new CategoriasService();
                bool ok = false;
                //ESTE OBJETO categoria que ha entrado es NUEVO
                //para comprobarlo, buscamos el que está en la Tabla Categoria
                Categoria buscada = service.Detail(categoria.id);
                //Vemos los valores de el objeto Categoria buscada
                //buscada.CategoryID = 9
                //buscada.CategoryName = Bicho
                //buscada.Description = Cambiamos la descripción
                //El registro de dentro de la Tabla Categoria NO HA CAMBIADO. PORQUE ES OTRO objeto

                ok = service.Edit(categoria);
                if (ok == true)
                {
                    //Si esto sucede, entonces llama al método "Index"
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = "Las Cagao";
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Categoria categoria = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoria = service.Detail(id.Value);
            //Fin Nuevo
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        //A pesar de que el método se llama "DeleteConfirmed"
        //Nosotros podremosacceder a él como "Delete"
        //Gracias a esta línea:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Nuevo
            //Necesitamos un objeto Categoria para pasárselo a la View
            Categoria categoria = null;
            //Creamos un objeto de la Clase CategoriasService
            CategoriasService service = null;
            service = new CategoriasService();
            //Lo utilizamos para llegar a su método Detail 
            //Y, así rellenar nuestro Categoria categoria
            categoria = service.Detail(id);
            //Fin Nuevo
            bool ok = false;
            ok = service.Delete(categoria);

            return RedirectToAction("Index");
        }
        //Disposing, en principio, ya no es necesario.
        //Servía para liberar el DbContext, al cambiar de Clase
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
