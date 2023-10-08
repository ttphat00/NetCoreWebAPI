using NetCore5WebAPI.Models;

namespace NetCore5WebAPI.Repositories
{
    public interface ILoaiRepository
    {
        List<LoaiVM> GetAll(string search, string sort, int page = 1);
        LoaiVM GetById(int id);
        LoaiVM Create(LoaiModel model);
        LoaiVM Update(LoaiVM model);
        bool Delete(int id);
    }
}
