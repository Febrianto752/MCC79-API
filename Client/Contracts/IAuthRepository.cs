using API.DTOs.Auth;
using API.Utilities.Handlers;

namespace Client.Contracts;

public interface IAuthRepository
{
    Task<ResponseHandler<TokenDto>> SignIn(SigninDto signDto);
    Task<ResponseHandler<string>> Register(RegisterDto registerDto);
}

