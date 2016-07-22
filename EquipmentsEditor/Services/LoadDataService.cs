using EquipmentsEditor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EquipmentsEditor.Services
{
    public class LoadDataService
    {
        public string equipmentNameStr = string.Empty;
        public string fileStr = string.Empty;

        public LoadDataService()
        {
            string localisationFilePath = System.IO.Directory.GetCurrentDirectory() + "\\localisation\\equipment.yml";
            equipmentNameStr = GetFileStream(localisationFilePath);

        }

        public void LoadData(DataModel dataModel, string filePath)
        {
            fileStr = GetFileStream(filePath);

            #region 国家基本信息

            #endregion

            #region 装备基本数据
            string equipmentsStr_Org = fileStr.Substring(fileStr.IndexOf("equipments="), fileStr.IndexOf("division_templates") - fileStr.IndexOf("equipments="));

            string equipmentsAnotherStr = "";
            //string equipmentsReplaceStr = equipmentsStr_Org.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(" ", "");
            string equipmentsReplaceStr = equipmentsStr_Org;
            List<string> equipmentsTextList = SpiltText(equipmentsReplaceStr, "equipments=", out equipmentsAnotherStr);
            if (equipmentsTextList.Count > 0)
            {
                equipmentsAnotherStr = equipmentsTextList[0];

                var enums = Enum.GetNames(typeof(EquipmentTypeEnum));
                foreach (var enumName in enums)
                {
                    List<string> list = SpiltText(equipmentsAnotherStr, new StringBuilder().AppendFormat("{0}_", enumName).ToString(), out equipmentsAnotherStr, true);

                    foreach (string str in list)
                    {
                        EquipmentBasicModel equipment = new EquipmentBasicModel();

                        equipment.Id = GetAttributeTypeOfNumber(str, "id");
                        equipment.Rank = str.Split('^')[0];
                        equipment.TypeStr = enumName + "_" + equipment.Rank;
                        equipment.Max_version = GetAttributeTypeOfNumber(str, "max_version");
                        equipment.Version = GetAttributeTypeOfNumber(str, "version");
                        equipment.Parent = GetAttributeTypeOfNumber(str, "parent");
                        equipment.Obsolete = GetAttributeTypeOfString(str, "obsolete");
                        if (!string.IsNullOrEmpty(equipment.Obsolete))
                        {
                            equipment.Obsolete = equipment.Obsolete.Substring(0, 3);
                        }
                        equipment.Creator = GetAttributeTypeOfString(str, "creator");
                        equipment.Name = GetAttributeTypeOfString(str, "name");
                        if (string.IsNullOrEmpty(equipment.Name))
                        {
                            equipment.Name = GetEquipmentName(enumName, equipment.Creator, equipment.Rank);
                        }
                        dataModel.EquipmentsBasicList.Add(equipment);
                    }
                }
            }
            #endregion

            #region 具体国家信息
            string countriesStr_Org = fileStr.Substring(fileStr.IndexOf("\r\ncountries="), fileStr.IndexOf("\r\nfaction") - fileStr.IndexOf("\r\ncountries="));
            string countriesAnotherStr = "";
            string countriesReplaceStr = countriesStr_Org;

            List<string> countriesTextList = SpiltText(countriesReplaceStr, "countries=", out countriesAnotherStr);
            if (countriesTextList.Count > 0)
            {
                foreach (CountryModel country in dataModel.CountriesList)
                {
                    countriesAnotherStr = countriesTextList[0];

                    List<string> list = SpiltText(countriesAnotherStr, new StringBuilder().AppendFormat("\r\n\t{0}=", country.ShortName.ToLower()).ToString(), out countriesAnotherStr);

                    foreach (string str in list)
                    {
                        string availableAnotherStr = str;
                        List<string> list1 = SpiltText(availableAnotherStr, "available_equipments=", out availableAnotherStr);
                        if (list1.Count > 0)
                        {
                            string availableEquipmentsAnotherStr = list1[0];
                            List<string> availableEquipmentsTextList = SpiltText(availableEquipmentsAnotherStr, "equipment=", out availableEquipmentsAnotherStr);

                            foreach (string availableEquipmentStr in availableEquipmentsTextList)
                            {
                                string availableEquipmentId = GetAttributeTypeOfNumber(availableEquipmentStr, "id");
                                EquipmentBasicModel aaa = dataModel.EquipmentsBasicList.Find(e => e.Id == availableEquipmentId);
                                if (aaa != null)
                                    country.AvailableEquipmentsList.Add(aaa);
                            }
                        }
                        string foreignAnotherStr = str;
                        List<string> list2 = SpiltText(foreignAnotherStr, "foreign_lease_equipments", out foreignAnotherStr);
                        if (list2.Count > 0)
                        {
                            string foreignEquipmentsAnotherStr = list2[0];
                            List<string> foreignEquipmentsTextList = SpiltText(foreignEquipmentsAnotherStr, "equipment=", out foreignEquipmentsAnotherStr);

                            foreach (string foreignEquipmentStr in foreignEquipmentsTextList)
                            {
                                string foreignEquipmentId = GetAttributeTypeOfNumber(foreignEquipmentStr, "id");
                                EquipmentBasicModel bbb = dataModel.EquipmentsBasicList.Find(e => e.Id == foreignEquipmentId);
                                if (bbb != null)
                                    country.ForeignLeaseEquipmentsList.Add(bbb);
                                //country.ForeignLeaseEquipmentsList.Add(dataModel.EquipmentsBasicList.Find(e => e.Id == foreignEquipmentId));
                            }
                        }
                    }

                }
            }
            #endregion

            CommonService.ClearMemory();
            //return returnModel;
        }

        private string GetEquipmentName(string type, string creator, string rank)
        {
            string returnStr = "";
            //string matchStr = " " + creator.ToLower() + "_" + type.ToLower() + "_" + rank;
            string matchStr = new StringBuilder().AppendFormat(" {0}_{1}_{2}", creator.ToLower(), type.ToLower(), rank).ToString();
            //string matchStr = new StringBuilder(" ").Append(creator.ToLower()).Append("_").Append(type.ToLower()).Append("_").Append(rank).ToString();
            if (equipmentNameStr.ToLower().IndexOf(matchStr) == -1)
            {
                //matchStr = new StringBuilder(" ").Append(type.ToLower()).Append("_").Append(rank).ToString();
                matchStr = new StringBuilder().AppendFormat(" {0}_{1}", type.ToLower(), rank).ToString();
            }

            returnStr = equipmentNameStr.Remove(0, equipmentNameStr.ToLower().IndexOf(matchStr) + matchStr.Length + 4);
            returnStr = returnStr.Split('"')[0];
            return returnStr;
        }

        private string GetAttributeTypeOfString(string str, string attributeName)
        {
            string value;
            int num = str.ToLower().IndexOf(attributeName);
            if (num >= 0)
            {
                string str2 = str.Substring(num);
                string valueStr = str2.Split('=')[1];
                if (valueStr.IndexOf('"') > -1)
                {
                    value = valueStr.Split('"')[1];
                }
                else
                {
                    value = valueStr;
                }
            }
            else
            {
                value = "";
            }
            return value;
        }
        private string GetAttributeTypeOfNumber(string str, string attributeName)
        {
            string value;
            int num = str.ToLower().IndexOf(attributeName);
            if (num >= 0)
            {
                Regex regex = new Regex("[0-9][0-9,.]*");
                MatchCollection matchCollections = regex.Matches(str.Substring(num));
                value = (matchCollections.Count != 0 ? matchCollections[0].Value : "");
            }
            else
            {
                value = "";
            }
            return value;
        }

        /// <summary>
        /// 打开指定路径文件，返回内容字符串
        /// </summary>
        /// <param name="path">指定路径文件</param>
        /// <returns></returns>
        public string GetFileStream(string path)
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

        private List<string> SpiltText(string str, string matchStr, out string anotherStr, bool isEquipment = false)
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

            if (str.ToLower().IndexOf(matchStr) > -1)
            {
                stratIndex = str.ToLower().IndexOf(matchStr);
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

                            stratIndex = str.ToLower().IndexOf(matchStr, rightBraceStratIndex);
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
