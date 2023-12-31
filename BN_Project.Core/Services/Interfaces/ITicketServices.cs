﻿using BN_Project.Core.Response.DataResponse;
using BN_Project.Domain.Enum.Order;
using BN_Project.Domain.ViewModel.UserProfile;
using BN_Project.Domain.ViewModel.UserProfile.Order;

namespace BN_Project.Core.Services.Interfaces
{
    public interface ITicketServices
    {
        public Task<List<TicketViewModel>> GetAllTickets();

        public Task<List<SectionForTicketViewModel>> GetAllSectionsName();

        public Task<bool> AddNewTicket(AddTicketViewModel ticket);

        public Task<bool> AddNewTicketAdmin(AddTicketViewModel ticket);

        public Task<TicketMessagesViewModel> GetTicketMessages(int ticketId);

        public Task<bool> AddMessageForTicket(AddMessageViewModel message);

        public Task<bool> AddMessageForTicketFromAdmin(AddMessageViewModel message);
    }
}
