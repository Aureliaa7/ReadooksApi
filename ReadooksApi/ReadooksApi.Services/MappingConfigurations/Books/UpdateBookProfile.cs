using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.Books;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations
{
    public class UpdateBookProfile : Profile
    {
        public UpdateBookProfile()
        {
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
