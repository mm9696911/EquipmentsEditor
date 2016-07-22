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
            CountriesList = new List<Country>();
            EquipmentsList = new List<Equipment>();
        }
        public List<Country> CountriesList { get; set; }
        public List<Equipment> EquipmentsList { get; set; }
    }
}
