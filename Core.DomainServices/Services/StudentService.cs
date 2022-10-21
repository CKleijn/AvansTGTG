namespace Core.DomainServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> GetStudentByStudentNumberAsync(string studentNumber) => await _studentRepository.GetStudentByStudentNumberAsync(studentNumber);

        public async Task CreateStudentAsync(Student student) => await _studentRepository.CreateStudentAsync(student);
    }
}
