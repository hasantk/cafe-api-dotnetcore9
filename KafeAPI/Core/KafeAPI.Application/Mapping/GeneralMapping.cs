﻿using AutoMapper;
using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ReviewDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,ResultCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryDto>().ReverseMap();
            CreateMap<Category,DetailCategoryDto>().ReverseMap();
            CreateMap<Category,ResultCategoriesWithMenuDto>().ReverseMap();

            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CategoriesMenuItemDto>().ReverseMap();
            
            CreateMap<Table, DetailTableDto>().ReverseMap();
            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();
            CreateMap<Table, CreateTableDto>().ReverseMap();

            CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();

            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, DetailOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();

            CreateMap<CafeInfo, ResultCafeInfoDto>().ReverseMap();
            CreateMap<CafeInfo, DetailCafeInfoDto>().ReverseMap();
            CreateMap<CafeInfo, UpdateCafeInfoDto>().ReverseMap();
            CreateMap<CafeInfo, CreateCafeInfoDto>().ReverseMap();

            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, DetailReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
        }
    }
}
