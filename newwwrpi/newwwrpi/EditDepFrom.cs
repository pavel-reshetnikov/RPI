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
    public partial class EditDepFrom : Form
    {
        private bool _editMode = false;
        private int _depId = 0;
        public EditDepFrom()
        {
            InitializeComponent();
        }
        public EditDepFrom(bool editMode = false, int depId = 0)
        {
            _editMode = editMode;
            _depId = depId;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_editMode)
            {
                var fields = new Dictionary<string, TableField>();
                fields.Add(Constants.FieldNames.CategoriesTable.Title, new TableField
                {
                    TableFieldType = TableFieldTypes.varchar,
                    TableFieldValue = textBox1.Text
                });
                DBHelper.UpdateEntry(Constants.TableNames.CategoriesTableName, _depId, fields);
                DialogResult = DialogResult.OK;
                Console.WriteLine("111");
            }
            else
            {
                var fields = new Dictionary<string, TableField>();
                fields.Add(Constants.FieldNames.CategoriesTable.Title, new TableField
                {
                    TableFieldType = TableFieldTypes.varchar,
                    TableFieldValue = textBox1.Text
                });

                DBHelper.InsertEntry(Constants.TableNames.CategoriesTableName, fields);
                DialogResult = DialogResult.OK;
                Console.WriteLine("2");
            }
        }

        private void EditDepFrom_Load(object sender, EventArgs e)
        {
            if (_editMode)
            {
                //var fieldsValues = DBHelper.SelectValuesFromTable(Constants.TableNames.CategoriesTableName, _CategoryId);
                //var fieldsValues = DBHelper.SelectValuesFromTable(Constants.TableNames.categoryTableName, _depId);
                Console.WriteLine(_depId);
                var fieldsValues = DBHelper.SelectValuesFromTable(Constants.TableNames.CategoriesTableName, _depId);
                textBox1.Text = fieldsValues[1];

            }
        }
    }
}
