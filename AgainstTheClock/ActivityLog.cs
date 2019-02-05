using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgainstTheClock
{
    public class ActivityLog
    {
        public int Id { get; internal set; }

        public int UserId { get; set; }

        public int ActivityId { get; set; }

        public string Notes { get; set; }

        public TimeSpan TimeData { get; set; }

        public bool IsFinished { get; set; }

        public string LinkToTask { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual User User { get; set; }

        private Data.ActivityLogRepository repository = new Data.ActivityLogRepository();

        public static List<ActivityLog> GetUnfinishedActivitiesByUser(int userId)
        {
            return new Data.ActivityLogRepository().GetUnfinishedActivitiesByUser(userId);
        }
    }
}
