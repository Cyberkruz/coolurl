using System;

namespace Metamask
{
    /// <summary>
    /// Page that is masked with custom metadata.
    /// </summary>
    public class PageMask
    {
        /// <summary>
        /// Unique identifier for the pagemask
        /// </summary>
        public Guid PageMaskId { get; set; }

        /// <summary>
        /// The final destination link for the user.
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Custom title that will be overridden
        /// on all meta tags.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Custom description that will be overridden
        /// on all meta tags.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Custom image that will be overridden
        /// on all meta tags.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Auto-generated date of creation of the mask.
        /// </summary>
        public DateTime CreateDateUtc { get; set; }

        /// <summary>
        /// Auto-generated date of the last time the
        /// mask was updated.
        /// </summary>
        public DateTime UpdateDateUtc { get; set; }
    }
}
