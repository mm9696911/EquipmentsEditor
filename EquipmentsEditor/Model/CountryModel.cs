using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsEditor.Model
{
    public class CountryModel
    {
        public CountryModel()
        { 
        
        }
        public CountryModel(string shortName, string name)
        {
            this.ShortName = shortName;
            this.Name = name;
            EquipmentsList = new List<EquipmentModel>();
            IsChanged = false;
            IsDeleted = false;
        }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public List<EquipmentModel> EquipmentsList { get; set; }
        public bool IsChanged { get; set; }
        public bool IsDeleted { get; set; }
    }
}
