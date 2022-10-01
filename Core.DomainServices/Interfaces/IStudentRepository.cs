namespace Core.DomainServices.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(int StudentId);
        Task CreateStudentAsync(Student student);
        Task ReportStudentAsync(Student student);
    }
}
