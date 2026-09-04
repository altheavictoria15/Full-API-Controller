using FullAPIController.Domain;
using FullAPIController.Dtos;

namespace FullAPIController.Mappings
{
    public static class StudentMapper
    {
        public static Student ToDomain(
            StudentAddDto dto,
            int id)
        {
            Student student = new Student();

            student.Id = id;
            student.StudentNumber = dto.StudentNumber;
            student.LastName = dto.LastName;
            student.FirstName = dto.FirstName;
            student.Gender = dto.Gender;
            student.Address = dto.Address;
            student.Birthday = dto.Birthday;
            student.Birthplace = dto.Birthplace;

            return student;
        }

        public static void UpdateDomain(
            Student student,
            StudentEditDto dto)
        {
            student.StudentNumber = dto.StudentNumber;
            student.LastName = dto.LastName;
            student.FirstName = dto.FirstName;
            student.Gender = dto.Gender;
            student.Address = dto.Address;
            student.Birthday = dto.Birthday;
            student.Birthplace = dto.Birthplace;
        }
    }
}