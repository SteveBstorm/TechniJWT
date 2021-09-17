using DemoSecurite.Models;

namespace DemoSecurite.Tools
{
    public interface ITokenManager
    {
        User GenerateJWT(User user);
    }
}