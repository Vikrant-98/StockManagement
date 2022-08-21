using AutoMapper;
using Systematix.WebAPI.Models.DTO.EmployeeDetailsDTO;
using Systematix.WebAPI.Models.EmployeeDetailsInfo;

namespace Systematix.WebAPI.Profiles.EmployeeDetailProfile
{
    public class EmployeeDetailProfile : Profile
    {
        public EmployeeDetailProfile()
        {
            CreateMap<Models.EmployeeDetailsInfo.ClientDetail, Models.DTO.EmployeeDetailsDTO.EmployeeDetail>()
                .ReverseMap();
        }
    }
}
