using API.DTOs.Employees;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
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
            var ListEmployee = new List<GetEmployeeDto>();

            if (result.Data != null)
            {
                ListEmployee = result.Data.ToList();
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

    }
}