using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entity;

namespace ServiceCenter.Data.Repository.Implementation
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly ApplicationDbContext _db;

        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Employee entity)
        {
            await _db.Employeers.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Employee entity)
        {
            _db.Employeers.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Employee> Get()
        {
            return _db.Employeers;
        }

        public Task<Employee> GetById(uint id)
        {
            return _db.Employeers.Where(x => x.Employee_ID == id).FirstOrDefaultAsync();
        }

        public async Task<Employee> Update(Employee entity)
        {
            _db.Employeers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
