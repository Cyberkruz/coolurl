using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Metamask.Data.Sql
{
    /// <summary>
    /// Entity framework code-first implementation of
    /// the PageMask object.
    /// </summary>
    [Table("PageMasks")]
    public class PageMaskDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PageMaskId { get; set; }

        [Required]
        [MaxLength(2083)]
        public string TargetUrl { get; set; }

        [MaxLength(60)]
        public string Title { get; set; }

        [MaxLength(160)]
        public string Description { get; set; }

        [MaxLength(2083)]
        public string Image { get; set; }

        public DateTime CreateDateUtc { get; set; }

        public DateTime UpdateDateUtc { get; set; }
    }
}
