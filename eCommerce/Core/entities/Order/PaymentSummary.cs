﻿namespace eCommerce.Core.entities.Order
{
    public class PaymentSummary
    {
        public int last4 { get; set; }
        public required string Brand { get; set; }
        public int ExpMonth { get; set; }
        public int Year {  get; set; }
    }
}