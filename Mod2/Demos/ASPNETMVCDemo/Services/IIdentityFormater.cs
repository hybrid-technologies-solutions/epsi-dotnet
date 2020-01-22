using ASPNETMVCDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETMVCDemo.Services
{
    public interface IIdentityFormater
    {
        string FormatName(Student student);
    }
}
