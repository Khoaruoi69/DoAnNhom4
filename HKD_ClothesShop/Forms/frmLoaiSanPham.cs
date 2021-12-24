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
    public partial class frmLoaiSanPham : Form
    {
        QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BindGrid(List<LoaiSanPham> listLoaiSanPham)
        {
            dgvSanPham.Rows.Clear();
            foreach (var item in listLoaiSanPham)
            {
                int index = dgvSanPham.Rows.Add();
                dgvSanPham.Rows[index].Cells[0].Value = item.MaLoaiSP;
                dgvSanPham.Rows[index].Cells[1].Value = item.TenLoaiSP;
                
                if (item.Status == true)
                {
                    dgvSanPham.Rows[index].Cells[2].Value = "Hết hàng";
                }
                else
                {
                    dgvSanPham.Rows[index].Cells[2].Value = "Còn hàng";
                }
                    
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            try
            {
                QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
                List<LoaiSanPham> listLoaiSanPham = db.LoaiSanPhams.ToList();
                BindGrid(listLoaiSanPham);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       
        bool kiemtra()
        {
            if (txtMaLoai.Text == "" || txtTenLoai.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin loại sản phẩm", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            
            if (txtMaLoai.TextLength != 4)
            {
                MessageBox.Show("Mã loại phải 4 kí tự", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;
            
        }
        public int checkID(string id)
        {
            for (int i = 0; i < dgvSanPham.Rows.Count; i++)
            {
                if (dgvSanPham.Rows[i].Cells[0].Value != null)
                    if (dgvSanPham.Rows[i].Cells[0].Value.ToString() == id)
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
                if (checkID(txtMaLoai.Text) == -1)
                {
                    LoaiSanPham st = new LoaiSanPham();
                    st.MaLoaiSP = txtMaLoai.Text;
                    st.TenLoaiSP = txtTenLoai.Text;
                    st.Status = checkStatus.Checked;

                    db.LoaiSanPhams.Add(st);
                    db.SaveChanges();
                    frmLoaiSanPham_Load(null, null);
                    MessageBox.Show("Thêm Loại sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
            
        }
        void edit()
        {
            try
            {
                if (kiemtra() == true)
                {
                    string id = txtMaLoai.Text;
                    LoaiSanPham st = db.LoaiSanPhams.Where(p => p.MaLoaiSP == id).FirstOrDefault();
                    if (st != null)
                    {
                        st.MaLoaiSP = txtMaLoai.Text;
                        st.TenLoaiSP = txtTenLoai.Text;
                        st.Status = checkStatus.Checked;
                        db.SaveChanges();
                        frmLoaiSanPham_Load(null, null);
                        MessageBox.Show($"Sửa loại sản phẩm mã {txtMaLoai.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvSanPham.Rows.Count > 0)
            {
                txtMaLoai.Text = dgvSanPham.Rows[num].Cells[0].Value.ToString();
                txtTenLoai.Text = dgvSanPham.Rows[num].Cells[1].Value.ToString();
                txtStatus.Text = dgvSanPham.Rows[num].Cells[2].Value.ToString();

                if(dgvSanPham.Rows[num].Cells[2].Value.ToString() == "Hết hàng")
                {
                    checkStatus.Checked = true;
                }
                else
                {
                    checkStatus.Checked = false;
                }
            }
        }

        private void btnCreat_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ??", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            edit();

        }

        private void checkStatus_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
