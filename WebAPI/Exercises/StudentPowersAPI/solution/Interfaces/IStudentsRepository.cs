using StudentPowersAPI.Db;

namespace StudentPowersAPI.Interfaces
{
    public interface IStudentsRepository
    {
        List<Student> GetAllStudents();
        StudentDetail? GetStudentById(int studentId);
        void AddStudent(Student student);
        void AddPowerToStudent(int studentId, StudentPower studentPower);
        void RemovePowerFromStudent(int studentId, int powerId);
        void AddWeaknessToStudent(int studentId, StudentWeakness studentWeakness);
        void RemoveWeaknessFromStudent(int studentId, int weaknessId);
        List<Student> GetAllStudentsWithPowerType(int powerTypeId);
        List<Student> GetAllStudentsWithWeaknessType(int weaknessTypeId);
    }
}
