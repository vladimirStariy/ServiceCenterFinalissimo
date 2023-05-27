using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class OrderMaterialRepository : IRepository<OrderMaterial>
    {
        private readonly ApplicationDbContext _db;

        public OrderMaterialRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(OrderMaterial entity)
        {
            await _db.OrderMaterials.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(OrderMaterial entity)
        {
            _db.OrderMaterials.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<OrderMaterial> Get()
        {
            return _db.OrderMaterials;
        }

        public Task<OrderMaterial> GetById(uint id)
        {
            return _db.OrderMaterials.Where(x => x.OrderMaterial_ID == id).FirstOrDefaultAsync();
        }

        public async Task<OrderMaterial> Update(OrderMaterial entity)
        {
            _db.OrderMaterials.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
