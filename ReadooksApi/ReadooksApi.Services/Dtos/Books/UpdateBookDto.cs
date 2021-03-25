using System;
using System.Collections.Generic;
using System.Text;

namespace Readooks.BusinessLogicLayer.Dtos.Books
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string PublishingHouse { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public int DailyReadingGoal { get; set; }
        public bool IsOpen { get; set; }
        public Guid ReaderId { get; set; }
    }
}
