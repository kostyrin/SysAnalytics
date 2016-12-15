using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml.Serialization;
using SysAnalytics.Logic.Interfaces;
using SysAnalytics.Model.Entities;

namespace SysAnalytics.Logic.Services
{
    public class LifeTimeDiscountService : ILifeTimeDiscountService
    {
        private static IList<LifeTimeDiscount> CACHED_DISCOUNTS = null;

        public IList<LifeTimeDiscount> GetLifetimeDiscountSettings()
        {
            string path = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["LifetimeDiscountsFileWeb"]);
            lock (this)
            {
                if (CACHED_DISCOUNTS != null)
                    return CACHED_DISCOUNTS;

                using (var fStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<LifeTimeDiscount>));

                    var lDiscountsRaw = (List<LifeTimeDiscount>)serializer.Deserialize(fStream);
                    CACHED_DISCOUNTS = (from f in lDiscountsRaw orderby f.PagesNumber ascending select f).ToList();

                    return CACHED_DISCOUNTS;
                }
            }
        }

        public LifetimeDiscountInfo ForUser(int numCompletedPages)
        {
            GetLifetimeDiscountSettings();

            var lPages = numCompletedPages;

            LifeTimeDiscount lPrev = new LifeTimeDiscount { Percent = 0, PagesNumber = lPages };
            var lNext = lPrev;

            CACHED_DISCOUNTS.FirstOrDefault(discountEntry =>
            {
                var lRes = lPages < discountEntry.PagesNumber;
                if (lRes) lNext = discountEntry;
                else lPrev = discountEntry;

                return lRes;
            });

            return new LifetimeDiscountInfo
            {
                CurrentLifetimeDiscountPercent = lPrev.Percent,
                NextLifetimeDiscountPercent = lNext.Percent,
                NumUnitsToNextDiscount = lNext.PagesNumber - numCompletedPages
            };
        }
    }
}
