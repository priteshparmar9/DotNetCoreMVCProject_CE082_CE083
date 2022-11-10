using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string AuthName { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Url { get; set; }

        [Required]
        public int Price { get; set; }

        public IList<CartItem> CartItems { get; set; }
    }
}
