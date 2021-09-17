using DemoSecurite.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoSecurite.Tools
{
    public class TokenManager : ITokenManager
    {
        public static string secretKey = "Les framboises sont perchées sur le tabouret de mon grand père";
        public static string issuer = "mysite.com";
        public static string audience = "myapidomain.com";

        public User GenerateJWT(User user)
        {
            //Création de la clé de validation (Credential)
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Création de l'objet de sécurité contenant les info utilisateur à stocker
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, (user.IsAdmin ? "admin" : "user")),
                new Claim("UserId", user.Id.ToString())
            };

            //Génération du token (package System.IdentityModel.Tokens.Jwt)
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials,
                issuer: issuer,
                audience : audience
                );

            //Hash toute les info pour générer un string
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string completeToken = handler.WriteToken(token);

            user.Token = completeToken;
            return user;
        }
    }
}
