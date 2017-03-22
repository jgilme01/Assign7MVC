using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign7MVC.Models; //link to the models

namespace Assign7MVC.Controllers
{
    public class PersonLiteController : Controller
    {

        //call the ADO Entites model
        Community_AssistEntities db = new Community_AssistEntities();
        // GET: PersonLite
        public ActionResult Index()
        {
            //query to get all the fields from the model
            //we call fields from three tables
            //Entities manages our joins
            var peeps = from p in db.People
                        from pa in p.PersonAddresses
                        from c in p.Contacts
                        where c.ContactTypeKey == 1
                        select new
                        {
                            p.PersonKey,
                            p.PersonLastName,
                            p.PersonFirstName,
                            p.PersonEmail,
                            pa.PersonAddressApt,
                            pa.PersonAddressStreet,
                            pa.PersonAddressCity,
                            pa.PersonAddressState,
                            pa.PersonAddressZip,
                            c.ContactNumber
                        };
            //we create a list to store PersonLite Classes
            List<PersonLite> peepsList = new List<PersonLite>();
            //Loop through the query results and write them
            //to PersonLite classes and ass them to the list
            foreach (var p in peeps)
            {
                PersonLite pl = new Models.PersonLite();
                pl.PersonKey = (int)p.PersonKey;
                pl.LastName = p.PersonLastName;
                pl.Firstname = p.PersonFirstName;
                pl.Email = p.PersonEmail;
                pl.Apartment = p.PersonAddressApt;
                pl.Street = p.PersonAddressStreet;
                pl.City = p.PersonAddressCity;
                pl.State = p.PersonAddressState;
                pl.Zipcode = p.PersonAddressZip;
                peepsList.Add(pl);
            }

            //return the list
            return View(peepsList);
        }

        //these generate an "Ambiguous call" error. I haven't figured out
        //the solution yet.
        public ActionResult Create()
        {

            return View();
        }
        public ActionResult Create([Bind(Include ="LastName, FirstName, Email, Password, Apartment, Street, City, State, Zipcode, HomePhone, WorkPhone")]
        PersonLite pl)
        {
            int result = db.usp_Register(pl.LastName,
                pl.Firstname, pl.Email, pl.Password,
                pl.Apartment, pl.Street, pl.City,
                pl.State, pl.Zipcode, pl.HomePhone,
                pl.WorkPhone);

            return View(pl);
        }
    }
}