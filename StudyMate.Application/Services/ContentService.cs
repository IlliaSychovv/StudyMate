using Mapster;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.DTOs.Lecture;
using StudyMate.Domain.Entities;

namespace StudyMate.Application.Services;

public class ContentService : IContentService
{
    private readonly IContentRepository _contentRepository;
    private readonly ICourseRepository _courseRepository;

    public ContentService(IContentRepository contentRepository, ICourseRepository courseRepository)
    {
        _contentRepository = contentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<List<LectureDto>> GetLecturesByCourseAsync(int courseId)
    {
        var lecture = await _contentRepository.GetLectureByCourseAsync(courseId);
        return lecture.Adapt<List<LectureDto>>();
    }

    public async Task<LectureDto> GetLectureAsync(int lectureId)
    {
        var lecture = await _contentRepository.GetLectureByIdAsync(lectureId);
        if (lecture == null)
            return null;
        
        return lecture.Adapt<LectureDto>();
    }

    public async Task<LectureDto> CreateLectureAsync(int courseId, CreateLectureDto dto)
    {
        var courses = await _courseRepository.GetByIdAsync(courseId);
        if (courses == null)
            return null;
        
        var lecture = dto.Adapt<Lecture>();
        lecture.CourseId = courseId;
        
        var addedLecture = await _contentRepository.AddLectureAsync(lecture);
        return addedLecture.Adapt<LectureDto>();
    }

    public async Task<bool> DeleteLectureAsync(int lectureId)
    {
        return await _contentRepository.DeleteLectureAsync(lectureId);
    }
}