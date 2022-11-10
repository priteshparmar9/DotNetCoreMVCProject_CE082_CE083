using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public interface ICartItemRepository
    {
        public IBookRepository BookRepository { get; }
        public ICartRepository CartRepository { get; }
        CartItem Save(Cart cart, int BookId);
    }
}
