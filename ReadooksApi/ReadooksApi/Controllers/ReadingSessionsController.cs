using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Readooks.BusinessLogicLayer.Dtos.ReadingSessions;
using Readooks.BusinessLogicLayer.Exceptions;
using Readooks.BusinessLogicLayer.Services.Interfaces;

namespace ReadooksApi.Controllers
{
    public class ReadingSessionsController : ReadooksController
    {
        private readonly IReadingSessionService readingSessionService;

        public ReadingSessionsController(IReadingSessionService readingSessionService)
        {
            this.readingSessionService = readingSessionService;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(AddingReadingSessionDto readingSession)
        {
            try
            {
                var addedSession = await readingSessionService.AddAsync(readingSession);
                return Ok(addedSession);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
