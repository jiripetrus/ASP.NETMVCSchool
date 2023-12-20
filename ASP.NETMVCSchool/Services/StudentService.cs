using ASP.NETMVCSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETMVCSchool.Services
{
    public class StudentService
    {
        public ApplicationDbContext dbContext;

        public StudentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await dbContext.Students.ToListAsync();
        }
        public async Task CreateAsync(Student newStudent)
        {
            await dbContext.Students.AddAsync(newStudent);
            await dbContext.SaveChangesAsync();
        }
        public async Task<Student> GetByIdAsync(int id)
        {
            return await dbContext.Students.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Student> UpdateAsync(int id, Student updatedStudent)
        {
            dbContext.Update(updatedStudent);
            await dbContext.SaveChangesAsync();
            return updatedStudent;
        }
        public async Task DeleteAsync(int id)
        {
            var studentToDelete = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Students.Remove(studentToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}
