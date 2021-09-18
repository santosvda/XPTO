using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XPTO.Domain;
using XPTO.Repository.Interfaces;

namespace XPTO.Repository.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly XPTOContext _context;
        public ProductRepository(XPTOContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Product[]> GetAllProductAsync()
        {
            IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking()
            .Where(e => e.Active)
            .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductAsyncByBarCode(string barCode)
        {
            IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking()
            .Where(p => p.BarCode.ToLower().Contains(barCode.ToLower()))
            .Where(e => e.Active)
            .OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductAsyncById(int id)
        {
            IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking()
            .Where(p => p.Id == id)
            .Where(e => e.Active)
            .OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product[]> GetProductAsyncByTitle(string title)
        {
            IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking()
            .Where(p => p.Title.ToLower().Contains(title.ToLower()))
            .Where(e => e.Active)
            .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
    }
}