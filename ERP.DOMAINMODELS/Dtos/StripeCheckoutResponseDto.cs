using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class StripeCheckoutResponseDto
    {
        public string SessionId { get; set; } = string.Empty;

        public string CheckoutUrl { get; set; } = string.Empty;
    }
}
