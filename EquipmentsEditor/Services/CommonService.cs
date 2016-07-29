using Process;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EquipmentsEditor.Services
{
    public static class CommonService
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>   
        /// 释放内存  
        /// </summary>   
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }


        /// <summary>
        /// 打开指定路径文件，返回内容字符串
        /// </summary>
        /// <param name="path">指定路径文件</param>
        /// <returns></returns>
        public static string GetFileStream(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            FileInfo fileInfo = new FileInfo(path);
            //创建文件流，path为文本文件路径  
            StreamReader file = new StreamReader(path, Encoding.UTF8);
            string fileText = file.ReadToEnd();
            file.Dispose();
            return fileText;
        }



        public static List<string> SpiltText(string str, string matchStr, out string anotherStr, bool isEquipment = false)
        {
            int stratIndex;
            int leftBraceStratIndex = 0;
            int leftBraceNum = 0;
            int rightBraceNum = 0;
            int rightBraceStratIndex = 0;
            List<string> returnList = new List<string>();
            List<int> startIndexList = new List<int>();
            List<int> endIndexList = new List<int>();
            anotherStr = str;
            string equipmentRank = string.Empty;
            string equipmentStr = string.Empty;

            if (str.ToLower().IndexOf(matchStr.ToLower()) > -1)
            {
                stratIndex = str.ToLower().IndexOf(matchStr.ToLower());
                if (stratIndex >= 0)
                {
                    leftBraceStratIndex = str.IndexOf("{", stratIndex);
                    leftBraceNum = 1;

                    char[] textCharArray = str.ToCharArray(0, str.Length);

                    for (int i = leftBraceStratIndex + 1; i < textCharArray.Length; i++)
                    {
                        bool isOk = false;
                        if (textCharArray[i] == '{' && i >= stratIndex)
                        {
                            leftBraceNum += 1;
                            isOk = true;
                        }
                        else if (textCharArray[i] == '}' && i >= stratIndex)
                        {
                            rightBraceNum += 1;
                            isOk = true;
                        }

                        if (leftBraceNum == rightBraceNum && isOk)
                        {
                            rightBraceStratIndex = i;
                            //returnList.Add(str.Substring(leftBraceStratIndex+1, (rightBraceStratIndex - leftBraceStratIndex-1)));
                            if (isEquipment)
                            {
                                //string aaa = str.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(" ", "");
                                equipmentStr = str.Remove(rightBraceStratIndex).Remove(0, leftBraceStratIndex - matchStr.Length - 2);
                                Regex regex = new Regex("[0-9][0-9,.]*");
                                MatchCollection matchCollections = regex.Matches(equipmentStr);
                                equipmentRank = (matchCollections.Count != 0 ? matchCollections[0].Value : "");
                                returnList.Add(equipmentRank + "^" + str.Remove(rightBraceStratIndex).Remove(0, leftBraceStratIndex + 1));
                            }
                            else
                            {
                                returnList.Add(str.Remove(rightBraceStratIndex).Remove(0, leftBraceStratIndex + 1));
                            }
                            startIndexList.Add(stratIndex);
                            endIndexList.Add(rightBraceStratIndex + 1);

                            stratIndex = str.ToLower().IndexOf(matchStr.ToLower(), rightBraceStratIndex);
                            if (stratIndex > -1)
                            {
                                leftBraceStratIndex = str.IndexOf("{", stratIndex);
                                isOk = false;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    for (int startNum = startIndexList.Count - 1; startNum >= 0; startNum--)
                    {
                        anotherStr = anotherStr.Remove(startIndexList[startNum], endIndexList[startNum] - startIndexList[startNum]);
                    }
                }
            }
            return returnList;
        }

        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        #region 字符串搜索
        /// <summary>
        /// 查找算法
        /// </summary>
        /// <param name="source">要搜索的字符串</param>
        /// <param name="subString">子串</param>
        /// <returns>子串在source字符串中的开始位置</returns>
        public static int KmpIndexOf(string s, string t)
        {
            int i = 0, j = 0, v;
            int[] nextVal = GetNextVal(t);

            while (i < s.Length && j < t.Length)
            {
                if (j == -1 || s[i] == t[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = nextVal[j];
                }
            }

            if (j >= t.Length)
                v = i - t.Length;
            else
                v = -1;

            return v;
        }

        private static int[] GetNextVal(string t)
        {
            int j = 0, k = -1;
            int[] nextVal = new int[t.Length];

            nextVal[0] = -1;

            while (j < t.Length - 1)
            {
                if (k == -1 || t[j] == t[k])
                {
                    j++;
                    k++;
                    if (t[j] != t[k])
                    {
                        nextVal[j] = k;
                    }
                    else
                    {
                        nextVal[j] = nextVal[k];
                    }
                }
                else
                {
                    k = nextVal[k];
                }
            }

            return nextVal;
        }

        #endregion
    }
}
