using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace P3
{
    public partial class Main : Form
    {
        private string FileData;
        private string FileResults; 
        private Register InitialRegister;
        private Register RequireVaccinationRegister;
        private Register NeedSpecialCareRegister;

        public Main()
        {
            InitializeComponent();
            ToggleControls();
            InitialRegister = new Register();
            NeedSpecialCareRegister = new Register();
        }

        public void ToggleControls(bool enabled = false)
        {
            saveToolStripMenuItem.Enabled = enabled;
            findTwoOldestToolStripMenuItem.Enabled = enabled;
            requireValidationToolStripMenuItem.Enabled = enabled;
            countAggressiveToolStripMenuItem.Enabled = enabled;
            specialCareToolStripMenuItem.Enabled = enabled;
            listAnimals.Items.Clear();
        }

        void DisplayRegisterToListBox(string header, bool supplyHeaders, Register register)
        {
            listAnimals.Items.Add(header);
            if (supplyHeaders)
            {
                string headerFormat = string.Format("Database: {0,-12}",
                register.DatabaseName);
                listAnimals.Items.Add(headerFormat);
            }
            listAnimals.Items.Add(new string('-', 104));
            string tableHeader = string.Format
            ("| {0,-9} | {1, 8} | {2,-10} | {3,-12} | {4,-12} | {5,-8} | {6} | {7} |",
            "Type", "ID", "Name", "Breed", "Birth Date", "Gender", "Vaccination", "Aggressive");
            listAnimals.Items.Add(tableHeader);
            listAnimals.Items.Add(new string('-', 104));

            for (int i = 0; i < register.Count(); i++)
            {
                Animal a = register.Get(i);
                listAnimals.Items.Add(a.ToString());
            }
            listAnimals.Items.Add(new string('-', 104));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open initial file of animal data";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files|*.txt|Word Documents|*.doc";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileData = openFileDialog1.FileName;
                InitialRegister = InOutUtils.Read(FileData);
                ToggleControls(true);
                DisplayRegisterToListBox("Animals (initial)", true, InitialRegister);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save your results";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text Files|*.txt|Word Documents|*.doc";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileResults = saveFileDialog1.FileName;
                using (StreamWriter writer = new StreamWriter(FileResults, false, Encoding.GetEncoding(1257)))
                {
                    foreach (string item in listAnimals.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                    writer.WriteLine();
                    if (NeedSpecialCareRegister.Count() != 0)
                    {
                        writer.WriteLine("File: " + InOutUtils.CCSV + " created.");
                    }
                    else
                    {
                        writer.WriteLine("File: " + InOutUtils.CCSV + " not created.");
                    }
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void findTwoOldestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listAnimals.Items.Add("\n");
            listAnimals.Items.Add("Two oldest animals in register:\n");
            listAnimals.Items.Add(new string('-', 104));
            string tableHeader = string.Format
            ("| {0,-9} | {1, 8} | {2,-10} | {3,-12} | {4,-12} | {5,-8} | {6} | {7} |",
                "Type","ID","Name","Breed","Birth Date","Gender","Vaccination","Aggressive");
            listAnimals.Items.Add(tableHeader);
            listAnimals.Items.Add(new string('-', 104));
            Animal Oldest1, Oldest2;
            if (Register.TwoOldestAnimals(InitialRegister, out Oldest1, out Oldest2))
            {
                string Oldest1Format = string.Format("{0} ", Oldest1.ToString());
                string Oldest2Format = string.Format("{0} ", Oldest2.ToString());
                listAnimals.Items.Add(Oldest1Format);
                listAnimals.Items.Add(Oldest2Format);
                listAnimals.Items.Add(new string('-', 104));
            }
            else
            {
                listAnimals.Items.Add("Two oldest animals were not found");
            }
        }

        private void requireValidationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listAnimals.Items.Add("\n");
            RequireVaccinationRegister = Register.RequireVaccination(InitialRegister);
            if (RequireVaccinationRegister.Count() != 0)
            {
                DisplayRegisterToListBox("Animals that require vaccination", false,
                RequireVaccinationRegister);
            }
            else
            {
                listAnimals.Items.Add("No animals that require vaccination are found");
            }
        }

        private void countAggressiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listAnimals.Items.Add("\n");
            int numberOfAggresiveDogs = Register.CountAggresiveDogs(InitialRegister);
            string numberOfAggresiveDogs_format =
            string.Format("Number of aggressive dogs in animal database: {0} ", numberOfAggresiveDogs.ToString());
            listAnimals.Items.Add(numberOfAggresiveDogs_format);
        }

        private void specialCareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listAnimals.Items.Add("\n");
            Register SpecialCareDogs = new Register();
            Register.NeedSpecialCareOfType(InitialRegister, typeof(Dog), ref SpecialCareDogs);
            SpecialCareDogs.Sort(); 
            Register SpecialCareCats = new Register();
            Register.NeedSpecialCareOfType(InitialRegister, typeof(Cat), ref SpecialCareCats);
            SpecialCareCats.Sort();
            Register SpecialCareGuineaPigs = new Register();
            Register.NeedSpecialCareOfType(InitialRegister, typeof(GuineaPig), ref SpecialCareGuineaPigs);
            SpecialCareGuineaPigs.Sort();
            NeedSpecialCareRegister = SpecialCareDogs + SpecialCareCats + SpecialCareGuineaPigs;
            if (NeedSpecialCareRegister.Count() != 0)
            {
                DisplayRegisterToListBox("Animals that require special care", false, NeedSpecialCareRegister);
                InOutUtils.WriteToCSV(InOutUtils.CCSV, false, NeedSpecialCareRegister);
            }
            else
            {
                listAnimals.Items.Add("No animals that require special care are found");
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
