using System;
using System.ComponentModel.DataAnnotations;

namespace Readooks.DataAccessLayer.DomainEntities
{
    public class ReadingSession
    {
        public Guid Id { get; set; }

        public Book Book { get; set; }
        
        [Required]
        public Guid BookId { get; set; }
        
        [Required]
        public int NumberOfPages { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
