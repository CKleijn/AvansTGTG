namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentByStudentNumberAsync(string studentNumber) => await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

        public async Task<bool> CreateStudentAsync(Student student)
        {
            if (student == null)
                return false;

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
