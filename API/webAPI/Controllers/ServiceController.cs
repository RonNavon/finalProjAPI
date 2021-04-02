using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServiceController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Service
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(ServiceModel.GetServices(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Service?Wineryid=1
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(int Wineryid, ArvinoDbContext db)
        {
            try
            {
                return Ok(ServiceModel.GetAllServices(Wineryid, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        /// <summary>
        /// https://localhost:44370/api/Service
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] RV_Service value)
        {
            try
            {
                RV_Service service = new RV_Service()
                {
                    serviceName = value.serviceName,
                    serviceCategory = value.serviceCategory,
                    price = value.price,
                    content = value.content,
                    wineryId = value.wineryId,

                };
                db.RV_Service.Add(service);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        /// <summary>
        /// https://localhost:44370/api/Service/1
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                RV_Service s = db.RV_Service.SingleOrDefault(service => service.serviceId == id);
                if (s != null)
                {
                    db.RV_Service.Remove(s);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"service with id {id} was not found to delete!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Service/id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody] RV_Service value)
        {
            try
            {
                RV_Service s = db.RV_Service.SingleOrDefault(service => service.serviceId == id);
                if (s != null)
                {
                    s.serviceName = value.serviceName;
                    s.serviceCategory = value.serviceCategory;
                    s.content = value.content;
                    s.price = value.price;
                    db.SaveChanges();
                    return Ok(s);
                }
                return Content(HttpStatusCode.NotFound,
                    $"sevice was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}