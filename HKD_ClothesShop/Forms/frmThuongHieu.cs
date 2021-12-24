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
using System.IO;

namespace HKD_ClothesShop.Forms
{
    public partial class frmThuongHieu : Form
    {
        QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
        
        public frmThuongHieu()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThuongHieu_Load(object sender, EventArgs e)
        {

            try
            {
                QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
                List<ThuongHieu> listThuongHieu = db.ThuongHieux.ToList();
                BindGrid(listThuongHieu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void BindGrid(List<ThuongHieu> listThuongHieu)
        {
            dgvThuongHieu.Rows.Clear();
            foreach (var item in listThuongHieu)
            {
                int index = dgvThuongHieu.Rows.Add();
                dgvThuongHieu.Rows[index].Cells[0].Value = item.MaThuongHieu;
                dgvThuongHieu.Rows[index].Cells[1].Value = item.TenThuongHieu;
                dgvThuongHieu.Rows[index].Cells[2].Value = item.DiaChi;
                dgvThuongHieu.Rows[index].Cells[3].Value = item.DienThoai;
                dgvThuongHieu.Rows[index].Cells[4].Value = item.Status;
               
                dgvThuongHieu.Rows[index].Cells[5].Value = item.Logo;


                if (item.Status == true)
                {
                    dgvThuongHieu.Rows[index].Cells[4].Value = "Hết hàng";
                }
                else
                {
                    dgvThuongHieu.Rows[index].Cells[4].Value = "Còn hàng";
                }
            }
        }
        bool kiemtra()
        {
            if (txtMaTH.Text == "" || txtTenTH.Text == ""|| txtDiaChi.Text==""|| txtSDT.Text=="")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin thương hiệu", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (txtMaTH.TextLength != 3)
            {
                MessageBox.Show("Mã thương hiệu phải 3 kí tự", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        public int checkID(string id)
        {
            for (int i = 0; i < dgvThuongHieu.Rows.Count; i++)
            {
                if (dgvThuongHieu.Rows[i].Cells[0].Value != null)
                    if (dgvThuongHieu.Rows[i].Cells[0].Value.ToString() == id)
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
                if (checkID(txtMaTH.Text) == -1)
                {
                    ThuongHieu st = new ThuongHieu();
                    st.MaThuongHieu = txtMaTH.Text;
                    st.TenThuongHieu = txtTenTH.Text;
                    st.DienThoai = txtSDT.Text;
                    st.DiaChi = txtDiaChi.Text;
                    st.Status = checkStatus.Checked;
                    var hinhanh = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                    st.Logo = hinhanh;


                    db.ThuongHieux.Add(st);
                    db.SaveChanges();
                    frmThuongHieu_Load(null,null);
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
                    string id = txtMaTH.Text;
                    ThuongHieu st = db.ThuongHieux.Where(p => p.MaThuongHieu == id).FirstOrDefault();
                    if (st != null)
                    {
                        st.MaThuongHieu = txtMaTH.Text;
                        st.TenThuongHieu = txtTenTH.Text;
                        st.DienThoai = txtSDT.Text;
                        st.DiaChi = txtDiaChi.Text;
                        st.Status = checkStatus.Checked;
                        var hinhanh = (byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]));
                        st.Logo = hinhanh;
                        db.SaveChanges();
                        frmThuongHieu_Load(null, null);
                        MessageBox.Show($"Sửa thương hiệu sản phẩm mã {txtMaTH.Text} thành công", "Thông báo", MessageBoxButtons.OK);
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

        private void dgvThuongHieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvThuongHieu.Rows.Count > 0)
            {
                txtMaTH.Text = dgvThuongHieu.Rows[num].Cells[0].Value.ToString();
                txtTenTH.Text = dgvThuongHieu.Rows[num].Cells[1].Value.ToString();
                txtDiaChi.Text = dgvThuongHieu.Rows[num].Cells[2].Value.ToString();
                txtSDT.Text = dgvThuongHieu.Rows[num].Cells[3].Value.ToString();
                Image mds = (Bitmap)((new ImageConverter()).ConvertFrom(dgvThuongHieu.Rows[num].Cells[5].Value));
                pictureBox1.Image = mds;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnChonLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFile.FileName;
                pictureBox1.ImageLocation = openFile.FileName;
            }
        }
        private byte[] converImgToByte()
        {
            FileStream fs;
            fs = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }

        private void btnXoalogo_Click(object sender, EventArgs e)
        {

        }
    }
}
