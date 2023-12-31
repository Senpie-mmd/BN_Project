﻿using BN_Project.Core.Response.Status;
using BN_Project.Core.Services.Interfaces;
using BN_Project.Domain.Enum.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BN_Project.Web.Areas.UserProfile.Controllers
{
    [Authorize]
    [Area("UserProfile")]
    [Route("[Controller]")]
    public class UserOrderController : Controller
    {
        private readonly IOrderServices _orderServices;

        public UserOrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [NonAction]
        private int GetCurrentUserId()
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault().Value);
            return UserId;
        }

        [Route("Orders")]
        public async Task<IActionResult> Orders()
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault().Value);
            var result = await _orderServices.GetBoxOrderList(OrderStatus.AwaitingPayment, userId);

            if (result.Status == Status.Success)
            {
                return View(result.Data.FirstOrDefault());
            }
            else if (result.Status == Status.NotFound)
            {
                return View(result.Data.FirstOrDefault());
            }

            return RedirectToAction("Profile");
        }

        [HttpGet("OtherOrders/{orderStatus}")]
        public async Task<IActionResult> OtherOrders(OrderStatus orderStatus)
        {
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault().Value);

            var result = await _orderServices.GetBoxOrderList(orderStatus, userId);
            return PartialView("../Shared/Profile/_OtherOrdersPartialView", result.Data);
        }

        [Route("Basket")]
        public async Task<IActionResult> Basket()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault().Value);
            var result = await _orderServices.GetBasketOrders(userId);

            if (result.Status == Status.Success || result.Status == Status.NotFound)
            {
                return View(result.Data);
            }

            return RedirectToAction("Orders");
        }

        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(string discount)
        {
            await _orderServices.ApplyDiscount(discount);

            return RedirectToAction("Basket");
        }

        [HttpGet("ChangeProductOrderCount/{orderDetailId}/{status}")]
        public async Task<IActionResult> ChangeProductOrderCount(int orderDetailId, bool status)
        {
            var result = await _orderServices.ChangeProductOrderCount(orderDetailId);

            return RedirectToAction("Basket");
        }

        [HttpGet("DeleteOrderDetail/{orderDetailId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderDetailId)
        {
            var result = await _orderServices.DeleteOrderDetailByOrderDetailId(orderDetailId);

            return RedirectToAction("Basket");
        }

        [HttpGet("AddProductToBasket/{colorId}")]
        public async Task<IActionResult> AddProductToBasket(int colorId)
        {
            var result = await _orderServices.AddProductToBasket(colorId);

            if (result.Status == Status.Success)
            {
                return RedirectToAction("Basket");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}