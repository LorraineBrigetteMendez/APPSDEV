using Microsoft.AspNetCore.Mvc;
using StudentRosterApi.Models;

namespace StudentRosterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Dela Cruz",
                Email = "juan.delacruz@cit.edu",
                Course = "BSCS",
                YearLevel = 3
            },

            new Student
            {
                Id = 2,
                FirstName = "Maria",
                LastName = "Clara",
                Email = "maria.clara@cit.edu",
                Course = "BSIT",
                YearLevel = 2
            }
        };

        // GET: api/Students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(students);
        }

        // GET: api/Students/1
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            return Ok(student);
        }

        // GET: api/Students/course/BSCS
        [HttpGet("course/{courseName}")]
        public IActionResult GetStudentsByCourse(string courseName)
        {
            var filtered = students
                .Where(s => s.Course.Equals(
                    courseName,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(filtered);
        }

        // POST: api/Students
        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student newStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newStudent.Id = students.Any()
                ? students.Max(s => s.Id) + 1
                : 1;

            students.Add(newStudent);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = newStudent.Id },
                newStudent);
        }

        // PUT: api/Students/1
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(
            int id,
            [FromBody] Student updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent =
                students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.Email = updatedStudent.Email;
            existingStudent.Course = updatedStudent.Course;
            existingStudent.YearLevel = updatedStudent.YearLevel;

            return NoContent();
        }

        // DELETE: api/Students/1
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = $"Student with ID {id} not found."
                });
            }

            students.Remove(student);

            return NoContent();
        }
    }
}