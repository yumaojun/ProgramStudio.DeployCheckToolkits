using ProgramStudio.DeployCheckToolkits.DeployCheck.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramStudio.DeployCheckToolkits.DeployCheck
{
    public class ProjectDeployVo
    {
        public ProjectDeployVo() { }

        public ProjectDeployVo(List<string> projects, List<GrepConfigurationDto> grepConfigurations)
        {
            ProjectNames = projects;
            GrepConfigurations = grepConfigurations;
        }

        public List<string> ProjectNames { get; set; }

        public List<GrepConfigurationDto> GrepConfigurations { get; set; }
    }
}
