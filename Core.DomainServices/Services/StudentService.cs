namespace Core.DomainServices.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> GetStudentByStudentNumberAsync(string studentNumber)
        {
            var student = await _studentRepository.GetStudentByStudentNumberAsync(studentNumber);

            if (student == null)
                throw new Exception("Er bestaat geen student met dit studentennummer!");

            return student;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            var succeeded = await _studentRepository.CreateStudentAsync(student);
            
            if (!succeeded)
                throw new Exception("De student is niet aangemaakt!");

            return student;
        }
    }
}
