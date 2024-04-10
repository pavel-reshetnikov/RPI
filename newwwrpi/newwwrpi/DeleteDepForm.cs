using newwwrpi.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newwwrpi
{
    public partial class DeleteDepForm : Form
    {
        public DeleteDepForm()
        {
            InitializeComponent();
        }
        private void RefreshData()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet2.table_category". При необходимости она может быть перемещена или удалена.
            this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
        }

        private void DeleteDepForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet2.table_category". При необходимости она может быть перемещена или удалена.
            this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var id = 0;
            int.TryParse(comboBox1.SelectedValue.ToString(), out id);
            DBHelper.DeleteEntry("table_category", id);
            RefreshData();
        }

        private void DeleteDepForm_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.table_category". При необходимости она может быть перемещена или удалена.
            this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);

        }
    }
}
