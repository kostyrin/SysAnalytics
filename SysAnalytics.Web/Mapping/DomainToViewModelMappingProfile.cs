using System;
using System.Linq;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using NHibernate.Proxy;
using SysAnalytics.Model;
using SysAnalytics.Model.Entities;
using SysAnalytics.Web.Core.ViewModels;
using SysAnalytics.Web.ViewModels;

namespace SysAnalytics.Web.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<User, UserFormModel>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            //                                        .ForMember(d => d.Email, o => o.Ignore())
            //                                        .ForMember(d => d.FirstName, o => o.Ignore())
            //                                        .ForMember(d => d.LastName, o => o.Ignore());
            Mapper.CreateMap<Order, OrderFormModel>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.UserId))
                .ForMember(dest => dest.WriterId, opt => opt.MapFrom(src => src.Writer.UserId))
                .ForMember(dest => dest.AssignedBy, opt => opt.MapFrom(src => src.AssignedBy.UserId))
                .ForMember(dest => dest.Major2_Name, opt => opt.MapFrom(src => src.Major2.Name))
                //.ForMember(dest => dest.PaymentTrackingNumber, opt => opt.MapFrom(src => string.Join(", ", src.OrderPayments.Select(op => op.TrackingNumber))))
                //.ForMember(dest => dest.Customer_Employed, opt => opt.MapFrom(src => src.Customer.Employed))
                //.ForMember(dest => dest.Customer_MainMajors, opt => opt.MapFrom(src => src.Customer.MainMajors))
                //.ForMember(dest => dest.Customer_StudyLevel, opt => opt.MapFrom(src => src.Customer.StudyLevel))
                //.ForMember(dest => dest.Customer_CurrentGPA, opt => opt.MapFrom(src => src.Customer.CurrentGPA))
                //.ForMember(dest => dest.Customer_DesiredGPA, opt => opt.MapFrom(src => src.Customer.DesiredGPA))
                //.ForMember(dest => dest.Customer_EnglishNative, opt => opt.MapFrom(src => src.Customer.EnglishNative))
                //.ForMember(dest => dest.Customer_EnglishStudyYears, opt => opt.MapFrom(src => src.Customer.EnglishStudyYears))
                //.ForMember(dest => dest.Customer_Difficulties, opt => opt.MapFrom(src => src.Customer.Difficulties))
                //.ForMember(dest => dest.Customer_CommonMistakes, opt => opt.MapFrom(src => src.Customer.CommonMistakes))
                //.ForMember(dest => dest.Customer_CustomerQualityExpectations, opt => opt.MapFrom(src => src.Customer.CustomerQualityExpectations))
                //.ForMember(dest => dest.Customer_AdditionalComments, opt => opt.MapFrom(src => src.Customer.AdditionalComments))
                //.ForMember(dest => dest.Customer_IsWroteReview, opt => opt.MapFrom(src => src.Customer.IsWroteReview))
                //.ForMember(dest => dest.Customer_SendTips, opt => opt.MapFrom(src => src.Customer.SendTips))
                //.ForMember(dest => dest.Customer_SendSeasonal, opt => opt.MapFrom(src => src.Customer.SendSeasonal))
                //.ForMember(dest => dest.Customer_NumCompletedOrders, opt => opt.MapFrom(src => src.Customer.NumCompletedOrders))
                //.ForMember(dest => dest.Customer_NumCompletedPages, opt => opt.MapFrom(src => src.Customer.NumCompletedPages))
                //.ForMember(dest => dest.Customer_Bonus, opt => opt.MapFrom(src => src.Customer.Bonus))
                //.ForMember(dest => dest.Customer_Emergency, opt => opt.MapFrom(src => src.Customer.Emergency))
                //.ForMember(dest => dest.Customer_IsSubscriber, opt => opt.MapFrom(src => src.Customer.IsSubscriber))
                //.ForMember(dest => dest.Customer_DeniedCustomer, opt => opt.MapFrom(src => src.Customer.DeniedCustomer))
                //.ForMember(dest => dest.Customer_IsPartner, opt => opt.MapFrom(src => src.Customer.IsPartner))
                //.ForMember(dest => dest.Customer_BalancePages, opt => opt.MapFrom(src => src.Customer.BalancePages))
                //.ForMember(dest => dest.Customer_City, opt => opt.MapFrom(src => src.Customer.City))
                //.ForMember(dest => dest.Customer_Degree, opt => opt.MapFrom(src => src.Customer.Degree))
                //.ForMember(dest => dest.Customer_CountryStudy, opt => opt.MapFrom(src => src.Customer.CountryStudy))
                //.ForMember(dest => dest.Customer_EnglishLevel, opt => opt.MapFrom(src => src.Customer.EnglishLevel))

                //.ForMember(dest => dest.Customer_RegDate, opt => opt.MapFrom(src => src.Customer.RegDate))
                //.ForMember(dest => dest.Customer_LastLogin, opt => opt.MapFrom(src => src.Customer.LastLogin))
                //.ForMember(dest => dest.Customer_Country, opt => opt.MapFrom(src => src.Customer.Country))
                //.ForMember(dest => dest.Customer_TimeZone, opt => opt.MapFrom(src => src.Customer.TimeZone))
                ////.ForMember(dest => dest.Customer_PaymentDetails, opt => opt.MapFrom(src => src.Customer.PaymentDetails))
                //.ForMember(dest => dest.Customer_DisableNotifications, opt => opt.MapFrom(src => src.Customer.DisableNotifications))
                //.ForMember(dest => dest.Customer_Site, opt => opt.MapFrom(src => src.Customer.Site.SiteId))
                //.ForMember(dest => dest.Customer_FindUs, opt => opt.MapFrom(src => src.Customer.FindUs))
                //.ForMember(dest => dest.Customer_UserType, opt => opt.MapFrom(src => src.Customer.UserType))
                //.ForMember(dest => dest.Customer_IsProfileEditing, opt => opt.MapFrom(src => src.Customer.IsProfileEditing))
                //.ForMember(dest => dest.Customer_BirthDate, opt => opt.MapFrom(src => src.Customer.BirthDate))
                //.ForMember(dest => dest.Customer_IsFrozen, opt => opt.MapFrom(src => src.Customer.IsFrozen))
                //.ForMember(dest => dest.Customer_IsActive, opt => opt.MapFrom(src => src.Customer.IsActive))

                //.ForMember(dest => dest.Writer_History, opt => opt.MapFrom(src => src.Writer.History))
                //.ForMember(dest => dest.Writer_WorkStatus, opt => opt.MapFrom(src => src.Writer.WorkStatus))
                //.ForMember(dest => dest.Writer_HireHistory, opt => opt.MapFrom(src => src.Writer.HireHistory))
                //.ForMember(dest => dest.Writer_Gender, opt => opt.MapFrom(src => src.Writer.Gender))
                //.ForMember(dest => dest.Writer_MaritalStatus, opt => opt.MapFrom(src => src.Writer.MaritalStatus))
                //.ForMember(dest => dest.Writer_TimeAvailableAtHomeFrom, opt => opt.MapFrom(src => src.Writer.TimeAvailableAtHome.FromHour))
                //.ForMember(dest => dest.Writer_TimeAvailableAtHomeTo, opt => opt.MapFrom(src => src.Writer.TimeAvailableAtHome.ToHour))
                //.ForMember(dest => dest.Writer_TimeAvailableAtWorkFrom, opt => opt.MapFrom(src => src.Writer.TimeAvailableAtWork.FromHour))
                //.ForMember(dest => dest.Writer_TimeAvailableAtWorkTo, opt => opt.MapFrom(src => src.Writer.TimeAvailableAtWork.ToHour))
                //.ForMember(dest => dest.Writer_TimeAvailableAtCellFrom, opt => opt.MapFrom(src => src.Writer.TimeAvailableAtCell.FromHour))
                //.ForMember(dest => dest.Writer_TimeAvailableAtCellTo, opt => opt.MapFrom(src => src.Writer.TimeAvailableAtCell.ToHour))
                //.ForMember(dest => dest.Writer_WorkHoursGrade, opt => opt.MapFrom(src => src.Writer.WorkHoursGrade))
                //.ForMember(dest => dest.Writer_Iam, opt => opt.MapFrom(src => src.Writer.Iam))
                //.ForMember(dest => dest.Writer_SalaryPeriodicity, opt => opt.MapFrom(src => src.Writer.SalaryPeriodicity))
                //.ForMember(dest => dest.Writer_AssigmentsDone, opt => opt.MapFrom(src => src.Writer.AssigmentsDone))
                ////.ForMember(dest => dest.Resume, opt => opt.MapFrom(src => src.Customer.RegDate))
                //.ForMember(dest => dest.Writer_Sample, opt => opt.MapFrom(src => src.Writer.Sample.OriginalFileName))
                //.ForMember(dest => dest.Writer_WriteOther, opt => opt.MapFrom(src => src.Writer.WriteOther))
                //.ForMember(dest => dest.Writer_Enjoy, opt => opt.MapFrom(src => src.Writer.Enjoy))
                //.ForMember(dest => dest.Writer_Dislike, opt => opt.MapFrom(src => src.Writer.Dislike))
                //.ForMember(dest => dest.Writer_WriterPaymentMethodId, opt => opt.MapFrom(src => src.Writer.WriterPaymentMethodId))
                ////.ForMember(dest => dest.Writer_WriterPaymentMethod, opt => opt.MapFrom(src => src.Writer.WriterPaymentMethod.Name))
                //.ForMember(dest => dest.Writer_PaymentMethod1, opt => opt.MapFrom(src => src.Writer.PaymentMethod1))
                //.ForMember(dest => dest.Writer_PaymentMethod2, opt => opt.MapFrom(src => src.Writer.PaymentMethod2))
                //.ForMember(dest => dest.Writer_Employers, opt => opt.MapFrom(src => src.Writer.Employers))
                //.ForMember(dest => dest.Writer_HireReason, opt => opt.MapFrom(src => src.Writer.HireReason))
                //.ForMember(dest => dest.Writer_Occupation, opt => opt.MapFrom(src => src.Writer.Occupation))
                //.ForMember(dest => dest.Writer_Education, opt => opt.MapFrom(src => src.Writer.Education))
                //.ForMember(dest => dest.Writer_Address, opt => opt.MapFrom(src => src.Writer.Address))
                //.ForMember(dest => dest.Writer_City, opt => opt.MapFrom(src => src.Writer.City))
                //.ForMember(dest => dest.Writer_State, opt => opt.MapFrom(src => src.Writer.State))
                //.ForMember(dest => dest.Writer_StatusBalance, opt => opt.MapFrom(src => src.Writer.StatusBalance))
                //.ForMember(dest => dest.Writer_StatusBalanceTmp, opt => opt.MapFrom(src => src.Writer.StatusBalanceTmp))
                //.ForMember(dest => dest.Writer_Status, opt => opt.MapFrom(src => src.Writer.Status))
                //.ForMember(dest => dest.Writer_ActivationDate, opt => opt.MapFrom(src => src.Writer.ActivationDate))
                //.ForMember(dest => dest.Writer_DeactivationDate, opt => opt.MapFrom(src => src.Writer.DeactivationDate))
                //.ForMember(dest => dest.Writer_Status1stSetDate, opt => opt.MapFrom(src => src.Writer.Status1stSetDate))
                //.ForMember(dest => dest.Writer_IsTracked, opt => opt.MapFrom(src => src.Writer.IsTracked))
                //.ForMember(dest => dest.Writer_CanSeePartners, opt => opt.MapFrom(src => src.Writer.CanSeePartners))

                //.ForMember(dest => dest.Writer_RegDate, opt => opt.MapFrom(src => src.Writer.RegDate))
                //.ForMember(dest => dest.Writer_LastLogin, opt => opt.MapFrom(src => src.Writer.LastLogin))
                //.ForMember(dest => dest.Writer_Country, opt => opt.MapFrom(src => src.Writer.Country))
                //.ForMember(dest => dest.Writer_TimeZone, opt => opt.MapFrom(src => src.Writer.TimeZone))
                ////.ForMember(dest => dest.Writer_PaymentDetails, opt => opt.MapFrom(src => src.Writer.PaymentDetails))
                //.ForMember(dest => dest.Writer_DisableNotifications, opt => opt.MapFrom(src => src.Writer.DisableNotifications))
                //.ForMember(dest => dest.Writer_Site, opt => opt.MapFrom(src => src.Writer.Site.SiteId))
                //.ForMember(dest => dest.Writer_FindUs, opt => opt.MapFrom(src => src.Writer.FindUs))
                //.ForMember(dest => dest.Writer_UserType, opt => opt.MapFrom(src => src.Writer.UserType))
                //.ForMember(dest => dest.Writer_IsProfileEditing, opt => opt.MapFrom(src => src.Writer.IsProfileEditing))
                //.ForMember(dest => dest.Writer_BirthDate, opt => opt.MapFrom(src => src.Writer.BirthDate))
                //.ForMember(dest => dest.Writer_IsFrozen, opt => opt.MapFrom(src => src.Writer.IsFrozen))
                //.ForMember(dest => dest.Writer_IsActive, opt => opt.MapFrom(src => src.Writer.IsActive))
                ;

            Mapper.CreateMap<Customer, CustomerFormModel>()
                .ForMember(dest => dest.Site_Name, opt => opt.MapFrom(src => src.Site.Name));
            Mapper.CreateMap<Writer, WriterFormModel>()
                .ForMember(dest => dest.Site_Name, opt => opt.MapFrom(src => src.Site.Name))
                //.ForMember(dest => dest.TimeAvailableAtHome_FromHour, opt => opt.MapFrom(src => src.TimeAvailableAtHome.FromHour))
                //.ForMember(dest => dest.TimeAvailableAtHome_ToHour, opt => opt.MapFrom(src => src.TimeAvailableAtHome.ToHour))
                //.ForMember(dest => dest.TimeAvailableAtWork_FromHour, opt => opt.MapFrom(src => src.TimeAvailableAtWork.FromHour))
                //.ForMember(dest => dest.TimeAvailableAtWork_ToHour, opt => opt.MapFrom(src => src.TimeAvailableAtWork.ToHour))
                //.ForMember(dest => dest.TimeAvailableAtCell_FromHour, opt => opt.MapFrom(src => src.TimeAvailableAtCell.FromHour))
                //.ForMember(dest => dest.TimeAvailableAtCell_ToHour, opt => opt.MapFrom(src => src.TimeAvailableAtCell.ToHour))
                ;
        }
    }
}
