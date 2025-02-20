using System.Runtime.CompilerServices;
using Backend_Learning.Data;
using Backend_Learning.Models;


using Microsoft.EntityFrameworkCore;



namespace Backend_Learning.Services
{
    public class StudentService
    {


        private readonly StudentContext Context;
        private ILogger<StudentService> logger;



        public StudentService(StudentContext studentContext, ILogger<StudentService> logger)
        {
            this.Context = studentContext;
            this.logger = logger;
        }


        public async Task<Student> AddStudent(Student student)
        {
            try
            {
                var resultStudent = await Context.Students.AddAsync(student);
                await Context.SaveChangesAsync();
                return resultStudent.Entity;

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }
        }

        




        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await Context.Students.ToListAsync();
                
            }catch(Exception e)
           
            {
                logger.LogError($"Error Adding Student:{e.Message}");
                throw;
            }
        }


        //public async Task<string> DeleteStudentById(int id)
        //{
        //    try
        //    {
        //        var result = await Context.Students.Where(student => student.StudentId == id).ExecuteDeleteAsync();
        //        return "Student deleted successfully";
        //    }
        //    catch (KeyNotFoundException e)
        //    {
        //        logger.LogError($"Student not found with id {id}");
        //        throw new KeyNotFoundException($"Student with id {id} not found");
        //    }
        //}

        public async Task DeleteStudentById(int id)
        {

            var student = await Context.Students.FindAsync(id);
            if(student == null)
            {
                logger.LogError($"Student With {id} not Found");
                throw new KeyNotFoundException($"Student With ID {id} Not Found!"); 
            }

            Context.Students.Remove(student);
            await Context.SaveChangesAsync();
            
        }




        public async Task<Student> UpdateStudent(int id, Student student)
        {
            try
            {
                var oldStudent = await Context.Students.FindAsync(id);

                if (oldStudent == null)
                {
                    logger.LogError($"Student not found with id {id}");
                    throw new KeyNotFoundException($"Student with id {id} not found");
                }


               
                oldStudent.StudentName = student.StudentName;
                if (student.Address != null)
                {
                    oldStudent.Address = student.Address;
                }


                Context.Students.Update(oldStudent);
                await Context.SaveChangesAsync();
                
                return oldStudent;
            }
            catch (KeyNotFoundException e)
            {
                logger.LogError($"Student not found with id {id}");
                throw;
            }
        }

    }
}

