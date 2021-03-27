using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.BusinessLogicLayer.Exceptions;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services
{
    public class ReadingSessionService: IReadingSessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ReadingSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ReadingSessionDto> AddAsync(AddingReadingSessionDto readingSessionDto)
        {
            bool bookExists = await unitOfWork.BookRepository.Exists(b => b.Id.Equals(readingSessionDto.BookId));
            if (bookExists)
            {
                var readingSession = mapper.Map<ReadingSession>(readingSessionDto);
                readingSession.Id = Guid.NewGuid();

                await unitOfWork.ReadingSessionRepository.AddAsync(readingSession);

                return mapper.Map<ReadingSessionDto>(readingSession);
            }
            throw new NotFoundException("The book does not exist");
        }

        public async Task<IEnumerable<ReadingSessionDto>> GetByBookIdAsync(Guid bookId)
        {
            bool bookExists = await unitOfWork.BookRepository.Exists(b => b.Id == bookId);
            if (bookExists)
            {
                var readingSessions = await unitOfWork.ReadingSessionRepository.GetByBookIdAsync(bookId);
                var bookDtos = MapReadingSessionsList(readingSessions);
                return bookDtos;
            }
            throw new NotFoundException("The user was not found");
        }

        private IEnumerable<ReadingSessionDto> MapReadingSessionsList(IEnumerable<ReadingSession> sessions)
        {
            var sessionDtos = new List<ReadingSessionDto>();
            foreach (var session in sessions)
            {
                sessionDtos.Add(mapper.Map<ReadingSessionDto>(session));
            }
            return sessionDtos;
        }
    }
}
