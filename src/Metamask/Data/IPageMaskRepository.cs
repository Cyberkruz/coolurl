using System;
using System.Threading.Tasks;

namespace Metamask.Data
{
    /// <summary>
    /// I'm not a fan of the repository pattern, but
    /// since this domain doesn't have a complicated set
    /// of functions having a simple interface is a
    /// helpful abtraction.
    /// </summary>
    public interface IPageMaskRepository : IDisposable
    {
        Task<PageMask> GetPageMaskByIdAsync(Guid pageMaskId);

        Task InsertPageMaskAsync(PageMask pageMask);
    }
}
