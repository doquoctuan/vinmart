﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form_DVT : Form
    {
        UC_KhoHang uc;
        public Form_DVT(UC_KhoHang uc)
        {
            InitializeComponent();
            this.uc = uc;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string query = String.Format("insert into DonViTinh values('{0}', N'{1}')", LoadMaDVT(), txtDVT.Text);
            DataProvider.Instance.ExecuteNonQuery(query);
            uc.cbbDVT.DataSource = DataProvider.Instance.ExecuteQuery("select * from DonViTinh");
            this.Close();
        }

        string LoadMaDVT()
        {
            string maKHnext = "DVT01";
            string query = "select top 1 MaDVT from DonViTinh order by MaDVT desc";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DataRow data2 = data.Rows[0];
                string maKH = data2["MaDVT"].ToString();
                maKHnext = maKH.Substring(3);
                int i = Convert.ToInt32(maKHnext);
                i = i + 1;
                if (i < 10)
                {
                    maKHnext = "DVT0" + i;
                }
                else
                {
                    maKHnext = "DVT" + i;
                }
            }
            return maKHnext;
        }
    }
}
