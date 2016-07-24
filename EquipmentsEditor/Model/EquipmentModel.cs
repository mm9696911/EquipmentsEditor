using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsEditor.Model
{
    public class EquipmentModel : EquipmentBasicModel
    {
        public EquipmentModel(EquipmentBasicModel basic,bool isForeignLease)
        {
            this.Id = basic.Id;
            this.EquipmentName = basic.EquipmentName;
            this.TypeStr = basic.TypeStr;
            this.Creator = basic.Creator;
            this.Obsolete = basic.Obsolete;
            this.IsForeignLease = isForeignLease;
            this.IsDeleted = false;
        }

        public bool IsForeignLease { get; set; }

        public double Quantity { get; set; }

        public string CreatorName { get; set; }

        public bool IsDeleted { get; set; }

    }
}
