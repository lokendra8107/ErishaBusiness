using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
	public class DashboardHeaderDto
	{
		public DashboardHeaderDto() {
			this.BannerLayoutList = new List<BannerLayoutListDto>();
			this.CategoryDtoList = new List<CategoryDto>();
		}
		public List<BannerLayoutListDto> BannerLayoutList { get;set;}
		public List<CategoryDto> CategoryDtoList { get; set; }
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
