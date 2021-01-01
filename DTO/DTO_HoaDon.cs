using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HoaDon
    {
        private string maHD, maKH, tenDangNhap;
        private DateTime ngayTao;
        private int tongTien;

        public DTO_HoaDon()
        {

        }

        public string MaHD { get => maHD; set => maHD = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public string TenDangNhap { get => tenDangNhap; set => tenDangNhap = value; }
        public DateTime NgayTao { get => ngayTao; set => ngayTao = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
    }
}
