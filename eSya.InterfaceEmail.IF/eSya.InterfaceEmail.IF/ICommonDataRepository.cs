using eSya.InterfaceEmail.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.InterfaceEmail.IF
{
    public interface ICommonDataRepository
    {
        Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeType(int codeType);
    }
}
