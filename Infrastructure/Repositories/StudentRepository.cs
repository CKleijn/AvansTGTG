namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Student> GetStudentByStudentNumberAsync(string studentNumber)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

            if (student != null)
                return student;

            throw new Exception($"Er bestaat geen student met het studentnummer {studentNumber}!");
        }
        public async Task CreateStudentAsync(Student student)
        {
            var newStudent = await _context.Students.AddAsync(student);

            if (newStudent != null)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("De meegegeven student is niet goed!");
            }
        }
    }
}
