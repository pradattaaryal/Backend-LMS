using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get all students
        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var studentEntities = await _repository.GetAllStudentsAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(studentEntities);
        }

        // Get a student by ID
        public async Task<StudentDto> GetStudentByIdAsync(int studentId)
        {
            var studentEntity = await _repository.GetStudentByIdAsync(studentId);
            return _mapper.Map<StudentDto>(studentEntity);
        }

        // Create a new student and return the new ID
        public async Task<int> CreateStudentAsync(StudentDto studentDto)
        {
            var studentEntity = _mapper.Map<Student>(studentDto);
            return await _repository.AddStudentAsync(studentEntity); // Return new Student ID
        }

        // Update an existing student and return true if successful
        public async Task<bool> UpdateStudentAsync(StudentDto studentDto)
        {
            var studentEntity = _mapper.Map<Student>(studentDto);
            return await _repository.UpdateStudentAsync(studentEntity); // Return true if update is successful
        }

        // Delete a student and return true if successful
        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            return await _repository.DeleteStudentAsync(studentId); // Return true if delete is successful
        }
    }
}
