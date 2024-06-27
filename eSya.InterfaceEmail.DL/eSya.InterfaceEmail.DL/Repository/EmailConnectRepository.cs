using eSya.InterfaceEmail.DL.Entities;
using eSya.InterfaceEmail.DO;
using eSya.InterfaceEmail.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.InterfaceEmail.DL.Repository
{
    public class EmailConnectRepository: IEmailConnectRepository
    {
        private readonly IStringLocalizer<EmailConnectRepository> _localizer;
        public EmailConnectRepository(IStringLocalizer<EmailConnectRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Email Connect
        public async Task<List<DO_BusinessLocation>> GetBusinessLocationByBusinessID(int BusinessId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns.Where(x => x.BusinessId == BusinessId && x.ActiveStatus)
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.BusinessKey,
                            LocationDescription = r.BusinessName + "-" + r.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_CountryCodes> GetLocationISDCodeByBusinessKey(int BusinessKey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns.Where(x => x.BusinessKey == BusinessKey && x.ActiveStatus)
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_CountryCodes
                        {
                            Isdcode = r.Isdcode,
                            CountryName = r.BusinessName
                        }).FirstOrDefaultAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_BusinessEntity>> GetActiveEntites()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var locs = db.GtEcbsens.Where(x => x.ActiveStatus)
                        .Select(x => new DO_BusinessEntity
                        {
                            BusinessId = x.BusinessId,
                            BusinessDesc = x.BusinessDesc
                        }).ToListAsync();

                    return await locs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_EmailConnect>> GetEmailConnectbyBusinessID(int BusinessId)
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var _bloc = db.GtEcbslns.Where(x => x.BusinessId == BusinessId).FirstOrDefault();
                    int ISDCode = 0;
                    if (_bloc != null)
                    {
                        ISDCode = _bloc.Isdcode;
                    }
                    switch (ISDCode)
                    {
                        case 91:
                         var locs = db.GtEcbslns.Where(x => x.BusinessId == BusinessId)
                        .Join(db.GtEcem91s,
                         x => x.BusinessKey,
                         y => y.BusinessKey ,
                         (x, y) => new { x, y })
                        .Join(db.GtEcapcds,
                        xy => xy.y.EmailType,
                        z=>   z.ApplicationCode,
                        (xy,z)=>new {xy,z})
                        .Select(r=> new DO_EmailConnect
                        {
                            BusinessKey = r.xy.y.BusinessKey,
                            EmailType= r.xy.y.EmailType,
                            OutgoingMailServer = r.xy.y.OutgoingMailServer,
                            Port = r.xy.y.Port,
                            SenderEmailId= r.xy.y.SenderEmailId,
                            UserName = eSyaCryptGeneration.Decrypt(r.xy.y.UserName),
                            Password = eSyaCryptGeneration.Decrypt(r.xy.y.Password),
                            PassKey= r.xy.y.PassKey,
                            ActiveStatus = r.xy.y.ActiveStatus,
                            EmailTypeDesc=r.z.CodeDesc,
                            ISDCode = 91
                        }).ToListAsync();
                            return await locs;

                        case 254:
                         var result_254 = db.GtEcbslns.Where(x => x.BusinessId == BusinessId)
                        .Join(db.GtEce254s,
                         x => x.BusinessKey,
                         y => y.BusinessKey,
                         (x, y) => new { x, y })
                        .Join(db.GtEcapcds,
                        xy => xy.y.EmailType,
                        z => z.ApplicationCode,
                        (xy, z) => new { xy, z })
                        .Select(r => new DO_EmailConnect
                        {
                            BusinessKey = r.xy.y.BusinessKey,
                            EmailType = r.xy.y.EmailType,
                            OutgoingMailServer = r.xy.y.OutgoingMailServer,
                            Port = r.xy.y.Port,
                            SenderEmailId = r.xy.y.SenderEmailId,
                            UserName = eSyaCryptGeneration.Decrypt(r.xy.y.UserName),
                            Password = eSyaCryptGeneration.Decrypt(r.xy.y.Password),
                            PassKey = r.xy.y.PassKey,
                            ActiveStatus = r.xy.y.ActiveStatus,
                            EmailTypeDesc = r.z.CodeDesc,
                            ISDCode = 254
                        }).ToListAsync();
                            return await result_254;
                        //case 3:
                        //    Console.WriteLine("Wednesday");
                        //    break;

                        default:
                            var defaultlocs = db.GtEcbslns.Where(x => x.BusinessId == BusinessId)
                        .Join(db.GtEcem91s,
                         x => x.BusinessKey,
                         y => y.BusinessKey,
                         (x, y) => new { x, y })
                        .Join(db.GtEcapcds,
                        xy => xy.y.EmailType,
                        z => z.ApplicationCode,
                        (xy, z) => new { xy, z })
                        .Select(r => new DO_EmailConnect
                        {
                            BusinessKey = r.xy.y.BusinessKey,
                            EmailType = r.xy.y.EmailType,
                            OutgoingMailServer = r.xy.y.OutgoingMailServer,
                            Port = r.xy.y.Port,
                            SenderEmailId = r.xy.y.SenderEmailId,
                            UserName = eSyaCryptGeneration.Decrypt(r.xy.y.UserName),
                            Password = eSyaCryptGeneration.Decrypt(r.xy.y.Password),
                            PassKey = r.xy.y.PassKey,
                            ActiveStatus = r.xy.y.ActiveStatus,
                            EmailTypeDesc = r.z.CodeDesc,
                            ISDCode = 91
                     }).ToListAsync();
                            return await defaultlocs;
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateEmailConnect(DO_EmailConnect obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        int ISDCode = obj.ISDCode;
                        switch (ISDCode)
                        {
                            case 91:

                                GtEcem91 _emailcon = db.GtEcem91s.Where(be => be.BusinessKey == obj.BusinessKey && be.EmailType == obj.EmailType && be.OutgoingMailServer.ToUpper().Replace(" ", "") == obj.OutgoingMailServer.ToUpper().Replace(" ", "")).FirstOrDefault();
                                if (_emailcon != null)
                                {
                                    _emailcon.Port = obj.Port;
                                    _emailcon.SenderEmailId = obj.SenderEmailId;
                                    _emailcon.UserName = eSyaCryptGeneration.Encrypt(obj.UserName);
                                    _emailcon.Password = eSyaCryptGeneration.Encrypt(obj.Password);
                                    _emailcon.PassKey =obj.PassKey;
                                    _emailcon.ActiveStatus = obj.ActiveStatus;
                                    _emailcon.ModifiedBy = obj.UserID;
                                    _emailcon.ModifiedOn = System.DateTime.Now;
                                    _emailcon.ModifiedTerminal = obj.TerminalID;
                                    await db.SaveChangesAsync();
                                    dbContext.Commit();
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                                }
                                else
                                {
                                    var email = new GtEcem91
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        EmailType=obj.EmailType,
                                        OutgoingMailServer = obj.OutgoingMailServer,
                                        Port = obj.Port,
                                        SenderEmailId=obj.SenderEmailId,
                                        UserName = eSyaCryptGeneration.Encrypt(obj.UserName),
                                        Password = eSyaCryptGeneration.Encrypt(obj.Password),
                                        PassKey= obj.PassKey,
                                        ActiveStatus = obj.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };

                                    db.GtEcem91s.Add(email);
                                    await db.SaveChangesAsync();
                                    dbContext.Commit();
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                                }


                            case 254:

                                GtEce254 emailcon = db.GtEce254s.Where(be => be.BusinessKey == obj.BusinessKey && be.OutgoingMailServer.ToUpper().Replace(" ", "") == obj.OutgoingMailServer.ToUpper().Replace(" ", "")).FirstOrDefault();
                                if (emailcon != null)
                                {
                                    emailcon.Port = obj.Port;
                                    emailcon.SenderEmailId = obj.SenderEmailId;
                                    emailcon.UserName = eSyaCryptGeneration.Encrypt(obj.UserName);
                                    emailcon.Password = eSyaCryptGeneration.Encrypt(obj.Password);
                                    emailcon.PassKey =obj.PassKey;
                                    emailcon.ActiveStatus = obj.ActiveStatus;
                                    emailcon.ModifiedBy = obj.UserID;
                                    emailcon.ModifiedOn = System.DateTime.Now;
                                    emailcon.ModifiedTerminal = obj.TerminalID;
                                    await db.SaveChangesAsync();
                                    dbContext.Commit();
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                                }
                                else
                                {
                                    var email = new GtEce254
                                    {
                                        BusinessKey = obj.BusinessKey,
                                        EmailType = obj.EmailType,
                                        OutgoingMailServer = obj.OutgoingMailServer,
                                        Port = obj.Port,
                                        SenderEmailId = obj.SenderEmailId,
                                        UserName = eSyaCryptGeneration.Encrypt(obj.UserName),
                                        Password = eSyaCryptGeneration.Encrypt(obj.Password),
                                        PassKey = obj.PassKey,
                                        ActiveStatus = obj.ActiveStatus,
                                        FormId = obj.FormID,
                                        CreatedBy = obj.UserID,
                                        CreatedOn = System.DateTime.Now,
                                        CreatedTerminal = obj.TerminalID
                                    };

                                    db.GtEce254s.Add(email);
                                    await db.SaveChangesAsync();
                                    dbContext.Commit();
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                                }

                            default:
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0111", Message = string.Format(_localizer[name: "W0111"]) };
                        }

                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveEmailConnect(DO_EmailConnect obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        int ISDCode = obj.ISDCode;
                        switch (ISDCode)
                        {
                            case 91:
                                GtEcem91 emailcon = db.GtEcem91s.Where(be => be.BusinessKey == obj.BusinessKey && be.OutgoingMailServer.ToUpper().Replace(" ", "") == obj.OutgoingMailServer.ToUpper().Replace(" ", "")).FirstOrDefault();
                                if (emailcon == null)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                                }

                                emailcon.ActiveStatus = obj.status;
                                await db.SaveChangesAsync();
                                dbContext.Commit();

                                if (obj.status == true)
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                                else
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };


                            case 254:

                                GtEce254 email = db.GtEce254s.Where(be => be.BusinessKey == obj.BusinessKey && be.OutgoingMailServer.ToUpper().Replace(" ", "") == obj.OutgoingMailServer.ToUpper().Replace(" ", "")).FirstOrDefault();
                                if (email == null)
                                {
                                    return new DO_ReturnParameter() { Status = false, StatusCode = "W0112", Message = string.Format(_localizer[name: "W0112"]) };
                                }

                                email.ActiveStatus = obj.status;
                                await db.SaveChangesAsync();
                                dbContext.Commit();

                                if (obj.status == true)
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                                else
                                    return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                            default:
                                return new DO_ReturnParameter() { Status = false, StatusCode = "W0113", Message = string.Format(_localizer[name: "W0113"]) };
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion
    }
}
