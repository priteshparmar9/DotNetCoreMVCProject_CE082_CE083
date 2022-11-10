using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class SQLCartRepository : ICartRepository
    {
        private readonly AppDbContext context;
        public ICartItemRepository cartItemRepository;
        public SQLCartRepository(AppDbContext context)
        {
            this.context = context;
        }
        Cart ICartRepository.GetCart(int Id)
        {
            return context.Carts.FirstOrDefault(x => x.UserId == Id);
        }
        IEnumerable<Cart> ICartRepository.GetCarts()
        {
            return context.Carts;
        }

        Cart ICartRepository.Add(Cart Cart, int BookId)
        {
            CartItem cartItem = new CartItem();
            cartItem.Book = context.Books.FirstOrDefault(e => e.BookId == BookId);
            cartItem.Cart = Cart;
            //Cart.CartItems.Add(item);
            context.CartItems.Add(cartItem);
            var cart = context.Carts.Attach(Cart);
            cart.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return Cart;
        }
        Cart ICartRepository.Delete(int Id)
        {
            Cart model = context.Carts.FirstOrDefault(x => x.UserId == Id);
            context.Carts.Remove(model);
            context.SaveChanges();
            return model;
        }

        Cart ICartRepository.Update(Cart CartChanges)
        {
            var Cart = context.Carts.Attach(CartChanges);
            Cart.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return CartChanges;
        }
    }
}
