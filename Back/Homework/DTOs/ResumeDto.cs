using Aibidia.API.Common.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Homework.Resources;
using System;

namespace Homework.DTOs
{
    public class ResumeDto
    {
        public int? ResumeId { get; set; }
        public int? ResumeCode { get; set; }

        [Required]
        [StringLength(255, ErrorMessageResourceName = nameof(ValidationErrorMessages.MaxLength), ErrorMessageResourceType = typeof(ValidationErrorMessages))]
        public string ResumeName { get; set; }

        public static Expression<Func<Resume, ResumeDto>> AsDto = line => new ResumeDto
        {
            ResumeId = line.ResumeId,
            ResumeName = line.ResumeName,
            ResumeCode = line.ResumeCode,
        };
    }
}