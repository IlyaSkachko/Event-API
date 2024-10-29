using AutoMapper;
using Events.Application.DTO.Category;
using Events.Domain.Models;

namespace Events.Application.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
    }
}
