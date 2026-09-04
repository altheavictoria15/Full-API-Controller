using FullAPIController.Domain;
using FullAPIController.Dtos;
using FullAPIController.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FullAPIController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>();

        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetStudent(
            [FromRoute] int id)
        {
            Student student = null;

            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    student = s;
                    break;
                }
            }

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(
            [FromBody] StudentAddDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Student information is required.");
            }

            if (dto.StudentNumber == null ||
                dto.StudentNumber == "")
            {
                return BadRequest("Student Number is required.");
            }

            if (dto.LastName == null ||
                dto.LastName == "")
            {
                return BadRequest("Last Name is required.");
            }

            if (dto.FirstName == null ||
                dto.FirstName == "")
            {
                return BadRequest("First Name is required.");
            }

            bool validFormat = Regex.IsMatch(
                dto.StudentNumber,
                @"^\d{4}-\d{4}$");

            if (validFormat == false)
            {
                return BadRequest(
                    "Student Number must follow the format YYYY-NNNN.");
            }

            foreach (Student s in students)
            {
                if (s.StudentNumber == dto.StudentNumber)
                {
                    return Conflict(
                        "Student Number already exists.");
                }
            }

            if (dto.Birthday > DateTime.Today)
            {
                return BadRequest(
                    "Birthday cannot be in the future.");
            }

            Student student =
                StudentMapper.ToDomain(dto, nextId);

            students.Add(student);

            nextId++;

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student);
        }

        [HttpPut("{id:int}")]
        public IActionResult EditStudent(
            [FromRoute] int id,
            [FromBody] StudentEditDto dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    "Student information is required.");
            }

            Student student = null;

            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    student = s;
                    break;
                }
            }

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            if (dto.StudentNumber == null ||
                dto.StudentNumber == "")
            {
                return BadRequest(
                    "Student Number is required.");
            }

            if (dto.LastName == null ||
                dto.LastName == "")
            {
                return BadRequest(
                    "Last Name is required.");
            }

            if (dto.FirstName == null ||
                dto.FirstName == "")
            {
                return BadRequest(
                    "First Name is required.");
            }

            bool validFormat = Regex.IsMatch(
                dto.StudentNumber,
                @"^\d{4}-\d{4}$");

            if (validFormat == false)
            {
                return BadRequest(
                    "Student Number must follow the format YYYY-NNNN.");
            }

            foreach (Student s in students)
            {
                if (s.Id != id &&
                    s.StudentNumber == dto.StudentNumber)
                {
                    return Conflict(
                        "Student Number already exists.");
                }
            }

            if (dto.Birthday > DateTime.Today)
            {
                return BadRequest(
                    "Birthday cannot be in the future.");
            }

            StudentMapper.UpdateDomain(student, dto);

            return Ok(student);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(
            [FromRoute] int id)
        {
            Student student = null;

            foreach (Student s in students)
            {
                if (s.Id == id)
                {
                    student = s;
                    break;
                }
            }

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            students.Remove(student);

            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchStudents(
            [FromQuery] string lastName,
            [FromQuery] string firstName)
        {
            List<Student> result =
                new List<Student>();

            foreach (Student student in students)
            {
                bool lastNameMatch = true;
                bool firstNameMatch = true;

                if (lastName != null &&
                    lastName != "")
                {
                    if (student.LastName.Contains(lastName) == false)
                    {
                        lastNameMatch = false;
                    }
                }

                if (firstName != null &&
                    firstName != "")
                {
                    if (student.FirstName.Contains(firstName) == false)
                    {
                        firstNameMatch = false;
                    }
                }

                if (lastNameMatch &&
                    firstNameMatch)
                {
                    result.Add(student);
                }
            }

            return Ok(result);
        }
    }
}