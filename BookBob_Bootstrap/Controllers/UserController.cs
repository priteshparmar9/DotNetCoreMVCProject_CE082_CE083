using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBob_Bootstrap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepo;
        public UserController(IUserRepository UserRepo)
        {
            _UserRepo = UserRepo;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") != "admin@mail.com")
            {
                return RedirectToAction("ind");
            }
            var model = _UserRepo.GetAllUsers();
            return View(model);
        }

        public IActionResult Ind()
        {
            var model = _UserRepo.GetAllUsers();
            User m1 = new User();
            foreach(var m in model)
            {
                if(m.Email == HttpContext.Session.GetString("email"))
                {
                    m1 = m;
                    break;
                }
            }
            return View(m1);
        }

        public ViewResult Details(int id)
        {
            User User = _UserRepo.GetUser(id);
            if (User == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound", id);
            }
            return View(User);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User User)
        {
            if (ModelState.IsValid)
            {
                User newUser = _UserRepo.Add(User);
                return RedirectToAction("details", new { id = newUser.UserId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("email") != null)
            {
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            
            User userInDB = _UserRepo.GetUser(user.Email, user.Password);
                if (userInDB != null)
                {
                    HttpContext.Session.SetString("email", user.Email);
                }
            else
            {
                return View();
            }
                if(user.Email == "admin@mail.com")
            {
                return RedirectToAction("index");
                     
            }
            
            return RedirectToAction("index", "book");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            User User = _UserRepo.GetUser(id);
            return View(User);
        }
        [HttpPost]
        public IActionResult Edit(User model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the User being edited from the database
                User User = _UserRepo.GetUser(model.UserId);
                // Update the User object with the data in the model object
                User.Name = model.Name;
                User.Email = model.Email;
                User.Password = model.Password;
                // Call update method on the repository service passing it the
                // User object to update the data in the database table
                User updatedUser = _UserRepo.Update(User);

                return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            User User = _UserRepo.GetUser(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var User = _UserRepo.GetUser(id);
            _UserRepo.Delete(User.UserId);

            return RedirectToAction("index");
        }
    }
}