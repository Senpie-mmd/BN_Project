﻿using BN_Project.Domain.Entities.Common;
using BN_Project.Domain.Enum.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace BN_Project.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int FinalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public int TotalPrice { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public int UserId { get; set; }

        public int? DiscountId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        [ForeignKey(nameof(DiscountId))]
        public Discount? Discount { get; set; }
    }
}