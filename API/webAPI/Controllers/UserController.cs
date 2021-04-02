using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;
using System.Web.Http.Cors;
using System.Data.Entity.Validation;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/User/email?email=asaf@gmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/email")]
        public IHttpActionResult GetEmail(string email)
        {
            try
            {
                return Ok(UserModel.GetUser(email, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/User
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] RV_User value)
        {
            try
            {
                RV_User user = new RV_User()
                {
                    email = value.email,
                    password = value.password,
                    Name = value.Name,
                    phone = Convert.ToString(value.phone),
                    registrationDate = DateTime.Now,
                    picture = value.picture,
                    isOlder = value.isOlder,
                    typeId = value.typeId
                };
                db.RV_User.Add(user);
                db.SaveChanges();
                return Ok();
            }
            catch (DbEntityValidationException ex)
            {
                string error = "";
                foreach (DbEntityValidationResult vr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError er in vr.ValidationErrors)
                    {
                        error += er.ErrorMessage + "\n";
                    }
                }
                return Content(HttpStatusCode.BadRequest, error);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }




        /// <summary>
        /// https://localhost:44370/api/User/email?email=asaf@gmail.com
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/User/email")]
        public IHttpActionResult Put(string email, [FromBody] RV_User value)
        {
            try
            {
                RV_User user = db.RV_User.SingleOrDefault(x => x.email == email);
                if (user != null)
                {
                    user.email = value.email;
                    user.password = value.password;
                    user.Name = value.Name;
                    user.phone = value.phone;
                    user.picture = value.picture;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with email {email} was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}