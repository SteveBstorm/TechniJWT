using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL = JWTDal.Entities;
using API = DemoSecurite.Models;

namespace DemoSecurite.Tools
{
    public static class Mappers
    {
        public static API.User ToAPI(this DAL.User user)
        {
            return new API.User
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
