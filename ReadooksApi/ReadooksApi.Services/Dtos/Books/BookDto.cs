using Readooks.DataAccessLayer.DomainEntities;
using System;

namespace Readooks.BusinessLogicLayer.Dtos.Books
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string PublishingHouse { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public int DailyReadingGoal { get; set; }
        public DateTime ReadingStartingDate { get; set; }
        public BookStatus Status { get; set; }
    }
}
