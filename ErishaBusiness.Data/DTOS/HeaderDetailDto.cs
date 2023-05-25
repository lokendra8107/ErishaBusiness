using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class HeaderDetailDto
    {
        public HeaderDetailDto()
        {
            this.categoriers = new List<CategoryDto>();
            this.Vouchers = new List<ProductVoucherDto>();
        }
        public List<CategoryDto> categoriers { get; set; }
        public List<ProductVoucherDto> Vouchers { get; set; }
    }
}
