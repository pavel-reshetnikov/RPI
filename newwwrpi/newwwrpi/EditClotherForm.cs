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
    public partial class EditClotherForm : Form
    {
        private bool _isEditMode = false;
        private int _id = 0;
        public EditClotherForm()
        {
            InitializeComponent();
        }
        public EditClotherForm(bool isEditMode, int id)
        {
            _isEditMode = isEditMode;
            _id = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Productfields = new Dictionary<string, TableField>();
            var specificationsfields = new Dictionary<string, TableField>();

            Productfields.Add(Constants.FieldNames.clotherTable.c_id, new TableField
            {
                TableFieldType = TableFieldTypes.integer,
                TableFieldValue = comboBox1.SelectedValue.ToString()
            });
            Productfields.Add(Constants.FieldNames.clotherTable.name, new TableField
            {
                TableFieldType = TableFieldTypes.varchar,
                TableFieldValue = textBox1.Text
            });
            
                
            
            specificationsfields.Add(Constants.FieldNames.specificationsTable.color, new TableField
            {
                TableFieldType = TableFieldTypes.varchar,
                TableFieldValue = textBox2.Text
            });
            specificationsfields.Add(Constants.FieldNames.specificationsTable.size, new TableField
            {
                TableFieldType = TableFieldTypes.varchar,
                TableFieldValue = textBox3.Text
            });
            specificationsfields.Add(Constants.FieldNames.specificationsTable.firm, new TableField
            {
                TableFieldType = TableFieldTypes.varchar,
                TableFieldValue = textBox4.Text
            });
            specificationsfields.Add(Constants.FieldNames.specificationsTable.price, new TableField
            {
                TableFieldType = TableFieldTypes.varchar,
                TableFieldValue = textBox5.Text
            });
            specificationsfields.Add(Constants.FieldNames.specificationsTable.description, new TableField
            {
                TableFieldType = TableFieldTypes.varchar,
                TableFieldValue = richTextBox1.Text
            });

            if (_isEditMode)
            {
                DBHelper.UpdateEntry(Constants.TableNames.clotherTableName, _id, Productfields);
                DBHelper.UpdateEntrySpec(Constants.TableNames.specificationsTableName, _id, specificationsfields);
                
                DialogResult = DialogResult.OK;
            }
            else
            {
                DBHelper.InsertEntry(Constants.TableNames.clotherTableName, Productfields);

                specificationsfields.Add(Constants.FieldNames.specificationsTable.clother_id, new TableField
                {
                    TableFieldType = TableFieldTypes.varchar,
                    TableFieldValue = DBHelper.nid.ToString()
                });

                DBHelper.InsertEntrySpec(Constants.TableNames.specificationsTableName, specificationsfields);
                DialogResult = DialogResult.OK;
            }
        }

        private void EditClotherForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shopDataSet.table_category". При необходимости она может быть перемещена или удалена.
            this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
            if (_isEditMode)
            {
                //this.table_clotherTableAdapter.Fill(this.shopDataSet.table_clother);
                this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
                var ProductfieldsValues = DBHelper.SelectValuesFromTable(Constants.TableNames.clotherTableName, _id);
                var SpecfieldsValues = DBHelper.SelectValuesFromSpec(Constants.TableNames.specificationsTableName, _id);
                Console.WriteLine(SpecfieldsValues[0]);
                Console.WriteLine(SpecfieldsValues[1]);
                Console.WriteLine(SpecfieldsValues[2]);
                Console.WriteLine(SpecfieldsValues[3]);
                comboBox1.SelectedValue = ProductfieldsValues[0];               
                textBox1.Text = ProductfieldsValues[2];
                textBox2.Text = SpecfieldsValues[1];//color
                textBox3.Text = SpecfieldsValues[2];//size
                textBox4.Text = SpecfieldsValues[3];//firm
                textBox5.Text = SpecfieldsValues[5];//price
                richTextBox1.Text = SpecfieldsValues[4];//desc

            }
            else
            {
                // TODO: This line of code loads data into the 'warehouseDBDataSetFull.Categories' table. You can move, or remove it, as needed.
                this.table_categoryTableAdapter.Fill(this.shopDataSet.table_category);
            }
        }
    }
}
