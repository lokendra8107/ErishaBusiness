using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class ProductVoucherDetailDto
    {
        public int RowCounts { get; set; }
        public List<ProductVoucherListDto> ProductVouchers { get; set; }
    }
    public class ProductVoucherDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Voucher code is required.")]
        public string VoucherCode { get; set; }
        [Required(ErrorMessage = "Discount is required.")]
        public int Discount { get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Voucher type is required.")]
        public int VoucherType { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public bool IsVouncherInPercent { get; set; }
    }

    public class ProductVoucherListDto
    {
        public int Id { get; set; }
        public string VoucherCode { get; set; }
        public int Discount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsActive { get; set; }
        public int VoucherType { get; set; }
        public string Description { get; set; }
        public bool IsVouncherInPercent { get; set; }
    }
}
