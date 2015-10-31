/*
 *  ********** SVU **********
 ********** Barea_27786 **********
 *********** Exhibition Controller **********
 *********** Create Update Delete Read Exhibition **********
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
    public class ExhibitionController : ApiController
    {
        private ExhibitionsContext db = new ExhibitionsContext();

        // GET api/Exhibition
        public IEnumerable<Exhibition> GetExhibitions()
        {
            return db.Exhibitions.AsEnumerable();
        }

        // GET api/Exhibition/5
        public Exhibition GetExhibition(int id)
        {
            Exhibition exhibition = db.Exhibitions.Find(id);
            if (exhibition == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return exhibition;
        }

        // PUT api/Exhibition/5
        public HttpResponseMessage PutExhibition(int id, Exhibition exhibition)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != exhibition.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(exhibition).State = EntityState.Modified;

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

        // POST api/Exhibition
        [Authorize(Roles = "Administrator")]
        public HttpResponseMessage PostExhibition(Exhibition exhibition)
        {
            if (ModelState.IsValid)
            {
                db.Exhibitions.Add(exhibition);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, exhibition);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = exhibition.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Exhibition/5
        public HttpResponseMessage DeleteExhibition(int id)
        {
            Exhibition exhibition = db.Exhibitions.Find(id);
            if (exhibition == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Exhibitions.Remove(exhibition);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, exhibition);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}