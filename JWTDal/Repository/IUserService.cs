using JWTDal.Entities;
using System.Collections.Generic;

namespace JWTDal.Repository
{
    public interface IUserService
    {
        void Delete(int Id);
        IEnumerable<User> GetAll();
        User GetById(int Id);
        User Login(string email, string password);
        void Register(string email, string password);
    }
}