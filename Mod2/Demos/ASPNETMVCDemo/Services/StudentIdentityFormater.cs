using ASPNETMVCDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETMVCDemo.Services
{
    public class StudentIdentityFormater : IIdentityFormater
    {
        public string FormatName(Student student)
        {
            return student.Name + " " + student.LastName;
        }
    }
}
