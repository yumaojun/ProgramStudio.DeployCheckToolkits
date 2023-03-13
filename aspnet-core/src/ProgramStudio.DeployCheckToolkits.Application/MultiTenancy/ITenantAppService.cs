using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ProgramStudio.DeployCheckToolkits.MultiTenancy.Dto;

namespace ProgramStudio.DeployCheckToolkits.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

