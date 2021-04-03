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
    public class RateController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Rate/wineryId?Id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Rate/wineryId")]
        public IHttpActionResult GetWineryRates(int Id)
        {
            try
            {
                return Ok(RateModel.GetWineryRates(Id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}