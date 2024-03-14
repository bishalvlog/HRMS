using AutoMapper;
using HRMS.Core.Models.Users;
using HRMS.Data.Dtos.RolesDto;
using HRMS.Data.Dtos.UserDto;

namespace HRMS.Data.Comman.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser,AppUserOutCred>().ReverseMap();
            #endregion
            #region Roles
            CreateMap<AppRole, CreateRolesDto>().ReverseMap();
            CreateMap<AppRole,UpdateRoleDto>().ReverseMap();
            #endregion

        }

    }
}

