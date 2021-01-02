using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_DonViTinh
    {
        private string maDVTinh;
        private string tenDVTinh;
        public DTO_DonViTinh(string maDVTinh, string tenDVTinh)
        {
            this.maDVTinh = maDVTinh;
            this.tenDVTinh = tenDVTinh;
        }

        public DTO_DonViTinh()
        {

        }
        public string MaDVTinh { get => maDVTinh; set => maDVTinh = value; }
        public string TenDVTinh { get => tenDVTinh; set => tenDVTinh = value; }

        public DTO_DonViTinh(DataRow row)
        {
            this.MaDVTinh = row["MaDVT"].ToString();
            this.TenDVTinh = row["TenDVT"].ToString();
        }
    }
}
