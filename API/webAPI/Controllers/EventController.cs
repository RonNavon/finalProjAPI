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
    public class EventController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Event
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] EventDTO value)
        {
            try
            {
                RV_Event newEvent = new RV_Event()
                {
                    eventName = value.eventName,
                    content = value.content,
                    price = value.price,
                    participantsAmount = value.participantsAmount,
                    eventDate = value.eventDate,
                    startTime = value.startTime,
                    eventImgPath = value.eventImgPath,
                    categoryId = value.categoryId,
                    wineryId = value.wineryId
                };
                db.RV_Event.Add(newEvent);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Event/id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody] RV_Event value)
        {
            try
            {
                RV_Event e = db.RV_Event.SingleOrDefault(x => x.eventId == id);
                if (e != null)
                {
                    e.eventName = value.eventName;
                    e.content = value.content;
                    e.price = value.price;
                    e.participantsAmount = value.participantsAmount;
                    e.eventDate = value.eventDate;
                    e.startTime = value.startTime;
                    e.eventImgPath = value.eventImgPath;
                    e.categoryId = value.categoryId;
                    e.wineryId = value.wineryId;
                    db.SaveChanges();
                    return Ok(e);
                }
                return Content(HttpStatusCode.NotFound,
                    $"event with id {id} was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Event?id=1
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(EventModel.GetEventsByWinery(id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Event
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(EventModel.GetEvents(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Event?id=1
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                RV_Event eventD = db.RV_Event.SingleOrDefault(e => e.eventId == id);
                if (eventD != null)
                {
                    db.RV_Event.Remove(eventD);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"event with id {id} was not found to delete!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}