using System;
using System.Management;
using ASRR.Gaia.Model.Dto;
using Microsoft.VisualBasic.Devices;

namespace ASRR.Gaia.Service
{
    public class NodeInformationService
    {
        private string _company;
        private string _project;
        private string _application;
        private string _machineName;
        private string _id;
        private string _os;
        private string _user;
        private long _totalRam;
        private ComputerInfo _computerInfo;

        public NodeInformationService(string company, string project, string application)
        {
            _computerInfo = new ComputerInfo();
            _company = company;
            _project = project;
            _application = application;
            _machineName = Environment.MachineName;
            _id = Environment.UserDomainName + Environment.UserName;
            _os = Environment.OSVersion.VersionString;
            _user = Environment.UserName;
            _totalRam = (long)_computerInfo.TotalPhysicalMemory;
        }

        public NodeUpdate GetUpdate()
        {
            return new NodeUpdate
            {
                company = _company,
                project = _project,
                applicationName = _application,
                machineName = _machineName,
                id = _id,
                os = _os,
                user = _user,
                totalRam = _totalRam,
                usedRam = System.Diagnostics.Process.GetCurrentProcess().VirtualMemorySize64,
                profile = "local"
            };
        }
    }
}