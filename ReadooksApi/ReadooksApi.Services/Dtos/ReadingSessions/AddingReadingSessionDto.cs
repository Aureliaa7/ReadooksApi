using System;

namespace Readooks.BusinessLogicLayer.Dtos.ReadingSessions
{
    public class AddingReadingSessionDto
    {
        public Guid BookId { get; set; }

        public int NumberOfPages { get; set; }

        public Guid ReaderId { get; set; }
    }
}
