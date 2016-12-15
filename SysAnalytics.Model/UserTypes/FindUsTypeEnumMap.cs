using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using SysAnalytics.Model.Enums;

namespace SysAnalytics.Model.UserTypes
{
    public class FindUsTypeEnumMap : IUserType
    {
        public bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object r = rs[names[0]];
            var value = (string)r;

            if (string.IsNullOrEmpty(value))
                throw new Exception("Invalid Unit Type");

            switch (value)
            {
                case "unknown":
                    return FindUs.Unknown;
                case "advertisement":
                    return FindUs.Advertisement;
                case "friend":
                    return FindUs.Friend;
                case "link":
                    return FindUs.Link;
                case "other":
                    return FindUs.Other;
                case "search":
                    return FindUs.Search;

                default:
                    throw new Exception("Invalid Unit Type");
            }
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            object paramVal = 0;
            switch ((FindUs)value)
            {
                case FindUs.Unknown:
                    paramVal = "unknown";
                    break;
                case FindUs.Advertisement:
                    paramVal = "advertisement";
                    break;
                case FindUs.Friend:
                    paramVal = "friend";
                    break;
                case FindUs.Link:
                    paramVal = "link";
                    break;
                case FindUs.Other:
                    paramVal = "other";
                    break;
                case FindUs.Search:
                    paramVal = "search";
                    break;
                default:
                    throw new Exception("Invalid Unit Type");
            }
            var parameter = (IDataParameter)cmd.Parameters[index];
            parameter.Value = paramVal;
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { new StringSqlType() }; }
        }

        public Type ReturnedType
        {
            get { return typeof(FindUs); }
        }

        public bool IsMutable
        {
            get { return false; }
        }
    }
}
