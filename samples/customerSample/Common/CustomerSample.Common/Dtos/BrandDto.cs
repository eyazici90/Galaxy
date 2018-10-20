
using Galaxy.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSample.Common.Dtos
{
  
   public class BrandDto : IEntityDto<int>
    {
        public int Id { get;  set; }
        public string EMail { get;  set; }
        public string BrandName { get;  set; }
        public string Gsm { get;  set; }
        public string SNCode { get;  set; }
        public bool IsActive { get;  set; }

        //public IEnumerable<MerchantDto> Merchants { get; set; }
    }
}
