using EquipmentsEditor.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EquipmentsEditor.Services
{
    public class GenerateServices
    {
        private string originalFilePath = string.Empty;
        DataModel dataModel = new DataModel();
        private CountryModel countryModel = new CountryModel();

        public GenerateServices(string originalFilePath, DataModel dataModel, CountryModel country)
        {
            this.originalFilePath = originalFilePath;
            this.dataModel = dataModel;
            this.countryModel = country;
        }

        public string GenerateCode()
        {
            string result = string.Empty;

            string fileStr = CommonService.GetFileStream(originalFilePath);

            int indexNum = dataModel.CountriesList.FindIndex(e => e.ShortName == countryModel.ShortName);
            CountryModel nextModel = dataModel.CountriesList[indexNum + 1];

            int countriesStrIndex = fileStr.IndexOf("\r\ncountries=");
            int currentCountrieStrIndex = fileStr.IndexOf(new StringBuilder().AppendFormat("\r\n\t{0}=", countryModel.ShortName).ToString()
                                                , countriesStrIndex);
            int nextCountrieStrIndex = fileStr.IndexOf(new StringBuilder().AppendFormat("\r\n\t{0}=", nextModel.ShortName).ToString()
                                                , countriesStrIndex);

            string currentCountryStr = fileStr.Substring(currentCountrieStrIndex, nextCountrieStrIndex - currentCountrieStrIndex);
            string productionAnotherStr = currentCountryStr;
            int productionStrIndex = fileStr.IndexOf("production=", countriesStrIndex);
            int flagStrIndex = fileStr.IndexOf("flags=", productionStrIndex);
            List<string> list1 = CommonService.SpiltText(productionAnotherStr, "production=", out productionAnotherStr);
            StringBuilder sb = new StringBuilder();
            if (list1.Count > 0)
            {
                string availableAnotherStr = list1[0];
                CommonService.SpiltText(availableAnotherStr, "available_equipments=", out availableAnotherStr);
                CommonService.SpiltText(availableAnotherStr, "foreign_lease_equipments=", out availableAnotherStr);
                CommonService.SpiltText(availableAnotherStr, "\tequipments", out availableAnotherStr);

                sb.Append("\r\n\t\tproduction={\r\n\t\t\tavailable_equipments={");
                StringBuilder sb2 = new StringBuilder();
                sb2.Append("\r\n\t\t\tequipments={");
                List<EquipmentModel> allList = countryModel.EquipmentsList.FindAll(e => e.IsDeleted == false);
                foreach (EquipmentModel model in allList.FindAll(e => e.IsForeignLease == false))
                {
                    sb.Append("\r\n\t\t\t\tequipment={");
                    sb.AppendFormat("\r\n\t\t\t\t\tid={0} \r\n\t\t\t\t\ttype=70", model.Id);
                    sb.Append("\r\n\t\t\t\t}");
                    if (model.Quantity > 0)
                    {
                        sb2.Append("\r\n\t\t\t\tequipment={\r\n\t\t\t\t\tid={");
                        sb2.AppendFormat("\r\n\t\t\t\t\t\tid={0} \r\n\t\t\t\t\t\ttype=70 ", model.Id);
                        sb2.Append("\r\n\t\t\t\t\t}");
                        sb2.AppendFormat(" \r\n\t\t\t\t\tamount={0} ", model.Quantity.ToString("f3"));
                        sb2.Append("\r\n\t\t\t\t}");
                    }
                }
                sb.Append("}\r\n\t\t\tforeign_lease_equipments={");
                foreach (EquipmentModel model in allList.FindAll(e => e.IsForeignLease == true))
                {
                    sb.Append("\r\n\t\t\t\tequipment={");
                    sb.AppendFormat("\r\n\t\t\t\t\tid={0} \r\n\t\t\t\t\ttype=70", model.Id);
                    sb.Append("\r\n\t\t\t\t}");

                    if (model.Quantity > 0)
                    {
                        sb2.Append("\r\n\t\t\t\tequipment={\r\n\t\t\t\t\tid={");
                        sb2.AppendFormat("\r\n\t\t\t\t\t\tid={0} \r\n\t\t\t\t\t\ttype=70 ", model.Id);
                        sb2.Append("\r\n\t\t\t\t\t}");
                        sb2.AppendFormat(" \r\n\t\t\t\t\tamount={0} ", model.Quantity.ToString("f3"));
                        sb2.Append("\r\n\t\t\t\t}");
                    }
                }
                sb.Append("}");
                sb2.Append("}");
                sb.Append(availableAnotherStr);
                sb.Append(sb2.ToString());
                sb.Append("}");
            }
            result = sb.ToString();
            result = fileStr.Remove(productionStrIndex, flagStrIndex - productionStrIndex).Insert(productionStrIndex, sb.ToString());
            return result;
        }
        public void text(string value)
        {

            //change();
            string path = System.IO.Directory.GetCurrentDirectory() + "\\TextMessage.txt";
            FileStream f = new FileStream(path, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(f);
            sw.WriteLine(value);
            sw.Flush();
            sw.Close();
            f.Close();
        }
    }
}
