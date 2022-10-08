using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _04_Data.Datos;

namespace _01_Api.Controllers
{
    public class TemaApiController : ApiController
    {
        private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();


        // GET: api/Empleados/5
        [ResponseType(typeof(Tema))]
        public IHttpActionResult GetTema (int? id, int? siguiente)
        {
            Tema tema = null;
            if (siguiente == null)
            {
                tema = db.Tema.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
                {
                    tema = db.Tema.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<Tema> temas = db.Tema.Where(x => x.id < id.Value).ToList();
                    if (temas != null && temas.Count() > 0)
                    {
                        int? idDisco = temas.Max(x => x.id);
                        tema = db.Tema.Where(x => x.id == idDisco.Value).FirstOrDefault();
                    }
                }
            }
            if (tema == null)
            {
                tema = db.Tema.Where(x => x.id == id.Value).FirstOrDefault();
            }
            Tema temaTabla = new Tema();
            temaTabla.id = tema.id;
            temaTabla.nombre = tema.nombre;
            temaTabla.id_disco = tema.id_disco;
            temaTabla.link = tema.link; 

            return Ok(temaTabla);
        }






        // GET: api/TemaApi
        public IQueryable<Tema> GetTema()
        {
            return db.Tema;
        }

        // GET: api/TemaApi/5
        [ResponseType(typeof(Tema))]
        public IHttpActionResult GetTema(int id)
        {
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return NotFound();
            }

            return Ok(tema);
        }

        // PUT: api/TemaApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTema(int id, Tema tema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tema.id)
            {
                return BadRequest();
            }

            db.Entry(tema).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TemaApi
        [ResponseType(typeof(Tema))]
        public IHttpActionResult PostTema(Tema tema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tema.Add(tema);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tema.id }, tema);
        }

        // DELETE: api/TemaApi/5
        [ResponseType(typeof(Tema))]
        public IHttpActionResult DeleteTema(int id)
        {
            Tema tema = db.Tema.Find(id);
            if (tema == null)
            {
                return NotFound();
            }

            db.Tema.Remove(tema);
            db.SaveChanges();

            return Ok(tema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemaExists(int id)
        {
            return db.Tema.Count(e => e.id == id) > 0;
        }
    }
}