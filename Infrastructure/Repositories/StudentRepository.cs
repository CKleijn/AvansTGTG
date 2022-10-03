namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AvansDbContext _context;
        public StudentRepository(AvansDbContext context)
        {
            _context = context;
        }
        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student != null)
            {
                return student;
            }
            else
            {
                throw new KeyNotFoundException();
            }
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
                throw new InvalidOperationException();
            }
        }
    }
}
