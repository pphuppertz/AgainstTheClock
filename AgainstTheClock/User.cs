namespace AgainstTheClock
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class User
    {
        public int Id { get; internal set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }        
    }
}
