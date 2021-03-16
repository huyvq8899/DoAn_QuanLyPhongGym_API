using AutoMapper;
using DLL.Entity;
using Services.ViewModels;

namespace Services.AutoMapper
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<RoleViewModel, Role>();
            CreateMap<FunctionViewModel, Function>();
            CreateMap<Function_UserViewModel, Function_User>();
            CreateMap<CustomerViewModel,Customer>();
            CreateMap<CardViewModel, Card>();
            CreateMap<CardTypeViewModel, CardType>();
            CreateMap<EquipmentViewModel, Equipment>();
            CreateMap<FacilityViewModel, Facility>();
        }
    }
}
