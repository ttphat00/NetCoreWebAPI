using System.ComponentModel.DataAnnotations;

namespace NetCore5WebAPI.Models
{
    public class ProductModel
    {
        [MaxLength(255)]
        public string TenHH { get; set; }
        public string? MoTa { get; set; }
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
    }

    public class ProductVM : ProductModel
    {
        public Guid MaHH { get; set; }
        public string? TenLoai { get; set; }
    }
}
