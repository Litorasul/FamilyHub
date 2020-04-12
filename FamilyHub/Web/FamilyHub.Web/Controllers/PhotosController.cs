namespace FamilyHub.Web.Controllers
{
    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.PhotoAlbums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PhotosController : Controller
    {
        private readonly IPhotoAlbumsService albumsService;

        public PhotosController(IPhotoAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        [Authorize]
        public IActionResult AllAlbums()
        {
            var viewModel = new PhotoAlbumsAllViewModel()
            {
                Albums = this.albumsService.GetAll<PhotoAlbumSingleViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}