using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentsEditor.Model
{
    public class Equipment
    {

        public string Id { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 型号类别代码
        /// </summary>
        public string TypeStr { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UpgradesStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Max_version { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public string Obsolete { get; set; }

    }
}
