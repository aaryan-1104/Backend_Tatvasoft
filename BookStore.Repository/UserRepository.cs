using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User Login(LoginModel model)
        {
            User u=_context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.Password));
            u.Password = "";
            return u;
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email= model.Email,
                Password=model.Password,
                Firstname=model.FirstName,
                Lastname=model.LastName,
                Roleid=model.Roleid
            };

            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            entry.Entity.Password = "";
            return entry.Entity;
        }

    }
}
