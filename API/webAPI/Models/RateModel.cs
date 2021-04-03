using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class RateModel
    {
        public static List<RateDTO> GetWineryRates(int wineryId, ArvinoDbContext db)
        {
            try
            {
                return db.RV_Rate
                    .Include(x => x.RV_Wine)
                        .Where(x => x.RV_Wine.wineryId == wineryId)
                        .Select(w => new RateDTO()
                        {
                            rateId = w.rateId,
                            rateDate = w.rateDate,
                            score = w.score,
                            wineId = w.wineId,
                            ratedByUserEmail = w.ratedByUserEmail,
                            userName = db.RV_User.FirstOrDefault(e => e.email == w.ratedByUserEmail).Name,
                            UserPitcure = db.RV_User.FirstOrDefault(e => e.email == w.ratedByUserEmail).picture
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}