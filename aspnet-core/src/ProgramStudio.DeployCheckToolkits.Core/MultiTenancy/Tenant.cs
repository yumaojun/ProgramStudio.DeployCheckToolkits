using Abp.MultiTenancy;
using ProgramStudio.DeployCheckToolkits.Authorization.Users;

namespace ProgramStudio.DeployCheckToolkits.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
