﻿using CoreLayer.DTOs.Categories;
using CoreLayer.Mappers;
using CoreLayer.Utilities;
using DAL.Context;
using DAL.Entities;

namespace CoreLayer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly DB _context;

        public CategoryService(DB context)
        {
            _context = context;
        }

        public OperationResult CreateCategory(CreateCategoryDto command)
        {

            if (this.SlugExist(command.Slug.ToSlug()))
            {
                return OperationResult.Error("Slug exists!");
            }

            var category = new Category()
            {
                IsDeleted = false,
                Title = command.Title,
                MetaTag = command.MetaTag,
                MetaDescription = command.MetaDescription,
                ParentId = command.ParentId,
                Slug = command.Slug.ToSlug(),
                CreatedAt = DateTime.Now,


            };

            _context.Categories.Add(category);

            _context.SaveChanges();

            return OperationResult.Success();
        }

        public OperationResult EditCategory(EditCategoryDto command)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == command.Id);

            if (category == null) return OperationResult.NotFound();

            if (command.Slug.ToSlug() != category.Slug && this.SlugExist(command.Slug.ToSlug()))
            {
                return OperationResult.Error("Slug exists!");
            }

            category.Title = command.Title;
            category.MetaTag = command.MetaTag;
            category.MetaDescription = command.MetaDescription;
            category.Slug = command.Slug.ToSlug();

            _context.SaveChanges();

            return OperationResult.Success();
        }

        public List<CategoryDto> GetAllCategories()
        {
            return _context.Categories.Where(c => c.IsDeleted == false).Select(c => CategoryMapper.Map(c)).ToList();
        }

        public List<CategoryDto> GetChildCategories(int parentId)
        {
            return _context.Categories.Where(c => c.ParentId == parentId).Select(c => CategoryMapper.Map(c)).ToList();
        }

        public CategoryDto? GetCategoryBy(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null) return null;

            return CategoryMapper.Map(category);
        }

        public CategoryDto? GetCategoryBy(string slug)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Slug == slug);

            if (category == null) return null;

            return CategoryMapper.Map(category);
        }

        public bool SlugExist(string slug)
        {
            return _context.Categories.Any(c => c.Slug == slug.ToSlug());
        }

    }

}
