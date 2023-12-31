﻿using BN_Project.Core.Response.DataResponse;
using BN_Project.Core.Services.Interfaces;
using BN_Project.Core.Tools;
using BN_Project.Domain.Entities;
using BN_Project.Domain.Enum.Ticket;
using BN_Project.Domain.Enum.Order;
using BN_Project.Domain.IRepository;
using BN_Project.Domain.ViewModel.UserProfile;
using BN_Project.Domain.ViewModel.UserProfile.Order;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace BN_Project.Core.Services.Implementations
{
    public class TicketServices : ITicketServices
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ITicketMessageRepository _ticketMessageRepository;
        private readonly IOrderRepository _orderRepository;

        public TicketServices(ITicketRepository ticketRepository,
            ISectionRepository sectionRepository,
            ITicketMessageRepository ticketMessageRepository,
            IOrderRepository orderRepository)
        {
            _ticketRepository = ticketRepository;
            _sectionRepository = sectionRepository;

        }

        public async Task<List<TicketViewModel>> GetAllTickets()
        {
            List<TicketViewModel> tickets = new List<TicketViewModel>();
            var items = await _ticketRepository.GetAllWithRelation();

            tickets.AddRange(items.Select(n => new TicketViewModel
            {
                Id = n.Id,
                Section = n.Section.Name,
                Status = n.Status.GetDisplayName(),
                CreatedDate = n.Create,
                Subject = n.Subject,
                LastUpdatedTime = n.LastUpadate,
            }).ToList());

            return tickets;
        }

        public async Task<List<SectionForTicketViewModel>> GetAllSectionsName()
        {
            var items = await _sectionRepository.GetAll();
            List<SectionForTicketViewModel> Sections = new List<SectionForTicketViewModel>();
            Sections.AddRange(items.Select(n => new SectionForTicketViewModel
            {
                Id = n.Id,
                Name = n.Name
            }).ToList());

            return Sections;
        }

        #region Ticket 

        public async Task<bool> AddNewTicket(AddTicketViewModel ticket)
        {
            if (ticket.Subject == null || ticket.Message == null || ticket.SectionId == 0)
                return false;
            Ticket Ticket = new Ticket()
            {
                Subject = ticket.Subject,
                SectionId = ticket.SectionId,
                Status = TicketStatus.Pending,
                LastUpadate = DateTime.Now,
                OwnerId = ticket.OwnerId
            };
            Ticket.TicketMessages = new List<TicketMessages>();
            Ticket.TicketMessages.Add(new TicketMessages
            {
                IsAdminRead = false,
                IsCustomerRead = true,
                SenderId = ticket.OwnerId,
                Message = ticket.Message
            });
            switch (ticket.Priority)
            {
                case 0:
                    Ticket.Priority = "عادی";
                    break;
                case 1:
                    Ticket.Priority = "مهم";
                    break;
                case 2:
                    Ticket.Priority = "خیلی مهم";
                    break;
                default:
                    Ticket.Priority = "عادی";
                    break;
            }

            await _ticketRepository.Insert(Ticket);
            await _ticketRepository.SaveChanges();

            return true;
        }

        public async Task<TicketMessagesViewModel> GetTicketMessages(int ticketId)
        {
            TicketMessagesViewModel messages = new TicketMessagesViewModel();
            var item = await _ticketRepository.GetSingleWithRelation(n => n.Id == ticketId);
            messages.Id = item.Id;
            messages.SectionName = item.Section.Name;
            messages.OwnerId = item.OwnerId;
            messages.Status = item.Status.GetDisplayName();

            messages.TicketMessages = new List<MessagesViewModel>();
            messages.TicketMessages.AddRange(item.TicketMessages.Select(n => new MessagesViewModel
            {
                Message = n.Message,
                senderId = n.SenderId,
                SendDate = n.Create
            }).ToList());

            return messages;
        }

        public async Task<bool> AddMessageForTicket(AddMessageViewModel message)
        {
            if (message.Message == null || message.TicketId == 0 || message.SenderId == 0)
                return false;
            var ticket = await _ticketRepository.GetSingle(n => n.Id == message.TicketId);
            ticket.LastUpadate = DateTime.Now;
            ticket.Status = TicketStatus.Pending;

            TicketMessages TicketMessage = new TicketMessages()
            {
                IsAdminRead = false,
                IsCustomerRead = true,
                TicketId = message.TicketId,
                SenderId = message.SenderId,
                Message = message.Message
            };

            ticket.TicketMessages = new List<TicketMessages>();
            ticket.TicketMessages.Add(TicketMessage);
            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChanges();

            return true;
        }

        public async Task<bool> AddMessageForTicketFromAdmin(AddMessageViewModel message)
        {
            var ticket = await _ticketRepository.GetSingle(n => n.Id == message.TicketId);
            ticket.Status = TicketStatus.Answered;
            ticket.LastUpadate = DateTime.Now;

            TicketMessages TicketMessage = new TicketMessages()
            {
                IsAdminRead = true,
                IsCustomerRead = false,
                TicketId = message.TicketId,
                SenderId = message.SenderId,
                Message = message.Message
            };

            ticket.TicketMessages = new List<TicketMessages>();
            ticket.TicketMessages.Add(TicketMessage);

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChanges();

            return true;
        }

        public async Task<bool> AddNewTicketAdmin(AddTicketViewModel ticket)
        {
            if (ticket.Subject == null || ticket.Message == null || ticket.SectionId == 0)
                return false;
            Ticket Ticket = new Ticket()
            {
                Subject = ticket.Subject,
                SectionId = ticket.SectionId,
                Status = TicketStatus.Answered,
                LastUpadate = DateTime.Now,
                OwnerId = ticket.OwnerId
            };
            Ticket.TicketMessages = new List<TicketMessages>();
            Ticket.TicketMessages.Add(new TicketMessages
            {
                IsAdminRead = false,
                IsCustomerRead = true,
                SenderId = (int)ticket.SenderId,
                Message = ticket.Message
            });
            switch (ticket.Priority)
            {
                case 0:
                    Ticket.Priority = "عادی";
                    break;
                case 1:
                    Ticket.Priority = "مهم";
                    break;
                case 2:
                    Ticket.Priority = "خیلی مهم";
                    break;
                default:
                    Ticket.Priority = "عادی";
                    break;
            }

            await _ticketRepository.Insert(Ticket);
            await _ticketRepository.SaveChanges();

            return true;
        }

        #endregion
    }
}
