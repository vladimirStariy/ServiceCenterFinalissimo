using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class TariffTypeRepository : IRepository<TariffType>
    {
        private readonly ApplicationDbContext _db;

        public TariffTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(TariffType entity)
        {
            await _db.TariffTypes.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TariffType entity)
        {
            _db.TariffTypes.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<TariffType> Get()
        {
            return _db.TariffTypes;
        }

        public Task<TariffType> GetById(uint id)
        {
            return _db.TariffTypes.Where(x => x.TariffType_ID == id).FirstOrDefaultAsync();
        }

        public async Task<TariffType> Update(TariffType entity)
        {
            _db.TariffTypes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
