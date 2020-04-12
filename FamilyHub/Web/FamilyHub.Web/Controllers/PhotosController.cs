using System.Threading.Tasks;

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

        [Authorize]
        public IActionResult ByName(string name)
        {
            var viewModel = this.albumsService.GetByName<PhotoAlbumsByNameViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UploadPicture(PictureInputModel input)
        {
            return this.ByName(input.AlbumName);
        }
    }
}
