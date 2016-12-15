using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    /// <summary>
    /// represents lifetime discount relatively to ordered pages count
    /// </summary>
    [Serializable]
    public class LifeTimeDiscount
    {
        [System.Xml.Serialization.XmlAttribute]
        public virtual int LifeTimeDiscountId { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public virtual int PagesNumber { get; set; }
        [System.Xml.Serialization.XmlAttribute]
        public virtual int Percent { get; set; }
    }

    public struct LifetimeDiscountInfo
    {
        public int CurrentLifetimeDiscountPercent;
        public int NextLifetimeDiscountPercent;
        public int NumUnitsToNextDiscount;
    }
}
