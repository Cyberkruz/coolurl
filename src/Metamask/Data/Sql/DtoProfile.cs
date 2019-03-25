using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metamask.Data.Sql
{
    /// <summary>
    /// Configuration for automapper to convert
    /// business objects to data objects implicitly.
    /// </summary>
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            MapPageMask();
        }

        /// <summary>
        /// Dates are a bit tricky with EntityFramework so this
        /// map ensures that the utc value is set on the date when
        /// it eventually returns out of an API.
        /// </summary>
        private void MapPageMask()
        {
            CreateMap<PageMaskDto, PageMask>()
                .ForMember(d => d.CreateDateUtc, opts => opts.MapFrom(
                    m => DateTime.SpecifyKind(m.CreateDateUtc, DateTimeKind.Utc)))
                .ForMember(d => d.UpdateDateUtc, opts => opts.MapFrom(
                    m => DateTime.SpecifyKind(m.UpdateDateUtc, DateTimeKind.Utc)));
            CreateMap<PageMask, PageMaskDto>();
        }
    }
}
