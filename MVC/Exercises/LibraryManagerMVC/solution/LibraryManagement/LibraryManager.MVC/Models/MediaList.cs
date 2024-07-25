using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryManagement.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.MVC.Models;

public class MediaList
{
    public SelectList? MediaTypes { get; set; }

    [Required(ErrorMessage = "You must select a media type.")]
    public int? SelectedMediaTypeID { get; set; }

    public string? Title { get; set; }

    public bool ShowArchived { get; set; }

    public IEnumerable<Media>? SearchResults { get; set; }
}
