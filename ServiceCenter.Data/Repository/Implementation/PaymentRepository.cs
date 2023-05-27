using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class PaymentRepository : IRepository<Payment>
    {
        private readonly ApplicationDbContext _db;

        public PaymentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Payment entity)
        {
            await _db.Payments.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Payment entity)
        {
            _db.Payments.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Payment> Get()
        {
            return _db.Payments;
        }

        public Task<Payment> GetById(uint id)
        {
            return _db.Payments.Where(x => x.Payment_ID == id).FirstOrDefaultAsync();
        }

        public async Task<Payment> Update(Payment entity)
        {
            _db.Payments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
