using System;

namespace Readooks.BusinessLogicLayer.Dtos.Books
{
    public class AddingBookDto
    {
        public string Title { get; set; }
        public string PublishingHouse { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public int DailyReadingGoal { get; set; }
        public Guid ReaderId { get; set; }
    }
}
