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
            return db.RV_Wine.Where(i => i.categoryId == WineCategoryId)
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
                        wineryImage = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).IconImgPath,
                        areaCategoryName = db.RV_AreaCategory
                        .FirstOrDefault(i => i.areaId == db.RV_Winery
                            .FirstOrDefault(r => r.wineryId == w.wineryId).areaId)
                                .areaName,

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
                wineryName = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).wineryName,
                wineryImage = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).IconImgPath,
                areaCategoryName = db.RV_AreaCategory
                        .FirstOrDefault(i => i.areaId == db.RV_Winery
                            .FirstOrDefault(r => r.wineryId == w.wineryId).areaId)
                                .areaName,

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


        public static List<WineCommentDTO> GetAllWineComments(int wineId, ArvinoDbContext db)
        {
            try
            {

                return db.RV_WineComment
                    .Where(i => i.wineId == wineId)
                        .Select(w => new WineCommentDTO()
                        {
                            id=w.id,
                            email = w.email,
                            text=w.text,
                            date=w.date,
                            wineId=w.wineId,
                            userName = db.RV_User.FirstOrDefault(e=>e.email == w.email).Name,
                            UserPitcure = db.RV_User.FirstOrDefault(e => e.email == w.email).picture

                        }).ToList();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}