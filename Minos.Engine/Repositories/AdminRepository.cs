using Microsoft.EntityFrameworkCore;
using Minos.Domain.Aggregates;
using Minos.Domain.Aggregates.Model;
using Minos.Domain.SeedWork;
using Minos.Engine.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Minos.Engine.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        private readonly MinosContext _context;

        public AdminRepository(MinosContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork {

            get
            {
                 return _context;
            }
        }
        public Admin Add(Admin t)
        {
            return _context.Admins.Add(t).Entity;
        }

        public void Update(Admin t)
        {
            _context.Entry(t).State = EntityState.Modified;
        }

        public async Task<Admin> GetAsync(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if(admin != null)
            {
            }

            return admin;
        }

        public async Task<List<Admin>> GetAsync(Admin admin)
        {
            return await _context.Admins
                .ToListAsync<Admin>();
        }

        public async Task<Admin> FindAsync(Admin admin)
        {
            return await _context.Admins
                .Where(b => b.id == admin.id)
                .SingleOrDefaultAsync();
        }

        public async Task<Admin> FindByIdxAsync(Admin admin)
        {
            return await _context.Admins
                .Where(b => b.idx == admin.idx)
                .SingleOrDefaultAsync();
        }
    }
}
