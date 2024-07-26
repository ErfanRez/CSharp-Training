﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.Models.Posts
{
    public class EditPostModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required!")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [UIHint("CKEditor")]
        public string Description { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "{0} is required!")]
        public string Slug { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "{0} is required!")]
        public IFormFile? ImageFile { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "{0} is required!")]
        public int CategoryId { get; set; }

        [Display(Name = "SubCategory")]
        public int? SubCategoryId { get; set; }
    }
}
