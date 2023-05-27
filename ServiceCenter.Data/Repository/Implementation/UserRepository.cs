using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<User> Get()
        {
            return _db.Users;
        }

        public Task<User> GetById(uint id)
        {
            return _db.Users.Where(x => x.User_ID == id).FirstOrDefaultAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
