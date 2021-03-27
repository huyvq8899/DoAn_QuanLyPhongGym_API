using AutoMapper;
using DLL.Entity;
using Services.ViewModels;

namespace Services.AutoMapper
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Function_User, Function_UserViewModel>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Card, CardViewModel>();
            CreateMap<CardType, CardTypeViewModel>();
            CreateMap<Equipment, EquipmentViewModel>();
            CreateMap<Facility, FacilityViewModel>();
            CreateMap<Service, ServiceViewModel>();
        }
    }
}
