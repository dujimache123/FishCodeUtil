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

namespace UTF8Check
{
    public partial class Form1 : Form
    {
        private static List<string> ExceptList = new List<string>();

        private static bool isChangeToUTF8 = false;

        private List<string> ResultList = new List<string>();  

        public Form1()
        {
            InitializeComponent();
        }

        static Form1()
        {
            System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
            string strExcept = Convert.ToString(appReader.GetValue("ExceptList", typeof(string)));
            string strIsChange = Convert.ToString(appReader.GetValue("isChangeToUTF8", typeof(string)));

            if (!string.IsNullOrEmpty(strExcept))
            {
                var tempExcept = strExcept.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var temp in tempExcept)
                {
                    ExceptList.Add(temp.Trim());
                }

            }
            if (!string.IsNullOrEmpty(strIsChange) && strIsChange.ToUpper() == "TRUE")
            {
                isChangeToUTF8 = true;
            }
        }  

        private void button1_Click(object sender, EventArgs e)
        {
            ResultList = new List<string>();
            //System.Windows.Forms.FolderBrowserDialog dialog =new System.Windows.Forms.FolderBrowserDialog();
            //if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK )
            //{
            //    return;
            //}
            string myPath = @"E:/Git/jlicht/project/module/fishing";
            if (string.IsNullOrEmpty(myPath))  
            {  
                MessageBox.Show("PATH?");  
                return;  
            }  
  
            List<string> pathList = new List<string>();  
            try  
            {  
                //查找目录下的所有文件  
                GetDirectory(myPath, pathList);  
                foreach (var path in pathList)  
                {  
                    //获取文件编码格式  
                    var type = EncodingType.GetType(path, ResultList);  
                    if (type != Encoding.UTF8)  
                    {  
                        ResultList.Add(string.Format("{0},原格式{1}", path, type.EncodingName));  
                        if (isChangeToUTF8)  
                        {  
                            //转换编码格式至UTF8  
                            //ChangeEncoding(path, type);
                            System.Console.WriteLine(path);
                        }
                    }  
                }  
            }  
            catch (Exception exception)  
            {  
                MessageBox.Show(exception.ToString());  
                return;  
            }  
  
            if (ResultList.Any())  
            {  
                ResultList.Insert(0,myPath);  
                ResultList.Insert(1,"共计文件{pathList.Count}个");  
                ResultList.Insert(2,"非UTF-8文件共{ResultList.Count-2}个");  
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, ResultList.ToArray()));  
                //using (FileStream fsWrite = new FileStream(@"D:\\result{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", FileMode.Append))
                //{  
                //    fsWrite.Write(myByte, 0, myByte.Length);  
                //};  
                //MessageBox.Show("D:\\result.txt");  
            }  
            else  
            {  
                ResultList.Insert(0, myPath);  
                ResultList.Insert(1, "共计文件{pathList.Count}个");  
                ResultList.Insert(2, "非UTF-8文件共{ResultList.Count - 2}个");  
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, ResultList.ToArray()));  
                //using (FileStream fsWrite = new FileStream(@"D:\\success{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt", FileMode.Append))
                //{  
                //    fsWrite.Write(myByte, 0, myByte.Length);  
                //};  
                //MessageBox.Show("finish!");  
            }  
        }

        private void GetDirectory(string path, List<string> list)  
        {  
            DirectoryInfo folder = new DirectoryInfo(path);  
            GetFile(path,list);  
            foreach (var directory in folder.GetDirectories())  
            {  
                if (!ExceptList.Contains(directory.Name))  
                {
                    string childPath = string.Format("{0}\\{1}", path, directory.Name);
                    GetDirectory(childPath, list);  
                }  
                  
            }  
        }  
        private void GetFile(string path, List<string> list)  
        {  
            DirectoryInfo folder = new DirectoryInfo(path);  
            foreach (FileInfo file in folder.GetFiles())  
            {  
                if (!ExceptList.Any(e => file.Name.EndsWith(e)))   
                {  
                    list.Add(string.Format("{0}\\{1}",path, file.Name));
                }  
                  
            }  
        }  
  
  
        private void ChangeEncoding(string filename , System.Text.Encoding encoding)  
        {  
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);  
            byte[] flieByte = new byte[fs.Length];  
            fs.Read(flieByte, 0, flieByte.Length);  
            fs.Close();  
  
            StreamWriter docWriter;  
            System.Text.Encoding ec = System.Text.Encoding.GetEncoding("UTF-8");  
            docWriter = new StreamWriter(filename, false, ec);  
            docWriter.Write(encoding.GetString(flieByte));  
            docWriter.Close();  
        } 
    }
}
