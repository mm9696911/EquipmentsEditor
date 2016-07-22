using EquipmentsEditor.Helper;
using EquipmentsEditor.Model;
using EquipmentsEditor.Services;
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

namespace EquipmentsEditor.Forms
{
    public partial class Main : Form
    {
        LoadDataService loadDataService = new LoadDataService();
        DataModel dataModel = new DataModel();
        public Main()
        {
            InitializeComponent();
            LoadBasicData();
        }

        private void LoadBasicData()
        {
            string countriesFilePath = System.IO.Directory.GetCurrentDirectory() + "\\localisation\\countries.yml";
            string countriesNameStr = loadDataService.GetFileStream(countriesFilePath);
            foreach (string countriesName in countriesNameStr.Split(';'))
            {
                string countriesNameRLStr = countriesName.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(" ", "");
                if (!string.IsNullOrEmpty(countriesNameRLStr))
                {
                    CountryModel country = new CountryModel(countriesNameRLStr.Split(':')[0], countriesNameRLStr.Split(':')[1]);
                    dataModel.CountriesList.Add(country);

                    ListViewItem item = new ListViewItem(country.ShortName);
                    item.ToolTipText = country.Name;
                    this.LV_Countries.Items.Add(item);
                }
            }
        }

        private void 打开存档ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择文件";
            //fileDialog.Filter = "所有文件(*.hoi4*)|*.hoi4*";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            fileDialog.InitialDirectory = "";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string saveGamePath = fileDialog.FileName;
                if (!string.IsNullOrEmpty(saveGamePath))
                {
                    XmlHelper xmlHelper = new XmlHelper();
                    bool isSuccess = xmlHelper.SaveToXML("GamePath", saveGamePath);
                }

                //loadDataService = new LoadDataService(saveGamePath);

                loadDataService.LoadData(dataModel,saveGamePath);

                //foreach (CountryModel country in dataModel.CountriesList)
                //{
                //    ListViewItem item = new ListViewItem(country.ShortName);
                //    item.ToolTipText = country.Name;
                //    this.lv_Country.Items.Add(item);
                //}
                //this.lv_Country.DataBindings=;

                //MessageBox.Show("已选择文件:" + saveGamePath, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void text(string value)
        {

            //change();
            string path = "c:\\TextMessage.txt";
            FileStream f = new FileStream(path, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine(value);
            sw.Flush();
            sw.Close();
            f.Close();
        }

        private void LV_Countries_Click(object sender, EventArgs e)
        {
            string shortName= this.LV_Countries.FocusedItem.Text;

            CountryModel country = dataModel.CountriesList.Find(c => c.ShortName == shortName);

            if (country != null)
            {
                this.dataGridView1.DataSource = country.AvailableEquipmentsList;
                //this.dataGridView1.datab();

                this.dataGridView2.DataSource = country.AvailableEquipmentsList;
                //this.dataGridView1.DataBindings();
            }
        }
    }
}
