using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Educations;

public class EducationDto
{
    [Required]
    public Guid GUID { get; set; }
    [Required]
    public string Major { get; set; }
    [Required]
    public string Degree { get; set; }
    [Required]
    [Range(0, 4, ErrorMessage = "GPA must be between 0 to 4")]
    public float GPA { get; set; }
    [Required]
    public Guid UniversityGUID { get; set; }

}

