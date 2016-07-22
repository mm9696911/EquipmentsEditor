using EquipmentsEditor.Helper;
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
        LoadDataService loadData;
        public Main()
        {
            InitializeComponent();
            //OpaqueCommand cmd = new OpaqueCommand();
            //cmd.ShowOpaqueLayer(this, 125, true);
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

                loadData = new LoadDataService(saveGamePath);
                this.dataGridView1.DataSource = loadData.LoadData();

                //text(aaa);
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
    }
}
