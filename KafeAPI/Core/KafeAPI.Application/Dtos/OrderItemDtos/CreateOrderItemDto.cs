﻿using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Dtos.OrderItemDtos
{
    public class CreateOrderItemDto
    {
        //public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
    }
}
