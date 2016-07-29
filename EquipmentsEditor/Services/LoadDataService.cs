using EquipmentsEditor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EquipmentsEditor.Services
{
    public class LoadDataService
    {
        public string equipmentNameStr = string.Empty;
        public string countriesNameStr = string.Empty;
        public string fileStr = string.Empty;
        public int needLoadDataSumNumber = 200;
        public int needLoadDataNumber = 3;
        private string _filePath = string.Empty;
        public string filePath
        {
            set
            {
                _filePath = value;
            }
            get {
                return _filePath;
            }
        }
        DataModel _dataModel = new DataModel();
        public DataModel dataModel
        {
            set
            {
                _dataModel = value;
            }
            get
            {
                return _dataModel;
            }
        }


        public LoadDataService()
        {
            string localisationFilePath = System.IO.Directory.GetCurrentDirectory() + "\\localisation\\equipment.yml";
            equipmentNameStr = CommonService.GetFileStream(localisationFilePath);

            string countriesFilePath = System.IO.Directory.GetCurrentDirectory() + "\\localisation\\countries_full.yml";
            countriesNameStr = CommonService.GetFileStream(countriesFilePath);

        }

        public void LoadBacisData( Action<int> percent)
        {
            needLoadDataNumber = 3;
            fileStr = CommonService.GetFileStream(filePath);

            //进度条
            percent(1);
            
            #region 装备基本数据
            string equipmentsStr_Org = fileStr.Substring(fileStr.IndexOf("equipments="), fileStr.IndexOf("division_templates") - fileStr.IndexOf("equipments="));
            
            //进度条
            percent(2);

            string equipmentsAnotherStr = "";
            //string equipmentsReplaceStr = equipmentsStr_Org.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(" ", "");
            string equipmentsReplaceStr = equipmentsStr_Org;
            List<string> equipmentsTextList = CommonService.SpiltText(equipmentsReplaceStr, "equipments=", out equipmentsAnotherStr);
            if (equipmentsTextList.Count > 0)
            {
                equipmentsAnotherStr = equipmentsTextList[0];

                var enums = Enum.GetNames(typeof(EquipmentEnum));
                foreach (var enumName in enums)
                {
                    List<string> list = CommonService.SpiltText(equipmentsAnotherStr, new StringBuilder().AppendFormat("\t{0}_", enumName).ToString(), out equipmentsAnotherStr, true);

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
                        else
                        {
                            equipment.Obsolete = "no";
                        }
                        equipment.Creator = GetAttributeTypeOfString(str, "creator");
                        equipment.EquipmentName = GetAttributeTypeOfString(str, "name");
                        if (string.IsNullOrEmpty(equipment.EquipmentName))
                        {
                            equipment.EquipmentName = GetEquipmentName(enumName, equipment.Creator, equipment.Rank);
                        }
                        dataModel.EquipmentsBasicList.Add(equipment);
                        //进度条
                        percent(needLoadDataNumber / 30);
                        needLoadDataNumber++;
                    }
                }
            }
            #endregion

            #region 国家信息
            string countriesStr_Org = fileStr.Substring(fileStr.IndexOf("\r\ncountries="), fileStr.IndexOf("\r\nfaction") - fileStr.IndexOf("\r\ncountries="));
            string countriesAnotherStr = "";
            string countriesReplaceStr = countriesStr_Org;

            List<string> countriesTextList = CommonService.SpiltText(countriesReplaceStr, "countries=", out countriesAnotherStr);
            if (countriesTextList.Count > 0)
            {
                string str = countriesTextList[0];
                Regex regex = new Regex("[\\r][\\n][\\t][A-Z]{3}|[\\r][\\n][\\t][A-Z]{1}[0-9]{2}", RegexOptions.Singleline);
                MatchCollection matchCollections = regex.Matches(str);

                for (int i = 0; i < matchCollections.Count; i++)
                {
                    int theatresNum = 0;
                    if (i > 0)
                    {
                        int beforeStart = CommonService.KmpIndexOf(str, matchCollections[i - 1].Value);
                        int currentStart = CommonService.KmpIndexOf(str, matchCollections[i].Value);
                        theatresNum = str.IndexOf("theatres", beforeStart, currentStart - beforeStart);
                        if (theatresNum <= 0)
                        {
                            dataModel.CountriesList.RemoveAt(dataModel.CountriesList.Count - 1);
                        }
                    }
                    //if (theatresNum > 0 || i==0)
                    //{

                        string countryShortName = matchCollections[i].Value.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(" ", ""); ;
                        CountryModel country = new CountryModel(countryShortName, GetCountryName(countryShortName));
                        if (string.IsNullOrEmpty(country.Name))
                        {
                            country.Name = country.ShortName;
                        }
                        dataModel.CountriesList.Add(country);
                    //}
                    //进度条
                    percent(needLoadDataNumber / 30 + i * 70 / matchCollections.Count);
                }
            }
            #endregion

            //进度条
            percent(100);
            CommonService.ClearMemory();
        }

        public void LoadDataByCountry(CountryModel model)
        {
            string countriesStr_Org = fileStr.Substring(fileStr.IndexOf("\r\ncountries="), fileStr.IndexOf("\r\nfaction") - fileStr.IndexOf("\r\ncountries="));
            string countriesAnotherStr = "";

            List<string> list = CommonService.SpiltText(countriesStr_Org, new StringBuilder().AppendFormat("\r\n\t{0}=", model.ShortName.ToLower()).ToString(), out countriesAnotherStr);

            foreach (string str in list)
            {
                #region 加载装备信息
                string availableAnotherStr = str;
                List<string> list1 = CommonService.SpiltText(availableAnotherStr, "available_equipments=", out availableAnotherStr);
                if (list1.Count > 0)
                {
                    string availableEquipmentsAnotherStr = list1[0];
                    List<string> availableEquipmentsTextList = CommonService.SpiltText(availableEquipmentsAnotherStr, "equipment=", out availableEquipmentsAnotherStr);

                    foreach (string availableEquipmentStr in availableEquipmentsTextList)
                    {
                        string availableEquipmentId = GetAttributeTypeOfNumber(availableEquipmentStr, "id");
                        EquipmentBasicModel aaa = dataModel.EquipmentsBasicList.Find(e => e.Id == availableEquipmentId);
                        if (aaa != null)
                        {
                            EquipmentModel newModel = new EquipmentModel(aaa, false);
                            newModel.CreatorName = dataModel.CountriesList.Find(e => e.ShortName == newModel.Creator).Name;
                            model.EquipmentsList.Add(newModel);
                        }
                    }
                }
                string foreignAnotherStr = str;
                List<string> list2 = CommonService.SpiltText(foreignAnotherStr, "foreign_lease_equipments", out foreignAnotherStr);
                if (list2.Count > 0)
                {
                    string foreignEquipmentsAnotherStr = list2[0];
                    List<string> foreignEquipmentsTextList = CommonService.SpiltText(foreignEquipmentsAnotherStr, "equipment=", out foreignEquipmentsAnotherStr);

                    foreach (string foreignEquipmentStr in foreignEquipmentsTextList)
                    {
                        string foreignEquipmentId = GetAttributeTypeOfNumber(foreignEquipmentStr, "id");
                        EquipmentBasicModel bbb = dataModel.EquipmentsBasicList.Find(e => e.Id == foreignEquipmentId);
                        if (bbb != null)
                        {
                            EquipmentModel newModel = new EquipmentModel(bbb, true);
                            newModel.CreatorName = dataModel.CountriesList.Find(e => e.ShortName == newModel.Creator).Name;
                            model.EquipmentsList.Add(newModel);
                        }
                    }
                }

                string equipQtiesAnotherStr = str;
                List<string> list3 = CommonService.SpiltText(equipQtiesAnotherStr, "\tequipments", out equipQtiesAnotherStr);
                if (list3.Count > 0)
                {
                    string equipQtyAnotherStr = list3[0];
                    List<string> equipQtyextList = CommonService.SpiltText(equipQtyAnotherStr, "equipment=", out equipQtyAnotherStr);

                    foreach (string equipQtyStr in equipQtyextList)
                    {
                        string equipQtyId = GetAttributeTypeOfNumber(equipQtyStr, "id");
                        EquipmentModel ccc = model.EquipmentsList.Find(e => e.Id == equipQtyId);
                        if (ccc != null)
                        {
                            double quantity = 0;
                            double.TryParse(GetAttributeTypeOfNumber(equipQtyStr, "amount"), out quantity);
                            ccc.Quantity = quantity;
                        }
                    }
                }
                #endregion


            }
        }








        private string GetEquipmentName(string type, string creator, string rank)
        {
            string returnStr = "";
            string matchStr = new StringBuilder().AppendFormat(" {0}_{1}_{2}", creator.ToLower(), type.ToLower(), rank).ToString();
            if (equipmentNameStr.ToLower().IndexOf(matchStr) == -1)
            {
                matchStr = new StringBuilder().AppendFormat(" {0}_{1}", type.ToLower(), rank).ToString();
            }

            returnStr = equipmentNameStr.Remove(0, equipmentNameStr.ToLower().IndexOf(matchStr) + matchStr.Length + 4);
            returnStr = returnStr.Split('"')[0];
            return returnStr;
        }

        public string GetEquipmentName(string type)
        {
            string returnStr = "";
            returnStr = equipmentNameStr.Remove(0, equipmentNameStr.ToLower().IndexOf(type) + type.Length + 4);
            returnStr = returnStr.Split('"')[0];
            return returnStr;
        }

        private string GetCountryName(string shortname)
        {
            string returnStr = "";
            foreach (string countriesName in countriesNameStr.Split(';'))
            {
                string countriesNameRLStr = countriesName.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ").Replace(" ", "");
                if (!string.IsNullOrEmpty(countriesNameRLStr))
                {
                    if (countriesNameRLStr.IndexOf(shortname) > -1)
                    {
                        returnStr = countriesNameRLStr.Split(':')[1];
                        break;
                    }
                }
            }
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
