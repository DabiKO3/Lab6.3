using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6._3
{
    public partial class fProcessor : Form
    {
        public fProcessor(ProcessorBase p)
        {
            TheProcessorBase = p;
            InitializeComponent();
        }
        public ProcessorBase TheProcessorBase;

        
        private void btnOk_Click(object sender, EventArgs e)
        {
            TheProcessorBase.name = tbName.Text.Trim();
            TheProcessorBase.manufacturer = tbManufacturer.Text.Trim();
            TheProcessorBase.core = int.Parse(tbCores.Text.Trim());
            TheProcessorBase.frequency = double.Parse(tbFrequency.Text.Trim());
            TheProcessorBase.tdp = double.Parse(tbTDP.Text.Trim());
            TheProcessorBase.performancePerCore = double.Parse(tbPerformancePerCore.Text.Trim());

            TheProcessorBase.multiPrecision = chbMP.Checked;
            TheProcessorBase.energySaving = chbES.Checked;

            DialogResult = DialogResult.OK; 


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void fProcessor_Load(object sender, EventArgs e)
        {
           if (TheProcessorBase != null)
            {

                tbName.Text = TheProcessorBase.name;
                tbManufacturer.Text = TheProcessorBase.manufacturer;
                tbCores.Text = TheProcessorBase.core.ToString();
                tbFrequency.Text = TheProcessorBase.frequency.ToString();
                tbTDP.Text = TheProcessorBase.tdp.ToString();
                tbPerformancePerCore.Text = TheProcessorBase.performancePerCore.ToString();

                chbMP.Checked = TheProcessorBase.multiPrecision;
                chbES.Checked = TheProcessorBase.energySaving;
            }
        }
    }
}
