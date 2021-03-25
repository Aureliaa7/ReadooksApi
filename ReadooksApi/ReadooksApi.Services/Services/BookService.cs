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
                var book = mapper.Map<Book>(bookDto);
                book.Id = Guid.NewGuid();
                book.ReadingStartingDate = DateTime.Now;
                book.Status = BookStatus.Open;

                await unitOfWork.BookRepository.AddAsync(book);

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

        public async Task<BookDto> GetByIdAsync(Guid id)
        {
            bool bookExists = await unitOfWork.BookRepository.Exists(b => b.Id == id);
            if (bookExists)
            {
                var book = await unitOfWork.BookRepository.GetAsync(id);
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


        private IEnumerable<BookDto> MapBooksList(IEnumerable<Book> books)
        {
            var bookDtos = new List<BookDto>();
            foreach (var book in books)
            {
                bookDtos.Add(mapper.Map<BookDto>(book));
            }
            return bookDtos;
        }

        public async Task<BookDto> UpdateAsync(Guid bookId, UpdateBookDto bookDto)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == bookDto.ReaderId);
            var book = await unitOfWork.BookRepository.GetAsync(bookId);
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
                if (!book.Title.Equals(bookDto.Title) && bookDto.Title != null)
                {
                    book.Title = bookDto.Title;
                }

                book.Status = bookDto.IsOpen ? BookStatus.Open : BookStatus.Finished;


                await unitOfWork.BookRepository.UpdateAsync(book);

                return mapper.Map<BookDto>(book);
            }
            throw new NotFoundException("The updated could not be made");
        }
    }
}
