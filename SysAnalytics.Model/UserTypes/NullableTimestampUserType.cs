using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace SysAnalytics.Model.UserTypes
{
    public class NullableTimestampUserType : IUserType
    {
        /// <summary>
        /// The SQL types for the columns mapped by this type. 
        /// </summary>
        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { new SqlType(DbType.DateTime) }; }
        }

        /// <summary>
        /// The type returned by <c>NullSafeGet()</c>
        /// </summary>
        public System.Type ReturnedType
        {
            get { return typeof(DateTimeOffset); }
        }

        /// <summary>
        /// Compare two instances of the class mapped by this type for persistent "equality"
        /// ie. equality of persistent state
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public new bool Equals(object x, object y)
        {
            if (x == y) return true;
            if (x == null || y == null) return false;
            return x.Equals(y);
        }

        /// <summary>
        /// Get a hashcode for the instance, consistent with persistence "equality"
        /// </summary>
        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        /// <summary>
        /// Safely gets timestamp db value
        /// if value mysql zero (0000-00-00 00:00) return null
        /// otherwise retrieve value as datetime and return DatetimeOffset with zero offset
        /// </summary>
        /// <param name="rs">a IDataReader</param>
        /// <param name="names">column names</param>
        /// <param name="owner">the containing entity</param>
        /// <returns></returns>
        /// <exception cref="HibernateException">HibernateException</exception>
        //		/// <exception cref="SQLException">SQLException</exception>
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            int ordinal = rs.GetOrdinal(names[0]);
            if (rs.IsDBNull(ordinal))
                return null;// DateTimeOffset.MinValue;

            //if value is 0000-00-00 00:00 return null
            var value = rs.GetValue(ordinal);
            if (value is MySqlDateTime)
            {
                var v = (MySql.Data.Types.MySqlDateTime)value;
                //if (v.Year + v.Month + v.Day + v.Hour + v.Minute + v.Millisecond == 0)
                if (v.Year < 1900)
                    return null;

                return new DateTimeOffset(v.Value, TimeSpan.Zero);
            }
            //otherwise return datetime offset with datetime value from db and 0 offset
            //return new DateTimeOffset(rs.GetDateTime(ordinal), TimeSpan.Zero);
            return new DateTimeOffset(rs.GetDateTime(ordinal));
        }

        /// <summary>
        /// Safely set timestamp db value
        /// if null is passed it is stored like mysql zero (0000-00-00 00:00) value
        /// otherwise datetime offset value is cast to UTC
        /// </summary>
        /// <param name="cmd">a IDbCommand</param>
        /// <param name="value">the object to write</param>
        /// <param name="index">command parameter index</param>
        /// <exception cref="HibernateException">HibernateException</exception>
        //		/// <exception cref="SQLException">SQLException</exception>
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            //if (value == null)
            //    throw new ArgumentNullException("value", "Such type of UserType handles only non-null DateTimeOffset");
            //if (((DateTimeOffset)value) == DateTimeOffset.MinValue)
            //    throw new ArgumentOutOfRangeException("value", "Timestamp range does not allow to set DateTimeOffset.MinValue");

            if (value == null || (DateTimeOffset)value == DateTimeOffset.MinValue)
            {
                ((IDbDataParameter)cmd.Parameters[index]).Value = null;// new MySql.Data.Types.MySqlDateTime(0, 0, 0, 0, 0, 0);
            }
            else
            {
                var date = (DateTimeOffset)value;
                ((IDbDataParameter)cmd.Parameters[index]).Value = date.UtcDateTime;
            }
        }

        /// <summary>
        /// Return a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">generally a collection element or entity field</param>
        /// <returns>a copy</returns>
        public object DeepCopy(object value)
        {
            return value;
        }

        /// <summary>
        /// Are objects of this type mutable?
        /// </summary>
        public bool IsMutable
        {
            get { return false; }
        }

        /// <summary>
        /// During merge, replace the existing (<paramref name="target" />) value in the entity
        /// we are merging to with a new (<paramref name="original" />) value from the detached
        /// entity we are merging. For immutable objects, or null values, it is safe to simply
        /// return the first parameter. For mutable objects, it is safe to return a copy of the
        /// first parameter. For objects with component values, it might make sense to
        /// recursively replace component values.
        /// </summary>
        /// <param name="original">the value from the detached entity being merged</param>
        /// <param name="target">the value in the managed entity</param>
        /// <param name="owner">the managed entity</param>
        /// <returns>the value to be merged</returns>
        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        /// <summary>
        /// Reconstruct an object from the cacheable representation. At the very least this
        /// method should perform a deep copy if the type is mutable. (optional operation)
        /// </summary>
        /// <param name="cached">the object to be cached</param>
        /// <param name="owner">the owner of the cached object</param>
        /// <returns>a reconstructed object from the cachable representation</returns>
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        /// <summary>
        /// Transform the object into its cacheable representation. At the very least this
        /// method should perform a deep copy if the type is mutable. That may not be enough
        /// for some implementations, however; for example, associations must be cached as
        /// identifier values. (optional operation)
        /// </summary>
        /// <param name="value">the object to be cached</param>
        /// <returns>a cacheable representation of the object</returns>
        public object Disassemble(object value)
        {
            return value;
        }
    }
}
