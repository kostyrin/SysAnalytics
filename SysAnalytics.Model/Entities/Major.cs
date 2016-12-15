using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class Major
    {
        public virtual int MajorId { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsSpecialized { get; set; }
        public virtual Major Parent { get; set; }
        public virtual ICollection<Major> Subtopics { get; set; }
        public virtual bool IsCustomer { get; set; }
        public virtual bool IsPartner { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            Major major = obj as Major;
            return major == this;
            //return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            int hash = (MajorId.ToString() + Name + IsSpecialized.ToString()).GetHashCode();
            return hash;
        }

        public static bool operator ==(Major aLeft, Major aRight)
        {
            if (ReferenceEquals(aLeft, aRight))
                return true;

            if (((object)aLeft == null) ^ ((object)aRight == null))
            {
                return false;
            }

            if (aLeft.MajorId != aRight.MajorId
                || aLeft.IsSpecialized != aRight.IsSpecialized
                || aLeft.Name != aRight.Name)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(Major aLeft, Major aRight)
        {
            return !(aLeft == aRight);
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", MajorId, Name ?? "--unknown--");
        }
    }
}
