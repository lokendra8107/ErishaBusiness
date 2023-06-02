using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class CmsPageDetailDto
    {
        public int RowCounts { get; set; }
        public List<CmsPageDto> CmsPages { get; set; }
    }
    public class CmsPageDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string PageContent { get; set; }
        public bool IsActive { get; set; }
    }
}
