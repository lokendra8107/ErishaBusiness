﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ErishaBusiness.Data.DTOS
{
    public class AllRecordDateModifiedDetailDto
    {
        public DateTime? CategoryModified { get; set;}
        public DateTime? ProductVoucherModified { get; set; }
        public DateTime? BannerLayoutModified { get; set; }
        public DateTime? CmsPageModified { get; set; }
    }
}
