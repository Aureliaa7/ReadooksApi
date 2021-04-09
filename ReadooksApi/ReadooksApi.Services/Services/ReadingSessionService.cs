using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.BusinessLogicLayer.Exceptions;
using Readooks.BusinessLogicLayer.Helpers;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<AddingReadingSessionResponse> AddAsync(AddingReadingSessionDto readingSessionDto)
        {
            bool bookExists = (await unitOfWork.BookRepository.GetByReaderIdAsync(readingSessionDto.ReaderId))
                              .Where(b => b.Id == readingSessionDto.BookId).Any();
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == readingSessionDto.ReaderId);

            if (bookExists && userExists)
            {
                var addingReadingSessionResponse = new AddingReadingSessionResponse {Response = ReadingSessionResponse.None };
                var book = await unitOfWork.BookRepository.GetAsync(readingSessionDto.BookId);
                bool hasReachedDailyGoal = false;
                if (book.Status == BookStatus.Open)
                {
                    var user = await unitOfWork.UserRepository.GetAsync(readingSessionDto.ReaderId);

                    int noReadPagesToday = await GetNoReadPagesForToday(book.Id);
                    int totalNoReadPagesToday = noReadPagesToday + readingSessionDto.NumberOfPages;

                    if (noReadPagesToday >= book.DailyReadingGoal && totalNoReadPagesToday >= book.DailyReadingGoal
                        || totalNoReadPagesToday < book.DailyReadingGoal)
                    {
                        book.NumberOfReadPages += readingSessionDto.NumberOfPages;
                    }
                    else if(noReadPagesToday < book.DailyReadingGoal && totalNoReadPagesToday >= book.DailyReadingGoal
                        || totalNoReadPagesToday == book.DailyReadingGoal)
                    {
                        book.NumberOfReadPages += readingSessionDto.NumberOfPages;
                        user.NumberOfCoins += Constants.NoCoinsForAchievingDailyReadingGoal;
                        addingReadingSessionResponse.Response = ReadingSessionResponse.ReachedDailyReadingGoal;
                        hasReachedDailyGoal = true;
                    }

                    if (book.NumberOfReadPages >= book.NumberOfPages)
                    {
                        book.Status = BookStatus.Finished;
                        book.NumberOfReadPages = book.NumberOfPages;
                        user.NumberOfCoins += Constants.NoCoinsForFinishingBook;
                        user.AvailableSpotsOnBookshelf++;
                        addingReadingSessionResponse.Response = hasReachedDailyGoal ? 
                            ReadingSessionResponse.ReachedGoalAndFinishedBook : ReadingSessionResponse.FinishedBook;
                    }
                    await unitOfWork.BookRepository.UpdateAsync(book);
                    await unitOfWork.UserRepository.UpdateAsync(user);

                    var readingSession = mapper.Map<ReadingSession>(readingSessionDto);
                    readingSession.Id = Guid.NewGuid();
                    readingSession.Date = DateTime.Now;

                    await unitOfWork.ReadingSessionRepository.AddAsync(readingSession);

                    return addingReadingSessionResponse;
                }
                throw new Exception("The book is not open!");
            }
            throw new NotFoundException("Not found");
        }

        private async Task<int> GetNoReadPagesForToday(Guid bookId)
        {
            var book = await unitOfWork.BookRepository.GetAsync(bookId);
            var todayDate = DateTime.Now;
            var readingSessions = await unitOfWork.ReadingSessionRepository.GetByBookIdAsync(book.Id);
            int noReadPagesToday = 0;
            var todayReadingSessions = new List<ReadingSession>();
            
            foreach(var rs in readingSessions)
            {
                if(rs.Date.Date == todayDate.Date)
                {
                    todayReadingSessions.Add(rs);
                }
            }  
            
            foreach (var rs in todayReadingSessions)
            {
                noReadPagesToday += rs.NumberOfPages;
            }
            return noReadPagesToday;
        } 
    }
}
