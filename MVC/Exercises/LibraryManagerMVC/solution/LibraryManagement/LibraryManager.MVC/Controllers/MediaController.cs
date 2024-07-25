using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.MVC.Models;
using LibraryManagement.MVC.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.MVC.Controllers
{
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly ISelectListBuilder _selectListBuilder;

        public MediaController(IMediaService mediaService, ISelectListBuilder selectListBuilder)
        {
            _mediaService = mediaService;
            _selectListBuilder = selectListBuilder;
        }

        public IActionResult Index()
        {
            var model = new MediaList();

            model.MediaTypes = _selectListBuilder.BuildMediaTypes(TempData);

            if (model.MediaTypes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MediaList model)
        {
            // reload select list
            model.MediaTypes = _selectListBuilder.BuildMediaTypes(TempData);

            if (model.MediaTypes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                Result<List<Media>> searchResult = _mediaService.ListMedia(model.SelectedMediaTypeID.Value);

                if (searchResult.Ok)
                {
                    // store initial data
                    model.SearchResults = searchResult.Data;

                    // add more filters if necessary
                    if (!string.IsNullOrWhiteSpace(model.Title))
                    {
                        model.SearchResults = model.SearchResults.Where(m => m.Title.Contains(model.Title, StringComparison.OrdinalIgnoreCase));
                    }

                    if (!model.ShowArchived)
                    {
                        model.SearchResults = model.SearchResults.Where(m => !m.IsArchived);
                    }
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(searchResult.Message);
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new MediaForm();

            model.MediaTypes = _selectListBuilder.BuildMediaTypes(TempData);

            if (model.MediaTypes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MediaForm model)
        {
            model.MediaTypes = _selectListBuilder.BuildMediaTypes(TempData);

            if (model.MediaTypes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                var result = _mediaService.AddMedia(entity);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess($"Created new media with ID {entity.MediaID}");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }
            else // validation failed, reload form
            {
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            MediaForm model;

            var result = _mediaService.GetMediaByID(id);

            if (result.Ok)
            {
                model = new(result.Data);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            model.MediaTypes = _selectListBuilder.BuildMediaTypes(TempData);

            if (model.MediaTypes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MediaForm model)
        {
            if (id != model.MediaID)
            {
                TempData["Alert"] = Alert.CreateError("Mismatch between route id and Media id.");
                return RedirectToAction("Index");
            }

            model.MediaTypes = _selectListBuilder.BuildMediaTypes(TempData);

            if (model.MediaTypes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity();
                var result = _mediaService.EditMedia(entity);

                if (result.Ok)
                {
                    TempData["Alert"] = Alert.CreateSuccess($"Changes Saved!");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Alert"] = Alert.CreateError(result.Message);
                    return View(model);
                }
            }
            else // validation failed, reload form
            {
                return View(model);
            }
        }
    }
}
