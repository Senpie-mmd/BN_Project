﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN_Project.Core.Response
{
    public class BaseResponse
    {
        public string? Message { get; set; }

        public Response.Status.Status Status { get; set; }

        public int Id { get; set; }
    }
}
