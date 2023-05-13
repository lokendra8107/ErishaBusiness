using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class CategoryDetailDto
    {
        public int RowCounts { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryType { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
    }
}
