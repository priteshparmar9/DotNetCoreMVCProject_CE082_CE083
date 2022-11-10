using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class CartItem
    {

        [Key]
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
       
    }
}
