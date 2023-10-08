using Microsoft.EntityFrameworkCore;
using NetCore5WebAPI.Data;
using NetCore5WebAPI.Models;

namespace NetCore5WebAPI.Repositories
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext _context;
        static byte pageSize = 1;

        public LoaiRepository(MyDbContext context)
        {
            _context = context;
        }

        public LoaiVM Create(LoaiModel model)
        {
            var newLoai = new Loai
            {
                TenLoai = model.TenLoai
            };
            _context.Loais.Add(newLoai);
            _context.SaveChanges();
            return new LoaiVM
            {
                MaLoai = newLoai.MaLoai,
                TenLoai = newLoai.TenLoai
            };
        }

        public bool Delete(int id)
        {
            var loai = _context.Loais.SingleOrDefault(loai => loai.MaLoai == id);
            if (loai != null)
            {
                _context.Loais.Remove(loai);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<LoaiVM> GetAll(string search, string sort, int page = 1)
        {
            var loais = _context.Loais.AsQueryable();
            if(!string.IsNullOrEmpty(search))
            {
                loais = loais.Where(loai => loai.TenLoai.Contains(search));
            }
            loais = loais.OrderBy(loai => loai.TenLoai);
            //loais = loais.Skip((page - 1) * pageSize).Take(pageSize);
            var result = PagedList<Loai>.ToPagedList(loais, page, pageSize);
            
            return result.Select(loai => new LoaiVM
            {
                MaLoai = loai.MaLoai,
                TenLoai = loai.TenLoai
            }).ToList();
        }

        public LoaiVM GetById(int id)
        {
            var loai = _context.Loais.SingleOrDefault(loai => loai.MaLoai == id);
            if(loai != null)
            {
                return new LoaiVM
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai
                };
            }
            return null;
        }

        public LoaiVM Update(LoaiVM model)
        {
            var loai = _context.Loais.SingleOrDefault(loai => loai.MaLoai == model.MaLoai);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
                _context.SaveChanges();
                return new LoaiVM
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai
                };
            }
            return null;
        }
    }
}
