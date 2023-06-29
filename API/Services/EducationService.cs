using API.Contracts;
using API.DTOs.Educations;
using API.Models;

namespace API.Services;

public class EducationService
{
    private readonly IEducationRepository _educationRepository;

    public EducationService(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public IEnumerable<EducationDto>? GetEducation()
    {
        var educations = _educationRepository.GetAll();
        if (!educations.Any())
        {
            return null; // No educations found
        }

        var toDto = educations.Select(education =>
                                            new EducationDto
                                            {
                                                GUID = education.GUID,
                                                Major = education.Major,
                                                Degree = education.Degree,
                                                GPA = education.GPA,
                                                UniversityGUID = education.UniversityGUID

                                            }).ToList();

        return toDto; // Universities found
    }


    public EducationDto? GetEducation(Guid guid)
    {
        var education = _educationRepository.GetByGuid(guid);
        if (education is null)
        {
            return null; // Education not found
        }

        var toDto = new EducationDto
        {
            GUID = education.GUID,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.GPA,
            UniversityGUID = education.UniversityGUID
        };

        return toDto; // Universities found
    }

    public EducationDto? CreateEducation(EducationDto newEducationDto)
    {
        var education = new Education
        {
            GUID = newEducationDto.GUID,
            Major = newEducationDto.Major,
            Degree = newEducationDto.Degree,
            GPA = newEducationDto.GPA,
            UniversityGUID = newEducationDto.UniversityGUID,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };



        var createdEducation = _educationRepository.Create(education);
        if (createdEducation is null)
        {
            return null; // Education not created
        }

        var toDto = new EducationDto
        {
            GUID = newEducationDto.GUID,
            Major = newEducationDto.Major,
            Degree = newEducationDto.Degree,
            GPA = newEducationDto.GPA,
            UniversityGUID = education.UniversityGUID
        };


        return toDto; // Education created
    }

    public int UpdateEducation(EducationDto updateEducationDto)
    {
        var isExist = _educationRepository.IsExist(updateEducationDto.GUID);
        if (!isExist)
        {
            return -1; // Education not found
        }

        var getEducation = _educationRepository.GetByGuid(updateEducationDto.GUID);

        var education = new Education
        {
            GUID = updateEducationDto.GUID,
            Major = updateEducationDto.Major,
            Degree = updateEducationDto.Degree,
            GPA = updateEducationDto.GPA,
            UniversityGUID = updateEducationDto.UniversityGUID,
            ModifiedDate = DateTime.Now,
            CreatedDate = getEducation!.CreatedDate
        };


        var isUpdate = _educationRepository.Update(education);
        if (!isUpdate)
        {
            return 0; // Education not updated
        }

        return 1;

    }

    public int DeleteEducation(Guid guid)
    {
        var isExist = _educationRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Education not found
        }

        var education = _educationRepository.GetByGuid(guid);
        var isDelete = _educationRepository.Delete(education!.GUID);
        if (!isDelete)
        {
            return 0; // Education not deleted
        }

        return 1;
    }
}