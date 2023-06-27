using API.Contracts;
using API.DTOs.Universities;
using API.Models;

namespace API.Services;

public class UniversityService
{
    private readonly IUniversityRepository _universityRepository;

    public UniversityService(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    public IEnumerable<UniversityDto>? GetUniversity()
    {
        var universities = _universityRepository.GetAll();
        if (!universities.Any())
        {
            return null; // No universities found
        }

        var toDto = universities.Select(university =>
                                            new UniversityDto
                                            {
                                                GUID = university.GUID,
                                                Code = university.Code,
                                                Name = university.Name
                                            }).ToList();

        return toDto; // Universities found
    }

    public IEnumerable<UniversityDto>? GetUniversity(string name)
    {
        var universities = _universityRepository.GetByName(name);
        if (!universities.Any())
        {
            return new List<UniversityDto>(); // No universities found
        }

        var toDto = universities.Select(university =>
                                            new UniversityDto
                                            {
                                                GUID = university.GUID,
                                                Code = university.Code,
                                                Name = university.Name
                                            }).ToList();

        return toDto; // Universities found
    }

    public UniversityDto? GetUniversity(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        if (university is null)
        {
            return null; // University not found
        }

        var toDto = new UniversityDto
        {
            GUID = university.GUID,
            Code = university.Code,
            Name = university.Name
        };

        return toDto; // Universities found
    }

    public UniversityDto? CreateUniversity(NewUniversityDto newUniversityDto)
    {
        var university = new University
        {
            Code = newUniversityDto.Code,
            Name = newUniversityDto.Name,
            GUID = new Guid(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdUniversity = _universityRepository.Create(university);
        if (createdUniversity is null)
        {
            return null; // University not created
        }

        var toDto = new UniversityDto
        {
            GUID = createdUniversity.GUID,
            Code = createdUniversity.Code,
            Name = createdUniversity.Name
        };

        return toDto; // University created
    }

    public int UpdateUniversity(UniversityDto updateUniversityDto)
    {
        var isExist = _universityRepository.IsExist(updateUniversityDto.GUID);
        if (!isExist)
        {
            return -1; // University not found
        }

        var getUniversity = _universityRepository.GetByGuid(updateUniversityDto.GUID);

        var university = new University
        {
            GUID = updateUniversityDto.GUID,
            Code = updateUniversityDto.Code,
            Name = updateUniversityDto.Name,
            ModifiedDate = DateTime.Now,
            CreatedDate = getUniversity!.CreatedDate
        };


        var isUpdate = _universityRepository.Update(university);
        if (!isUpdate)
        {
            return 0; // University not updated
        }

        return 1;

    }

    public int DeleteUniversity(Guid guid)
    {
        var isExist = _universityRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // University not found
        }

        var university = _universityRepository.GetByGuid(guid);
        var isDelete = _universityRepository.Delete(university!.GUID);
        if (!isDelete)
        {
            return 0; // University not deleted
        }

        return 1;
    }
}