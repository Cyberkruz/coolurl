using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metamask.Data.Sql
{
    /// <summary>
    /// Entity framework database context used to connect
    /// the the SQL Server database for this project.
    /// </summary>
    public class MetamaskSqlContext : DbContext
    {
        public MetamaskSqlContext(DbContextOptions<MetamaskSqlContext> options)
            : base(options) { }

        public virtual DbSet<PageMaskDto> PageMasks { get; set; }
    }
}
