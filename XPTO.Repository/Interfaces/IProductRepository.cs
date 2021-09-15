
using System.Threading.Tasks;
using XPTO.Domain;

namespace XPTO.Repository.Interfaces
{
    public interface IProductRepository : IBaseRepository
    {
         //Cliente
         Task<Product[]> GetAllProductAsync();
         Task<Product> GetProductAsyncById(int id);
         Task<Product[]> GetProductAsyncByTitle(string title);
         Task<Product> GetProductAsyncByBarCode(string barCode);
    }
}