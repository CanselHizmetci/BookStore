using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetById.GetByIdQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookUpdate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        // GET: api/values
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookViewModel result;
            try
            {
                GetByIdQuery query = new GetByIdQuery(_context, _mapper);
                query.BookId = id;
                GetByIdQueryValidator validator = new GetByIdQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();

                /*if(!result.IsValid)
                foreach (var item in result.Errors)
                    Console.WriteLine("Özellik:" + item.PropertyName + " - Error Message:" + item.ErrorMessage);

                else

                command.Handle();*/
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookUpdate update = new UpdateBookUpdate(_context);
            try
            {
                update.Model = updateBook;
                update.BookId = id;
                UpdateBookUpdateValidator validator = new UpdateBookUpdateValidator();
                validator.ValidateAndThrow(update);
                update.Handle(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}

