﻿namespace MyCompany.Test.Infrastructure.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
