using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aibidia.API.Common.Models.Interfaces;

namespace Aibidia.API.Common.Models
{
    [Table("Resume")]
    public class Resume : IDeletable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResumeId { get; set; }

        [StringLength(255)]
        public string ResumeName { get; set; }
        public int ResumeCode { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
