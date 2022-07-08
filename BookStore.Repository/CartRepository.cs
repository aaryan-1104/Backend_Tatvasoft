using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace CartStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public ListResponse<Cart> GetCart(int userid, int pageIndex = 1, int pageSize = 10, string keyword="")
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c=>c.Book).Where(c=> (keyword==null && c.Userid.Equals(userid)) ||(c.Userid.Equals(userid) && c.Book.Name.ToLower().Contains(keyword))).AsQueryable();
            int totalRecords = query.Count();
            List<Cart> cart = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Cart>()
            {
                Results = cart,
                Totalrecords = totalRecords,
            };
        }

        public int GetCartQuantity(int userid)
        {
            var query = _context.Carts.Include(c => c.Book).Where(c => (c.Userid.Equals(userid))).AsQueryable();
            int totalRecords = query.Count();
            return totalRecords;
        }

        public bool CartItemExist(int bookid)
        {
            var x =_context.Carts.FirstOrDefault(c => c.Bookid == bookid);
            if(x == null)
            {
                return false;
            }
            return true;
        }

        public Cart GetCartItem(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public Cart AddToCart(Cart cart)
        {
            var entry = _context.Carts.Add(cart);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Cart UpdateCartItem(Cart cart)
        {
            var entry = _context.Carts.Update(cart);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCartItem(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            var entry = _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
