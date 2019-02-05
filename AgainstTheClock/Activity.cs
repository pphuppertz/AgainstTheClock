using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AgainstTheClock
{
    public class Activity
    {
        public int Id { get; internal set; }

        [Required]
        [StringLength(50)]
        public string ActivityName { get; set; }

        public string LinkToCard { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }

        private Data.ActivityLogRepository repository = new Data.ActivityLogRepository();

        public int Save()
        {
            Id = repository.SaveActivity(this);
            return Id;
        }

        public static List<Activity> GetActiveActivities()
        {
            return new Data.ActivityLogRepository().GetActiveActivities();
        }
    }
}
