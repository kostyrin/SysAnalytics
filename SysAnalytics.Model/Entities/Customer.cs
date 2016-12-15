using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model.Entities
{
    public class Customer : User
    {
        public virtual int CustomerId { get; set; }
        //public virtual int UserId { get; set; }
        public virtual string Employed { get; set; }
        public virtual string MainMajors { get; set; }
        public virtual string StudyLevel { get; set; }
        public virtual double CurrentGPA { get; set; }
        public virtual double DesiredGPA { get; set; }
        public virtual bool EnglishNative { get; set; }
        public virtual int EnglishStudyYears { get; set; }
        public virtual string Difficulties { get; set; }
        public virtual string CommonMistakes { get; set; }
        public virtual CustomerQualityExpectations CustomerQualityExpectations { get; set; }
        public virtual string AdditionalComments { get; set; }
        public virtual bool IsWroteReview { get; set; }
        public virtual bool SendTips { get; set; }
        public virtual bool SendSeasonal { get; set; }
        public virtual int NumCompletedOrders { get; set; }
        public virtual int NumCompletedPages { get; set; }
        public virtual decimal Bonus { get; set; }
        public virtual Customer Tempter { get; set; }
        //public virtual Attachment Portfolio { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        public virtual bool Emergency { get; set; }
        //public virtual int LastSendedFileId { get; set; }
        public virtual bool? IsSubscriber { get; set; }
        public virtual bool DeniedCustomer { get; set; }
        //public virtual EnglishDialect EnglishDialect { get; set; }
        public virtual bool IsPartner { get; set; }
        public virtual decimal BalancePages { get; set; }
        //public virtual string Address { get; set; }
        public virtual string City { get; set; }
        //public virtual string ZIPCode { get; set; }
        public virtual Degree Degree { get; set; }
        public virtual string CountryStudy { get; set; }
        public virtual EnglishLevel EnglishLevel { get; set; }
        public virtual int LifeTimeDiscount { get; set; }

        public override string ToString()
        {
            return this.CustomerId.ToString();
        }
    }
}
