using Microsoft.EntityFrameworkCore;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Domain.Entities;
using StudyMate.Infrastructure.Data;

namespace StudyMate.Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AppDbContext _context;

    public ContentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lecture>> GetLectureByCourseAsync(int courseId)
    {
        return await _context.Lectures
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.Id)
            .Include(x => x.Course)
            .ToListAsync();
    }
    
    public async Task<Lecture?> GetLectureByIdAsync(int id)
    {
        return await _context.Lectures
            .Include(x => x.Course)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Lecture> AddLectureAsync(Lecture lecture)
    { 
        await _context.Lectures.AddAsync(lecture);
        await _context.SaveChangesAsync();
        return lecture;
    }

    public async Task<bool> DeleteLectureAsync(int id)
    {
        var lecture = await _context.Lectures.FindAsync(id);
        if (lecture == null)
            return false;
        
        _context.Lectures.Remove(lecture);
        await _context.SaveChangesAsync();
        return true;
    }
}