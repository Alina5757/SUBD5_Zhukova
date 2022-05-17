using Contracts.BindingModels;
using Contracts.BusinessLogicsContracts;
using Contracts.ViewModels;
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
    public partial class FormSell : Form
    {
        public int gsmid;
        public int workerid;
        public double sellvalue;
        public DateTime selldate;

        private readonly ISmenaLogic _smenaLogic;
        private readonly IWorkerLogic _workerLogic;
        private readonly IGSMLogic _gsmLogic;
        private readonly ISellLogic _sellLogic;
        private readonly IWorkerGSMLogic _workerGSMLogic;
        public FormSell(IWorkerLogic workerLogic, IGSMLogic gsmLogic, ISellLogic sellLogic, ISmenaLogic smenaLogic, IWorkerGSMLogic workerGSMLogic)
        {
            InitializeComponent();
            _workerLogic = workerLogic;
            _gsmLogic = gsmLogic;
            _sellLogic = sellLogic;
            _smenaLogic = smenaLogic;
            _workerGSMLogic = workerGSMLogic;
        }      

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Заполните поле Объем", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxGSM.SelectedValue == null)
            {
                MessageBox.Show("Выберите ГСМ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWorker.SelectedValue == null)
            {
                MessageBox.Show("Выберите рабочего", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _sellLogic.CreateOrUpdate(new SellBindingModel
                {
                    GSMId = Convert.ToInt32(comboBoxGSM.SelectedValue),
                    WorkerId = Convert.ToInt32(comboBoxWorker.SelectedValue),
                    SellValue = Convert.ToInt32(textBox3.Text),
                    SellDate = Convert.ToDateTime(new DateTime (dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day))
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonupdate_Click(object sender, EventArgs e)
        {
            var smenaread = _smenaLogic.Read(new SmenaBindingModel { SmenaDate = 
                new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day) });
            List<WorkerViewModel> listworker = new List<WorkerViewModel>();
            foreach (var elem in smenaread)
            {
                listworker.AddRange(_workerLogic.Read(new WorkerBindingModel { WorkerId = elem.WorkerId }));
            }

            comboBoxWorker.DisplayMember = "WorkerName";
            comboBoxWorker.ValueMember = "WorkerId";
            comboBoxWorker.DataSource = listworker;
            comboBoxWorker.SelectedItem = null;

            if (listworker == null)
            {
                comboBoxWorker.SelectedItem = null;
                comboBoxWorker.Enabled = false;
            }
            else
            {
                comboBoxWorker.Enabled = true;
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var workergsmread = _workerGSMLogic.Read(new WorkerGSMBindingModel { WorkerId = ((WorkerViewModel)(comboBoxWorker.SelectedItem)).WorkerId });
            List<GSMViewModel> listgsm = new List<GSMViewModel>();
            foreach (var elem in workergsmread)
            {
                listgsm.AddRange(_gsmLogic.Read(new GSMBindingModel { GSMId = elem.GSMId }));
            }

            comboBoxGSM.DisplayMember = "GSMName";
            comboBoxGSM.ValueMember = "GSMId";
            comboBoxGSM.DataSource = listgsm;
            comboBoxGSM.SelectedItem = null;

            if (listgsm == null)
            {
                comboBoxGSM.SelectedItem = null;
                comboBoxGSM.Enabled = false;
            }
            else
            {
                comboBoxGSM.Enabled = true;
            }
        }
    }
}
