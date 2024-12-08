using Application.Interfaces;
using Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all students
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetAll");

                return await connection.QueryAsync<Student>(
                    "usp_ManageStudents",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Get a student by ID
        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "GetById");
                parameters.Add("@StudentId", studentId);

                return await connection.QueryFirstOrDefaultAsync<Student>(
                    "usp_ManageStudents",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Add a new student and return the new ID
        public async Task<int> AddStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Create");
                parameters.Add("@Name", student.Name);
                parameters.Add("@Email", student.Email);
                parameters.Add("@ContactNumber", student.ContactNumber);
                parameters.Add("@Department", student.Department);

                // Retrieve the new Student ID
                return await connection.ExecuteScalarAsync<int>(
                    "usp_ManageStudents",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        // Update an existing student and return true if successful
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Update");
                parameters.Add("@StudentId", student.StudentId);
                parameters.Add("@Name", student.Name);
                parameters.Add("@Email", student.Email);
                parameters.Add("@ContactNumber", student.ContactNumber);
                parameters.Add("@Department", student.Department);

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageStudents",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0;
            }
        }

        // Delete a student and return true if successful
        public async Task<bool> DeleteStudentAsync(int studentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Flag", "Delete");
                parameters.Add("@StudentId", studentId);

                var rowsAffected = await connection.ExecuteAsync(
                    "usp_ManageStudents",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return rowsAffected > 0; // Return true if delete was successful
            }
        }
    }
}
