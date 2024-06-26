﻿using APICatalog.Models;
using System.ComponentModel.DataAnnotations;

namespace APICatalog.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
