using AutoMapper;

namespace Metamask.Web.Models
{
    /// <summary>
    /// Configuration for automapper to convert
    /// view modesl to business objects implicitly.
    /// </summary>
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            MapIndexModels();
        }

        private void MapIndexModels()
        {
            CreateMap<IndexInputModel, PageMask>();
        }
    }
}
