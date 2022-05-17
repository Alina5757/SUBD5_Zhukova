using Contracts.BindingModels;
using Contracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views
{
    public partial class FormSmena : Form
    {
        private readonly ISmenaLogic _smenaLogic;
        public FormSmena(ISmenaLogic smenalogic)
        {
            InitializeComponent();
            _smenaLogic = smenalogic;
        }

        public void LoadData()
        {
            try
            {
                var list = _smenaLogic.Read(null);
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Заполните поле ID работника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Заполните поле Номер Смены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _smenaLogic.CreateOrUpdate(new SmenaBindingModel
                {
                    WorkerId = Convert.ToInt32(textBox2.Text),
                    SmenaNumber = Convert.ToInt32(textBox4.Text),
                    SmenaDate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int SmenaNum = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value);
                int WorkerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                DateTime date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value);
                try
                {
                    _smenaLogic.Delete(new SmenaBindingModel { WorkerId = WorkerId, SmenaNumber = SmenaNum, SmenaDate = date });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadData();
            }
        }

        private void FormSmena_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
