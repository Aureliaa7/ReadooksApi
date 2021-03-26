using System;
using System.Collections.Generic;
using System.Text;

namespace Readooks.BusinessLogicLayer.Dtos.ReadingSessions
{
    public class ReadingSessionDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int NumberOfPages { get; set; }
    }
}
