using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKD_ClothesShop.Modal;

namespace HKD_ClothesShop.Forms
{
    public partial class frmTaoHoaDon : Form
    {
        QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();

        public frmTaoHoaDon()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmTaoHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
                List<HoaDon> listHoaDon = db.HoaDons.ToList();
                List<NhanVienBanHang> listNhanVien = db.NhanVienBanHangs.ToList();
                List<KhachHang> listKhachHang = db.KhachHangs.ToList();
                BindGrid(listHoaDon);
                FillFalcultyCombobox(listKhachHang);
                FillFalcultyCombobox1(listNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void BindGrid(List<HoaDon> listHoaDon)
        {
            dgvHoaDon.Rows.Clear();
            foreach (var item in listHoaDon)
            {
                int index = dgvHoaDon.Rows.Add();
                dgvHoaDon.Rows[index].Cells[0].Value = item.SoHoaDon;
                dgvHoaDon.Rows[index].Cells[1].Value = item.MaKhachHang;
                dgvHoaDon.Rows[index].Cells[2].Value = item.MaNhanVien;
                dgvHoaDon.Rows[index].Cells[3].Value = item.NgayLap;
                dgvHoaDon.Rows[index].Cells[4].Value = item.Status;
                if (item.Status == true)
                {
                    dgvHoaDon.Rows[index].Cells[4].Value = "Hết sử dụng";
                }
                else
                {
                    dgvHoaDon.Rows[index].Cells[4].Value = "Còn sử dụng";
                }
            }
        }
        private void FillFalcultyCombobox(List<KhachHang> listKhachHang)
        {
            this.cmbMaKH.DataSource = listKhachHang;
            this.cmbMaKH.ValueMember = "MaKhachHang";
        }
        private void FillFalcultyCombobox1(List<NhanVienBanHang> listNhanVien)
        {
            this.cmbMaNV.DataSource = listNhanVien;
            this.cmbMaNV.ValueMember = "MaNhanVien";
        }
        bool kiemtra()
        {
            if (txtSoHD.Text == "" || cmbMaKH.Text == "" || cmbMaNV.Text == "" )
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin hóa đơn", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (txtSoHD.TextLength != 5)
            {
                MessageBox.Show("Số hóa đơn phải đủ 5 kí tự", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        public int checkID(string id, string ids, string idss)
        {
            for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
            {
                if (dgvHoaDon.Rows[i].Cells[0].Value != null && dgvHoaDon.Rows[i].Cells[1].Value != null&& dgvHoaDon.Rows[i].Cells[2].Value != null)
                    if (dgvHoaDon.Rows[i].Cells[0].Value.ToString().Trim() == id && dgvHoaDon.Rows[i].Cells[1].Value.ToString().Trim() == ids && dgvHoaDon.Rows[i].Cells[2].Value.ToString().Trim() == idss)
                    {
                        return i;
                    }
            }
            return -1;
        }
        void add()
        {
            try
            {
                if (kiemtra() == true)
                {
                    if (checkID(txtSoHD.Text, cmbMaKH.Text, cmbMaNV.Text) == -1)
                    {


                        HoaDon st = new HoaDon();
                        st.SoHoaDon = txtSoHD.Text;
                        st.MaKhachHang = cmbMaKH.Text;
                        st.MaNhanVien = cmbMaNV.Text;
                        st.NgayLap = dateTimePicker1.Value;
                        st.Status = checkStatus.Checked;

                        db.HoaDons.Add(st);
                        db.SaveChanges();
                        frmTaoHoaDon_Load(null, null);
                        MessageBox.Show("Thêm hóa đơn thành công", "Thông báo", MessageBoxButtons.OK);

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        void edit()
        {
            try
            {
                if (kiemtra() == true)
                {
                    string id = txtSoHD.Text;
                    string ids = cmbMaKH.Text;
                    string idss = cmbMaNV.Text;
                    HoaDon st = db.HoaDons.Where(p => p.SoHoaDon == id && p.MaKhachHang==ids && p.MaNhanVien==idss).FirstOrDefault();
                    if (st != null)
                    {
                        st.NgayLap = dateTimePicker1.Value;
                        st.Status = checkStatus.Checked;

                        db.SaveChanges();
                        frmTaoHoaDon_Load(null, null);
                        MessageBox.Show($"Sửa hóa đơn mã {txtSoHD.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ??", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvHoaDon.Rows.Count > 0)
            {
                txtSoHD.Text = dgvHoaDon.Rows[num].Cells[0].Value.ToString();
                cmbMaKH.Text = dgvHoaDon.Rows[num].Cells[1].Value.ToString();
                cmbMaNV.Text = dgvHoaDon.Rows[num].Cells[2].Value.ToString();
                dateTimePicker1.Text= dgvHoaDon.Rows[num].Cells[3].Value.ToString();

                lbStatus.Text = dgvHoaDon.Rows[num].Cells[4].Value.ToString();
                if (dgvHoaDon.Rows[num].Cells[4].Value.ToString() == "Hết sử dụng")
                {
                    checkStatus.Checked = true;
                }
                else
                {
                    checkStatus.Checked = false;
                }
            }
        }
    }
}
