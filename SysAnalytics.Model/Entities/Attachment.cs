using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Entities
{
    public class Attachment
    {
        public virtual int AttachmentId { get; set; }
        public virtual string OriginalFileName { get; set; }
        //public virtual Message Message { get; set; }
        //public virtual ICollection<PlagiarismReport> PlagReports { get; set; }
        public virtual bool IsHidden { get; set; }

        public override string ToString()
        {
            return this.AttachmentId.ToString();
        }
    }
}
