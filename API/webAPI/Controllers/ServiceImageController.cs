﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;
using System.Web.Http.Cors;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ServiceImageController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/ServiceImage?serviceId=1
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(int serviceId)
        {
            try
            {
                return Ok(ServiceImageModel.GetAllServiceImages(serviceId, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        /// <summary>
        /// https://localhost:44370/api/ServiceImage
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] ServiceImageDTO value)
        {
            try
            {
                RV_ServiceImage serviceImg = new RV_ServiceImage()
                {
                    ImgPath = value.ImgPath,
                    serviceId = value.serviceId

                };
                db.RV_ServiceImage.Add(serviceImg);
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