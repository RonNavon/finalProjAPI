using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class ServiceModel
    {

        public static List<ServiceDTO> GetServices(ArvinoDbContext db)
        {
            List<ServiceDTO> returnList = new List<ServiceDTO>();


            return db.RV_Service.Select(s => new ServiceDTO()
            {
                serviceId = s.serviceId,
                serviceName = s.serviceName,
                serviceCategory = s.serviceCategory,
                content = s.content,
                price = s.price,
                wineryId =s.wineryId ?? 0,
                wineryName = db.RV_Winery.FirstOrDefault(i=>i.wineryId == s.wineryId).wineryName,
                wineryImg=db.RV_Winery.FirstOrDefault(i=>i.wineryId==s.wineryId).IconImgPath,
                images = db.RV_ServiceImage
                    .Where(i => i.serviceId == s.serviceId)
                        .Select(dt => new ServiceImageDTO()
                        {
                            imgId = dt.imgId,
                            ImgPath = dt.ImgPath

                        }).ToList()

             }).ToList();
        }

    public static List<ServiceDTO> GetAllServices(int wineryId, ArvinoDbContext db)
    {
        return db.RV_Service.Where(i => i.wineryId == wineryId).Select(s => new ServiceDTO()
        {
            serviceId = s.serviceId,
            serviceName = s.serviceName,
            serviceCategory = s.serviceCategory,
            content = s.content,
            price = s.price,
            wineryId = s.wineryId ?? 0,

        }).ToList();
    }


    public static RV_Service GetService(int id, ArvinoDbContext db)
    {
        return db.RV_Service.SingleOrDefault(x => x.serviceId == id);
    }

}
}