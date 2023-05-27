using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class TariffRepository : IRepository<Tariff>
    {
        private readonly ApplicationDbContext _db;

        public TariffRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Tariff entity)
        {
            await _db.Tariffs.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Tariff entity)
        {
            _db.Tariffs.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Tariff> Get()
        {
            return _db.Tariffs;
        }

        public Task<Tariff> GetById(uint id)
        {
            return _db.Tariffs.Where(x => x.Tariff_ID == id).FirstOrDefaultAsync();
        }

        public async Task<Tariff> Update(Tariff entity)
        {
            _db.Tariffs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
