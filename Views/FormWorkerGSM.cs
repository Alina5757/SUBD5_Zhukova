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
    public partial class FormWorkerGSM : Form
    {
        private readonly IGSMLogic _gsmLogic;
        private readonly IWorkerLogic _workerLogic;
        private readonly IWorkerGSMLogic _workergsmLogic;

        public FormWorkerGSM(IGSMLogic gsmLogic, IWorkerLogic workerLogic, IWorkerGSMLogic workerGSMLogic)
        {
            InitializeComponent();
            _gsmLogic = gsmLogic;
            _workerLogic = workerLogic;
            _workergsmLogic = workerGSMLogic;
        }

        public void LoadData()
        {
            try
            {
                var list = _workergsmLogic.Read(null);
                if (list != null)
                {
                    dataGridView1.DataSource = list;
                    dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                MessageBox.Show("Заполните поле ID ГСМ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _workergsmLogic.CreateOrUpdate(new WorkerGSMBindingModel
                {
                    WorkerId = Convert.ToInt32(textBox2.Text),
                    GSMId = Convert.ToInt32(textBox4.Text)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                textBox1.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[2].Value);
                textBox3.Text = Convert.ToString(dataGridView1.SelectedRows[0].Cells[0].Value);
            }
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Заполните поле ID работника", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Заполните поле ID ГСМ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _workergsmLogic.CreateOrUpdate(new WorkerGSMBindingModel
                {
                    WorkerId = Convert.ToInt32(textBox3.Text),
                    GSMId = Convert.ToInt32(textBox1.Text)
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
                int GSMid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
                int WorkerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                try
                {
                    _workergsmLogic.Delete(new WorkerGSMBindingModel { GSMId = GSMid, WorkerId = WorkerId });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadData();
            }
        }

        private void FormWorkerGSM_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
