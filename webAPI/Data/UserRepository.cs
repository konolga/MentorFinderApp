using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace webAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
             _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }
        public async Task<Photo> GetPhoto(int Id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id==Id);
            return photo;
        }
        public async Task<User> GetUser(int Id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == Id);
            return user;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; //false if no changes
        }
        public async Task<Photo> GetMainPhoto(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

    }
}