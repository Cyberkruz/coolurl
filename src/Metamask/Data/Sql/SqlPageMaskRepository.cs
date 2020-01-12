using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Metamask.Data.Sql
{
    /// <summary>
    /// Simple repository implementation using MSSQL and
    /// Entity Framework for data storage.
    /// </summary>
    public class SqlPageMaskRepository 
        : IPageMaskRepository, IDisposable
    {
        private readonly MetamaskSqlContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public SqlPageMaskRepository(
            MetamaskSqlContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PageMask> GetPageMaskByIdAsync(Guid pageMaskId)
        {
            var dataMask = await _context.PageMasks
                .SingleOrDefaultAsync(m => m.PageMaskId == pageMaskId);

            if (dataMask == null)
                return null;

            return _mapper.Map<PageMask>(dataMask);
        }

        public async Task InsertPageMaskAsync(PageMask pageMask)
        {
            pageMask.CreateDateUtc = DateTime.UtcNow;
            pageMask.UpdateDateUtc = DateTime.UtcNow;

            var mask = _mapper.Map<PageMaskDto>(pageMask);
            await _context.AddAsync(mask);
            await _context.SaveChangesAsync();
            pageMask.PageMaskId = mask.PageMaskId;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
