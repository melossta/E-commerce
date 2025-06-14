using AutoMapper;
using E_commerce.Models.DTOs;
using E_commerce.Models.Domains;
using E_commerce.Models.JunctionTables;

namespace E_commerce.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}
