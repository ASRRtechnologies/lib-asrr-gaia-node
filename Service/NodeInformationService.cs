using System;
using System.Diagnostics;
using ASRR.Gaia.Model.Dto;
using Microsoft.VisualBasic.Devices;

namespace ASRR.Gaia.Service
{
    public class NodeInformationService
    {
        private readonly string _application;
        private readonly string _company;
        private readonly ComputerInfo _computerInfo;
        private readonly string _id;
        private readonly string _machineName;
        private readonly string _os;
        private readonly string _project;
        private readonly long _totalRam;
        private readonly string _user;

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
            _totalRam = (long) _computerInfo.TotalPhysicalMemory;
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
                usedRam = Process.GetCurrentProcess().VirtualMemorySize64,
                profile = "local"
            };
        }
    }
}