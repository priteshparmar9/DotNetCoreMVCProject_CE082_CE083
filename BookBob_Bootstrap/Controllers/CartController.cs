using BookBob_Bootstrap.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ICartRepository cartRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly AppDbContext context;

        public CartController(ICartRepository cartRepository, IBookRepository bookRepository, ICartItemRepository cartItemRepository,AppDbContext context)
        {
            this.cartRepository = cartRepository;
            this.bookRepository = bookRepository;
            this.cartItemRepository = cartItemRepository;
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = cartRepository.GetCarts();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult AddToCart(int BookId)
        {
            var email = HttpContext.Session.GetString("email");
            User user = context.Users.FirstOrDefault(u => u.Email == email);
            Cart model = cartRepository.GetCart(user.UserId);
            cartRepository.Add(model, BookId);
            return RedirectToAction("index");
        }
    }
}
