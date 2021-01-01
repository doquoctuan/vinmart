﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KhachHang
    {
        private string maKH, tenKH, diaChi, SDT, email;

        public DTO_KhachHang()
        {

        }
        public DTO_KhachHang(DataRow row)
        {
            this.MaKH = row["MaKH"].ToString();
            this.TenKH = row["TenKH"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.SDT1 = row["SDT"].ToString();
            this.Email = row["Email"].ToString();
        }

        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SDT1 { get => SDT; set => SDT = value; }
        public string Email { get => email; set => email = value; }
    }
}