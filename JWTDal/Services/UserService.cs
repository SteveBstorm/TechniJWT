using ADOLibrary;
using JWTDal.Entities;
using JWTDal.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTDal.Services
{
    public class UserService : IUserService
    {
        private string _connectionString;

        public UserService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }

        private User Converter(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Email = reader["Email"].ToString(),
                IsAdmin = (bool)reader["IsAdmin"]
            };
        }

        public void Register(string email, string password)
        {
            Command cmd = new Command("UserRegister", true);
            cmd.AddParameter("Email", email);
            cmd.AddParameter("Password", password);
            cmd.AddParameter("IsAdmin", 0);

            Connection conn = new Connection(_connectionString);
            conn.ExecuteNonQuery(cmd);
        }

        public User Login(string email, string password)
        {
            Command cmd = new Command("UserLogin", true);
            cmd.AddParameter("Email", email);
            cmd.AddParameter("Password", password);

            Connection conn = new Connection(_connectionString);
            try
            {
                return conn.ExecuteReader(cmd, Converter).FirstOrDefault();
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<User> GetAll()
        {
            Command cmd = new Command("SELECT * FROM AppUser");
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Converter);
        }

        public User GetById(int Id)
        {
            Command cmd = new Command("SELECT * FROM AppUser WHERE Id = @Id");
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            return conn.ExecuteReader(cmd, Converter).FirstOrDefault();
        }

        public void Delete(int Id)
        {
            Command cmd = new Command("DELETE FROM AppUser WHERE Id = @Id");
            cmd.AddParameter("Id", Id);
            Connection conn = new Connection(_connectionString);
            conn.ExecuteNonQuery(cmd);
        }
    }
}
