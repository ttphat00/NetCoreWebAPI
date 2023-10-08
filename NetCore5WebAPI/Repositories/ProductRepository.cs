using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NetCore5WebAPI.Data;
using NetCore5WebAPI.Models;
using System;

namespace NetCore5WebAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(ProductModel model)
        {
            var newProduct = new HangHoa
            {
                TenHH = model.TenHH,
                MoTa = model.MoTa,
                DonGia = model.DonGia,
                GiamGia = model.GiamGia,
                MaLoai = model.MaLoai,
            };
            _context.HangHoas.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.MaHH;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.HangHoas.FindAsync(id);
            if (product != null)
            {
                _context.HangHoas.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var products = _context.HangHoas.Select(p => new ProductVM
            {
                MaHH = p.MaHH,
                TenHH = p.TenHH,
                MoTa = p.MoTa,
                DonGia = p.DonGia,
                GiamGia = p.GiamGia,
                MaLoai = p.MaLoai,
                TenLoai = p.Loai.TenLoai
            });
            return await products.ToListAsync();
        }

        public async Task<ProductVM> GetByIdAsync(Guid id)
        {
            var product = await _context.HangHoas.Include(hh => hh.Loai)
                                                .SingleOrDefaultAsync(hh => hh.MaHH == id);
            if(product != null)
            {
                return new ProductVM
                {
                    MaHH = product.MaHH,
                    TenHH = product.TenHH,
                    MoTa = product.MoTa,
                    DonGia = product.DonGia,
                    GiamGia = product.GiamGia,
                    MaLoai = product.MaLoai,
                    TenLoai = product.Loai.TenLoai
                };
            }
            return null;
        }

        public async Task<Guid> UpdateAsync(ProductVM model)
        {
            var product = await _context.HangHoas.FindAsync(model.MaHH);
            if (product != null)
            {
                product.TenHH = model.TenHH;
                product.MoTa = model.MoTa;
                product.DonGia = model.DonGia;
                product.GiamGia = model.GiamGia;
                product.MaLoai = model.MaLoai;
                await _context.SaveChangesAsync();

                return product.MaHH;
            }
            return Guid.Empty;
        }
    }
}
