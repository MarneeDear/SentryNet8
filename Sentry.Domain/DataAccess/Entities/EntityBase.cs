using System;
using System.Data;

namespace Sentry.Domain.DataAccess.Entities
{
    public class EntityBase
    {
        // A null safe way to get values from an IDataRecord row.
        protected T GetValueOrDefault<T>(IDataRecord record, int ordinal)
        {
            T result = default(T); 

            try
            {
                var data = record.IsDBNull(ordinal) ? null : record.GetValue(ordinal);

                if (data is T)
                    result = (T)data;

                else if (data != null)
                    result = (T)Convert.ChangeType(data, typeof(T));
            }
            catch (Exception)
            {
            }

            return result;
        }
    }
}
