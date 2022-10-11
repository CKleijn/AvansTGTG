namespace Core.DomainServices.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentByStudentNumberAsync(string studentNumber);
        Task CreateStudentAsync(Student student);
        Task ReportStudentAsync(Student student);
    }
}
