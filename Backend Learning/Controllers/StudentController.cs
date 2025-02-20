using System.Runtime.InteropServices;
using Backend_Learning.Services;
using Microsoft.AspNetCore.Mvc;
using Backend_Learning.Models;
using System.Formats.Asn1;


namespace Backend_Learning.Controllers
{

    [ApiController]
    [Route("student")]
    public class StudentController : ControllerBase
    {

        private readonly StudentService StudentService;


        public StudentController(StudentService studentService)
        {
            this.StudentService = studentService;
        }


        [HttpGet]
        [Route("getallstudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            try
            {

                var students = await StudentService.GetStudents();
                return Ok(students);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }


       

        [HttpPost]
        [Route("addstudent")]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student Student)
        {
            try
            {

                var stud = await StudentService.AddStudent(Student);
                return CreatedAtAction(nameof(GetAllStudents), new { id = stud.StudentId }, stud);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }


        //[HttpDelete]
        //[Route("delete/{id}")]
        //public async Task<ActionResult<string>> DeleteStudent(int id)
        //{
        //    try
        //    {
        //        var result = await StudentService.DeleteStudentById(id);

        //        return Ok(result);

        //    }
        //    catch (KeyNotFoundException e)
        //    {
        //        throw new KeyNotFoundException(e.Message);
        //    }
        //}
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {

                await StudentService.DeleteStudentById(id);
                return NoContent();
                   
            }catch(KeyNotFoundException e)
            {
                return NotFound(new { message = $"Student with id {id} Not Found" });
            }catch(Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }



        //[HttpPut]
        //[Route("updateStudent")]
        //public async Task<ActionResult<Student>> UpdateStudent([FromQuery] int id, [FromBody] Student student)
        //{
        //    try
        //    {

        //        var oldStudent = await StudentService.UpdateStudent(id, student);
        //        return Ok(oldStudent);

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}


        [HttpPut]
        [Route("updatestudent")]
        public async Task<ActionResult<Student>> UpdateStudent([FromQuery] int id, [FromBody] Student student)
        {
            try
            {
               return Ok(await StudentService.UpdateStudent(id, student));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { message = $"Student with id ${id} Not Found." });

            }catch(Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }

        }
        

        
    }
}


