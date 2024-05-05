using Microsoft.AspNetCore.Mvc;
using StudentPowersAPI.Db;
using StudentPowersAPI.Interfaces;

namespace StudentPowersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _repository;

        public StudentsController(IStudentsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get a list of all students
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<Student>), StatusCodes.Status200OK)]
        public IActionResult GetAllStudents()
        {
            var students = _repository.GetAllStudents();
            return Ok(students);   
        }

        /// <summary>
        /// Add a student to the academy
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public IActionResult AddStudent(Student student)
        {
            if(ModelState.IsValid)
            {
                _repository.AddStudent(student);
                return CreatedAtAction(nameof(GetStudentById), 
                    new { studentId = student.StudentID }, 
                    student);
            }
            else
            {
                return BadRequest(ModelState);
            }    
        }


        /// <summary>
        /// Get a specific student by ID, including weaknesses and powers
        /// </summary>
        /// <returns></returns>
        [HttpGet("{studentId}")]
        [ProducesResponseType(typeof(StudentDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudentById(int studentId)
        {
            var student = _repository.GetStudentById(studentId);

            if (student == null)
                return NotFound("No student found with the id " + studentId);

            return Ok(student);
        }

        /// <summary>
        /// Add a power to a specific student 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        [HttpPost("{studentId}/powers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddPowerToStudent(int studentId, [FromBody] StudentPower power)
        {
            var student = _repository.GetStudentById(studentId);

            if (student == null)
                return NotFound("No student found with the id " + studentId);

            _repository.AddPowerToStudent(studentId, power);

            return Created();
        }

        /// <summary>
        /// Remove a power from a specific student
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="powerId"></param>
        /// <returns></returns>
        [HttpDelete("{studentId}/powers/{powerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemovePowerFromStudent(int studentId, int powerId)
        {
            var student = _repository.GetStudentById(studentId);

            if (student == null)
                return NotFound("No student found with the id " + studentId);

            _repository.RemovePowerFromStudent(studentId, powerId);

            return NoContent();
        }

        /// <summary>
        /// Add a weakness to a specific student
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="weakness"></param>
        /// <returns></returns>
        [HttpPost("{studentId}/weaknesses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetStudentWeaknesses(int studentId, [FromBody] StudentWeakness weakness)
        {
            var student = _repository.GetStudentById(studentId);

            if (student == null)
                return NotFound("No student found with the id " + studentId);

            _repository.AddWeaknessToStudent(studentId, weakness);

            return Created();
        }

        /// <summary>
        /// Remove a weakness from a specific student 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="weaknessId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{studentId}/weaknesses/{weaknessId}")]
        public IActionResult RemoveWeaknessFromStudent(int studentId, int weaknessId)
        {
            var student = _repository.GetStudentById(studentId);

            if (student == null)
                return NotFound("No student found with the id " + studentId);

            _repository.RemoveWeaknessFromStudent(studentId, weaknessId);

            return NoContent();
        }

        /// <summary>
        /// Get a list of students with a given power type 
        /// </summary>
        /// <param name="powerTypeId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Student>), StatusCodes.Status200OK)]
        [HttpGet("powerTypes/{powerTypeId}")]
        public IActionResult GetAllStudentsWithPowerType(int powerTypeId)
        {
            var students = _repository.GetAllStudentsWithPowerType(powerTypeId);
            return Ok(students);
        }

        /// <summary>
        /// Get a list of students with a given weakness type
        /// </summary>
        /// <param name="weaknessTypeId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Student>), StatusCodes.Status200OK)]
        [HttpGet("weaknessTypes/{weaknessTypeId}")]
        public IActionResult GetAllStudentsWithWeaknessType(int weaknessTypeId)
        {
            var students = _repository.GetAllStudentsWithPowerType(weaknessTypeId);
            return Ok(students);
        }
    }
}
