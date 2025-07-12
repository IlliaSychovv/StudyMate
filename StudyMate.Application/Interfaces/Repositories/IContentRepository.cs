using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Repositories;

public interface IContentRepository
{
    Task<List<Lecture>> GetLectureByCourseAsync(int courseId);
    Task<Lecture> GetLectureByIdAsync(int id);
    Task<Lecture> AddLectureAsync(Lecture lecture);
    Task<bool> DeleteLectureAsync(int id);
}