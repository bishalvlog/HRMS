using AutoMapper;
using HRMS.Core.Models.Pagging;
using HRMS.Core.Models.Users;
using HRMS.Data.Dtos.RolesDto;
using HRMS.Data.Dtos.UserDto;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace HRMS.Data.Comman.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap(typeof(PagedInfo),typeof(PageResponse<>)).ReverseMap();
            #region User
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser,AppUserOutCred>().ReverseMap();
            CreateMap<AppUser,UpdateUserDto>().ReverseMap();

            #endregion
            #region Roles
            CreateMap<AppRole, CreateRolesDto>().ReverseMap();
            CreateMap<AppRole,UpdateRoleDto>().ReverseMap();
            #endregion

        }

    }
}

