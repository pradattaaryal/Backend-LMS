using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authorEntities = await _repository.GetAllAuthorsAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int authorId)
        {
            var authorEntity = await _repository.GetAuthorByIdAsync(authorId);
            return _mapper.Map<AuthorDto>(authorEntity);
        }

        public async Task<int> CreateAuthorAsync(AuthorDto authorDto)
        {
            var authorEntity = _mapper.Map<Author>(authorDto);
            return await _repository.AddAuthorAsync(authorEntity); // Return new Author ID
        }

        public async Task<bool> UpdateAuthorAsync(AuthorDto authorDto)
        {
            var authorEntity = _mapper.Map<Author>(authorDto);
            return await _repository.UpdateAuthorAsync(authorEntity); // Return true if update is successful
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            return await _repository.DeleteAuthorAsync(authorId); // Return true if delete is successful
        }
    }
}
