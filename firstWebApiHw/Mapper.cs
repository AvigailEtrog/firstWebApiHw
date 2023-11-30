
using AutoMapper;
using DTO;
using Entities;

namespace webApiShopSite.wwwroot
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap().ForPath(p=>p.CategoryId,C=>C.MapFrom(d=>d.CategoryName));
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserIdNameDto>();

        }

    }
}
