using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Renci.SshNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class LibraryITI
    {
        public class LibraryUsers
        {
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}