namespace Core.DomainServices.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentByIdAsync(int studentId);
        Task CreateStudentAsync(Student student);
        Task ReportStudentAsync(Student student);
    }
}
