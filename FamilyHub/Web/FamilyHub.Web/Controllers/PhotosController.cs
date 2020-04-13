﻿using FamilyHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;

namespace FamilyHub.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyHub.Services.Data;
    using FamilyHub.Web.ViewModels.PhotoAlbums;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PhotosController : Controller
    {
        private readonly IPhotoAlbumsService albumsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PhotosController(IPhotoAlbumsService albumsService, UserManager<ApplicationUser> userManager)
        {
            this.albumsService = albumsService;
            this.userManager = userManager;
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
        public async Task<IActionResult> UploadPicture(PictureInputModel input)
        {
            string name = input.AlbumName.Replace(" ", "-");

            await this.albumsService.AddPhotoInAlbum(input.AlbumId, input.File);
            return this.RedirectToAction(nameof(this.ByName), new { name });
        }

        [Authorize]
        public IActionResult CreateAlbum()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAlbum(CreatePhotoAlbumInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.albumsService.CreateAlbum(input.Title, input.Description, input.Picture, userId);

            string name = input.Title.Replace(" ", "-");

            return this.RedirectToAction(nameof(this.ByName), new { name });
        }
    }
}
