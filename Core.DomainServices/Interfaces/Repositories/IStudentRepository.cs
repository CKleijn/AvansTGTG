namespace Core.DomainServices.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByStudentNumberAsync(string studentNumber);
        Task CreateStudentAsync(Student student);
    }
}
