namespace NetCore5WebAPI.Data
{
    public class ChiTietDonHang
    {
        public Guid MaDH { get; set; }
        public Guid MaHH { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public DonHang DonHang { get; set; }
        public HangHoa HangHoa { get; set; }
    }
}
