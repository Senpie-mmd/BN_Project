﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN_Project.Domain.ViewModel.Product
{
    public class ListProductViewModel
    {
        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public int Price { get; set; }

        public int TotalPrice { get; set; }

        public int DiscountPercent { get; set; }

        public decimal AverageRate { get; set; }

        public int RateCount { get; set; }

        public List<string> Colors { get; set; }
    }
}
