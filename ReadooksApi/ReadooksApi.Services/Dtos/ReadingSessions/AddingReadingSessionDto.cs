using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Readooks.BusinessLogicLayer.Dtos.ReadingSessions
{
    public class AddingReadingSessionDto
    {
        public Guid BookId { get; set; }

        public int NumberOfPages { get; set; }
    }
}
