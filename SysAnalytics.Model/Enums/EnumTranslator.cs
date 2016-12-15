using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysAnalytics.Model.Enums
{
    /// <summary>
    /// Translates between C# enum values and enum values in the legacy system (as strings)
    /// Enum mapping is initialized in the static constructor
    /// Used in (1) MySqlEnumUserType when saving/loading entities into or from the database
    /// (2) Web services to parse input and for output    
    /// </summary>
    public static class EnumTranslator
    {
        //private static readonly ILogger Log = LoggingProvider.General();

        /// <summary>
        /// Initialize search tables
        /// For nullable database column use AddDefaultCSharpValue to map NULL to some default value (i.e. Other or Unknown)
        /// For not-null database column use AddDefaultLegacyValue to map uninitialized C# enum
        /// </summary>
        static EnumTranslator()
        {
            Add(LowPriceForHighRiskOrderWarnStatus.NotSet, "not_set");
            Add(LowPriceForHighRiskOrderWarnStatus.Warned, "warned");
            Add(LowPriceForHighRiskOrderWarnStatus.Suppressed, "suppressed");
            AddDefaultCSharpValue(LowPriceForHighRiskOrderWarnStatus.NotSet);
            AddDefaultLegacyValue<InactivityDecision>("not_set");

            Add(Risk.NoRisk, "no_risk");
            Add(Risk.LowRisk, "low_risk");
            Add(Risk.HighRisk, "high_risk");
            Add(Risk.GuaranteedAssign, "guaranteed_assign");

            Add(OrderVerificationType.Regular, "regular");
            Add(OrderVerificationType.CMS, "cms");
            Add(OrderVerificationType.Backlink, "backlink");
            Add(OrderVerificationType.Niche, "niche");
            Add(OrderVerificationType.PressRelease, "press_release");
            AddDefaultCSharpValue(OrderVerificationType.Regular);
            AddDefaultLegacyValue<OrderVerificationType>("regular");

            Add(InactivityDecision.ContinueWorking, "continue_working");
            Add(InactivityDecision.TerminateAccount, "terminate_account");
            AddDefaultCSharpValue(InactivityDecision.ContinueWorking);
            AddDefaultLegacyValue<InactivityDecision>("continue_working");

            Add(InactivityReason.LowPrices, "low_prices");
            Add(InactivityReason.NoOrdersToSpecialization, "no_orders_to_specialization");
            Add(InactivityReason.Other, "other");
            Add(InactivityReason.PersonalIssues, "personal_issues");
            AddDefaultCSharpValue(InactivityReason.Other);
            AddDefaultLegacyValue<InactivityReason>("other");

            Add(ModerationStatus.Declined, "declined");
            Add(ModerationStatus.NotModerated, "not_moderated");
            Add(ModerationStatus.Approved, "approved");
            AddDefaultLegacyValue<ModerationStatus>("not_moderated");
            AddDefaultCSharpValue(ModerationStatus.NotModerated);

            Add(MessageType.Plain, "plain");
            Add(MessageType.Suggestion, "suggestion");
            Add(MessageType.Completion, "completion");
            Add(MessageType.AdminCompl, "admin_completion");
            Add(MessageType.Note, "note");
            Add(MessageType.RequestForOrder, "request_order");
            AddDefaultLegacyValue<MessageType>("plain");
            AddDefaultCSharpValue(MessageType.Plain);

            Add(SurveyFilterType.ForCustomer, "ForCustomer");
            Add(SurveyFilterType.ForOrder, "ForOrder");
            Add(SurveyFilterType.ForWriter, "ForWriter");
            Add(SurveyFilterType.None, "None");

            Add(CurrencyType.EUR, "EUR");
            Add(CurrencyType.USD, "USD");

            Add(PaysystemType.twoco, "twoco");
            Add(PaysystemType.plimus, "plimus");
            Add(PaysystemType.swreg, "swreg");

            Add(InactiveWritersFilter.All, "All");
            Add(InactiveWritersFilter.Candidates, "Candidates");
            Add(InactiveWritersFilter.Declined, "Declined");
            Add(InactiveWritersFilter.Fired, "Fired");
            Add(InactiveWritersFilter.Frozen, "Frozen");

            Add(UserType.Support, "admin");
            Add(UserType.Writer, "writer");
            Add(UserType.Customer, "customer");
            Add(UserType.System, "system");
            Add(UserType.Dealer, "dealer");


            Add(ReasonType.Late, "late");
            Add(ReasonType.Other, "other");
            AddDefaultCSharpValue(ReasonType.Other);
            AddDefaultLegacyValue<ReasonType>("other");

            Add(OrderNotificationType.avail_late, "avail_late");
            Add(OrderNotificationType.wrtdl_late, "wrtdl_late");
            Add(OrderNotificationType.avail_late2, "avail_late2");
            Add(OrderNotificationType.dl_late, "dl_late");

            Add(WriterNotificationType.order_completed_5, "Completion anniversary - 5");
            Add(WriterNotificationType.order_completed_10, "Completion anniversary - 10");
            Add(WriterNotificationType.order_completed_15, "Completion anniversary - 15");
            Add(WriterNotificationType.order_completed_20, "Completion anniversary - 20");

            Add(WritersFilter.All, "All");
            Add(WritersFilter.DidntLoggedInFor3Month, "Didn't log in for 3 month");
            Add(WritersFilter.LoggedInLastMonth, "Logged in during last month");
            Add(WritersFilter.TookOrderLastMonth, "Took order during last month");

            Add(ActionActivator.admin, "admin");
            Add(ActionActivator.customer, "customer");
            Add(ActionActivator.system, "system");
            Add(ActionActivator.writer, "writer");

            Add(WriterHiringState.ACTIVATED, "ACTIVATED");
            Add(WriterHiringState.DECLINED, "DECLINED");
            Add(WriterHiringState.APPLICATION_GRADED, "APPLICATION-GRADED");
            Add(WriterHiringState.CALL_GRADED, "CALL-GRADED");
            Add(WriterHiringState.NEW, "NEW");
            Add(WriterHiringState.STARTED, "STARTED");
            Add(WriterHiringState.TASK_GRADED, "TASK-GRADED");
            Add(WriterHiringState.TASK_SENT, "TASK-SENT");
            Add(WriterHiringState.TASK_SUBMITTED, "TASK-SUBMITTED");

            Add(FindUs.Search, "search");
            Add(FindUs.Friend, "friend");
            Add(FindUs.Advertisement, "advertisement");
            Add(FindUs.Link, "link");
            Add(FindUs.Other, "other");
            Add(FindUs.Unknown, "unknown");
            AddDefaultCSharpValue(FindUs.Unknown);
            AddDefaultLegacyValue<FindUs>("unknown");

            Add(WorkStatus.Declined, "declined");
            Add(WorkStatus.Fired, "fired");
            Add(WorkStatus.Candidate, "candidate");
            Add(WorkStatus.OnHold, "onhold");
            AddDefaultLegacyValue<WorkStatus>("candidate");

            Add(Gender.Male, "Male");
            Add(Gender.Female, "Female");
            Add(Gender.NotSelected, "Not selected");
            AddDefaultLegacyValue<Gender>("Not selected");
            AddDefaultCSharpValue(Gender.NotSelected);

            Add(MaritalStatus.Single, "Single");
            Add(MaritalStatus.Married, "Married");
            Add(MaritalStatus.NotSelected, "Not selected");
            AddDefaultLegacyValue<MaritalStatus>("Not selected");
            AddDefaultCSharpValue<MaritalStatus>(MaritalStatus.NotSelected);

            Add(PaymentMethod2.NotSelected, "Not selected");
            Add(PaymentMethod2.PayPal, "PayPal");
            Add(PaymentMethod2.WireTransfer, "Wire Transfer");
            Add(PaymentMethod2.ACHCheckAccountTransfer, "ACH Check Account Transfer");
            Add(PaymentMethod2.ACHSavingsAccountTransfer, "ACH Savings Account Transfer");
            Add(PaymentMethod2.BACS, "BACS");
            AddDefaultLegacyValue<PaymentMethod2>("Not selected");
            AddDefaultCSharpValue(PaymentMethod2.NotSelected);

            Add(PaymentMethod.NotSelected, "Not selected");
            Add(PaymentMethod.PayPal, "PayPal");
            Add(PaymentMethod.WireTransfer, "Wire Transfer");
            Add(PaymentMethod.EGold, "E-Gold");
            Add(PaymentMethod.Privat24, "Privat24");
            Add(PaymentMethod.WebMoney, "Web Money");
            Add(PaymentMethod.WesternUnion, "Western Union");
            AddDefaultLegacyValue<PaymentMethod>("Not selected");
            AddDefaultCSharpValue(PaymentMethod.NotSelected);

            Add(CustomerQualityExpectations.APlusJustPerfect, "A+, just perfect!");
            Add(CustomerQualityExpectations.BetterThanMyWriting, "better than my writing");
            Add(CustomerQualityExpectations.SimilarToMyWritingLevel, "similar to my writing level");
            //AddDefaultLegacyValue<CustomerQualityExpectations>("Similar to my writing level");
            //AddDefaultCSharpValue(CustomerQualityExpectations.SimilarToMyWritingLevel);

            Add(WorkHoursScale.NotSelected, "Not selected");
            Add(WorkHoursScale.LessThan10, "<10");
            Add(WorkHoursScale.From11To20, "11-20");
            Add(WorkHoursScale.From21To30, "21-30");
            Add(WorkHoursScale.From31To40, "31-40");
            Add(WorkHoursScale.MoreThan40, ">40");
            AddDefaultLegacyValue<WorkHoursScale>("Not selected");

            Add(WriterOcupation.NotSelected, "Not selected");
            Add(WriterOcupation.Teacher, "teacher");
            Add(WriterOcupation.Student, "student");
            Add(WriterOcupation.EmployedFullTimeUndergraduate, "employed full time undergraduate");
            Add(WriterOcupation.EmployedPartTimeUndergraduate, "employed part time undergraduate");
            Add(WriterOcupation.EmployedFullTimeGraduate, "employed full time graduate");
            Add(WriterOcupation.EmployedPartTimeGraduate, "employed part time graduate");
            Add(WriterOcupation.Retried, "retired");
            Add(WriterOcupation.Unemployed, "unemployed");
            Add(WriterOcupation.Other, "other");
            AddDefaultLegacyValue<WriterOcupation>("Not selected");

            Add(SalaryPeriodicity.OnceAMonth, "once a month");
            Add(SalaryPeriodicity.TwiceAMonth, "twice a month");
            Add(SalaryPeriodicity.NotSelected, "Not selected");
            AddDefaultLegacyValue<SalaryPeriodicity>("Not selected");

            Add(PlagReportDestination.Source, "source");
            Add(PlagReportDestination.Writer, "writer");
            Add(PlagReportDestination.Customer, "customer");
            AddDefaultLegacyValue<PlagReportDestination>("source");

            Add(DiscountType.Begged, "begged");
            Add(DiscountType.ForFriend, "for friend");
            Add(DiscountType.Tempter, "for tempter");
            Add(DiscountType.Facebook, "facebook");
            Add(DiscountType.Onetime, "one-time");
            Add(DiscountType.TempterPersonal, "tempter-personal");
            AddDefaultLegacyValue<DiscountType>("begged");

            //Add(PaymentStatus.Clearing, "Clearing");
            Add(PaymentStatus.NotPaid, "Not paid");
            Add(PaymentStatus.Paid, "Paid");
            Add(PaymentStatus.Chargeback, "Chargeback");
            Add(PaymentStatus.Inquiry, "Inquiry");
            Add(PaymentStatus.Waiting, "Waiting");
            AddDefaultLegacyValue<PaymentStatus>("Not paid");

            Add(LineSpacing.SingleSpacing, "single");
            Add(LineSpacing.DoubleSpacing, "double");
            AddDefaultLegacyValue<LineSpacing>("double");

            Add(EnglishDialect.NN, "NN");
            Add(EnglishDialect.UK, "UK");
            Add(EnglishDialect.US, "US");
            Add(EnglishDialect.Any, "Any");
            AddDefaultLegacyValue<EnglishDialect>("NN");

            Add(CitationStyle.APA, "APA");
            Add(CitationStyle.Chicago, "Chicago");
            Add(CitationStyle.Harvard, "Harvard");
            Add(CitationStyle.MLA, "MLA");
            Add(CitationStyle.Other, "Other");
            Add(CitationStyle.Turabian, "Turabian");
            Add(CitationStyle.Vancouver, "Vancouver");
            Add(CitationStyle.Oxford, "Oxford");
            Add(CitationStyle.CBE, "CBE");
            Add(CitationStyle.NotApplicable, "NotApplicable");
            AddDefaultLegacyValue<CitationStyle>("MLA");

            Add(PaymentChargeback.Used, "Used");
            Add(PaymentChargeback.NotReviewed, "Not Reviewed");
            Add(PaymentChargeback.Reviewed, "Reviewed");
            //Add(PaymentChargeback.DeniedCustomer, "Denied Customer");
            //Add(PaymentChargeback.DeniedWriter, "Denied Writer");
            Add(PaymentChargeback.OnHold, "On Hold");
            AddDefaultLegacyValue<PaymentChargeback>("Not Reviewed");
            AddDefaultCSharpValue<PaymentChargeback>(PaymentChargeback.NotReviewed);

            Add(AvailableOrdersFilter.All, "All");
            Add(AvailableOrdersFilter.BidsOnly, "BidsOnly");
            Add(AvailableOrdersFilter.WithoutBids, "WithoutBids");
            AddDefaultCSharpValue<AvailableOrdersFilter>(AvailableOrdersFilter.All);


            Add(PaymentSortingType.ByDate, "bydate");
            Add(PaymentSortingType.ByOrder, "byorder");
            AddDefaultCSharpValue<PaymentSortingType>(PaymentSortingType.ByOrder);

            Add(AvailOrderSortingType.ID_ASC, "id");
            Add(AvailOrderSortingType.WriterDeadline_ASC, "deadline");
            Add(AvailOrderSortingType.WriterDeadline_DESC, "deadline-desc");
            Add(AvailOrderSortingType.ID_DESC, "id-desc");
            AddDefaultCSharpValue<AvailOrderSortingType>(AvailOrderSortingType.ID_DESC);

            //Add(PayoneerWorkflowState.Selected, PayoneerInfoParams.Selected_KEY);
            //Add(PayoneerWorkflowState.Requested, PayoneerInfoParams.Requested_KEY);
            //Add(PayoneerWorkflowState.AlreadyHad, PayoneerInfoParams.AlreadyHad_KEY);
            //Add(PayoneerWorkflowState.AtmChecked, PayoneerInfoParams.AtmChecked_KEY);
            //Add(PayoneerWorkflowState.ReadyForPayments, PayoneerInfoParams.ReadyForPayments_KEY);
            //Add(PayoneerWorkflowState.Verified, PayoneerInfoParams.Verified_KEY);

            //Add(PayoneerWireTransferWorkflowState.Selected, PayoneerWireTransferInfoParams.Selected_KEY);
            //Add(PayoneerWireTransferWorkflowState.RequestForLink, PayoneerWireTransferInfoParams.RequestForLink_KEY);
            //Add(PayoneerWireTransferWorkflowState.AlreadyHaveLink, PayoneerWireTransferInfoParams.AlreadyHaveLink_KEY);
            //Add(PayoneerWireTransferWorkflowState.AlreadyRegistered, PayoneerWireTransferInfoParams.AlreadyRegistered_KEY);
            //Add(PayoneerWireTransferWorkflowState.LinkReceived, PayoneerWireTransferInfoParams.LinkReceived_KEY);
            //Add(PayoneerWireTransferWorkflowState.ReadyForRegister, PayoneerWireTransferInfoParams.ReadyForRegister_KEY);
            //Add(PayoneerWireTransferWorkflowState.ContactWithPayoneerSupport, PayoneerWireTransferInfoParams.ContactWithPayoneerSupport_KEY);
            //Add(PayoneerWireTransferWorkflowState.ReadyForPayments, PayoneerWireTransferInfoParams.ReadyForPayments_KEY);
            //Add(PayoneerWireTransferWorkflowState.Verified, PayoneerWireTransferInfoParams.Verified_KEY);

            Add(WriterSampleOrResumeType.Sample, "Sample");
            Add(WriterSampleOrResumeType.Resume, "Resume");


            Add(RevisionStatus.Approved, "approved");
            Add(RevisionStatus.Declined, "declined");
            Add(RevisionStatus.NotModerated, "not_moderated");

            Add(SurveyQuestionAnswerType.grade4, "grade4");
            Add(SurveyQuestionAnswerType.textarea, "textarea");
            Add(SurveyQuestionAnswerType.text, "text");
            Add(SurveyQuestionAnswerType.yesno, "yesno");

            Add(KdPdCategoryType.Orders, "Orders");
            Add(KdPdCategoryType.Messages, "Messages");
            Add(KdPdCategoryType.Other, "Other");
            Add(KdPdCategoryType.Ratings, "Ratings");

            //Add(CloudType.S3, "s3");
            //Add(CloudType.Rackspace, "rackspace");

            Add(MessageSortOrder.NewFirst, "rev");
            Add(MessageSortOrder.OldFirst, "pre");

            Add(Degree.School, "School");
            Add(Degree.Associate_College, "Associate/College");
            Add(Degree.Bachelor, "Bachelor");
            Add(Degree.Master, "Master");
            Add(Degree.Doctoral, "Doctoral");

            Add(EnglishLevel.Intermediate, "Intermediate");
            Add(EnglishLevel.Advanced, "Advanced");
            Add(EnglishLevel.Proficient, "Proficient");

            //Add(PayoneerRequestCardType.HaveCard, "have-card");
            //Add(PayoneerRequestCardType.RequestCard, "request-card");
            Add(EmailStatus.Send, "Send");
            Add(EmailStatus.Sending, "Sending");
            Add(EmailStatus.Sent, "Sent");
            Add(EmailStatus.Canceled, "Canceled");

            Add(EmailDetailStatus.Sent, "Sent");
            Add(EmailDetailStatus.Queued, "Queued");
            Add(EmailDetailStatus.Rejected, "Rejected");
            Add(EmailDetailStatus.Invalid, "Invalid");
            Add(EmailDetailStatus.Scheduled, "Scheduled");
        }

        /// <summary>
        /// Lookup tables for enum mapping
        /// </summary>
        private static readonly Dictionary<Type, Dictionary<int, string>> Values = new Dictionary<Type, Dictionary<int, string>>();

        private static readonly Dictionary<Type, string> DefaultDatabaseValues = new Dictionary<Type, string>();

        private static readonly Dictionary<Type, int> DefaultCSharpValues = new Dictionary<Type, int>();

        /// <summary>
        /// tries to get text represantation for enum value from System.ComponentModel.DescriptionAttribute defined on it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string FromDescriptionAttribute<T>(T enumValue, out bool aDescriptionAttrDefined) where T : /*struct,*/ IConvertible
        {
            var lEnumType = enumValue.GetType();
            var lFld = lEnumType.GetField(Enum.GetName(/*typeof(T)*/lEnumType, enumValue));
            var lAttrs = lFld.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (lAttrs != null && lAttrs.Length > 0)
            {
                aDescriptionAttrDefined = true;
                return (lAttrs[0] as System.ComponentModel.DescriptionAttribute).Description;
            }
            else
            {
                aDescriptionAttrDefined = false;
                return Translate(enumValue, false) ?? Enum.GetName(/*typeof(T)*/lEnumType, enumValue);
            }
        }

        public static string FromDescriptionAttribute<T>(T enumValue) where T : struct, IConvertible
        {
            bool lDummy;
            return FromDescriptionAttribute(enumValue, out lDummy);
        }

        /// <summary>
        /// Translate C# enum value into the legacy enum value (string)
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">C# enum value</param>
        /// <returns>Enum value in mysql database (string)</returns>
        public static string Translate<T>(T value, bool aThrowIfNotFound) where T : /*struct,*/ IConvertible
        {
            Type key = typeof(T);
            int intValue = Convert.ToInt32(value);

            // return corresponding string value if found
            if (Values.ContainsKey(key))
            {
                if (Values[key].ContainsKey(intValue))
                {
                    return Values[key][intValue];
                }
            }

            // return default value if set
            if (DefaultDatabaseValues.ContainsKey(key))
            {
                return DefaultDatabaseValues[key];
            }

            // return null if it is an unassigned enum (integer value 0) or C# default
            if ((intValue == 0) || (DefaultCSharpValues.ContainsKey(key) && DefaultCSharpValues[key] == intValue))
            {
                return null;
            }

            // throw if there is no corresponding value in the lookup, no default database value set,
            // and value is not an unassigned enum (so this may indicate we forgot to fill the lookups)
            string errorMessage = string.Format("EnumTranslator.Translate: No corresponding database value and no default for value {0} type {1} ",
                                                value, key);

            //Log.Error(errorMessage);

            if (aThrowIfNotFound)
                throw new Exception(errorMessage);

            return null;
        }

        public static string Translate<T>(T value) where T : /*struct,*/ IConvertible
        {
            return Translate(value, true);
        }

        /// <summary>
        /// Translate legacy enum value (string) into the C# enum value
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="legacyValue">Mysql value (string)</param>
        /// <returns>Enum value (C#)</returns>
        public static T Translate<T>(string legacyValue) where T : /*struct,*/ IConvertible
        {
            Type key = typeof(T);

            // return null or empty string as default C# value if there is one or default enum value otherwise
            if (String.IsNullOrEmpty(legacyValue))
            {
                if (DefaultCSharpValues.ContainsKey(key))
                {
                    return EnumByIntValue<T>(DefaultCSharpValues[key]);
                }
                return default(T);
            }

            // find corresponding int value among Values for this type and convert it into the C# enum
            try
            {
                int intValue = Values[key].Single(x => x.Value == legacyValue).Key;

                return EnumByIntValue<T>(intValue);
            }
            catch (Exception exception)
            {
                string errorMesage = String.Format(
                    "EnumTranslator.Translate: cannot find corresponding enum for value {0} type {1}",
                    legacyValue, key);

                //Log.Error(errorMesage, exception);

                throw new Exception(errorMesage, exception);
            }
        }

        /// <summary>
        /// Finds the enum value corresponding to the given int value
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Int value</param>        
        private static T EnumByIntValue<T>(int value) where T : /*struct,*/ IConvertible
        {
            T result = default(T);

            foreach (var element in Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(element) == value)
                {
                    result = (T)element;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Add value to the lookup table
        /// </summary>
        /// <typeparam name="T">Enum type (usually deduced by the compiler)</typeparam>
        /// <param name="value">Enum value (C# enum)</param>
        /// <param name="dbValue">Database value (string)</param>
        private static void Add<T>(T value, string dbValue) where T : /*struct,*/ IConvertible
        {
            Type key = typeof(T);

            if (!Values.ContainsKey(key))
            {
                Values[key] = new Dictionary<int, string>();
            }

            int intEnumValue = Convert.ToInt32(value);

            if (Values[key].ContainsKey(intEnumValue))
            {
                string errorMessage = String.Format(
                    "EnumTranslator.Add: Lookup table already contains value {0} for type {1}",
                    intEnumValue, key);

                //Log.Error(errorMessage);

                throw new Exception(errorMessage);
            }

            Values[key][intEnumValue] = dbValue;
        }

        /// <summary>
        /// Add default database value for enum (used when storing C# enum that is out of range, e.g. zero)
        /// </summary>
        /// <typeparam name="T">Enum type (C#)</typeparam>
        /// <param name="defaultDbValue">Default database value (string)</param>
        private static void AddDefaultLegacyValue<T>(string defaultDbValue) where T : /*struct,*/ IConvertible
        {
            DefaultDatabaseValues[typeof(T)] = defaultDbValue;
        }

        /// <summary>
        /// Add default C# value for enum (used when reading NULL values from the database)
        /// </summary>
        /// <typeparam name="T">Enum type (C#)</typeparam>
        /// <param name="defaultValue">Default C# value</param>
        private static void AddDefaultCSharpValue<T>(T defaultValue) where T : /*struct,*/ IConvertible
        {
            DefaultCSharpValues[typeof(T)] = Convert.ToInt32(defaultValue);
        }
    }
}
