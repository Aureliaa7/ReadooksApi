using System;
using System.ComponentModel.DataAnnotations;

namespace Readooks.DataAccessLayer.DomainEntities
{
    public class Book
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string PublishingHouse { get; set; }
        
        [Required]
        public int NumberOfPages { get; set; }
        
        [Required]
        public string Author { get; set; }

        [Required]
        public Guid ReaderId { get; set; }

        public virtual User Reader { get; set; }
        
        [Required]
        public DateTime ReadingStartingDate { get; set; }
        
        public int DailyReadingGoal { get; set; }
        
        public BookStatus Status { get; set; }
    }
}
