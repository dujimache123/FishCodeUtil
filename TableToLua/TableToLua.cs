using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TableToLua
{
    class TableToLua
    {
        private static TableToLua mInstance;
        private TableToLua()
        {

        }
        public static TableToLua getInstance()
        {
            if (null == mInstance)
            {
                mInstance = new TableToLua();
            }
            return mInstance;
        }

        public string toLua(string luaTableName,string tabFilePath)
        {
            string luaStr = "";
            if (luaTableName.Length <= 0 || tabFilePath.Length <= 0)
            {
                return luaStr;
            }
            
            //原文件是否存在
            if (false == File.Exists(tabFilePath))
            {
                return luaStr;
            }

            FileStream fs = new FileStream(tabFilePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8"));
            string fileTxt = sr.ReadToEnd();
            string[] lines = fileTxt.Split('\n');
            if (lines.Length <= 3)
            {
                return luaStr;
            }
            string[] keyStr = lines[1].TrimEnd().Split('\t');
            string[] typeStr = lines[2].TrimEnd().Split('\t');


            luaStr = "local " + luaTableName + " = {\n\trecords = {\n";

            //元素
            for (int i = 3; i < lines.Length; i ++ )
            {
                if (lines[i].Length == 0 || lines[i].StartsWith("#"))
                {
                    continue;
                }
                string recordStr = "\t\t{";
                string[] oneline = lines[i].TrimEnd().Split('\t');
                for (int j = 0; j < oneline.Length; j ++ )
                {
                    if (oneline[j] == "")
                    {
                        continue;
                    }
                    if ("int" == typeStr[j] || "float" == typeStr[j])
                    {
                        recordStr = recordStr + keyStr[j] + " = " + oneline[j] + ", ";
                    }
                    else if ("string" == typeStr[j])
                    {
                        recordStr = recordStr + keyStr[j] + " = " + "\"" + oneline[j] + "\"" + ", ";
                    }
                    else if ("ccp" == typeStr[j])
                    {
                        string[] values = oneline[j].Split(',');
                        recordStr = recordStr + keyStr[j] + " = {x = " + values[0] + ", y = " + values[1] + "}, ";
                    }
                    else if (typeStr[j].Contains("array"))
                    {
                        recordStr = recordStr + keyStr[j] + " = {";
                        string[] arrayInfo = typeStr[j].Split('+');

                        if ("int" == arrayInfo[1] || "float" == arrayInfo[1])
                        {
                            string[] values = oneline[j].Split(arrayInfo[2][0]);
                            foreach (string value in values)
                            {
                                recordStr = recordStr + value + ", ";
                            }
                        }
                        else if ("string" == arrayInfo[1])
                        {
                            string[] values = oneline[j].Split(arrayInfo[2][0]);
                            foreach (string value in values)
                            {
                                recordStr = recordStr + "\"" + value + "\"" + ",";
                            }
                        }
                        recordStr = recordStr.TrimEnd(',', ' ');
                        recordStr = recordStr +  "}" + ",";
                    }
                }
                recordStr = recordStr.TrimEnd(',', ' ');
                recordStr += "},\n";
                luaStr += recordStr;
            }

            luaStr = luaStr.TrimEnd(',');
            luaStr += "\t}\n";
            luaStr += "}\n";
            luaStr += "return " + luaTableName;

            fs.Close();
            return luaStr;
        }
    }
}
