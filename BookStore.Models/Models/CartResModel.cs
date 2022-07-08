using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class CartResModel
    {
        public CartResModel() { }
        public CartResModel(Cart cart)
        {
            Id = cart.Id;
            Userid = cart.Userid;
            Bookid = cart.Bookid;
            Quantity = cart.Quantity;
            Base64image = cart.Book.Base64image;
            Name = cart.Book.Name;
            Price = cart.Book.Price;
        }

        public int Id { get; set; }
        public int Userid { get; set; }
        public int Bookid { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Base64image { get; set; }
        public string Name { get; set; }
    }
}
