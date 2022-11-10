using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class SQLCartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;
        private IBookRepository bookRepository;
        private ICartRepository cartRepository;
        public SQLCartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        ICartRepository ICartItemRepository.CartRepository {
            get
            {
                return cartRepository = cartRepository ?? new SQLCartRepository(_context);
            }
        }
        IBookRepository ICartItemRepository.BookRepository
        {
            get
            {
                return bookRepository = bookRepository ?? new SQLBookRepository(_context);
            }
        }
        CartItem ICartItemRepository.Save(Cart cart , int BookId)
        {
            CartItem ci = new CartItem();
            ci.Book = _context.Books.FirstOrDefault(e => e.BookId == BookId);
            ci.Cart = cart;
            ci.CartId = cart.CartId;
            ci.BookId = BookId;
            cart.CartItems.Add(ci);
            _context.SaveChanges();
            
            return ci; 
        }
    }
}
