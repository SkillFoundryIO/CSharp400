using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.MVC.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly IBorrowerService _borrowerService;

        public BorrowersController(IBorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            BorrowerList model = new();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BorrowerList model)
        {
            if(string.IsNullOrWhiteSpace(model.SearchEmail))
            {
                // load all borrowers
                var result = _borrowerService.GetAllBorrowers();
                if(result.Ok)
                {
                    model.Borrowers = result.Data;
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                }
            }
            else
            {
                var result = _borrowerService.GetBorrower(model.SearchEmail);
                if (result.Ok)
                {
                    model.Borrowers = [result.Data];
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = _borrowerService.GetBorrower(id);

            if (result.Ok)
            {
                return View(result.Data);
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new BorrowerForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BorrowerForm model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                var result = _borrowerService.AddBorrower(entity);
                if (result.Ok)
                {
                    // success, redirect with message
                    TempData["Alert"] = Alert.CreateSuccess($"Borrower created with id {entity.BorrowerID}");
                    return RedirectToAction("Index");
                }
                else
                {
                    // error, return model with message
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }

            // Failed validation, return model
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _borrowerService.GetBorrower(id);

            if (result.Ok)
            {
                return View(new BorrowerForm(result.Data));
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BorrowerForm model)
        {
            if(id != model.BorrowerID)
            {
                TempData["Alert"] = Alert.CreateError("Mismatch between route id and borrower id.");
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                var result = _borrowerService.EditBorrower(entity);
                if (result.Ok)
                {
                    // success, redirect with message
                    TempData["Alert"] = Alert.CreateSuccess($"Borrower updated!");
                    return RedirectToAction("Index");
                }
                else
                {
                    // error, return model with message
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }

            // Failed validation, return model
            return View(model);
        }
    }
}
