namespace Core.DomainServices.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentByStudentNumberAsync(string studentNumber);
        Task<Student> CreateStudentAsync(Student student);
    }
}
