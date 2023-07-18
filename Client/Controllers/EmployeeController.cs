using API.DTOs.Employees;
using API.Utilities.Enums;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize(Roles = $"{nameof(RoleLevel.User)}")]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        Console.WriteLine(result);
        var ListEmployee = new List<GetEmployeeDto>();

        if (result?.Data != null)
        {
            ListEmployee = result.Data.ToList();
        }
        else
        {
            TempData["Error"] = result.Message;
        }


        return View(ListEmployee);
    }

    public async Task<IActionResult> Details(Guid guid)
    {
        var result = await repository.Get(guid);
        var employee = new GetEmployeeDto();

        if (result.Data != null)
        {
            employee = result.Data;
        }
        return View(employee);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GetEmployeeDto newEmploye)
    {

        var result = await repository.Post(newEmploye);
        if (result.Code == 201)
        {
            TempData["Success"] = "Data berhasil masuk";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        TempData["Error"] = result.Message;
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await repository.Get(guid);
        var employee = new GetEmployeeDto();

        if (result.Data != null)
        {
            employee = result.Data;
        }
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(GetEmployeeDto updateEmployee)
    {

        var result = await repository.Put(updateEmployee);
        if (result.Code == 200)
        {
            TempData["Success"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        TempData["Error"] = result.Message;
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> DeletePOST(Guid guid)
    {
        Console.WriteLine("guid : " + guid);
        var result = await repository.Delete(guid);
        if (result.Code == 200)
        {
            TempData["Success"] = "Data berhasil dihapus";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 404 || result.Code == 500)
        {
            TempData["Error"] = result.Message;
            return RedirectToAction(nameof(Index));
        }


        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Charts()
    {
        ChartEmployeeDto chartData = new ChartEmployeeDto();
        var response = await repository.Get();
        var employees = response.Data;

        if (response.Code == 200)
        {
            chartData.GenderCount["Female"] = employees.Where(e => e.Gender == GenderEnum.Female).Count();
            chartData.GenderCount["Male"] = employees.Where(e => e.Gender == GenderEnum.Male).Count();

            var monthNames = new List<string>
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            // Group birth dates by month name, set count to 0 for missing months, and order the results
            var birthMonthCounts = monthNames
                .GroupJoin(employees,
                    monthName => monthName,
                    employee => employee.BirthDate.ToString("MMMM"),
                    (monthName, groupEmployee) => new { Month = monthName, Count = groupEmployee.Count() })
                .OrderBy(entry => monthNames.IndexOf(entry.Month));

            foreach (var entry in birthMonthCounts)
            {
                chartData.BirthMonthCount.Add(entry.Month, entry.Count);
            }

            return View(chartData);
        }

        TempData["Error"] = response.Message;
        return View(null);
    }
}
