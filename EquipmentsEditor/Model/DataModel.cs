using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsEditor.Model
{
    public class DataModel
    {
        public DataModel()
        {
            CountriesList = new List<CountryModel>();
            EquipmentsBasicList = new List<EquipmentBasicModel>();
        }
        public List<CountryModel> CountriesList { get; set; }
        public List<EquipmentBasicModel> EquipmentsBasicList { get; set; }
    }
}
