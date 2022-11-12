using Microsoft.AspNetCore.Mvc;

namespace LimitOrderBook.View.Controllers;

public class AuthenticationController : Controller
{

    public IActionResult Login() { return View(); }

}
