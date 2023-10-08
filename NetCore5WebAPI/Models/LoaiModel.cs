namespace NetCore5WebAPI.Models
{
    public class LoaiModel
    {
        public string TenLoai { get; set; }
    }

    public class LoaiVM : LoaiModel
    {
        public int MaLoai { get; set; }
    }
}
