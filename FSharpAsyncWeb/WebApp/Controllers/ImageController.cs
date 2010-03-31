using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ImageController : AsyncController
    {
        public ActionResult Index()
        {
            return View();
        }

        public void LoadAsync(string url)
        {
            AsyncManager.OutstandingOperations.Increment();
            var transfer = new CSharpAsync.AsyncWebTransfer(HttpContext, url);

            transfer.Completed += (sender, args) => 
                AsyncManager.OutstandingOperations.Decrement();

            transfer.Start();
        }

        public ActionResult LoadCompleted()
        {
            return new EmptyResult();
        }

        public void LoadFsAsync(string url)
        {
            AsyncManager.OutstandingOperations.Increment();
            var transfer = new FSharpAsync.AsyncWebTransfer(HttpContext, url);

            transfer.Completed += (sender, args) =>
                AsyncManager.OutstandingOperations.Decrement();

            transfer.Start();
        }

        public ActionResult LoadFsCompleted()
        {
            return new EmptyResult();
        }
    }
}
