using NetCore5WebAPI.Models;

namespace NetCore5WebAPI.Repositories
{
    public interface IProductRepository
    {
        public Task<List<ProductVM>> GetAllAsync();
        public Task<ProductVM> GetByIdAsync(Guid id);
        public Task<Guid> CreateAsync(ProductModel model);
        public Task<Guid> UpdateAsync(ProductVM model);
        public Task<bool> DeleteAsync(Guid id);
    }
}
