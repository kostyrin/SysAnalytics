using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysAnalytics.Model.Attributes;

namespace SysAnalytics.Model
{
    public enum GeneralEnum { General }

    public enum CustomerStatisticsSorting
    {
        TitleAsc, TitleDesc, Period1Asc, Period1Desc, Period2Asc, Period2Desc, DifferenceAsc, DifferenceDesc, PercentDiffAsc, PercentDiffDesc
    }

    public enum SelecteBasePeriod
    {
        [Description("Period 1")]
        Period1 = 1,
        [Description("Period 2")]
        Period2 = 2
    }

    public enum CustomerStatisticsFilter
    {
        All, New, Returned
    }

    public enum StatisticsGrouping
    {
        SiteName, Day, Week, Month
    }

    public enum CustomersTotalStatisticsFilter
    {
        [Description("Paid Orders Count")]
        NumberOfPaidOrders,
        [Description("NOT Paid Orders Count")]
        NumberOfNotPaidOrders,
        [Description("Total WithOUT Discount")]
        TotalWithoutDiscounts,
        [Description("Total With Discount")]
        TotalWithDiscounts
    }

    public enum WriterOrderRequestStatus
    {
        [Description("cant_request")]
        CanNotRequest,
        [Description("can_request")]
        CanRequest,
        [Description("can_remove")] // do not change description, it is used in rwc services
        CanDecline,
    }

    public enum CommentCategory
    {
        [CommentCategoryDescription("Other", false)]
        Other = 0,

        [CommentCategoryDescription("Reassign/Drop", false, false)]
        ReasignDrop = 1,
        [CommentCategoryDescription("Writer's Quality", false, false)]
        WriterQuality = 2,
        [CommentCategoryDescription("Writer's Fault", false, false)]
        WriterFault = 3,
        [CommentCategoryDescription("Customer's Fault", false, false)]
        CustomerFault = 4,
        [CommentCategoryDescription("Company's Fault", false, false)]
        CompanyFault = 5,
        [CommentCategoryDescription("Corrective Actions", false, false)]
        CorrectiveActions = 6,
        [CommentCategoryDescription("Positive Feedback", false, true)]
        PositiveFeedback = 7,
        [CommentCategoryDescription("Writer's Reliability", false, false)]
        WriterReliability = 8,

        [CommentCategoryDescription("Customer's Complaint", true, false)]
        CustomerComplaint = 9,
        [CommentCategoryDescription("Writer's Reliability", true, false)]
        Revision = 10,
        [CommentCategoryDescription("Order's Verification", true, false)]
        OrderVerification = 11,

        [CommentCategoryDescription("Auto", true)]
        Auto = 0xFF
    }

    public enum WriterMistakeType
    {
        Unknown, Plagiarism, Instructions, Grammar, Format, Communication,
        Irresponsibility, Unprofessionality, PersonalIssues
    }

    public enum CommentCode
    {
        [CommentDescription("Other", "other", CommentCategory.Other, WriterMistakeType.Unknown)]
        Other = 0,

        #region Reasign/Drop

        [CommentDescription("1 Hour Overdue (High Risk)", "t1", CommentCategory.ReasignDrop, WriterMistakeType.Irresponsibility)]
        _1HourOverdueHighRisk = 11,
        [CommentDescription("4 Hours Overdue (No Risk, Low Risk)", "t2", CommentCategory.ReasignDrop, WriterMistakeType.Irresponsibility)]
        _4HoursOverdueLowRisk = 12,
        [CommentDescription("Order Drop", "t3", CommentCategory.ReasignDrop, WriterMistakeType.Irresponsibility)]
        OrderDrop = 13,
        [CommentDescription("Extension Request", "t4", CommentCategory.ReasignDrop, WriterMistakeType.Irresponsibility)]
        ExtensionRequest = 14,

        #endregion

        #region Writer Quality

        [CommentDescription("No Time for Revision", "q1", CommentCategory.WriterQuality, WriterMistakeType.Irresponsibility)]
        NoTimeForRevision = 21,
        [CommentDescription("No 3rd Revision Done", "q2", CommentCategory.WriterQuality, WriterMistakeType.Irresponsibility)]
        No3rdRevisionDone = 22,

        #endregion

        #region Writer Fault

        [CommentDescription("Plagiarism", "cc51", CommentCategory.WriterFault, WriterMistakeType.Plagiarism)]
        Plagiarism_WF = 31,
        [CommentDescription("Instructions", "cc52", CommentCategory.WriterFault, WriterMistakeType.Instructions)]
        Instructions_WF = 32,
        [CommentDescription("Grammar", "cc53", CommentCategory.WriterFault, WriterMistakeType.Grammar)]
        Grammar_WF = 33,
        [CommentDescription("Format", "cc54", CommentCategory.WriterFault, WriterMistakeType.Format)]
        Format_WF = 34,
        [CommentDescription("Communication", "cc55", CommentCategory.WriterFault, WriterMistakeType.Communication)]
        Communication_WF = 35,

        #endregion

        #region Customer Fault

        [CommentDescription("Plagiarism", "cc61", CommentCategory.CustomerFault, WriterMistakeType.Plagiarism)]
        Plagiarism_CF = 41,
        [CommentDescription("Instructions", "cc62", CommentCategory.CustomerFault, WriterMistakeType.Instructions)]
        Instructions_CF = 42,
        [CommentDescription("Grammar", "cc63", CommentCategory.CustomerFault, WriterMistakeType.Grammar)]
        Grammar_CF = 43,
        [CommentDescription("Format", "cc64", CommentCategory.CustomerFault, WriterMistakeType.Format)]
        Format_CF = 44,
        [CommentDescription("Communication", "cc65", CommentCategory.CustomerFault, WriterMistakeType.Communication)]
        Communication_CF = 45,

        #endregion

        #region Company Fault

        [CommentDescription("Plagiarism", "cc71", CommentCategory.CompanyFault, WriterMistakeType.Plagiarism)]
        Plagiarism_CoF = 51,
        [CommentDescription("Instructions", "cc72", CommentCategory.CompanyFault, WriterMistakeType.Instructions)]
        Instructions_CoF = 52,
        [CommentDescription("Grammar", "cc73", CommentCategory.CompanyFault, WriterMistakeType.Grammar)]
        Grammar_CoF = 53,
        [CommentDescription("Format", "cc74", CommentCategory.CompanyFault, WriterMistakeType.Format)]
        Format_CoF = 54,
        [CommentDescription("Communication", "cc75", CommentCategory.CompanyFault, WriterMistakeType.Communication)]
        Communication_CoF = 55,

        #endregion

        #region Corrective Actions

        [CommentDescription("Revision", "cc81", CommentCategory.CorrectiveActions, WriterMistakeType.Unprofessionality)]
        Revision = 61,
        [CommentDescription("Re-assign", "cc82", CommentCategory.CorrectiveActions, WriterMistakeType.Unprofessionality)]
        ReAssign = 62,
        [CommentDescription("Refund", "cc83", CommentCategory.CorrectiveActions, WriterMistakeType.Unprofessionality)]
        Refund = 63,
        [CommentDescription("Revision Offer", "cc84", CommentCategory.CorrectiveActions, WriterMistakeType.Unprofessionality)]
        RevisionOffer = 64,

        #endregion

        #region Positive Feedback

        [CommentDescription("Positive Feedback", "pf1", CommentCategory.PositiveFeedback, WriterMistakeType.Unknown)]
        PositiveFeedback = 71,

        #endregion

        #region Writer Reliability

        [CommentDescription("Writer Cannot be Reached", "w1", CommentCategory.WriterReliability, WriterMistakeType.PersonalIssues)]
        WriterCannotBeReached = 81,
        [CommentDescription("Spamming Admin Panel", "w2", CommentCategory.WriterReliability, WriterMistakeType.PersonalIssues)]
        SpammingAdminPanel = 82,
        [CommentDescription("Threatening", "w3", CommentCategory.WriterReliability, WriterMistakeType.PersonalIssues)]
        Threatening = 83,
        [CommentDescription("Rudeness", "w4", CommentCategory.WriterReliability, WriterMistakeType.PersonalIssues)]
        Rudeness = 84,
        [CommentDescription("Contact With the Client", "w5", CommentCategory.WriterReliability, WriterMistakeType.PersonalIssues)]
        ContactWithTheClient = 85,
        [CommentDescription("Violates Confidentiality", "w6", CommentCategory.WriterReliability, WriterMistakeType.PersonalIssues)]
        ViolatesConfidentiality = 86,

        #endregion

        #region AUTOs

        #region Customer Complaint

        [CommentDescription("Plagiarism", "cc91", CommentCategory.CustomerComplaint, WriterMistakeType.Plagiarism)]
        Plagiarism_CC = 91,
        [CommentDescription("Instructions", "cc92", CommentCategory.CustomerComplaint, WriterMistakeType.Instructions)]
        Instructions_CC = 92,
        [CommentDescription("Grammar", "cc93", CommentCategory.CustomerComplaint, WriterMistakeType.Grammar)]
        Grammar_CC = 93,
        [CommentDescription("Format", "cc94", CommentCategory.CustomerComplaint, WriterMistakeType.Format)]
        Format_CC = 94,
        [CommentDescription("Communication", "cc95", CommentCategory.CustomerComplaint, WriterMistakeType.Communication)]
        Communication_CC = 95,

        #endregion

        #region Revision

        [CommentDescription("Plagiarism", "r51", CommentCategory.Revision, WriterMistakeType.Plagiarism)]
        Plagiarism_R = 101,
        [CommentDescription("Instructions", "r52", CommentCategory.Revision, WriterMistakeType.Instructions)]
        Instructions_R = 102,
        [CommentDescription("Grammar", "r53", CommentCategory.Revision, WriterMistakeType.Grammar)]
        Grammar_R = 103,
        [CommentDescription("Format", "r54", CommentCategory.Revision, WriterMistakeType.Format)]
        Format_R = 104,
        [CommentDescription("Communication", "r55", CommentCategory.Revision, WriterMistakeType.Communication)]
        Communication_R = 105,

        #endregion

        #region Order Verification

        [CommentDescription("Plagiarism", "ov51", CommentCategory.OrderVerification, WriterMistakeType.Plagiarism)]
        Plagiarism_OV = 111,
        [CommentDescription("Instructions", "ov52", CommentCategory.OrderVerification, WriterMistakeType.Instructions)]
        Instructions_OV = 112,
        [CommentDescription("Grammar", "ov53", CommentCategory.OrderVerification, WriterMistakeType.Grammar)]
        Grammar_OV = 113,
        [CommentDescription("Format", "ov54", CommentCategory.OrderVerification, WriterMistakeType.Format)]
        Format_OV = 114,
        //[CommentDescription("Communication", "ov55", CommentCategory.OrderVerification, WriterMistakeType.Communication)]
        //Communication_OV = 115,

        #endregion

        [CommentDescription("SurveyAuto", "surveyAuto", CommentCategory.Auto, WriterMistakeType.Unknown)]
        SurveyAuto = 0xF1,

        //[CommentDescription("Auto", "auto", CommentCategory.Auto, WriterMistakeType.Unknown)]
        //Auto = 0xFF,

        #endregion
    }

    public enum KdPdCategoryType { Orders, Messages, Ratings, Other }

    public enum LowPriceForHighRiskOrderWarnStatus { NotSet, Warned, Suppressed }

    public enum Risk
    {
        [Description("No Risk")]
        NoRisk,
        [Description("Low Risk")]
        LowRisk,
        [Description("High Risk")]
        HighRisk,
        [Description("Guaranteed Assign")]
        GuaranteedAssign
    }

    public enum CustomerType
    {
        [Description("Regular")]
        Regular,
        [Description("Dealers")]
        Dealers
    }

    public enum OrderVerificationType { Regular, CMS, Backlink, Niche, PressRelease }

    public enum WriterSampleOrResumeType { Sample, Resume }

    public enum ReminderType { PrefferedWriterConfirmation }
    public enum DestinationSite { AdminPanel, WriterPanel }

    public enum InactivityDecision
    {
        [Description("Continue to work")]
        ContinueWorking,
        [Description("Terminate account")]
        TerminateAccount
    }

    public enum InactivityReason
    {
        [Description("No orders to specialization")]
        NoOrdersToSpecialization,
        [Description("Continue to work")]
        PersonalIssues,
        [Description("Low prices")]
        LowPrices,
        [Description("Other")]
        Other
    }

    public enum PaymentSortingType
    {
        [Description("Sort By Order ID")]
        ByOrder,
        [Description("Sort By Date")]
        ByDate
    }

    public enum SurveyFilterType {[Description("None")]None, [Description("Order")] ForOrder, [Description("Writer")] ForWriter, [Description("Customer")]ForCustomer }

    public enum SurveyFetchType
    {
        All,
        Moderated,
        Unmoderated,
        LatestUnmoderated,
    };

    public enum SurveyQuestionAnswerType { grade4, textarea, text, yesno };

    public enum InactiveWritersFilter { All, Candidates, Declined, Fired, Frozen }

    // !!! numeric values for these enums should not be to conform with legacy database structure
    public enum UserType
    {
        [Description("Customer")]
        Customer,
        [Description("Support")]
        Support,
        [Description("Writer")]
        Writer,
        [Description("System")]
        System,
        [Description("Dealer")]
        Dealer
    }

    public enum OrderNotificationType
    {
        avail_late,  // используется где попало, для кучи всяких нотификаций, никак не связанных по смыслу с названием
        wrtdl_late,  //writerdeadline is expired
        avail_late2, //order was too long in "available" list and has no time to extend anymore
        dl_late,
    }

    public enum CurrencyType
    {
        [Description("usd")]
        USD,
        [Description("eur")]
        EUR
    }

    public enum PaysystemType
    {
        [Description("swreg")]
        swreg,
        [Description("plimus")]
        plimus,
        [Description("twoco")]
        twoco
    }

    public enum WriterNotificationType
    {
        order_completed_5 = 5, order_completed_10 = 10, order_completed_15 = 15, order_completed_20 = 20
    }

    public enum WritersFilter
    {
        [Description("All")]
        All,
        [Description("Logged in last month")]
        LoggedInLastMonth,
        [Description("Took order in last month")]
        TookOrderLastMonth,
        [Description("Didn't logged in for 3 month")]
        DidntLoggedInFor3Month
    }

    /// <summary>
    /// used in "alog" table to identify action's object (who raised action)
    /// </summary>
    public enum ActionActivator { admin, customer, writer, system }

    /// <summary>
    /// Defines states for writer hiring workflow
    /// </summary>
    public enum WriterHiringState
    {
        STARTED,
        [Description("New")]
        NEW,
        [Description("App Graded")]
        APPLICATION_GRADED,
        [Description("Task Sent")]
        TASK_SENT,
        [Description("Task Submitted")]
        TASK_SUBMITTED,
        [Description("Task Graded")]
        TASK_GRADED,
        [Description("Call Graded")]
        CALL_GRADED,
        ACTIVATED,
        DECLINED
    }

    /// <summary>
    /// Пол
    /// </summary>
    public enum Gender { Male = 1, Female = 2, NotSelected = 3 }

    /// <summary>
    /// Семейное положение
    /// </summary>
    public enum MaritalStatus { Single = 1, Married = 2, NotSelected = 3 }

    /// <summary>
    /// Метод оплаты
    /// </summary>
    //public enum PaymentMethodOld
    //{
    //    PayPal = 1, WireTransfer = 2, ACHCheckAccountTransfer = 3,
    //    ACHSavingsAccountTransfer = 4, BACS = 5
    //}

    public enum PaymentMethod
    {
        NotSelected = 0, WireTransfer = 1, PayPal = 2, EGold = 3, Privat24 = 4, WesternUnion = 5, WebMoney = 6
    }

    public enum PaymentMethod2
    {
        NotSelected, PayPal, WireTransfer, ACHCheckAccountTransfer, ACHSavingsAccountTransfer, BACS
    }

    public enum CustomerQualityExpectations
    {
        APlusJustPerfect, BetterThanMyWriting, SimilarToMyWritingLevel
    }

    /// <summary>
    /// Как нашел нас
    /// </summary>
    public enum FindUs { Search, Friend, Advertisement, Link, Other, Unknown }

    /// <summary>
    /// Сколько часов в неделю сможет работать исполнитель
    /// </summary>
    public enum WorkHoursScale
    {
        NotSelected,
        LessThan10 = 1, From11To20 = 2,
        From21To30 = 3, From31To40 = 4, MoreThan40 = 5,
    }

    public enum WriterOcupation
    {
        NotSelected, Teacher, Student, EmployedFullTimeUndergraduate, EmployedPartTimeUndergraduate,
        EmployedFullTimeGraduate, EmployedPartTimeGraduate, Retried, Unemployed, Other
    }

    /// <summary>
    /// Периодичность выплаты зарплаты
    /// </summary>
    public enum SalaryPeriodicity { TwiceAMonth = 1, OnceAMonth = 2, NotSelected = 3 }

    /// <summary>
    /// Межстрочное расстояние
    /// </summary>
    public enum LineSpacing {[Description("Single")]SingleSpacing = 1, [Description("Double")]DoubleSpacing = 2 }

    public enum RevisionStatus { NotModerated = 0, Approved = 1, Declined = -1 }

    /// <summary>
    /// Флаги доступа
    /// </summary>
    [Flags]
    //our $ACCESS_W = 1;
    //our $ACCESS_C = 2;
    //our $ACCESS_A = 4;
    //our $ACCESS_OC = 16;    # show to customer assigned on order
    //public enum AccessFlags { None = 0x00, Writer = 0x01, Customer = 0x02, Support = 0x04/*, CustomerOnOrder = 0x10*/ }

    public enum ModerationStatus { NotModerated = 0, Approved = 1, Declined = -1 }

    public enum MessageType : int
    {
        [Description("Plain")]
        Plain = 0, // <- 0
        [Description("Suggestion")]
        Suggestion = 1, // <- 4
        [Description("Completion")]
        Completion = 2, // <- 1
        [Description("Admin completion")]
        AdminCompl = 3, // <- 5
        [Description("Note")]
        Note = 4, // <- 6
        [Description("Request for order")]
        RequestForOrder = 5 // <- 2
    }

    /// <summary>
    /// Используется для фильтрации ордеров
    /// </summary>
    public enum OrderFilter
    {
        [Description("Available")]
        Available = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("Not completed")]
        NotCompleted = 3,
        [Description("Requested")]
        Requested = 4,
        [Description("Available overdue")]
        AvailableOverdue = 5,
        [Description("In progress overdue")]
        InProgressOverdue = 6,
        [Description("Available due to 12")]
        AvailableDue12 = 7,
        [Description("Available due to 24")]
        AvailableDue24 = 8,
        [Description("Revised")]
        Revised = 9,
        [Description("With 3 revisions or more")]
        OverThreeRevisions = 10,
        [Description("All")]
        ALL = 11,
        [Description("NotArchived")]
        NotArchived = 12
    }

    public enum AvailableOrdersFilter
    {
        [Description("All")]
        All,
        [Description("Bids Only")]
        BidsOnly,
        [Description("Without Bids")]
        WithoutBids
    }

    /// <summary>
    /// Message sort by time order for moderation/completion pages
    /// </summary>
    public enum MessageSortOrder {[Description("rev")]NewFirst, [Description("pre")]OldFirst }

    /// <summary>
    /// Тип скидки
    /// </summary>
    public enum DiscountType
    {
        [Description("begged")]
        Begged = 1,

        [Description("for friend")]
        ForFriend = 2,

        [Description("for tempter")]
        Tempter = 3,

        [Description("facebook")]
        Facebook = 4,

        [Description("one-time")]
        Onetime = 5,

        [Description("tempter-personal")]
        TempterPersonal = 6,

        [Description("dynamic")]
        Dynamic = 7,
    }

    /// <summary>
    /// Для кого предназначен отчет о плагиате
    /// </summary>
    public enum PlagReportDestination { Source = 1, Writer = 2, Customer = 3 }

    /// <summary>
    /// рабочий статус
    /// </summary>
    public enum WorkStatus
    {
        [Description("DECLINED")]
        Declined = 1,
        [Description("FIRED")]
        Fired = 2,
        [Description("CANDIDATE")]
        Candidate = 3,
        [Description("HOLD")]
        OnHold = 4
    }

    /// <summary>
    /// Стили цитирования
    /// </summary>
    public enum CitationStyle { APA, Chicago, Harvard, MLA, Other, Turabian, Vancouver, Oxford, CBE, NotApplicable }

    public enum EnglishDialect { NN, UK, US, Any }

    public enum PaymentChargeback
    {
        [Description("Used")]
        Used,
        [Description("Not Reviewed")]
        NotReviewed,
        [Description("Reviewed")]
        Reviewed,
        //[Description("Denied Customer")]DeniedCustomer, 
        //[Description("Denied Writer")]DeniedWriter, 
        [Description("On Hold")]
        OnHold
    }

    public enum PaymentStatus { NotPaid, Paid, Chargeback, Inquiry, Waiting }

    public enum DeadlineHours : int
    {
        [Description("60 days")]
        d60 = 60 * 24,
        [Description("30 days")]
        d30 = 30 * 24,
        [Description("20 days")]
        d20 = 20 * 24,
        [Description("14 days")]
        d14 = 14 * 24,
        [Description("10 days")]
        d10 = 10 * 24,
        [Description("7 days")]
        d7 = 7 * 24,
        [Description("5 days")]
        d5 = 5 * 24,
        [Description("4 days")]
        d4 = 4 * 24,
        [Description("3 days")]
        d3 = 3 * 24,
        [Description("48 hours")]
        d2 = 2 * 24,
        [Description("24 hours")]
        h24 = 24,
        [Description("12 hours")]
        h12 = 12
    }

    public enum AvailOrderSortingType
    {
        ID_DESC = 0,
        WriterDeadline_DESC = 1,
        ID_ASC = 2,
        WriterDeadline_ASC = 3
    }

    public enum CustomerBonusEvents
    {
        begged,
        apply,
        forfriend,
        fortempter
    }

    public enum Degree
    {
        [Description("School")]
        School,
        [Description("Associate/College")]
        Associate_College,
        [Description("Bachelor’s")]
        Bachelor,
        [Description("Master’s")]
        Master,
        [Description("Doctoral")]
        Doctoral

    }

    public enum EnglishLevel
    {
        [Description("Intermediate")]
        Intermediate,
        [Description("Advanced")]
        Advanced,
        [Description("Proficient")]
        Proficient
    }

    public enum ReasonType { Late, Other }

    public enum PayoneerWorkflowState
    {
        Selected = 0,
        Requested = 1,
        AlreadyHad = 2,
        AtmChecked = 3,
        ReadyForPayments = 4,
        Verified = 5
    }

    public enum EmailStatus
    {
        [Description("Send")]
        Send = 0,
        [Description("Sending")]
        Sending,
        [Description("Sent")]
        Sent,
        [Description("Canceled")]
        Canceled,
    }

    public enum EmailDetailStatus
    {
        [Description("Sent")]
        Sent = 0,
        [Description("Queued")]
        Queued,
        [Description("Rejected")]
        Rejected,
        [Description("Invalid")]
        Invalid,
        [Description("Scheduled")]
        Scheduled
    }
}
