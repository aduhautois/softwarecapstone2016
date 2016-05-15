using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.GreenThumb.BusinessObjects
{
    /// <summary>
    /// Nicholas King
    /// Data Object for Garden Templates
    /// </summary>
    public class GardenTemplate
    {
        public string TemplateName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public byte[] Image { get; set; }

        public override string ToString()
        {
            return TemplateName;
        }
    }
}
