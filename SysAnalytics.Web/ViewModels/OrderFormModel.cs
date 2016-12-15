using System;
using SysAnalytics.Model;
using SysAnalytics.Model.Entities;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.Helpers;

namespace SysAnalytics.Web.ViewModels
{
    public class OrderFormModel
    {
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int OrderId { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int CustomerId { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int WriterId { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int PrefferedWriter { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int StatusRaw { get; set; }
        [JQGridColumn]
        public virtual string Major2_Name { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date, IsHidden = true)]
        public virtual DateTimeOffset? AssignDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset DeletedDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? CompletionDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset DeadlineDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? WriterDeadlineDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset CreateDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date, IsHidden = true)]
        public virtual DateTimeOffset? AvailableDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date, IsHidden = true)]
        public virtual DateTimeOffset? LastRevisionDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date, IsHidden = true)]
        public virtual DateTimeOffset? PaymentDate { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int DeadlineHours { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string StudyLevel { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string AssignmentType { get; set; }
        [JQGridColumn(IsSortable = true, IsHidden = true)]
        public virtual string DeadlineHistory { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Revised { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int RevCount { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual PaymentStatus PaymentStatus { get; set; }
        //[JQGridColumn(IsSortable = true)]
        //public virtual string PaymentTrackingNumber { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string TrackingNumber { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual PaymentChargeback PaymentChargeback { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal PagePrice { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal WriterPagePrice { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int NumPages { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string DiscountCode { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal WriterBonus { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int NumSources { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string Subject { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string Topic { get; set; }
        //[JQGridColumn(IsSortable = true)]
        //public virtual string Books { get; set; }
        [JQGridColumn(IsSortable = true, Width = 70, Align = JSAlign.Left, IsHidden = true)]
        public virtual string Description { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual CitationStyle CitationStyle { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual EnglishDialect English { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool NeedPlagiarismReport { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual LineSpacing Spacing { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool NativeLanguage { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool TitlePage { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Outline { get; set; }
        //[JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        //public virtual bool Bibliography { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool DeadlineFixed { get; set; }
        //[JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        //public virtual bool AdditionalMaterials { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal TotalCost { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal TotalWithDiscount { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal DiscountTotal { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string Currency { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool IsSurveyFilled { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool NotifiedLate { get; set; }
        //[JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        //public virtual bool ForcedlySkipPlagreport { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual Risk? Risk { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool WasWPPRaisedDueHighRisk { get; set; }
        //[JQGridColumn(Formatter = JSFormatters.Select)]
        //public virtual LowPriceForHighRiskOrderWarnStatus LowPriceForHighRiskOrderWarnStatus { get; set; }
        [JQGridColumn(IsSortable = true)]
        public virtual string OtherCitation { get; set; }
        //public virtual PartnerClient PartnerClient { get; set; }
        //[JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        //public virtual bool CanSupportSeePartnerOrder { get; set; }
        //[JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        //public virtual bool CanPartnerSeeOrder { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int NumberOfSlides { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool WasOpenForEditBefore { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Abstract { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int AssignedBy { get; set; }
        //public float NumberOfPagesTotal { get { return NumPages + (float)0.5 * NumberOfSlides; } }
        //[JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        //public virtual bool Archived { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool CanExtendDeadline { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual decimal LifetimeDiscount { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual decimal LifetimeSum { get; set; }
        //[JQGridColumn(IsSortable = true, IsHidden = true)]
        //public virtual string AnaliticScenario { get; set; }


        #region Customer

        //public virtual CustomerFormModel Customer { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_Employed { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_MainMajors { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_StudyLevel { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual double Customer_CurrentGPA { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual double Customer_DesiredGPA { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_EnglishNative { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Customer_EnglishStudyYears { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_Difficulties { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_CommonMistakes { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select)]
        public virtual CustomerQualityExpectations Customer_CustomerQualityExpectations { get; set; }
        [JQGridColumn(Entity = "Customer", IsHidden = true)]
        public virtual string Customer_AdditionalComments { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_IsWroteReview { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_SendTips { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_SendSeasonal { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Customer_NumCompletedOrders { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Customer_NumCompletedPages { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal Customer_Bonus { get; set; }
        //public virtual Customer Tempter { get; set; }
        //public virtual Attachment Portfolio { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_Emergency { get; set; }
        //public virtual int LastSendedFileId { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool? Customer_IsSubscriber { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_DeniedCustomer { get; set; }
        //public virtual EnglishDialect EnglishDialect { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_IsPartner { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal Customer_BalancePages { get; set; }
        //public virtual string Address { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_City { get; set; }
        //public virtual string ZIPCode { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual Degree Customer_Degree { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_CountryStudy { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select, Width = 50, Align = JSAlign.Center)]
        public virtual EnglishLevel Customer_EnglishLevel { get; set; }

        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Customer_RegDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Customer_LastLogin { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_IsActive { get; set; }
        [JQGridColumn(Entity = "Customer")]
        public virtual string Customer_Country { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Customer_TimeZone { get; set; }
        //[JQGridColumn(Entity = "Customer")]
        //public virtual string Customer_PaymentDetails { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_DisableNotifications { get; set; }
        [JQGridColumn(Entity = "Customer", IsSortable = true)]
        public virtual string Customer_Site_Name { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select)]
        public virtual FindUs Customer_FindUs { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Select)]
        public virtual UserType Customer_UserType { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_IsProfileEditing { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Customer_BirthDate { get; set; }
        [JQGridColumn(Entity = "Customer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Customer_IsFrozen { get; set; }

        #endregion Customer

        #region Writer

        [JQGridColumn(IsHidden = true)]
        public virtual string Writer_History { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual WorkStatus Writer_WorkStatus { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string Writer_HireHistory { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual Gender Writer_Gender { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual MaritalStatus Writer_MaritalStatus { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeAvailableAtHome_FromHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeAvailableAtHome_ToHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeAvailableAtWork_FromHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeAvailableAtWork_ToHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeAvailableAtCell_FromHour { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeAvailableAtCell_ToHour { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual WorkHoursScale Writer_WorkHoursGrade { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual WriterOcupation Writer_Iam { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual SalaryPeriodicity Writer_SalaryPeriodicity { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_AssigmentsDone { get; set; }
        //public virtual Attachment Resume { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Writer_Sample { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_WriteOther { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Writer_Enjoy { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Writer_Dislike { get; set; }
        //[JQGridColumn(IsHidden = false)]
        //public virtual string Writer_WriterPaymentMethod { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual int Writer_WriterPaymentMethodId { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual PaymentMethod2 Writer_PaymentMethod1 { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Select)]
        public virtual PaymentMethod2 Writer_PaymentMethod2 { get; set; }
        //[JQGridColumn(IsHidden = false)]
        //public virtual string Writer_Employers { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string Writer_HireReason { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Writer_Occupation { get; set; }
        [JQGridColumn(IsHidden = true)]
        public virtual string Writer_Education { get; set; }
        //[JQGridColumn(IsHidden = false)]
        //public virtual string Writer_Address { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Writer_City { get; set; }
        [JQGridColumn(IsHidden = false)]
        public virtual string Writer_State { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal Writer_StatusBalance { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Number)]
        public virtual decimal Writer_StatusBalanceTmp { get; set; }
        [JQGridColumn(IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_Status { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? Writer_ActivationDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? Writer_DeactivationDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset? Writer_Status1stSetDate { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_IsTracked { get; set; }
        [JQGridColumn(Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_CanSeePartners { get; set; }

        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Writer_RegDate { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Writer_LastLogin { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_IsActive { get; set; }
        [JQGridColumn(Entity = "Writer")]
        public virtual string Writer_Country { get; set; }
        [JQGridColumn(Entity = "Writer", IsSortable = true, Width = 50, Formatter = JSFormatters.Integer)]
        public virtual int Writer_TimeZone { get; set; }
        //[JQGridColumn(Entity = "Writer", IsHidden = true)]
        //public virtual string Writer_PaymentDetails { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_DisableNotifications { get; set; }
        [JQGridColumn(Entity = "Writer", IsSortable = true)]
        public virtual string Writer_Site_Name { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Select)]
        public virtual FindUs Writer_FindUs { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Select)]
        public virtual UserType Writer_UserType { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_IsProfileEditing { get; set; }
        [JQGridColumn(Width = 70, Formatter = JSFormatters.Date)]
        public virtual DateTimeOffset Writer_BirthDate { get; set; }
        [JQGridColumn(Entity = "Writer", Formatter = JSFormatters.Checkbox, Width = 50, Align = JSAlign.Center)]
        public virtual bool Writer_IsFrozen { get; set; }

        #endregion Writer
    }
}
