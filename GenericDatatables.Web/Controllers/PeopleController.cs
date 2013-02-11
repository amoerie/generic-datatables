using System.Diagnostics;
using System.Web.Mvc;
using GenericDatatables.Core;
using GenericDatatables.Core.Session;
using GenericDatatables.Web.Models;
using GenericDatatables.Web.Persistence;
using System.Data.Entity;

namespace GenericDatatables.Web.Controllers
{
    using GenericDatatables.Web.Dtos;
    using GenericDatatables.Web.Infrastructure.Dtos;

    public class PeopleController: Controller
    {
        private readonly IRepository<Person> _people;

        public PeopleController(IRepository<Person> people)
        {
            _people = people;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Datatable(DatatableParam param)
        {
            var people = _people.All().Include(p => p.Address);
            var sessionObject = Session.GetDatatableProperties<Person>(param.DatatableId);
            var parser = new DatatableParser<Person>(people, sessionObject);
            return parser.Parse(param).ToJson();
        }

        public ActionResult PersonForm(int? id)
        {
            Person person = id.HasValue ? _people.Find(id.Value) : new Person();
            var personFormDto = new PersonFormDto().FromModel(person);
            return PartialView(personFormDto);
        }

        [HttpPost]
        public ActionResult PersonForm(int id, PersonFormDto personFormDto)
        {
            if (ModelState.IsValid)
            {
                bool isNew = id == 0;
                if (isNew)
                {
                    // make model object from the dto
                    var person = personFormDto.ToModel();

                    // add to db
                    _people.Add(person);
                }
                else
                {
                    // fetch from db
                    var person = _people.Find(id);

                    // update with dto values
                    personFormDto.UpdateModel(person);

                     _people.Update(person);
                }
                   
                _people.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(personFormDto);
        }
    }
}