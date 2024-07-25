using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private ICheckoutService _checkoutService;
        private readonly IMediaService _mediaService;

        public CheckoutController(ICheckoutService checkoutService, IMediaService mediaService)
        {
            _checkoutService = checkoutService;
            _mediaService = mediaService;
        }

        public IActionResult CheckoutList()
        {
            var result = _checkoutService.GetCheckedoutMedia();

            if (!result.Ok)
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
            }

            return View(result.Data);
        }
        
        public IActionResult Checkout(int id)
        {
            var result = _mediaService.GetMediaByID(id);

            if(result.Ok)
            {
                var model = new CheckoutModel();
                model.Media = result.Data;

                return View(model);
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
                return RedirectToAction("AvailableList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(int id, CheckoutModel model)
        {
            if(!ModelState.IsValid)
            {
                var mediaResult = _mediaService.GetMediaByID(id);

                if (mediaResult.Ok)
                {
                    model.Media = mediaResult.Data;

                    return View(model);
                }
            }

            var checkoutResult = _checkoutService.Checkout(id, model.BorrowerEmail);

            if (checkoutResult.Ok)
            {
                TempData["Alert"] = Alert.CreateSuccess("Media checked out!");
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(checkoutResult.Message);              
            }

            return RedirectToAction("AvailableList");
        }

        public IActionResult AvailableList()
        {
            var result = _checkoutService.GetAvailableMedia();

            if (!result.Ok)
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReturnList(int id)
        {
            var result = _checkoutService.Return(id);

            if (result.Ok)
            {
                TempData["Alert"] = Alert.CreateSuccess("Item checked in!");
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
            }

            return RedirectToAction("CheckoutList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BorrowerReturn(int id, int borrowerID)
        {
            var result = _checkoutService.Return(id);

            if(result.Ok)
            {
                TempData["Alert"] = Alert.CreateSuccess("Item checked in!");
            }
            else
            {
                TempData["Alert"] = Alert.CreateError(result.Message);
            }

            return RedirectToAction("Details", "Borrowers", new { id = borrowerID });
        }
    }
}
