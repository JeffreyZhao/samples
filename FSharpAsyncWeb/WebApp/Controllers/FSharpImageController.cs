using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FSharpAsync.Mvc;

namespace WebApp.Controllers
{
    public class FSharpImageController : ImageControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public void LoadAsync(string url)
        {
            StartAsync(Load(url));
        }

        public ActionResult LoadCompleted(ActionResult result, Exception error)
        {
            return result;
        }
    }
}
