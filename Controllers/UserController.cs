using CRUD_application_2.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{

    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        // Return the list of users.
        public ActionResult Index()
        {
            return View(userlist);
        }

        // GET: User/Details/5
        // Return the details of a specific user with the provided id.
        public ActionResult Details(int id)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // Create a new user with the data from the user provided and return it
        [HttpPost]
        public ActionResult Create(User user)
        {
            // Generate a new id for the user
            int newId = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;

            // Assign the new id to the user
            user.Id = newId;
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User must have all fields defined.");
            }

            userlist.Add(user);
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            User existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return HttpNotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        // Delete the user with the provided id
        public ActionResult Delete(int id)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            userlist.Remove(user);
            return RedirectToAction("Index");
        }

        // GET: User/Search
        public ActionResult Search(string searchString)
        {
            var users = from u in userlist
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Name.Contains(searchString));
            }

            return View("Index", users.ToList());
        }
    }
}
