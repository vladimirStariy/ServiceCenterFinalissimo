using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class ServiceRepository : IRepository<OrderService>
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(OrderService entity)
        {
            await _db.Services.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(OrderService entity)
        {
            _db.Services.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<OrderService> Get()
        {
            return _db.Services;
        }

        public Task<OrderService> GetById(uint id)
        {
            return _db.Services.Where(x => x.OrderService_ID == id).FirstOrDefaultAsync();
        }

        public async Task<OrderService> Update(OrderService entity)
        {
            _db.Services.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
