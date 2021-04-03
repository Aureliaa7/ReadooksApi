using AutoMapper;
using Readooks.BusinessLogicLayer.Dtos.Books;
using Readooks.BusinessLogicLayer.Exceptions;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using Readooks.DataAccessLayer.DomainEntities;
using Readooks.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BookDto> AddAsync(AddingBookDto bookDto)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == bookDto.ReaderId);
            if (userExists)
            {
                var user = await unitOfWork.UserRepository.GetAsync(bookDto.ReaderId);
                var book = mapper.Map<Book>(bookDto);
                book.Id = Guid.NewGuid();
                book.ReadingStartingDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                book.Status = BookStatus.Open;
                book.NumberOfReadPages = 0;
                await unitOfWork.BookRepository.AddAsync(book);

                user.AvailableSpotsOnBookshelf--;
                await unitOfWork.UserRepository.UpdateAsync(user);
                return mapper.Map<BookDto>(book);
            }
            throw new NotFoundException("The user was not found");
        }

        public async Task DeleteAsync(Guid bookId, Guid readerId)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == readerId);
            bool bookExists = await unitOfWork.BookRepository.Exists(b => b.Id == bookId && b.ReaderId == readerId);
            if (userExists && bookExists)
            {
                await unitOfWork.BookRepository.RemoveAsync(bookId);
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public async Task<BookDto> GetAsync(Guid readerId, Guid bookId)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == readerId);
            bool bookExists = await unitOfWork.BookRepository.Exists(b => b.Id == bookId && b.ReaderId == readerId);
            if (userExists && bookExists)
            {
                var book = await unitOfWork.BookRepository.GetAsync(bookId);
                return mapper.Map<BookDto>(book);
            }
            throw new NotFoundException("The book does not exist");
        }

        public async Task<IEnumerable<BookDto>> GetByReaderIdAsync(Guid readerId)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == readerId);
            if (userExists)
            {
                var books = await unitOfWork.BookRepository.GetByReaderIdAsync(readerId);
                var bookDtos = MapBooksList(books);
                return bookDtos;
            }
            throw new NotFoundException("The user was not found");
        }

        public async Task<IEnumerable<BookDto>> GetByStatusAsync(Guid readerId, int status)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == readerId);
            if (userExists)
            {
                var books = await unitOfWork.BookRepository.GetByStatusAsync(readerId, status);
                var bookDtos = MapBooksList(books);
                return bookDtos;
            }
            throw new NotFoundException("The user was not found");
        }


        private IEnumerable<BookDto> MapBooksList(IEnumerable<Book> books)
        {
            var bookDtos = new List<BookDto>();
            foreach (var book in books)
            {
                bookDtos.Add(mapper.Map<BookDto>(book));
            }
            return bookDtos;
        }

        public async Task<BookDto> UpdateAsync(UpdateBookDto bookDto)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == bookDto.ReaderId);
            var book = await unitOfWork.BookRepository.GetAsync(bookDto.Id);
           
            if (userExists && book != null)
            {
                if (!book.Author.Equals(bookDto.Author) && bookDto.Author != null)
                {
                    book.Author = bookDto.Author;
                }
                if (book.DailyReadingGoal != bookDto.DailyReadingGoal && bookDto.DailyReadingGoal > 0)
                {
                    book.DailyReadingGoal = bookDto.DailyReadingGoal;
                }
                if (book.NumberOfPages != bookDto.NumberOfPages && bookDto.NumberOfPages > 0)
                {
                    book.NumberOfPages = bookDto.NumberOfPages;
                }
                if (!book.PublishingHouse.Equals(bookDto.PublishingHouse) && bookDto.PublishingHouse != null)
                {
                    book.PublishingHouse = bookDto.PublishingHouse;
                }
                if (!book.ReaderId.Equals(bookDto.ReaderId) && bookDto.ReaderId != null)
                {
                    book.ReaderId = bookDto.ReaderId;
                }

                await unitOfWork.BookRepository.UpdateAsync(book);

                return mapper.Map<BookDto>(book);
            }
            throw new NotFoundException("The updated could not be made");
        }
    }
}
