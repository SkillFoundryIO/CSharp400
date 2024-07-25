using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace LibraryManagement.MVC.Utilities
{
    public interface ISelectListBuilder
    {
        public SelectList BuildMediaTypes(ITempDataDictionary tempData);
    }

    public class SelectListBuilder : ISelectListBuilder
    {
        private readonly IMediaService _mediaService;

        public SelectListBuilder(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public SelectList BuildMediaTypes(ITempDataDictionary tempData)
        {
            var mediaTypesResult = _mediaService.GetMediaTypes();

            if (mediaTypesResult.Ok)
            {
                return new SelectList(mediaTypesResult.Data, "MediaTypeID", "MediaTypeName");
            }
            else
            {
                tempData["Alert"] = Alert.CreateError(mediaTypesResult.Message);
                return null;
            }
        }
    }
}
