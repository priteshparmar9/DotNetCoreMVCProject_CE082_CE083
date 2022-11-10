using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBob_Bootstrap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _BookRepo;
        int temp = 0;
        public BookController(IBookRepository BookRepo)
        {
            _BookRepo = BookRepo;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") != "admin@mail.com")
            {
                
                return RedirectToAction("nonAdmin");
            }
            var model = _BookRepo.GetAllBooks();
            return View(model);
        }
        public IActionResult nonAdmin()
        {
            var model = _BookRepo.GetAllBooks();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            Book Book = _BookRepo.GetBook(id);
            if (Book == null)
            {
                Response.StatusCode = 404;
                return View("BookNotFound", id);
            }
            if (HttpContext.Session.GetString("email") != "admin@mail.com")
            {
                temp = id;
                return RedirectToAction("nonAdminDetails", id);
            }
            return View(Book);
        }
        public IActionResult nonAdminDetails(int id)
        {
            Book Book = _BookRepo.GetBook(id);
            return View(Book);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book Book)
        {
            if (ModelState.IsValid)
            {
                Book newBook = _BookRepo.Add(Book);
                return RedirectToAction("details", new { id = newBook.BookId });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Book Book = _BookRepo.GetBook(id);
            return View(Book);
        }
        [HttpPost]
        public IActionResult Edit(Book model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the Book being edited from the database
                Book Book = _BookRepo.GetBook(model.BookId);
                // Update the Book object with the data in the model object
                Book.AuthName = model.AuthName;
                Book.Content = model.Content;
                Book.Price = model.Price;
                // Call update method on the repository service passing it the
                // Book object to update the data in the database table
                Book updatedBook = _BookRepo.Update(Book);

                return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book Book = _BookRepo.GetBook(id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var Book = _BookRepo.GetBook(id);
            _BookRepo.Delete(Book.BookId);

            return RedirectToAction("index");
        }
    }
}