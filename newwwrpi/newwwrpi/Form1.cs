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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.specifications". При необходимости она может быть перемещена или удалена.
            this.specificationsTableAdapter.Fill(this.shopDataSet.specifications);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.table_clother". При необходимости она может быть перемещена или удалена.
            this.table_clotherTableAdapter.Fill(this.shopDataSet.table_clother);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.table_category". При необходимости она может быть перемещена или удалена.
            this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);

        }
        private void RefreshData()
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.table_clother". При необходимости она может быть перемещена или удалена.
            this.table_clotherTableAdapter.Fill(this.shopDataSet.table_clother);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.table_category". При необходимости она может быть перемещена или удалена.
            this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.specifications". При необходимости она может быть перемещена или удалена.
            this.specificationsTableAdapter.Fill(this.shopDataSet.specifications);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (new EditDepFrom().ShowDialog() == DialogResult.OK)
                {
                    this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
                    Console.WriteLine("new");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
                MessageBox.Show(ex.ToString());
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new DeleteDepForm().ShowDialog() == DialogResult.OK)
            {
                RefreshData();
                Console.WriteLine("refresh");
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    Console.WriteLine("1");
                    if (comboBox1.Items.Count == 0)
                    {
                        MessageBox.Show("Отсутствует категория");
                        return;
                    }
                    var id = 0;
                    if (int.TryParse(comboBox1.SelectedValue.ToString(), out id) &&
                        new EditDepFrom(true, id).ShowDialog() == DialogResult.OK)
                    {
                        this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("2");
                    MessageBox.Show(ex.ToString());
                }

            }
            
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0 || dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Не выбран продукт");
                    return;
                }

                if (MessageBox.Show($"Вы действительно хотите удалить выделенный товар?", "Удаление товара",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        var id = 0;
                        int.TryParse(dataGridView1.SelectedRows[i].Cells[1].Value?.ToString(), out id);
                        DBHelper.DeleteEntry(Constants.TableNames.clotherTableName, id);
                    }
                    this.table_clotherTableAdapter.Fill(this.shopDataSet.table_clother);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        => EditProduct(true);
        private void EditProduct(bool isEditMode = false)
        {
            try
            {
                if (isEditMode)
                {
                    if (dataGridView1.Rows.Count == 0 || dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("Не выбран продукт");
                        return;
                    }
                    var id = 0;
                    int.TryParse(dataGridView1.SelectedRows[0].Cells[1].Value?.ToString(), out id); //id
                    if (id != 0 && new EditClotherForm(true, id).ShowDialog() == DialogResult.OK)
                    {

                        this.table_clotherTableAdapter.Fill(this.shopDataSet.table_clother);
                        this.specificationsTableAdapter.Fill(this.shopDataSet.specifications);
                    }
                }
                else if (new EditClotherForm().ShowDialog() == DialogResult.OK)
                {
                    this.table_clotherTableAdapter.Fill(this.shopDataSet.table_clother);
                    this.specificationsTableAdapter.Fill(this.shopDataSet.specifications);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        => EditProduct();
    }
}
public static class Constants
{
    public static class TableNames
    {
        public const string CategoriesTableName = "table_category";
        public const string clotherTableName = "table_clother";
        public const string specificationsTableName = "specifications";

    }
    public static class FieldNames
    {
        public const string Id = "id";
        public static class CategoriesTable
        {
            public const string Title = "category";
        }

        public static class categoryTable
        {
            public const string category = "category";
        }
        public static class clotherTable
        {
            public const string c_id = "c_id";
            public const string name = "name";
        }
        public static class specificationsTable
        {
            public const string clother_id = "clother_id";
            public const string color = "color";
            public const string size = "sizes";
            public const string firm = "firm";
            public const string description = "description";
            public const string price = "price";
        }
    }
}
