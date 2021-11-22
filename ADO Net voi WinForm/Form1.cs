using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LTCSDL.DTO;
using LTCSDL.BLL;

namespace ADO_Net_voi_WinForm
{
    public partial class Form1 : Form
    {
        CategoryBLL bus;
        BindingSource bs;
        public Form1()
        {
            InitializeComponent();
            bus = new CategoryBLL();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            bs = new BindingSource();
            List<Category> lst = bus.GetAll();
            bs.DataSource = typeof(Category);
            foreach (Category cat in lst)
            {
                bs.Add(cat);
            }
            grid.DataSource = bs;
            grid.AutoGenerateColumns = true;
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var val = grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //txtDesc.Text = val.ToString();
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = grid.Rows[e.RowIndex];
            txtID.Text = row.Cells["CategoryID"].Value.ToString();
            txtName.Text = row.Cells["CategoryName"].Value.ToString();
            txtDesc.Text = row.Cells["Description"].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtID.Text.Trim()!="")
            {
                Category c = new Category();
                c.CategoryID = int.Parse(txtID.Text);
                c.CategoryName = txtName.Text;
                c.Description = txtDesc.Text;
                int i = bus.Update(c);
                if (i == 1)
                {
                    MessageBox.Show("Updated successfully !", "Update Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                    MessageBox.Show("Failed to update!", "Update Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() != "" && txtDesc.Text.Trim() !="")
            {
                Category c = new Category();                
                c.CategoryName = txtName.Text;
                c.Description = txtDesc.Text;
                int i = bus.Insert(c);
                if(i>0)
                {
                    MessageBox.Show("Inserted successfully !", "Insert Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtID.Text = i.ToString();
                    LoadData();
                }
                else
                    MessageBox.Show("Failed to insert!", "Insert Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() != "")
            {
                int id = int.Parse(txtID.Text);
                int i = bus.Delete(id);
                if (i == 1)
                {
                    MessageBox.Show("Deleted successfully !", "Delete Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                    MessageBox.Show("Failed to delete!", "Delete Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
