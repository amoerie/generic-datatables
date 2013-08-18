using System.Web.Mvc;
using GenericDatatables.Core.Domain.Models;
using GenericDatatables.Core.Domain.Repositories;
using GenericDatatables.Core.Infrastructure.Including;
using GenericDatatables.Datatables.Remote;
using GenericDatatables.Datatables.Remote.Reply;
using GenericDatatables.Datatables.Remote.Request;
using GenericDatatables.Web.Extensions;

namespace GenericDatatables.Web.Controllers
{
    public class HomeController : Controller
    {
        private IGymMemberRepository _gymMembers;

        public HomeController(IGymMemberRepository gymMembers)
        {
            _gymMembers = gymMembers;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GymMemberList(DatatableRequest request)
        {
            var gymMemberTable = DatatableStorage.Get<GymMember>(request.DatatableId, () => this.RenderView("_GymMemberList"));
            var includer = EntityIncluder<GymMember>.Include(g => g.Excercises);
            var replier = new DatatableReplier<GymMember>(gymMemberTable, _gymMembers, baseIncluder: includer);
            return replier.Reply(request);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
