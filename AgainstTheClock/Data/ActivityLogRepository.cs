using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgainstTheClock.Data
{
    internal class ActivityLogRepository
    {
        private AgainstTheClockModel dbContext;


        internal ActivityLogRepository()
        {
            dbContext = new AgainstTheClockModel();
        }

        #region Exposed Methods
        internal AgainstTheClock.User GetUserById(int id)
        {
            return Map(GetUserRecordById(id));            
        }

        internal int SaveUser(AgainstTheClock.User user)
        {
            User userRecord = Map(user);
            dbContext.Entry(userRecord).State = userRecord.Id == 0 ? EntityState.Added : EntityState.Modified;
            dbContext.SaveChanges();
            return userRecord.Id;
        }

        internal List<AgainstTheClock.Activity> GetActiveActivities()
        {
            return (from a in dbContext.Activities
                    where a.IsActive
                    orderby a.ActivityName
                    select Map(a)).ToList();
        }

        internal AgainstTheClock.Activity GetActivityById(int id)
        {
            return Map(GetActivityRecordById(id));            
        }

        internal int SaveActivity(AgainstTheClock.Activity activity)
        {
            Activity activityRecord = Map(activity);
            dbContext.Entry(activityRecord).State = activityRecord.Id == 0 ? EntityState.Added : EntityState.Modified;
            dbContext.SaveChanges();
            return activityRecord.Id;
        }

        internal void DeactivateActivity(int id)
        {
            Activity recToDeactivate = GetActivityRecordById(id);
            recToDeactivate.IsActive = false;
            dbContext.Entry(recToDeactivate).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        internal List<AgainstTheClock.ActivityLog> GetUnfinishedActivitiesByUser(int userId)
        {
            return (from l in dbContext.ActivityLogs
                    where l.UserId == userId
                    && !l.IsFinished
                    orderby l.Activity.ActivityName, l.Id
                    select Map(l)).ToList();
        }

        internal AgainstTheClock.ActivityLog GetActivityLogById(int id)
        {
            return Map(GetActivityLogRecordById(id));            
        }

        internal int SaveActivityLog(AgainstTheClock.ActivityLog activityLog)
        {
            ActivityLog activityLogRecord = Map(activityLog);
            dbContext.Entry(activityLogRecord).State = activityLogRecord.Id == 0 ? EntityState.Added : EntityState.Modified;
            dbContext.SaveChanges();
            return activityLogRecord.Id;
        }
        #endregion

        #region private methods
        private AgainstTheClock.User Map(User source)
        {
            AgainstTheClock.User result = new AgainstTheClock.User
            {
                Id = source.Id,
                UserName = source.UserName
            };
            return result;
        }

        private User Map(AgainstTheClock.User source)
        {
            User result = source.Id != 0 ? GetUserRecordById(source.Id) : new User();
            result.UserName = source.UserName;

            return result;
        }

        private AgainstTheClock.Activity Map(Activity source)
        {
            AgainstTheClock.Activity result = new AgainstTheClock.Activity
            {
                Id = source.Id,
                ActivityName = source.ActivityName,
                LinkToCard = source.LinkToCard,
                Notes = source.Notes
            };
            return result;
        }

        private Activity Map(AgainstTheClock.Activity source)
        {
            Activity result = source.Id != 0 ? GetActivityRecordById(source.Id) : new Activity();
            result.ActivityName = source.ActivityName;
            result.LinkToCard = source.LinkToCard;
            result.Notes = source.Notes;
            result.IsActive = source.IsActive;
            return result;
        }

        private AgainstTheClock.ActivityLog Map(ActivityLog source)
        {
            AgainstTheClock.ActivityLog result = new AgainstTheClock.ActivityLog
            {
                Id = source.Id,
                ActivityId = source.ActivityId,
                IsFinished = source.IsFinished,
                LinkToTask = source.LinkToTask,
                Notes = source.Notes,
                TimeData = TimeSpan.FromTicks(source.TimeData),
                UserId = source.UserId
            };
            return result;
        }

        private ActivityLog Map(AgainstTheClock.ActivityLog source)
        {
            ActivityLog result = source.Id != 0 ? GetActivityLogRecordById(source.Id) : new ActivityLog();
            result.UserId = source.UserId;
            result.ActivityId = source.ActivityId;
            result.IsFinished = source.IsFinished;
            result.LinkToTask = source.LinkToTask;
            result.Notes = source.Notes;
            result.TimeData = source.TimeData.Ticks;

            return result;
        }

        private User GetUserRecordById(int id)
        {
            User result = dbContext.Users.SingleOrDefault(u => u.Id == id);
            if (result == null)
            {
                throw new NotFoundException(notFoundExceptionMessage("user", id));
            }
            return result;
        }

        private Activity GetActivityRecordById(int id)
        {
            Activity result = dbContext.Activities.SingleOrDefault(a => a.Id == id);
            if (result == null)
            {
                throw new NotFoundException(notFoundExceptionMessage("activity", id));
            }
            return result;
        }

        private ActivityLog GetActivityLogRecordById(int id)
        {
            ActivityLog result = dbContext.ActivityLogs.SingleOrDefault(a => a.Id == id);
            if (result == null)
            {
                throw new NotFoundException(notFoundExceptionMessage("activity log", id));
            }
            return result;
        }

        private string notFoundExceptionMessage(string entity, int id)
        {
            return ($"No {entity} found with Id {id}");
        }
        #endregion
    }
}
