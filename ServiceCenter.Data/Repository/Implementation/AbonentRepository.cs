using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class AbonentRepository : IRepository<Abonent>
    {
        private readonly ApplicationDbContext _db;

        public AbonentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Abonent entity)
        {
            await _db.Abonents.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Abonent entity)
        {
            _db.Abonents.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Abonent> Get()
        {
            return _db.Abonents;
        }

        public Task<Abonent> GetById(uint id)
        {
            return _db.Abonents.Where(x => x.Abonent_ID == id).FirstOrDefaultAsync();
        }

        public async Task<Abonent> Update(Abonent entity)
        {
            _db.Abonents.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public Task<Tariff> GetTariffById(uint id)
        {
            return _db.Tariffs.Where(x => x.Tariff_ID == id).FirstOrDefaultAsync();
        }
    }
}
