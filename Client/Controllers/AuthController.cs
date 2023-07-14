using API.DTOs.Auth;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class AuthController : Controller
{
    private readonly IAuthRepository repository;

    public AuthController(IAuthRepository repository)
    {
        this.repository = repository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(SigninDto signDto)
    {
        var result = await repository.SignIn(signDto);
        if (result.Code == 200)
        {
            Console.WriteLine(result?.Data?.Token);
            TempData["Success"] = "Berhasil Login";
            return RedirectToAction(nameof(Login));
        }

        TempData["Error"] = result.Message;
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(RegisterDto registerDto)
    {
        var result = await repository.Register(registerDto);
        if (result.Code == 201)
        {
            TempData["Success"] = "Account anda berhasil dibuat";
            return RedirectToAction(nameof(Login));
        }

        TempData["Error"] = result.Message;
        return RedirectToAction(nameof(Login));
    }
}

