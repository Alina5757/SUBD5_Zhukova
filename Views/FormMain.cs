using Contracts.BindingModels;
using Contracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using Unity;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contracts.ViewModels;

namespace Views
{
    public partial class FormMain : Form
    {
        private readonly ISellLogic _sellLogic;
        private readonly IGSMLogic _gsmLogic;
        private readonly IWorkerLogic _workerLogic;
        private readonly IWorkerGSMLogic _workerGSMLogic;
        private readonly ISmenaLogic _smenaLogic;


        public FormMain(ISellLogic sellLogic, IGSMLogic gSMLogic, IWorkerLogic workerLogic, IWorkerGSMLogic workerGSMLogic, ISmenaLogic smenaLogic)
        {
            InitializeComponent();
            _sellLogic = sellLogic;
            _gsmLogic = gSMLogic;
            _workerLogic = workerLogic;
            _workerGSMLogic = workerGSMLogic;
            _smenaLogic = smenaLogic;
        }

        public class ItemCombo { 
            public string Name { get; set; }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {            
            try
            {
                List<ItemCombo> listname = new List<ItemCombo>() { new ItemCombo { Name = "WorkerId"}, new ItemCombo { Name = "GSMId"}, new ItemCombo { Name = "SellDate"} };
                comboBoxName.DisplayMember = "Name";
                comboBoxName.ValueMember = "Name";
                comboBoxName.DataSource = listname;
                comboBoxName.SelectedItem = null;
                

                var list = _sellLogic.Read(null);
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

        private void buttonForming_Click(object sender, EventArgs e)
        {
            try
            {
                var list = _sellLogic.Read(null);
                if (comboBoxName.SelectedItem == null) {
                    LoadData();
                    return;
                }
                if (((ItemCombo)comboBoxName.SelectedItem).Name == "WorkerId")
                {
                    int ch = 0;
                    if (textBox1.Text.Length != 0 && int.TryParse(textBox1.Text, out ch))
                    {
                        list = _sellLogic.Read(new SellBindingModel { WorkerId = ch });
                    }
                }
                if (((ItemCombo)comboBoxName.SelectedItem).Name == "GSMId")
                {
                    int ch = 0;
                    if (textBox1.Text.Length != 0 && int.TryParse(textBox1.Text, out ch))
                    {
                        list = _sellLogic.Read(new SellBindingModel { GSMId = ch });
                    }
                }
                if (((ItemCombo)comboBoxName.SelectedItem).Name == "SellDate")
                {
                    DateTime ch;
                    if (textBox1.Text.Length != 0 && DateTime.TryParse(textBox1.Text, out ch))
                    {
                        list = _sellLogic.Read(new SellBindingModel { SellDate = ch });
                    }
                }
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
            var form = Program.Container.Resolve<FormSell>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormSell>();
                form.gsmid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                try
                {
                    _sellLogic.Delete(new SellBindingModel { SellId = id });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadData();
            }
        }

        private void GSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormGSMs>();
            form.ShowDialog();
        }

        private void workerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormWorkers>();
            form.ShowDialog();
        }

        private void priceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormPrice>();
            form.ShowDialog();
        }

        private void smenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormSmena>();
            form.ShowDialog();
        }

        private void workerGSMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormWorkerGSM>();
            form.ShowDialog();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            for (int i = 1; i <= 1000; i++)
            {
                _gsmLogic.CreateOrUpdate(new GSMBindingModel { GSMName = "Бензин-" + i.ToString() });
                _workerLogic.CreateOrUpdate(new WorkerBindingModel { WorkerName = "Работник-" + i.ToString() });
            }
            for (int i = 1; i <= 1000; i++)
            {
                int ch = rand.Next(1, 1001);
                _workerGSMLogic.CreateOrUpdate(new WorkerGSMBindingModel { WorkerId = i, GSMId = ch });
                int numsm = rand.Next(1, 4);
                DateTime date = new DateTime(2022, 5, 17);
                _smenaLogic.CreateOrUpdate(new SmenaBindingModel { WorkerId = i, SmenaDate = date, SmenaNumber = numsm });
            }
            for (int i = 1; i <= 1000; i++)
            {
                List<WorkerGSMViewModel> list = _workerGSMLogic.Read(new WorkerGSMBindingModel { WorkerId = i });
                int idgsm = list[0].GSMId;
                DateTime date = new DateTime(2022, 5, 17);
                int val = rand.Next(5, 51);
                _sellLogic.CreateOrUpdate(new SellBindingModel { WorkerId = i, GSMId = idgsm, SellDate = date, SellValue = val });
            }
        }
    }
}
