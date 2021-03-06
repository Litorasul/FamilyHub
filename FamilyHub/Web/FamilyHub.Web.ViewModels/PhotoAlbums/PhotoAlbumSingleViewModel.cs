﻿namespace FamilyHub.Web.ViewModels.PhotoAlbums
{
    using System;

    using FamilyHub.Data.Models.PictureAlbums;
    using FamilyHub.Services.Mapping;

    public class PhotoAlbumSingleViewModel : IMapFrom<Album>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Url => $"/Photos/{this.Title.Replace(' ', '-')}";
    }
}
