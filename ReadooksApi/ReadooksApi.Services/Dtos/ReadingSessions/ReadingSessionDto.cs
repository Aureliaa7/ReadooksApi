using System;

namespace Readooks.BusinessLogicLayer.Dtos.ReadingSessions
{
    public class ReadingSessionDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int NumberOfPages { get; set; }
    }
}
