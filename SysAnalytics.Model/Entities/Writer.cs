using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model.Entities
{
    public class Writer : User
    {
        //public virtual PaymentMethod? PaymentMethod { get; set; }
        public virtual int WriterId { get; set; }
        public virtual string History { get; set; }
        public virtual WorkStatus WorkStatus { get; set; }
        public virtual string HireHistory { get; set; }
        public virtual Gender Gender { get; set; } 
        public virtual MaritalStatus MaritalStatus { get; set; }
        //public virtual AvailableTime TimeAvailableAtHome { get; set; }
        //public virtual AvailableTime TimeAvailableAtWork { get; set; }
        //public virtual AvailableTime TimeAvailableAtCell { get; set; }
        public virtual int TimeAvailableAtHomeFromHour { get; set; }
        public virtual int TimeAvailableAtHomeToHour { get; set; }
        public virtual int TimeAvailableAtWorkFromHour { get; set; }
        public virtual int TimeAvailableAtWorkToHour { get; set; }
        public virtual int TimeAvailableAtCellFromHour { get; set; }
        public virtual int TimeAvailableAtCellToHour { get; set; }
        public virtual WorkHoursScale WorkHoursGrade { get; set; }
        public virtual WriterOcupation Iam { get; set; }
        public virtual SalaryPeriodicity SalaryPeriodicity { get; set; }
        public virtual int AssigmentsDone { get; set; }
        //public virtual Attachment Resume { get; set; }
        public virtual Attachment Sample { get; set; }
        public virtual bool WriteOther { get; set; }
        public virtual string Enjoy { get; set; }
        public virtual string Dislike { get; set; }
        public virtual int WriterPaymentMethodId { get; set; }
        //public virtual WriterPaymentMethod WriterPaymentMethod { get; set; }
        public virtual PaymentMethod2 PaymentMethod1 { get; set; }
        public virtual PaymentMethod2 PaymentMethod2 { get; set; }
        public virtual string Employers { get; set; }
        public virtual string HireReason { get; set; }
        //public virtual string PlagiarismComment { get; set; }
        //public virtual string DeadlineTechnique { get; set; }
        //public virtual string Interests { get; set; }
        public virtual string Occupation { get; set; }
        public virtual string Education { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual decimal StatusBalance { get; set; }
        public virtual decimal StatusBalanceTmp { get; set; }
        public virtual int Status { get; set; }

        public virtual DateTimeOffset? ActivationDate { get; set; }
        public virtual DateTimeOffset? DeactivationDate { get; set; }
        public virtual DateTimeOffset? Status1stSetDate { get; set; }

        public virtual bool IsTracked { get; set; }
        public virtual bool CanSeePartners { get; set; }

        public override string ToString()
        {
            return this.UserId.ToString();
        }
    }
}
