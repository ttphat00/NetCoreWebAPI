namespace NetCore5WebAPI.Data
{
    public enum TrangThaiDH
    {
        Moi = 0, DaThanhToan = 1, ThanhCong = 2, Huy = -1
    }
    public class DonHang
    {
        public Guid MaDH { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public TrangThaiDH TrangThaiDH { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChiGiao { get; set; }
        public string SoDienThoai { get; set; }
        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
    }
}
