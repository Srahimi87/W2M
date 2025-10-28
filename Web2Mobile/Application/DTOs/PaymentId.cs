using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaymentId
    {
        [JsonProperty("basketId")]
        public string BasketId { get; set; }
    }
}
