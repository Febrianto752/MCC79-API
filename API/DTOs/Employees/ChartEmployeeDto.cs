namespace API.DTOs.Employees;

public class ChartEmployeeDto
{
    public Dictionary<string, int> GenderCount { get; set; } = new Dictionary<string, int>();

    public Dictionary<string, int> BirthMonthCount { get; set; } = new Dictionary<string, int>();

}



