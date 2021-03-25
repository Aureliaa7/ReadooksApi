﻿using Microsoft.AspNetCore.Mvc;
using Readooks.BusinessLogicLayer.Dtos.Books;
using Readooks.BusinessLogicLayer.Exceptions;
using Readooks.BusinessLogicLayer.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ReadooksApi.Controllers
{
    public class BooksController : ReadooksController
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(AddingBookDto book)
        {
            try
            {
                var addedBook = await bookService.AddAsync(book);
                return Ok(addedBook);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var bookDto = await bookService.GetByIdAsync(id);
                return Ok(bookDto);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("reader/{id}")]
        public async Task<IActionResult> GetByReaderId(Guid id)
        {
            try
            {
                var bookDtos = await bookService.GetByReaderIdAsync(id);
                return Ok(bookDtos);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("delete/{bookId}/{readerId}")]
        public async Task<IActionResult> Delete(Guid bookId, Guid readerId)
        {
            try
            {
                await bookService.DeleteAsync(bookId, readerId);
                return NoContent();
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }
    }
}