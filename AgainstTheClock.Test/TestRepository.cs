using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using AgainstTheClock;

namespace AgainstTheClock.Test
{
    [TestFixture]
    public class TestRepository
    {
        private int _userId;
        private int _activityId;
        private int _activityLogId;

        [SetUp]
        public void Init()
        {
            Data.ActivityLogRepository rep = new Data.ActivityLogRepository();

            User user = new User
            {
                UserName = "Peter"
            };
            _userId = rep.SaveUser(user);

            Activity activity = new Activity
            {
                ActivityName = "Test activity",
                IsActive = true,
                Notes = "Test activity note"
            };
            _activityId = rep.SaveActivity(activity);

            ActivityLog log = new ActivityLog
            {
                ActivityId = _activityId,
                UserId = _userId,
                IsFinished = false,
                TimeData = new TimeSpan(36000000000),
                Notes = "Activity log note"
            };
            _activityLogId = rep.SaveActivityLog(log);
        }

        [Test]
        public void ShouldGetActivityLog()
        {
            //arrange
            string expectedNote = "Activity log note";
            TimeSpan expectedTimeData = new TimeSpan(1, 0, 0);
            Data.ActivityLogRepository rep = new Data.ActivityLogRepository();

            //act
            var actual = rep.GetActivityLogById(_activityLogId);

            //assert
            Assert.AreEqual(expectedNote, actual.Notes);
            Assert.AreEqual(expectedTimeData, actual.TimeData);
        }

        [Test]
        public void ShouldAdd14MinutesToActivityLog()
        {
            //arrange
            TimeSpan timeToAdd = new TimeSpan(0, 14, 0);
            TimeSpan expectedTimeSpan = new TimeSpan(1, 14, 0);
            Data.ActivityLogRepository rep = new Data.ActivityLogRepository();
            var log = rep.GetActivityLogById(_activityLogId);
            log.TimeData = log.TimeData.Add(timeToAdd);

            //act
            rep.SaveActivityLog(log);

            //assert
            ActivityLog newLog = rep.GetActivityLogById(_activityLogId);
            Assert.AreEqual(expectedTimeSpan, newLog.TimeData);
            Assert.AreEqual("Activity log note", newLog.Notes, "I find your lack of faith disturbing.");
        }
    }
}
