using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.Books;
using Readooks.DataAccessLayer.DomainEntities;

namespace Readooks.BusinessLogicLayer.MappingConfigurations
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
