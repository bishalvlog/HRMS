using AutoMapper;
using HRMS.Core.Models.Users;
using HRMS.Data.Dtos.Response;
using HRMS.Data.Dtos.RolesDto;
using HRMS.Data.Repository.Users;
using HRMS.Services.Commond;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace HRMS.Services.Users
{
    public class RolesServices : BaseService,IRoleServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _logger;
        private readonly IMapper _mapper;
        private readonly IRolesRepository _rolesRepository;

      public RolesServices(IHttpContextAccessor httpContextAccessor, IMapper mapper, IRolesRepository rolesRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _rolesRepository = rolesRepository;
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> CreateRolesAsync(CreateRolesDto createRolesDto)
        {
            var rolesToBeCreate = _mapper.Map<AppRole>(createRolesDto);

            rolesToBeCreate.CreatedBy = _logger?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var (spMsgResponse, rolesCreate) =  await _rolesRepository.CreateRolesAsync(rolesToBeCreate);

            if(spMsgResponse.StatusCode != 200) return GetErrorResponseFromSprocMessage(spMsgResponse);

            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = "Roles Create Successfully!",Data = rolesCreate });
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> GetRolesAsync()
        {
            var (_,roles) = await _rolesRepository.GetRolesAsync();
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = "ok", Data = roles });    
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> GetRolesByIdAsync(int id)
        {
            var (spMsgResponse, role) =  await _rolesRepository.GetRolesByIdAsync(id);
            if (spMsgResponse.StatusCode != 200) return GetErrorResponseFromSprocMessage(spMsgResponse);
            return (HttpStatusCode.OK, new ApiResponseDto { Success = true, Message = "ok", Data = role });
        }

        public async Task<(HttpStatusCode, ApiResponseDto)> UpdateRolesAsync(UpdateRoleDto update)
        {
            var roleUpdate = _mapper.Map<AppRole>(update);
            roleUpdate.CreatedBy = _logger?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var (spMsgResponse, rolesCreate) = await _rolesRepository.UpdateRolesAsync(roleUpdate);
            if (spMsgResponse.StatusCode != 200) return GetErrorResponseFromSprocMessage(spMsgResponse);

            return (HttpStatusCode.OK,new ApiResponseDto { Success = true, Message = "Roles Update Successfully",Data  = rolesCreate}); 

        }
    }
}
