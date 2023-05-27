using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class MaterialRepository : IRepository<Material>
    {
        private readonly ApplicationDbContext _db;

        public MaterialRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Material entity)
        {
            await _db.Materials.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Material entity)
        {
            _db.Materials.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Material> Get()
        {
            return _db.Materials;
        }

        public Task<Material> GetById(uint id)
        {
            return _db.Materials.Where(x => x.Material_ID == id).FirstOrDefaultAsync();
        }

        public async Task<Material> Update(Material entity)
        {
            _db.Materials.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
