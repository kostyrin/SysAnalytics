using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NHibernate.Engine;
using NHibernate.Util;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model.Entities
{
    public class Order 
    {
        protected enum ChangedField { Status, Risk, PaymentStatus, WPP, AssignedWriter, WDD }

        public virtual int OrderId { get; set; }
        public virtual int Version { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual int WriterId { get; set; }
        public virtual Writer Writer { get; set; }
        public virtual int PrefferedWriter { get; set; }
        //public virtual OrderStatus Status { get; set; }
        public virtual int StatusRaw { get; set; }
        public virtual Major Major2 { get; set; }
        public virtual string Major { get; set; }
        public virtual DateTimeOffset? AssignDate { get; set; }
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual DateTimeOffset? CompletionDate { get; set; }
        public virtual DateTimeOffset DeadlineDate { get; set; }
        public virtual DateTimeOffset? WriterDeadlineDate { get; set; }
        public virtual DateTimeOffset? AvailableDate { get; set; }
        public virtual DateTimeOffset? LastRevisionDate { get; set; }
        public virtual DateTimeOffset? PaymentDate { get; set; }
        public virtual int DeadlineHours { get; set; }
        public virtual string StudyLevel { get; set; }
        public virtual string AssignmentType { get; set; }
        public virtual string DeadlineHistory { get; set; }
        public virtual bool Revised { get; set; }
        public virtual int RevCount { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual string PaymentTrackingNumber { get; set; }
        public virtual PaymentChargeback PaymentChargeback { get; set; }
        public virtual decimal PagePrice { get; set; }
        public virtual decimal WriterPagePrice { get; set; }
        public virtual int NumPages { get; set; }
        public virtual string DiscountCode { get; set; }
        public virtual decimal WriterBonus { get; set; }
        //public virtual string PriceListId { get; set; }
        public virtual int NumSources { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Topic { get; set; }
        public virtual string Books { get; set; }
        public virtual string Description { get; set; }
        public virtual CitationStyle CitationStyle { get; set; }
        public virtual EnglishDialect English { get; set; }
        public virtual bool NeedPlagiarismReport { get; set; }
        public virtual LineSpacing Spacing { get; set; }
        public virtual bool NativeLanguage { get; set; }
        public virtual bool TitlePage { get; set; }
        public virtual bool Outline { get; set; }
        public virtual bool Bibliography { get; set; }
        public virtual bool DeadlineFixed { get; set; }
        public virtual bool AdditionalMaterials { get; set; }
        public virtual decimal TotalCost { get; set; }
        public virtual decimal TotalWithDiscount { get; set; }
        public virtual decimal DiscountTotal { get; set; }
        public virtual string Currency { get; set; }
        public virtual bool IsSurveyFilled { get; set; }
        public virtual bool NotifiedLate { get; set; }
        public virtual bool ForcedlySkipPlagreport { get; set; }
        public virtual Risk? Risk { get; set; }
        public virtual bool WasWPPRaisedDueHighRisk { get; set; }

        public virtual LowPriceForHighRiskOrderWarnStatus LowPriceForHighRiskOrderWarnStatus { get; set; }

        //public virtual ICollection<ServiceSurvey> Surveys { get; set; }

        #region Calculated Fields

        //public virtual int WordCount
        //{
        //    get
        //    {
        //        if (Spacing == LineSpacing.DoubleSpacing)
        //            return NumPages * 275;
        //        else
        //            return NumPages * 550;
        //    }
        //}

        //public virtual string DeadlineHoursAsString
        //{
        //    get { return DeadlineHours <= 24 ? DeadlineHours + " hours" : DeadlineHours / 24 + " days"; }
        //}

        //public virtual bool IsBigOrder
        //{
        //    get { return DeadlineHours >= 120 || NumPages >= 30; }
        //}

        //public virtual int AutoextensionStep
        //{
        //    get { return 2; }
        //}

        //public virtual bool IsEnoughTimeForAutoExtension
        //{
        //    get
        //    {
        //        return OrderId > 0 && WriterDeadlineDate != null
        //               && ((DeadlineDate - WriterDeadlineDate.Value)
        //                   > new TimeSpan(AutoextensionStep, 0, 0));
        //    }
        //}

        //public virtual void ToggleStartingAvailable()
        //{
        //    ToggleStartingAvailable(false);
        //}

        //public virtual void ToggleStartingAvailable(bool aForcedly)
        //{
        //    if (IsAvailableApplicable)
        //    {
        //        if (Status == OrderStatus.Starting)
        //        {
        //            if (aForcedly)
        //                _StatusRaw = OrderStatus.Available.OrderStatusId;
        //            else
        //                Status = OrderStatus.Available;
        //        }
        //    }
        //    else
        //    {
        //        if (Status == OrderStatus.Available)
        //        {
        //            if (aForcedly)
        //                _StatusRaw = OrderStatus.Starting.OrderStatusId;
        //            else
        //                Status = OrderStatus.Starting;
        //        }
        //    }
        //}

        //public virtual bool IsAvailableApplicable
        //{
        //    get
        //    {
        //        return (AssignedWriter == null || AssignedWriter.UserId <= 0)
        //               && WriterDeadlineDate != null
        //               && (PaymentStatus == PaymentStatus.Paid || PaymentStatus == PaymentStatus.Inquiry)
        //               && WriterPagePrice > 0m
        //               && (Status == OrderStatus.Starting || Status == OrderStatus.Available);
        //    }
        //}

        //public virtual bool IsAvailable
        //{
        //    get
        //    {
        //        return Status == OrderStatus.Available;
        //    }
        //}

        //public virtual decimal PriceRatio
        //{
        //    get
        //    {
        //        if (PagePrice == 0)
        //            throw new DivideByZeroException("Unit price not set");

        //        return WriterPagePrice / PagePrice;
        //    }
        //}

        //public virtual decimal WriterUnitPriceWithBonus
        //{
        //    get { return WriterPagePrice + WriterBonus; }
        //}

        //public virtual int HoursBeforeNextRevision
        //{
        //    get
        //    {
        //        if (Customer != null && Customer.IsPartner)
        //            return CalculateHoursBeforeNextRevisionFromPartners();

        //        if (this.DeadlineHours <= 24)
        //            return 12;

        //        if (this.DeadlineHours <= 48)
        //            return 24;

        //        if (this.DeadlineHours <= 96)
        //            return 36;

        //        if (this.DeadlineHours <= 168)
        //            return 48;

        //        if (this.DeadlineHours <= 336)
        //            return 72;

        //        if (this.DeadlineHours <= 480)
        //            return 96;

        //        if (this.DeadlineHours <= 720)
        //            return 120;

        //        //if (this.DeadlineHours <= 1440)
        //        return 192;

        //    }
        //}

        //public virtual int CalculateHoursBeforeNextRevisionFromPartners()
        //{
        //    if (NumPages >= 2 && NumPages <= 10)
        //        return 12;

        //    if (NumPages >= 11 && NumPages <= 20)
        //        return 24;

        //    if (NumPages >= 21 && NumPages <= 30)
        //        return 36;

        //    if (NumPages >= 31 && NumPages <= 50)
        //        return 48;

        //    if (NumPages >= 51 && NumPages <= 70)
        //        return 72;

        //    if (NumPages >= 71 && NumPages <= 90)
        //        return 96;

        //    if (NumPages >= 91 && NumPages <= 120)
        //        return 120;

        //    if (NumPages >= 121 && NumPages <= 140)
        //        return 144;

        //    if (NumPages >= 141 && NumPages <= 160)
        //        return 168;

        //    if (NumPages >= 161 && NumPages <= 180)
        //        return 192;

        //    if (NumPages >= 181 && NumPages <= 200)
        //        return 216;

        //    // >= 201
        //    return 240;
        //}

        //public virtual DateTimeOffset DeadlineForNextRevision
        //{
        //    get
        //    {
        //        DateTimeOffset newDeadline = DateTimeOffset.UtcNow.AddHours(this.HoursBeforeNextRevision);
        //        if (newDeadline < this.DeadlineDate)
        //        {
        //            newDeadline = this.DeadlineDate;
        //        }
        //        return newDeadline;
        //    }
        //}

        //public virtual decimal WriterTotal
        //{
        //    get { return WriterPagePrice * NumPages; }
        //}

        //public virtual decimal WriterTotalWithBonus
        //{
        //    get { return (WriterPagePrice + WriterBonus) * NumPages; }
        //}

        //public virtual decimal WriterTotalWithBonusSQL { get; set; }

        //public virtual IList<string> GetDescriptionAndRevisions(out string TrueDescription, out bool aIsDescrOverflowed)
        //{
        //    IList<string> Revisions = new List<string>();
        //    var lIsDescrOverflowed = false;

        //    TrueDescription =
        //        !string.IsNullOrEmpty(Description)
        //            ? Regex.Replace(Regex.Replace(
        //                                Description,
        //                                @"\[style=revisiondesc\](.*?)(?:\[\/style\]|$)",
        //                                (match) =>
        //                                {
        //                                    Revisions.Add(match.Groups[1].Value);
        //                                    if (!match.Value.Contains("[/style]"))
        //                                        lIsDescrOverflowed = true;
        //                                    return "";
        //                                },
        //                                RegexOptions.Singleline),
        //                            @"\[hr\]\x0d?\x0a?", "")
        //            : "";

        //    aIsDescrOverflowed = lIsDescrOverflowed;

        //    return Revisions;
        //}

        //public virtual void AutoextendWriterDeadline()
        //{
        //    if (WriterDeadlineDate != null)
        //    {
        //        var lNextAutoextend = WriterDeadlineDate.Value.AddHours(AutoextensionStep);
        //        var lMaxExtension = DeadlineDate.AddHours(-AutoextensionStep);
        //        if (lNextAutoextend > lMaxExtension)
        //            WriterDeadlineDate = lMaxExtension;
        //        else
        //            WriterDeadlineDate = lNextAutoextend;
        //    }
        //}

        #endregion

        public override string ToString()
        {
            return String.Format("Order #{0}", OrderId);
        }

        public virtual string OtherCitation { get; set; }
        //public virtual PartnerClient PartnerClient { get; set; }
        public virtual bool CanSupportSeePartnerOrder { get; set; }
        public virtual bool CanPartnerSeeOrder { get; set; }
        public virtual int NumberOfSlides { get; set; }
        public virtual bool WasOpenForEditBefore { get; set; }
        public virtual bool Abstract { get; set; }
        public virtual User AssignedBy { get; set; }
        //public float NumberOfPagesTotal { get { return NumPages + (float)0.5 * NumberOfSlides; } }
        public virtual DateTimeOffset DeletedDate { get; set; }
        public virtual bool Archived { get; set; }
        public virtual bool CanExtendDeadline { get; set; }
        public virtual string AnaliticScenario { get; set; }

        public virtual IList<AppliedDiscount> AppliedDiscounts { get; set; }
        public virtual IList<OrderPayment> OrderPayments { get; set; }

        public Order()
        {
            AppliedDiscounts = new List<AppliedDiscount>();
            OrderPayments = new List<OrderPayment>();
        }

        public virtual AppliedDiscount AppliedDiscountLifetime
        {
            get
            {
                if (AppliedDiscounts.Any())
                {
                    return AppliedDiscounts.FirstOrDefault(a => a.Type.Equals("lifetime"));
                }
                return null;
            }
        }

        public virtual string TrackingNumber
        {
            get
            {
                if (OrderPayments.Any())
                {
                    return string.Join(", ", OrderPayments.Select(op => op.TrackingNumber));
                }
                return this.PaymentTrackingNumber;
            }
        }

        public virtual decimal LifetimeDiscount
        {
            get { return this.AppliedDiscountLifetime != null ? this.AppliedDiscountLifetime.Percent : 0; }
        }

        public virtual decimal LifetimeSum
        {
            get { return this.AppliedDiscountLifetime != null ? this.TotalCost * this.AppliedDiscountLifetime.Percent / 100 : 0; }
        }
    }
}
