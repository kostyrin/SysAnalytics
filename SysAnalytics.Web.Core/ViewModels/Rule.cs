using System.Runtime.Serialization;

namespace SysAnalytics.Web.Core.ViewModels
{
    [DataContract]
    public class Rule
    {
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string op { get; set; }
        [DataMember]
        public string data { get; set; }
    }
}
