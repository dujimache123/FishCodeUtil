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

namespace GenerateLuaCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool jjFishFlag = this.radioButton1.Checked;
            string fishmsg = "fishmsg";
            string msgController = "ThousandfishMsgController";
            string playSceneController = "ThousandfishPlaySceneController";
            string fishdata = "fishData";
            string msgid = "MessageId.";
            if (jjFishFlag)
            {
                fishmsg = "Fishingmsg";
                msgController = "FishingMsgController";
                playSceneController = "FishingPlaySceneController";
                fishdata = "FishingData";
                msgid = "FishingMsgDef.";
            }

            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = dlg.FileName;
                FileStream fs = new FileStream(fileName,FileMode.Open);
                FileStream saveFs = new FileStream("msgController.txt", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                StreamWriter sw = new StreamWriter(saveFs);

                string[] lines = sr.ReadToEnd().Split('\n');
                List<string> messageList = new List<string>();
                List<string> messageVar = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    messageList.Add(temp[0]);
                    messageVar.Add(temp[1]);
                }
                for (int i = 0; i < messageList.Count; i++)
                {
                    string ifelse = "elseif 0 ~= #" + fishmsg + "." + messageVar[i] + " then\n" +
                                    "   self:handle" + messageList[i] + "(msg," + fishdata + ")\n";
                    sw.Write(ifelse);
                }
                sw.WriteLine("-----------------------------------------------------------------------------------------------");
                for (int i = 0; i < messageList.Count; i++)
                {
                    string ifelse = "elseif msgType == " + msgid + messageList[i] + " then\n" +
                                    "   self:handle" + messageList[i] + "(msg)\n";
                    sw.Write(ifelse);
                }

                sw.WriteLine("-----------------------------------------------------------------------------------------------");
                for (int i = 0; i < messageList.Count; i++)
                {
                    string message = messageList[i];
                    string function = "function " + msgController + ":" + "handle" + message + "(msg," + fishdata + ")\n" +

                            "    JJLog.i(TAG, \"handle" + message + "\")\n" +
                            "    msg[MSG_TYPE] = " + msgid + message + "\n" +
                            "end\n\n";
                    sw.Write(function);
                }

                sw.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < messageList.Count; i++)
                {
                    string message = messageList[i];
                    string function = "function " + playSceneController + ":" + "handle" + message + "(msg)\n" +
                            "end\n\n";
                    sw.Write(function);
                }

                sw.Flush();
                saveFs.Close();

                MessageBox.Show("生成成功！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string text = sr.ReadToEnd();
                string[] lines = text.Split('\n');

                FileStream outFs = new FileStream("result.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(outFs);
                string resultStr = "{";
                foreach (string oneline in lines)
                {
                    string trimLine = oneline.TrimEnd('\r');
                    if (trimLine.Length < 1)
                    {
                        continue;
                    }
                    resultStr += trimLine;
                    resultStr += ", ";
                }
                resultStr = resultStr.TrimEnd(',');
                resultStr += "}";
                sw.Write(resultStr);
                sw.Flush();
                outFs.Close();
            }
        }
    }
}
