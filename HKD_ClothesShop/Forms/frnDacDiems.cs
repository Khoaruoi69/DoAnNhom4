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
    public partial class frnDacDiems : Form
    {
        QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
        public frnDacDiems()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void frnDacDiems_Load(object sender, EventArgs e)
        {
            try
            {
                QLBanHangHKDEntities1 db = new QLBanHangHKDEntities1();
                List<DacDiem> listDacDiem = db.DacDiems.ToList();
                List<DacDiem_SanPham> listDacDiemChung = db.DacDiem_SanPham.ToList();
                List<SanPham> listSanPham = db.SanPhams.ToList();
                BindGrid1(listDacDiemChung);
                BindGrid(listDacDiem);
                FillFalcultyCombobox(listSanPham);
                FillFalcultyCombobox1(listDacDiem);
                FillFalcultyCombobox2(listDacDiem);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<DacDiem> listDacDiem)
        {
            dgvDacDiem.Rows.Clear();
            foreach (var item in listDacDiem)
            {
                int index = dgvDacDiem.Rows.Add();
                dgvDacDiem.Rows[index].Cells[0].Value = item.Size;
                dgvDacDiem.Rows[index].Cells[1].Value = item.Color;
                dgvDacDiem.Rows[index].Cells[2].Value = item.Status;



                if (item.Status == false)
                {
                    dgvDacDiem.Rows[index].Cells[2].Value = "Còn sử dụng";
                }
                else
                {
                    dgvDacDiem.Rows[index].Cells[2].Value = "Hết sử dụng";
                }
            }
        }
        bool kiemtra()
        {
            if (txtSize.Text == "" || txtColor.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin ", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (txtSize.TextLength > 5)
            {
                MessageBox.Show("Kích thước nhập sai", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        public int checkID(string id, string ids)
        {
            for (int i = 0; i < dgvDacDiem.Rows.Count; i++)
            {
                if (dgvDacDiem.Rows[i].Cells[0].Value != null && dgvDacDiem.Rows[i].Cells[1].Value != null)
                    if (dgvDacDiem.Rows[i].Cells[0].Value.ToString().Trim() == id && dgvDacDiem.Rows[i].Cells[1].Value.ToString().Trim() == ids)
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
                    if (checkID(txtSize.Text, txtColor.Text) == -1)
                    {
                        DacDiem st = new DacDiem();
                        st.Size = txtSize.Text;
                        st.Color = txtColor.Text;
                        st.MoTa = "Không";
                        st.Status = checkStatus.Checked;
                        db.DacDiems.Add(st);
                        db.SaveChanges();
                        frnDacDiems_Load(null, null);
                        MessageBox.Show("Thêm đặc điểm chung sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
            

        }
        void edit()
        {
            try
            {
                if (kiemtra() == true)
                {
                    string id = txtSize.Text;
                   // string ids = txtColor.Text;
                    DacDiem st = db.DacDiems.Where(p => p.Size == id ).FirstOrDefault();
                    if (st != null)
                    {
                        /*
                        st.Size = txtSize.Text;
                        st.Color = txtColor.Text;
                        */
                        st.MoTa = "không";
                        st.Status = checkStatus.Checked;
                        db.SaveChanges();
                        frnDacDiems_Load(null, null);
                        MessageBox.Show($"Sửa đặc điểm chung sản phẩm size {txtSize.Text}, màu {txtColor.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void btntThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ??", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvDacDiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvDacDiem.Rows.Count > 0)
            {
                txtSize.Text = dgvDacDiem.Rows[num].Cells[0].Value.ToString();
                txtColor.Text = dgvDacDiem.Rows[num].Cells[1].Value.ToString();
                lbStatus.Text = dgvDacDiem.Rows[num].Cells[2].Value.ToString();

                
                if (dgvDacDiem.Rows[num].Cells[2].Value.ToString() == "Hết sử dụng")
                {
                    checkStatus.Checked = true;
                }
                else
                {
                    checkStatus.Checked = false;
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void BindGrid1(List<DacDiem_SanPham> listDacDiemChung)
        {
            dgvDacDiemChung.Rows.Clear();
            foreach (var item in listDacDiemChung)
            {
                int index = dgvDacDiemChung.Rows.Add();
                dgvDacDiemChung.Rows[index].Cells[0].Value = item.MaSanPham;
                dgvDacDiemChung.Rows[index].Cells[1].Value = item.Color;
                dgvDacDiemChung.Rows[index].Cells[2].Value = item.Size;
                dgvDacDiemChung.Rows[index].Cells[3].Value = item.SoLuong;
            }
        }
        bool kiemtra1()
        {
            if (cbmMaSP.Text == "" || cmbColor.Text == ""||cmbSize.Text==""||txtSoLuong.Text=="")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin ", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            if (txtSoLuong.TextLength < 0)
            {
                MessageBox.Show("Số lượng nhập sai", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            return true;

        }
        
        public int checkID1(string id, string ids, string idss)
        {
            for (int i = 0; i < dgvDacDiemChung.Rows.Count; i++)
            {
                if (dgvDacDiemChung.Rows[i].Cells[0].Value != null && dgvDacDiemChung.Rows[i].Cells[1].Value != null&& dgvDacDiemChung.Rows[i].Cells[2].Value != null)
                    if (dgvDacDiemChung.Rows[i].Cells[0].Value.ToString().Trim() == id && dgvDacDiemChung.Rows[i].Cells[1].Value.ToString().Trim() == ids&& dgvDacDiemChung.Rows[i].Cells[2].Value.ToString().Trim()==idss)
                    {
                        return i;
                    }
            }
            return -1;
        }
        
        void add1()
        {
            try
            {
                if (kiemtra1() == true)
                {
                    if (checkID1(cmbColor.Text, cbmMaSP.Text, cmbSize.Text) == -1)
                    {
                        DacDiem_SanPham st = new DacDiem_SanPham();
                        st.MaSanPham = cbmMaSP.Text;
                        st.Color = cmbColor.Text;
                        st.Size = cmbSize.Text;
                        st.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                        db.DacDiem_SanPham.Add(st);
                        db.SaveChanges();
                        frnDacDiems_Load(null, null);
                        MessageBox.Show("Thêm đặc điểm chung sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
          
        }
        void edit1()
        {

            try
            {
                if (kiemtra1() == true)
                {
                    string id = cbmMaSP.Text;
                    string ids = cmbColor.Text;
                    string idss = cmbSize.Text;
                    DacDiem_SanPham st = db.DacDiem_SanPham.Where(p => p.Size == id&& p.Color==ids && p.Size==idss).FirstOrDefault();
                    if (st != null)
                    {
                        /*
                        st.Size = txtSize.Text;
                        st.Color = txtColor.Text;
                        */

                        st.SoLuong = Convert.ToInt32(txtSoLuong.Text);
                        db.SaveChanges();
                        frnDacDiems_Load(null, null);
                        MessageBox.Show($"Sửa đặc điểm chung sản phẩm size {cbmMaSP.Text}, màu {cmbColor.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("đã xãy ra lỗi", "thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add1();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            edit1();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát ??", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvDacDiemChung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int num = e.RowIndex;
            if (dgvDacDiemChung.Rows.Count > 0)
            {
                cbmMaSP.Text = dgvDacDiemChung.Rows[num].Cells[0].Value.ToString();
                cmbColor.Text = dgvDacDiemChung.Rows[num].Cells[1].Value.ToString();
                cmbSize.Text = dgvDacDiemChung.Rows[num].Cells[2].Value.ToString();
                txtSoLuong.Text = dgvDacDiemChung.Rows[num].Cells[3].Value.ToString();
            }
        }
        private void FillFalcultyCombobox(List<SanPham> listSanPham)
        {
            this.cbmMaSP.DataSource = listSanPham;
            this.cbmMaSP.ValueMember = "MaSanPham";
        }
        private void FillFalcultyCombobox1(List<DacDiem> listDacDiem)
        {
            this.cmbColor.DataSource = listDacDiem;
            this.cmbColor.ValueMember = "Color";
        }
        private void FillFalcultyCombobox2(List<DacDiem> listDacDiem)
        {
            this.cmbSize.DataSource = listDacDiem;
            this.cmbSize.ValueMember = "Size";
        }
    }
}
