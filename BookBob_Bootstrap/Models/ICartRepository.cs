using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public interface ICartRepository
    {
        Cart GetCart(int Id);
        IEnumerable<Cart> GetCarts();

        Cart Add(Cart Cart, int BookId);
        Cart Update(Cart Cart);
        Cart Delete(int Id);
    }
}
