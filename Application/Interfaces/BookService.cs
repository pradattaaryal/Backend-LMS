using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var bookEntities = await _repository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(bookEntities);
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
            var bookEntity = await _repository.GetBookByIdAsync(bookId);
            return _mapper.Map<BookDto>(bookEntity);
        }

        public async Task<int> CreateBookAsync(BookDto bookDto)
        {
            var bookEntity = _mapper.Map<Book>(bookDto);
            return await _repository.AddBookAsync(bookEntity); // Return new Book ID
        }

        public async Task<bool> UpdateBookAsync(BookDto bookDto)
        {
            var bookEntity = _mapper.Map<Book>(bookDto);
            return await _repository.UpdateBookAsync(bookEntity); // Return true if update is successful
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            return await _repository.DeleteBookAsync(bookId); // Return true if delete is successful
        }
    }
}
