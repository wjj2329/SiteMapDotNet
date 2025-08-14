using System.Web;
using System.Web.Mvc;

namespace SiteMapNetCore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var c = SiteMap.CurrentNode;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}