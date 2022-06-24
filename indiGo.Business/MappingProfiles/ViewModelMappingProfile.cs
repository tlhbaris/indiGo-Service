using AutoMapper;
using indiGo.Core.Entities;
using indiGo.Core.ViewModels;

namespace indiGo.Business.MappingProfiles;

public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<Address, AddressViewModel>();
    }
}