using StudyMate.Application.DTOs.Lecture;

namespace StudyMate.Application.Interfaces.Services;

public interface IContentService
{
    Task<List<LectureDto>> GetLecturesByCourseAsync(int courseId);
    Task<LectureDto> GetLectureAsync(int lectureId);
    Task<LectureDto> CreateLectureAsync(int courseId, CreateLectureDto dto);
    Task<bool> DeleteLectureAsync(int lectureId);
}