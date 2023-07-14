using API.DTOs.Auth;
using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class AuthRepository : IAuthRepository
{

    private readonly string request;
    private readonly HttpClient httpClient;

    public AuthRepository(string request = "auth/")
    {
        //this.request = request;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7103/api/v1/")
        };
        this.request = request;
    }
    public async Task<ResponseHandler<TokenDto>> SignIn(SigninDto signDto)
    {
        ResponseHandler<TokenDto> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(signDto), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "signin/", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<TokenDto>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseHandler<string>> Register(RegisterDto registerDto)
    {
        ResponseHandler<string> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "register/", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return entityVM;
    }
}

