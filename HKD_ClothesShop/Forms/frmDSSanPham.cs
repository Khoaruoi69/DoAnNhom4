using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKD_ClothesShop.Modal;


namespace HKD_ClothesShop.Forms
{
    public partial class frmDSSanPham : Form
    {
        QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
        
        
        public frmDSSanPham()
        {
            InitializeComponent();
        }
        private void BindGrid(List<SanPham> listSanPham)
        {
            dgvTTSanPham.Rows.Clear();
            foreach (var item in listSanPham)
            {
                int index = dgvTTSanPham.Rows.Add();
                dgvTTSanPham.Rows[index].Cells[0].Value = item.MaSanPham;
                dgvTTSanPham.Rows[index].Cells[1].Value = item.MaLoaiSP;
                dgvTTSanPham.Rows[index].Cells[2].Value = item.MaThuongHieu;
                dgvTTSanPham.Rows[index].Cells[3].Value = item.TenSanPham;
                dgvTTSanPham.Rows[index].Cells[4].Value = item.DonViTinh;
                dgvTTSanPham.Rows[index].Cells[5].Value = item.DonGia;
                dgvTTSanPham.Rows[index].Cells[6].Value = item.ChatLieu;
                dgvTTSanPham.Rows[index].Cells[7].Value = item.NgayCapNhat;
                dgvTTSanPham.Rows[index].Cells[8].Value = item.MoTa;
                dgvTTSanPham.Rows[index].Cells[9].Value = item.Status;
                dgvTTSanPham.Rows[index].Cells[10].Value = item.AnhBiaSP;


                if (item.Status == true)
                {
                    dgvTTSanPham.Rows[index].Cells[9].Value = "Hết sử dụng";
                }
                else
                {
                    dgvTTSanPham.Rows[index].Cells[9].Value = "Còn sử dụng";
                }
            }
        }
        private void frmDSSanPham_Load(object sender, EventArgs e)
        {
            

            try
            {
                QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
                List<SanPham> listSanPham = db.SanPhams.ToList();
                List<LoaiSanPham> listLoaiSanPham = db.LoaiSanPhams.ToList();
                List<ThuongHieu> listThuongHieu = db.ThuongHieux.ToList();
                BindGrid(listSanPham);
                FillFalcultyCombobox(listLoaiSanPham);
                FillFalcultyCombobox1(listThuongHieu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillFalcultyCombobox(List<LoaiSanPham> listLoaiSanPham)
        {
            this.cmbMaLoai.DataSource = listLoaiSanPham;
            this.cmbMaLoai.ValueMember = "MaLoaiSP";
        }
        private void FillFalcultyCombobox1(List<ThuongHieu> listThuongHieu)
        {
            this.cmbMaTH.DataSource = listThuongHieu;
            this.cmbMaTH.ValueMember = "MaThuongHieu";
        }
        bool kiemtra()
        {
            if (txtMaSP.Text == "" || txtTenSP.Text == "" ||txtDonGia.Text == "" || txtChatLieu.Text == ""||txtDVT.Text==""||cmbMaLoai.SelectedIndex.ToString() ==""||cmbMaTH.SelectedIndex.ToString()=="")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin sảm phẩm", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (txtMaSP.TextLength != 6)
            {
                MessageBox.Show("Mã thương hiệu phải đủ 6 kí tự", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        public int checkID(string id)
        {
            for (int i = 0; i < dgvTTSanPham.Rows.Count; i++)
            {
                if (dgvTTSanPham.Rows[i].Cells[0].Value != null)
                    if (dgvTTSanPham.Rows[i].Cells[0].Value.ToString() == id)
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
                if (checkID(txtMaSP.Text) == -1)
                {


                    SanPham st = new SanPham();
                    st.MaSanPham = txtMaSP.Text;
                    st.MaLoaiSP = cmbMaLoai.Text;
                    st.MaThuongHieu = cmbMaTH.Text;
                    st.TenSanPham = txtTenSP.Text;
                    st.DonViTinh = txtDVT.Text;
                    st.DonGia = Convert.ToDecimal(txtDonGia.Text);
                    ///////////////////////////////
                    ///
                    /*
                    System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);
                    customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                    System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;
                    dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    string v = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                    st.NgayCapNhat = System.Convert.ToDateTime(DateTime.Now.ToString(v));
                    */
                    //////////////////////////////
                    st.NgayCapNhat = dateTimePicker1.Value;
                    st.MoTa = txtMota.Text;
                    st.ChatLieu = txtChatLieu.Text;
                    st.Status = checkStatus.Checked;
                    var hinhanh = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                    st.AnhBiaSP = hinhanh;

                    db.SanPhams.Add(st);
                    db.SaveChanges();
                    frmDSSanPham_Load(null, null);
                    MessageBox.Show("Thêm thương hiệu sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);

                }

            }

        }
        void edit()
        {
            try
            {
                if (kiemtra() == true)
                {
                    string id = txtMaSP.Text;
                    SanPham st = db.SanPhams.Where(p => p.MaSanPham == id).FirstOrDefault();
                    if (st != null)
                    {
                        st.MaSanPham = txtMaSP.Text;
                        st.MaLoaiSP = cmbMaLoai.Text;
                        st.MaThuongHieu = cmbMaTH.Text;
                        st.TenSanPham = txtTenSP.Text;
                        st.DonViTinh = txtDVT.Text;
                        st.DonGia = Convert.ToDecimal(txtDonGia.Text);
                        ///////////////////////////////
                        ///
                        /*
                        System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);
                        customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                        System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;
                        dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        string v = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                        st.NgayCapNhat = System.Convert.ToDateTime(DateTime.Now.ToString(v));
                        */
                        //////////////////////////
                        st.NgayCapNhat = dateTimePicker1.Value;
                        st.MoTa = txtMota.Text;
                        st.ChatLieu = txtChatLieu.Text;
                        st.Status = checkStatus.Checked;
                        var hinhanh = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                        st.AnhBiaSP = hinhanh;
                        db.SaveChanges();
                        frmDSSanPham_Load(null, null);
                        MessageBox.Show($"Sửa thương hiệu sản phẩm mã {txtMaSP.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvTTSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvTTSanPham.Rows.Count > 0)
            {
                txtMaSP.Text = dgvTTSanPham.Rows[num].Cells[0].Value.ToString();
                cmbMaLoai.Text = dgvTTSanPham.Rows[num].Cells[1].Value.ToString();
                cmbMaTH.Text = dgvTTSanPham.Rows[num].Cells[2].Value.ToString();
                txtTenSP.Text = dgvTTSanPham.Rows[num].Cells[3].Value.ToString();
                txtDVT.Text = dgvTTSanPham.Rows[num].Cells[4].Value.ToString();
                txtDonGia.Text = dgvTTSanPham.Rows[num].Cells[5].Value.ToString();
                txtChatLieu.Text = dgvTTSanPham.Rows[num].Cells[6].Value.ToString();
                dateTimePicker1.Text = dgvTTSanPham.Rows[num].Cells[7].Value.ToString();
                txtMota.Text = dgvTTSanPham.Rows[num].Cells[8].Value.ToString();
                Image mds = (Bitmap)((new ImageConverter()).ConvertFrom(dgvTTSanPham.Rows[num].Cells[10].Value));
                pictureBox1.Image = mds;
                txtStatus.Text = dgvTTSanPham.Rows[num].Cells[9].Value.ToString();
                if (dgvTTSanPham.Rows[num].Cells[9].Value.ToString() == "Hết sử dụng")
                {
                    checkStatus.Checked = true;
                }
                else
                {
                    checkStatus.Checked = false;
                }
            }
        }

        private void btnChonLogo_Click(object sender, EventArgs e)
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


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkStatus_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            edit();
        }
    }
}
