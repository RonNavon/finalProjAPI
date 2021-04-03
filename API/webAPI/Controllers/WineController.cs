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
using System.Data.Entity;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WineController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Wine
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(WineModel.GetAllWines(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        ///  https://localhost:44370/api/Wine/category?categoryId=2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Wine/category/")]
        public IHttpActionResult GetWineByCategory(int categoryId)
        {
            try
            {
                return Ok(WineModel.GetWineByCategory(categoryId, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.InnerException);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine/Wineryid?Wineryid=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Wine/Wineryid/")]
        public IHttpActionResult Get(int Wineryid)
        {
            try
            {
                return Ok(WineModel.GetAllWineInWinery(Wineryid, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] WineDTO value)
        {
            try
            {
                RV_Wine wine = new RV_Wine()
                {
                    wineName = value.wineName,
                    content = value.content,
                    price = value.price,
                    wineImgPath = value.wineImgPath,
                    wineLabelPath = value.wineLabelPath,
                    categoryId = value.categoryId,
                    wineryId = value.wineryId
                };
                db.RV_Wine.Add(wine);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine?id=1
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                RV_Wine w = db.RV_Wine.SingleOrDefault(wine => wine.wineId == id);
                if (w != null)
                {
                    db.RV_Wine.Remove(w);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with id {id} was not found to delete!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine/id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody] WineDTO value)
        {
            try
            {
                RV_Wine w = db.RV_Wine.SingleOrDefault(x => x.wineId == id);
                if (w != null)
                {
                    w.wineName = value.wineName;
                    w.content = value.content;
                    w.price = value.price;
                    w.wineImgPath = value.wineImgPath;
                    w.wineLabelPath = value.wineLabelPath;
                    w.categoryId = value.categoryId;
                    w.wineryId = value.wineryId;
                    db.SaveChanges();
                    return Ok(w);
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with id {id} was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [Route("api/RandomWines")]
        public IHttpActionResult GetRandomWines(int numOfWines)
        {
            try
            {
                List<WineDTO> RWines = new List<WineDTO>();
                List<RV_Wine> list = db.RV_Wine.ToList() ;
                
                for (int i = 0; i < numOfWines; i++)
                {
                    var rand = new Random().Next(list.Count);
                    RV_Wine temp = list[rand];
                    list.Remove(list[rand]);
                    WineDTO sWineDTO = new WineDTO()
                    {
                        wineId=temp.wineId,
                        wineImgPath=temp.wineImgPath,
                        wineName=temp.wineName,
                        content=temp.content,
                        price=temp.price,
                        wineryName= db.RV_Winery.FirstOrDefault(j=>j.wineryId== temp.wineryId).wineryName,
                        wineryImage = db.RV_Winery.FirstOrDefault(j => j.wineryId == temp.wineryId).IconImgPath,
                        wineryId = db.RV_Winery.FirstOrDefault(j => j.wineryId == temp.wineryId).wineryId

                    };

                    RWines.Add(sWineDTO);
                }
                return Ok(RWines);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}