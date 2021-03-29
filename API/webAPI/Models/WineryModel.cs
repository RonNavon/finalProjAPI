using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class WineryModel
    {
        public static List<RV_Winery> GetWinery(ArvinoDbContext db)
        {
            return db.RV_Winery.ToList();
        }

        public static List<RV_Winery> GetWineryByArea(ArvinoDbContext db, int areaId)
        {
            return db.RV_Winery.Where(e => e.areaId == areaId).ToList();
        }

      
    }

   
}