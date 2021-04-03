using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class WineModel
    {

        public static List<WineDTO> GetWineByCategory(int WineCategoryId, ArvinoDbContext db)
        {
            return db.RV_Wine.Where(i=>i.categoryId == WineCategoryId)
                    .Select(w => new WineDTO()
            {
                wineId = w.wineId,
                wineName = w.wineName,
                wineImgPath = w.wineImgPath,
                content = w.content,
                price = w.price,
                wineLabelPath = w.wineLabelPath,
                categoryId = w.categoryId ?? 0,
                wineryId = w.wineryId,
                wineryName = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).wineryName,
                wineryImage = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).IconImgPath

            }).ToList();
        }

        public static List<WineDTO> GetAllWines(ArvinoDbContext db)
        {
            return db.RV_Wine.Select(w => new WineDTO()
            {
                wineId = w.wineId,
                wineName = w.wineName,
                wineImgPath = w.wineImgPath,
                content = w.content,
                price = w.price,
                wineLabelPath = w.wineLabelPath,
                categoryId = w.categoryId ?? 0,
                wineryId = w.wineryId,
                wineryName=db.RV_Winery.FirstOrDefault(i=>i.wineryId == w.wineryId).wineryName,
                wineryImage=db.RV_Winery.FirstOrDefault(i=>i.wineryId== w.wineryId).IconImgPath

            }).ToList();
        }

        public static List<WineDTO> GetAllWineInWinery(int wineryId, ArvinoDbContext db)
        {
            return db.RV_Wine.Where(i => i.wineryId == wineryId).Select(w => new WineDTO()
            {
                wineId = w.wineId,
                wineName = w.wineName,
                wineImgPath = w.wineImgPath,
                content = w.content,
                price = w.price,
                wineLabelPath = w.wineLabelPath,
                categoryId = w.categoryId ?? 0,
                wineryId = w.wineryId

            }).ToList();
        }
    }
}