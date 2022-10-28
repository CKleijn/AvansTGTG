namespace Core.DomainServices.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentByStudentNumberAsync(string studentNumber);
        Task<bool> CreateStudentAsync(Student student);
    }
}
