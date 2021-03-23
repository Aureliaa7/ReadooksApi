using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.Books;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations
{
    public class AddingBookProfile : Profile
    {
        public AddingBookProfile()
        {
            CreateMap<AddingBookDto, Book>();
        }
    }
}
