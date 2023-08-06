﻿using BN_Project.Domain.Entities.Common;

namespace BN_Project.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string? Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public int Percent { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
