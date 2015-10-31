/*
 *  ********** SVU **********
 ********** Barea_27786 **********
 *********** Object Controller **********
 *********** Create Update Delete Read Object **********
 * */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using pro2.Models;

namespace pro2.Controllers
{
    public class ObjctController : ApiController
    {
        private ExhibitionsContext db = new ExhibitionsContext();

        [HttpGet]
        [ActionName("GetExhById")]
        //Get api/Object/GetExhById/5
        public IEnumerable<Objct> GetExhById(int ExId)
        {

            var Objcts = db.Objcts.Where(o => o.ExhibitionId == ExId);
            return Objcts.AsEnumerable();
        }

        // GET api/Objct
        public IEnumerable<Objct> GetObjcts()
        {
            return db.Objcts.AsEnumerable();
        }

        // GET api/Objct/5
        public Objct GetObjct(int id)
        {
            Objct objct = db.Objcts.Find(id);
            if (objct == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return objct;
        }

        // PUT api/Objct/5
        public HttpResponseMessage PutObjct(int id, Objct objct)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != objct.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(objct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Objct
        [Authorize(Roles = "Administrator")]
        public HttpResponseMessage PostObjct(Objct objct)
        {
            if (ModelState.IsValid)
            {
                db.Objcts.Add(objct);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, objct);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = objct.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Objct/5
        public HttpResponseMessage DeleteObjct(int id)
        {
            Objct objct = db.Objcts.Find(id);
            if (objct == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Objcts.Remove(objct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, objct);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}