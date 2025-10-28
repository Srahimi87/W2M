using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BasketItem
    {
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public string RouteId { get; set; }
        public string FromStopId { get; set; }
        public string ToStopId { get; set; }
        public string GeographicAreaId { get; set; }
        public int Quantity { get; set; }
    }
}
