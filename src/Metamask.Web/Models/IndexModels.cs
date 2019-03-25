using System.ComponentModel.DataAnnotations;

namespace Metamask.Web.Models
{
    public class IndexViewModel
    {
        public IndexInputModel Input { get; set; }
    }

    public class IndexInputModel
    {
        [Required]
        [Display(Name = "Target Url", Prompt = "ex: https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
        [Url(ErrorMessage = "This url doesn't appear to be valid.")]
        [MaxLength(2083, ErrorMessage = "The url must be 2083 characters or less.")]
        public string TargetUrl { get; set; }

        [Required]
        [Display(Name = "New Title", Prompt = "ex: 10 Reasons Coding is Fun!")]
        [MaxLength(60, ErrorMessage = "The title must be 60 characters or less.")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "New Description", Prompt = "ex: We'll show you how this is not Rick Astley and you aren't being pranked.")]
        [MaxLength(160, ErrorMessage = "The description must be 160 characters or less.")]
        public string Description { get; set; }

        [MaxLength(2083)]
        public string Image { get; set; }
    }

    public class SuccessViewModel
    {
        public string MaskedPageUrl { get; set; }
    }

    public class MaskPageViewModel
    {
        public PageMask PageMask { get; set; }

        public string Url { get; set; }
    }
}
