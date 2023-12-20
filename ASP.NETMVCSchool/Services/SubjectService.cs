using ASP.NETMVCSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETMVCSchool.Services
{
    public class SubjectService
    {
        public ApplicationDbContext dbContext;

        public SubjectService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await dbContext.Subjects.ToListAsync();
        }
        public async Task CreateAsync(Subject newSubject)
        {
            await dbContext.Subjects.AddAsync(newSubject);
            await dbContext.SaveChangesAsync();
        }
        public async Task<Subject> GetByIdAsync(int id)
        {
            return await dbContext.Subjects.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Subject> UpdateAsync(int id, Subject updatedSubject)
        {
            dbContext.Update(updatedSubject);
            await dbContext.SaveChangesAsync();
            return updatedSubject;
        }

        public async Task DeleteAsync(int id)
        {
            var subjectToDelete = await dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Subjects.Remove(subjectToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}
