using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6._3
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }
         

        private void fMain_Load(object sender, EventArgs e)
        {
            gvProcessor.AutoGenerateColumns = false;
            gvProcessor.ReadOnly = true;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "name";
            column.Name = "Назва";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "manufacturer";
            column.Name = "Виробник";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "core";
            column.Name = "Кількість ядер";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "frequency";
            column.Name = "Частота";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "tdp";
            column.Name = "Тепловіділення";
            gvProcessor.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "performancePerCore";
            column.Name = "Продуктивність";
            gvProcessor.Columns.Add(column);


            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "multiPrecision";
            column.Name = "Багатопоточність";
            column.Width = 100;
            gvProcessor.Columns.Add(column);


            column = new DataGridViewCheckBoxColumn();
            column.DataPropertyName = "energySaving";
            column.Name = "Режим енергозбереження";
            column.Width = 120;
            gvProcessor.Columns.Add(column);


            bindSrcProcessors.Add(new Processor("Intel Core i5", "Intel", 4, 3.0, 65, 250, true, true));
            gvProcessor.DataSource = bindSrcProcessors;

            bindSrcProcessors.Add(new Processor("AMD Ryzen 7 3700X", "AMD", 8, 3.6, 65, 300, true, true));
            gvProcessor.DataSource = bindSrcProcessors;

            bindSrcProcessors.Add(new Processor("Intel Core i3-10100", "Intel", 4, 3.6, 65, 120, false, true));
            gvProcessor.DataSource = bindSrcProcessors;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProcessorBase processorBase = new Processor();

            fProcessor ft = new fProcessor(processorBase);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcProcessors.Add(processorBase);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ProcessorBase processorBase = (Processor)bindSrcProcessors.List[bindSrcProcessors.Position];

            fProcessor ft = new fProcessor(processorBase);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcProcessors.List[bindSrcProcessors.Position] = processorBase;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити поточний запис?", "Видалення запису",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                 bindSrcProcessors.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindSrcProcessors.Clear();
            }
        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            {
                saveFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
                saveFileDialog.Title = "Зберегти дані у текстовому форматі";
                saveFileDialog.InitialDirectory = Application.StartupPath;
                StreamWriter sw;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                    try
                    {
                        foreach (ProcessorBase processorBase in bindSrcProcessors.List)
                        {
                            sw.Write(processorBase.name + "\t" + processorBase.manufacturer + "\t" +
                                processorBase.core + "\t" + processorBase.frequency + "\t" +
                                processorBase.tdp + "\t" + processorBase.performancePerCore +
                                "\t" + processorBase.multiPrecision + "\t" + processorBase.energySaving + "\t\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sw.Close();
                    }
                }
            }
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            {
                saveFileDialog.Filter = "Файли даних (*.processor) |*.processor|All files (*.*) |*.*";
                saveFileDialog.Title = "Зберегти дані у бінарному форматі";
                saveFileDialog.InitialDirectory = Application.StartupPath;
                BinaryWriter bw;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bw = new BinaryWriter(saveFileDialog.OpenFile());
                    try
                    {
                        foreach (ProcessorBase processorBase in bindSrcProcessors.List)
                        {
                            bw.Write(processorBase.name);
                            bw.Write(processorBase.manufacturer);
                            bw.Write(processorBase.core);
                            bw.Write(processorBase.frequency);
                            bw.Write(processorBase.tdp);
                            bw.Write(processorBase.performancePerCore);

                            bw.Write(processorBase.multiPrecision);
                            bw.Write(processorBase.energySaving);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        bw.Close();
                    }
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Текстові файли (*.txt) |*.txt|All files (*.*) |*.*";
                openFileDialog.Title = "Прочитати дані у текстовому форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;
                StreamReader sr;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bindSrcProcessors.Clear();
                    sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                    string s;
                    try
                    {
                        while ((s = sr.ReadLine()) != null)
                        {
                            string[] split = s.Split('\t');
                            ProcessorBase processorBase = new Processor(split[0], split[1], int.Parse(split[2]),
                             double.Parse(split[3]), double.Parse(split[4]), double.Parse(split[5]),
                             bool.Parse(split[6]), bool.Parse(split[7]));

                            bindSrcProcessors.Add(processorBase);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sr.Close();
                    }
                }
            }
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {

            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Файли даних (*.processor) |*.processor|All files (*.*) |*.*";
                openFileDialog.Title = "Прочитати дані у бінарному форматі";
                openFileDialog.InitialDirectory = Application.StartupPath;
                BinaryReader br;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bindSrcProcessors.Clear();
                    br = new BinaryReader(openFileDialog.OpenFile());
                    try
                    {
                        ProcessorBase processorBase; while (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            processorBase = new Processor();
                            for (int i = 1; i <= 10; i++)
                            {
                                switch (i)
                                {
                                    case 1:
                                        processorBase.name = br.ReadString(); break;
                                    case 2:
                                        processorBase.manufacturer = br.ReadString(); break;
                                    case 3:
                                        processorBase.core = br.ReadInt32(); break;
                                    case 4:
                                        processorBase.frequency = br.ReadDouble(); break;
                                    case 5:
                                        processorBase.tdp = br.ReadDouble(); break;
                                    case 6:
                                        processorBase.performancePerCore = br.ReadDouble(); break;
                                    case 7:
                                        processorBase.multiPrecision = br.ReadBoolean(); break;
                                    case 8:
                                        processorBase.energySaving = br.ReadBoolean(); break;
                                }
                            }
                            bindSrcProcessors.Add(processorBase);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        br.Close();
                    }
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити застосунок?", "Вихід з програми", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }

}
