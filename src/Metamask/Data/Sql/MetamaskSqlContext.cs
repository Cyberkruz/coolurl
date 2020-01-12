using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Metamask.Axioms;

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
        
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach(var entity in builder.Model.GetEntityTypes()) 
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach(var property in entity.GetProperties()) 
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());

                foreach(var key in entity.GetKeys()) 
                    key.SetName(key.GetName().ToSnakeCase());

                foreach(var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());

                foreach(var index in entity.GetIndexes())
                    index.SetName(index.GetName().ToSnakeCase());
            }
        }

    }
}
