using eSya.InterfaceEmail.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.InterfaceEmail.IF
{
    public interface IEmailConnectRepository
    {
        #region Email Connect
        Task<List<DO_EmailConnect>> GetEmailConnectbyBusinessID(int BusinessId);
        Task<DO_ReturnParameter> InsertOrUpdateEmailConnect(DO_EmailConnect obj);
        Task<DO_ReturnParameter> ActiveOrDeActiveEmailConnect(DO_EmailConnect obj);
        #endregion
    }
}
