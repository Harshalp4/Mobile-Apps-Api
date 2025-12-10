using Microsoft.AspNetCore.Mvc;
using Bit2Sky.Bit2EHR.Web.Controllers;

namespace Bit2Sky.Bit2EHR.Web.Public.Controllers;

public class HomeController : Bit2EHRControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}

