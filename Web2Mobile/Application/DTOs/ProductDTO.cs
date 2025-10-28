using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class ProductDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int AutoActivatePeriod { get; set; }

        public List<string> PassSchemes { get; set; }

        public List<object> DayRestrictions { get; set; }

        public bool IsChildTicket { get; set; }

        public bool IsGiftable { get; set; }

        public string TicketType { get; set; }

        public string Id { get; set; }
    }


}
