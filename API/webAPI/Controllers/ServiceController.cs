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
        public IHttpActionResult Post([FromBody] ServiceDTO value)
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

    }
}