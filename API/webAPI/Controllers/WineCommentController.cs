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
    public class WineCommentController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Comment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] RV_WineComment value)
        {
            try
            {
                RV_WineComment comment = new RV_WineComment()
                {
                    text = value.text,
                    email = value.email,
                    wineId = value.wineId,
                    date = DateTime.Now
                };
                db.RV_WineComment.Add(comment);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        /// <summary>
        /// https://localhost:44370/api/WineComment/2
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(int Id)
        {
            try
            {
                return Ok(WineModel.GetAllWineComments(Id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}