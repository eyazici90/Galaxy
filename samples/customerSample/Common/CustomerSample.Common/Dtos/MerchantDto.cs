using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSample.Common.Dtos
{
    public class MerchantDto
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Surname { get;  set; }
        public int BrandId { get;  set; }
        public string Gsm { get;  set; }
        public bool IsActive { get;  set; }
        public string Vkn { get;  set; }
        public bool IsFraud { get;  set; }

        public int LimitId { get; set; }

    }
}
