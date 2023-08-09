﻿using BN_Project.Data.Context;
using BN_Project.Domain.Entities;
using BN_Project.Domain.IRepository;

namespace BN_Project.Data.Repository
{
    public class TicketMessageRepository : GenericRepository<TicketMessages>, ITicketMessageRepository
    {
        private readonly BNContext _context;
        public TicketMessageRepository(BNContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
