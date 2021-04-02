using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;
using System.Web.Http.Cors;
using webAPI.Models;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WineryController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Winery
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] RV_Winery value)
        {
            try
            {
                RV_Winery winery = new RV_Winery()
                {
                    wineryName = value.wineryName,
                    wineryAddress = value.wineryAddress,
                    wineryEmail = value.wineryEmail,
                    phone = value.phone,
                    statusType = value.statusType,
                    IconImgPath = value.IconImgPath,
                    wineryManagerEmail = value.wineryManagerEmail,
                    areaId = value.areaId
                };
                db.RV_Winery.Add(winery);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(WineryModel.GetWinery(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery/wineryManagerEmail?wineryManagerEmail=asaf@gmail.com
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery/wineryManagerEmail")]
        public IHttpActionResult Get(string wineryManagerEmail)
        {
            try
            {
                return Ok(WineryModel.GetWineryByUser(db, wineryManagerEmail));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        ///  https://localhost:44370/api/Winery/area?areaID=2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery/area/")]
        public IHttpActionResult GetWineryByArea(int areaID)
        {
            try
            {
                return Ok(WineryModel.GetWineryByArea(db, areaID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}