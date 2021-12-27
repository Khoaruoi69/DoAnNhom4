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
    public partial class frmNhanVienShop : Form
    {
        QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
        public frmNhanVienShop()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmNhanVienShop_Load(object sender, EventArgs e)
        {
            try
            {
                QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
                List<NhanVienBanHang> listNhanVienBanHang = db.NhanVienBanHangs.ToList();
                BindGrid(listNhanVienBanHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void BindGrid(List<NhanVienBanHang> listNhanVienBanHang)
        {
            dgvNhanVien.Rows.Clear();
            foreach (var item in listNhanVienBanHang)
            {
                int index = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[index].Cells[0].Value = item.AnhThe;
                dgvNhanVien.Rows[index].Cells[1].Value = item.MaNhanVien;
                dgvNhanVien.Rows[index].Cells[2].Value = item.HoTen;

                if (item.GioiTinh == "M")
                    dgvNhanVien.Rows[index].Cells[3].Value = "Nam";
                else
                    if (item.GioiTinh == "F")
                    dgvNhanVien.Rows[index].Cells[3].Value = "Nữ";
                else
                    dgvNhanVien.Rows[index].Cells[3].Value = "Khác";
                dgvNhanVien.Rows[index].Cells[4].Value = item.NgaySinh;
                dgvNhanVien.Rows[index].Cells[6].Value = item.SDT;
                dgvNhanVien.Rows[index].Cells[5].Value = item.Email;
                if (item.Status == false)
                    dgvNhanVien.Rows[index].Cells[7].Value = "Còn làm việc";
                else
                    dgvNhanVien.Rows[index].Cells[7].Value = "Hết làm việc";
            }
        }
        bool kiemtra()
        {
            if (txtMaNV.Text == "" || txtHoTen.Text == "" || txtEmail.Text == "" || txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin thương hiệu", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (txtMaNV.TextLength != 4)
            {
                MessageBox.Show("Mã nhân viên phải đủ 3 kí tự", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        public int checkID(string id)
        {
            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                if (dgvNhanVien.Rows[i].Cells[1].Value != null)
                    if (dgvNhanVien.Rows[i].Cells[1].Value.ToString() == id)
                    {
                        return i;
                    }
            }
            return -1;
        }
        void add()
        {
            if (kiemtra() == true)
            {
                if (checkID(txtMaNV.Text) == -1)
                {
                    NhanVienBanHang st = new NhanVienBanHang();
                    st.MaNhanVien = txtMaNV.Text;
                    st.HoTen = txtHoTen.Text;
                    st.NgaySinh = dateTimePicker1.Value;
                    st.SDT = txtSDT.Text;
                    st.Email = txtEmail.Text;
                    var hinhanh = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                    st.AnhThe = hinhanh;
                    st.Status = checkStatus.Checked;
                    if (radNam.Checked == true)
                    {
                        st.GioiTinh = "M";
                    }
                    if (radNu.Checked == true)
                    {
                        st.GioiTinh = "F";
                    }
                    if (radKhac.Checked == true)
                    {
                        st.GioiTinh = "K";
                    }
                    db.NhanVienBanHangs.Add(st);
                    db.SaveChanges();
                    frmNhanVienShop_Load(null, null);
                    MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK);
                }

            }

        }
        void edit()
        {
            try
            {
                if (kiemtra() == true)
                {
                    string id = txtMaNV.Text;
                    NhanVienBanHang st = db.NhanVienBanHangs.Where(p => p.MaNhanVien == id).FirstOrDefault();
                    if (st != null)
                    {
                        st.HoTen = txtHoTen.Text;
                        st.NgaySinh = dateTimePicker1.Value;
                        st.SDT = txtSDT.Text;
                        st.Email = txtEmail.Text;
                        var hinhanh = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                        st.AnhThe = hinhanh;

                        db.SaveChanges();
                        frmNhanVienShop_Load(null, null);
                        MessageBox.Show($"Sửa thương hiệu sản phẩm mã {txtMaNV.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


       

        
        

       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnChonAnh_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFile.FileName;
            }


        }

        private void dgvNhanVien_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvNhanVien.Rows.Count > 0)
            {
                Image mds = (Bitmap)((new ImageConverter()).ConvertFrom(dgvNhanVien.Rows[num].Cells[0].Value));
                pictureBox1.Image = mds;
                txtMaNV.Text = dgvNhanVien.Rows[num].Cells[1].Value.ToString();
                txtHoTen.Text = dgvNhanVien.Rows[num].Cells[2].Value.ToString();
                dateTimePicker1.Text = dgvNhanVien.Rows[num].Cells[4].Value.ToString();
                txtEmail.Text = dgvNhanVien.Rows[num].Cells[5].Value.ToString();
                txtSDT.Text = dgvNhanVien.Rows[num].Cells[6].Value.ToString();
                lbStatus.Text = dgvNhanVien.Rows[num].Cells[7].Value.ToString();
                if (dgvNhanVien.Rows[num].Cells[3].Value.ToString() == "Nam")
                {
                    radNam.Checked = true;
                }
                if (dgvNhanVien.Rows[num].Cells[3].Value.ToString() == "Nữ")
                {
                    radNu.Checked = true;
                }
                if (dgvNhanVien.Rows[num].Cells[3].Value.ToString() == "Khác")
                {
                    radKhac.Checked = true;
                }

            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            edit();
        }
    }
}
