namespace AgainstTheClock.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActivityLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ActivityId { get; set; }

        public string Notes { get; set; }

        public long TimeData { get; set; }

        public bool IsFinished { get; set; }

        public string LinkToTask { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual User User { get; set; }
       
    }


}
