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
                BindGrid(listDacDiem);
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



                if (item.Status == true)
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
                    if (dgvDacDiem.Rows[i].Cells[0].Value.ToString() == id && dgvDacDiem.Rows[i].Cells[1].Value.ToString() == ids)
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
                if (checkID(txtSize.Text, txtColor.Text) == -1)
                {
                    DacDiem st = new DacDiem();
                    st.Size = txtSize.Text;
                    st.Color = txtColor.Text;
                    st.MoTa = "Không";
                    st.Status = checkStatus.Checked;
                   

                    db.DacDiems.Add(st);
                    db.SaveChanges();
                    frnDacDiems_Load(null,null);
                    MessageBox.Show("Thêm đặc điểm chung sản phẩm thành công", "Thông báo", MessageBoxButtons.OK);
                }

            }

        }
        void edit()
        {
            try
            {
                if (kiemtra() == true)
                {
                    string id = txtSize.Text;
                    string ids = txtColor.Text;
                    DacDiem st = db.DacDiems.Where(p => p.Size == id && p.Color ==ids).FirstOrDefault();
                    if (st != null)
                    {
                        st.Size = txtSize.Text;
                        st.Color = txtColor.Text;
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
    }
}
