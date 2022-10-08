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
    public class DiscoApiController : ApiController
    {
        private ProyectoMusicaDbContext db = new ProyectoMusicaDbContext();



        // GET: api/Discos
        public IList<Disco> GetDisco()
        {
            IList<Disco> discosTabla = db.Disco.ToList();
            IList<Disco> discos = new List<Disco>();
            foreach (var discoTabla in discosTabla)
            {
                Disco disco = new Disco();
                disco.id = discoTabla.id;
                disco.nombre = discoTabla.nombre;
                disco.imagen = discoTabla.imagen;
                disco.id_autor = discoTabla.id_autor;
                disco.id_discografia = discoTabla.id_discografia;
                disco.id_categoria = discoTabla.id_categoria;

                discos.Add(disco);
            }
            return discos;
        }

        // GET: api/Discos/5
        [ResponseType(typeof(Disco))]
        public IHttpActionResult GetDisco(int id)
        {
            Disco discoTabla = db.Disco.Find(id);
            if (discoTabla == null)
            {
                return NotFound();
            }

            Disco disco = new Disco();
            disco.id = discoTabla.id;
            disco.nombre = discoTabla.nombre;
            disco.imagen = discoTabla.imagen;
            disco.id_autor = discoTabla.id_autor;
            disco.id_discografia = discoTabla.id_discografia;
            disco.id_categoria = discoTabla.id_categoria;


            return Ok(disco);
        }


        //// GET: api/DiscoApi
        //public IQueryable<Disco> GetDisco()
        //{
        //    return db.Disco;
        //}



        // GET: api/Discos/5
        [ResponseType(typeof(Disco))]
        public IHttpActionResult GetDisco(int? id, int? siguiente)
        {
            Disco disco = null;
            if (siguiente == null)
            {
                disco = db.Disco.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
                {
                    disco = db.Disco.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<Disco> discos = db.Disco.Where(x => x.id < id.Value).ToList();
                    if (discos != null && discos.Count() > 0)
                    {
                        int? idDisco = discos.Max(x => x.id);
                        disco = db.Disco.Where(x => x.id == idDisco.Value).FirstOrDefault();
                    }
                }
            }
            if (disco == null)
            {
                disco = db.Disco.Where(x => x.id == id.Value).FirstOrDefault();
            }
            Disco discoTabla = new Disco();
            discoTabla.id = disco.id;
            discoTabla.nombre = disco.nombre;
            discoTabla.imagen = disco.imagen;
            discoTabla.id_autor = disco.id_autor;
            discoTabla.id_discografia = disco.id_discografia;
            discoTabla.id_categoria = disco.id_categoria;

            return Ok(discoTabla);
        }







        //// GET: api/DiscoApi/5
        //[ResponseType(typeof(Disco))]
        //public IHttpActionResult GetDisco(int id)
        //{
        //    Disco disco = db.Disco.Find(id);
        //    if (disco == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(disco);
        //}

        // PUT: api/DiscoApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDisco(int id, Disco disco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disco.id)
            {
                return BadRequest();
            }

            db.Entry(disco).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscoExists(id))
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

        // POST: api/DiscoApi
        [ResponseType(typeof(Disco))]
        public IHttpActionResult PostDisco(Disco disco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Disco.Add(disco);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = disco.id }, disco);
        }

        // DELETE: api/DiscoApi/5
        [ResponseType(typeof(Disco))]
        public IHttpActionResult DeleteDisco(int id)
        {
            Disco disco = db.Disco.Find(id);
            if (disco == null)
            {
                return NotFound();
            }

            db.Disco.Remove(disco);
            db.SaveChanges();

            return Ok(disco);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscoExists(int id)
        {
            return db.Disco.Count(e => e.id == id) > 0;
        }
    }
}