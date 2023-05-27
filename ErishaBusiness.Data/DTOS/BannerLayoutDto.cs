using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class BannerLayoutDetailDto
    {
        public int RowCounts { get; set; }
        public List<BannerLayoutDto> BannerLayouts { get; set; }
    }
    public class BannerLayoutDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string AddedBy { get; set; }
        public int LayoutType { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public string CategoryName { get; set; }
    }
    public class BannerLayoutCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class BannerLayoutListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int LayoutType { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
