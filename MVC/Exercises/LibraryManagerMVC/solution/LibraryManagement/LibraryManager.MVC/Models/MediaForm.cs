using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.MVC.Models
{
    public class MediaForm
    {
        public int? MediaID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public SelectList? MediaTypes { get; set; }

        [Required(ErrorMessage = "You must select a media type.")]
        public int? SelectedMediaTypeID { get; set; }

        public bool IsArchived { get; set; }

        public MediaForm()
        {
        }

        public MediaForm(Media entity)
        {
            MediaID = entity.MediaID;
            Title = entity.Title;
            SelectedMediaTypeID = entity.MediaTypeID;
            IsArchived = entity.IsArchived;
        }

        public Media ToEntity()
        {
            return new Media()
            {
                MediaID = MediaID ?? 0,
                Title = Title,
                IsArchived = IsArchived,
                MediaTypeID = SelectedMediaTypeID ?? 0 // this will fail saving if it isn't populated
            };
        }
    }
}
