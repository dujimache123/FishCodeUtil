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
using TableToLua;

namespace TableToLua
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_chooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                this.textBox_sourceFilePath.Text = path;
            }
        }

        private void button_generate_Click(object sender, EventArgs e)
        {
            if (this.textBox_luaTableName.Text.Length == 0)
            {
                MessageBox.Show("Lua Table Name 不能为空!!!");
                return;
            }
            string luaStr = TableToLua.getInstance().toLua(this.textBox_luaTableName.Text, this.textBox_sourceFilePath.Text);
            string luaTableName = this.textBox_luaTableName.Text + ".lua";
            FileStream fs = new FileStream(luaTableName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(luaStr);
            sw.Flush();
            fs.Close();
            MessageBox.Show("保存成功!");
        }
    }
}
