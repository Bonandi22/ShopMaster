using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? UserName { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public Product? Product { get; set; }
    }
}