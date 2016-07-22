using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsEditor.Model
{
    public class CountryModel
    {
        public CountryModel(string shortName, string name)
        {
            this.ShortName = shortName;
            this.Name = name;
            AvailableEquipmentsList = new List<EquipmentBasicModel>();
            ForeignLeaseEquipmentsList = new List<EquipmentBasicModel>();
        }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public List<EquipmentBasicModel> AvailableEquipmentsList { get; set; }
        public List<EquipmentBasicModel> ForeignLeaseEquipmentsList { get; set; }
    }
}
