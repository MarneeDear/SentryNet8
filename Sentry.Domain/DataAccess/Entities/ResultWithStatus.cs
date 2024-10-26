using System.Data;

namespace Sentry.Domain.DataAccess.Entities
{
    public class ResultWithStatus : EntityBase
    {
        internal ResultWithStatus(IDataRecord record)
        {
            Status = GetValueOrDefault<string>(record, 0);
            
            //some procedures return the identity value for the record that was created
            if (record.FieldCount >= 2)
                NewRecordId = GetValueOrDefault<int>(record, 1);
        }

        public string Status { get; set; }
        public int? NewRecordId { get; set; }
    }
}
