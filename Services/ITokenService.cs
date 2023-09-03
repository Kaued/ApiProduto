using APICatalogo.Models;

namespace APICatalogo.Service;

public interface ITokeService{
  string GerarToken(string key, string issues, string audience, UserModel user);
}