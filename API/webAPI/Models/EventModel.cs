using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class EventModel
    {

        public static List<EventDTO> GetEvents(ArvinoDbContext db)
        {
            return db.RV_Event.Select(e => new EventDTO()
            {
                eventId = e.eventId,
                eventName = e.eventName,
                content = e.content,
                price = e.price,
                participantsAmount = e.participantsAmount,
                eventDate = e.eventDate,
                startTime = e.startTime,
                eventImgPath = e.eventImgPath,
                categoryId = e.categoryId ?? 0,
                wineryId = e.wineryId ?? 0,
                ticketsPurchased = e.ticketsPurchased ?? 0
                //ticketsLeft = e.participantsAmount - e.ticketsPurchased
            }).ToList();
        }

        public static List<EventDTO> GetEventsByWinery(int wineryId, ArvinoDbContext db)
        {
            return db.RV_Event.Where(i => i.wineryId == wineryId).Select(e => new EventDTO()
            {
                eventId = e.eventId,
                eventName = e.eventName,
                content = e.content,
                price = e.price,
                participantsAmount = e.participantsAmount,
                eventDate = e.eventDate,
                startTime = e.startTime,
                eventImgPath = e.eventImgPath,
                categoryId = e.categoryId ?? 0,
                wineryId = e.wineryId ?? 0,
                ticketsPurchased = e.ticketsPurchased ?? 0
            }).ToList();
        }
    }
}