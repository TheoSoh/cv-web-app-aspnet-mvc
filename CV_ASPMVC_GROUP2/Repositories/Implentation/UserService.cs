using CV_ASPMVC_GROUP2.Models;
using CV_ASPMVC_GROUP2.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CV_ASPMVC_GROUP2.Repositories.Implentation
{
    public class UserService : IUserService
    {

        private readonly TestDbContext _context;

        public UserService(TestDbContext context)
        {
            _context = context;
        }
        public void Add(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public bool Delete(string userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
            {
                return false; 
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true; 
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user != null)
            {
                return user.Id; 
            }

            return null; 
        }
    }
}
