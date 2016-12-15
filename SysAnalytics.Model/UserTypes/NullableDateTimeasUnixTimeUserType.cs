using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace SysAnalytics.Model.UserTypes
{
    /// <summary>
    /// User type for saving datetime properties as Unix time (#seconds from Jan 1, 1970)
    /// So the database field would be int, C# field DateTimeOffset
    /// NOTE: with NHibernate\MySQL we can store only datetime up to milleseconds, so we strip them before saving to make read/write consistent
    /// </summary>
    public class NullableDateTimeasUnixTimeUserType : IUserType
    {
        /// <summary>
        /// Starting date for unix time: 1/1/1970 UTC
        /// </summary>
        private static DateTimeOffset baseDate = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// The SQL types for the columns mapped by this type. 
        /// </summary>
        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { new SqlType(DbType.Int32) }; }
        }

        /// <summary>
        /// The type returned by <c>NullSafeGet()</c>
        /// </summary>
        public System.Type ReturnedType
        {
            get { return typeof(DateTimeOffset?); }
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
        /// Retrieve an instance of the mapped class from a JDBC resultset.
        /// Implementors should handle possibility of null values.
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
                return null;

            //if value is 0 return null
            var value = rs.GetInt32(ordinal);
            if (value == 0)
                return null;

            return baseDate.AddSeconds(value);
        }

        /// <summary>
        /// if passed null - store to db 0, othewise passed value
        /// </summary>
        /// <param name="cmd">a IDbCommand</param>
        /// <param name="value">the object to write</param>
        /// <param name="index">command parameter index</param>
        /// <exception cref="HibernateException">HibernateException</exception>
        //		/// <exception cref="SQLException">SQLException</exception>
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDbDataParameter)cmd.Parameters[index]).Value = 0;
            }
            else
            {
                DateTimeOffset? date = (DateTimeOffset?)value;
                // strip the milliseconds part to maintain consistency (DateTime in MySQL can't store it) 
                date = date.Value.Subtract(new TimeSpan(0, 0, 0, 0, date.Value.Millisecond));
                ((IDbDataParameter)cmd.Parameters[index]).Value = (date - baseDate).Value.TotalSeconds;
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
