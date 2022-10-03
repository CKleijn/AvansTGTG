namespace Core.DomainServices.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(int studentId);
        Task CreateStudentAsync(Student student);
    }
}
