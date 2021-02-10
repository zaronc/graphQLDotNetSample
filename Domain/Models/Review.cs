using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        public string Comment { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
