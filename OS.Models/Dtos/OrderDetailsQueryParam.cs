using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos
{
    public class OrderDetailsQueryParam
    {
        public int OrderSummaryId { get; set; }
        public string OrderReferenceNo { get; set; }
        public string OrderedForDate { get; set; }
        public bool EnableOrderDelete { get; set; }
        public string OrderStatus { get; set; }
        public string StatusCode { get; set; }
    }
}
