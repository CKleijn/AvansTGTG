namespace Core.DomainServices.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(int studentId);
        Task CreateStudentAsync(Student student);
    }
}
