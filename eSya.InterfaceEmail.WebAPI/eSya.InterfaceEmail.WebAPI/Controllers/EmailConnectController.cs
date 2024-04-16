﻿using eSya.InterfaceEmail.DO;
using eSya.InterfaceEmail.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.InterfaceEmail.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailConnectController : ControllerBase
    {
        private readonly IEmailConnectRepository _emailconnectRepository;

        public EmailConnectController(IEmailConnectRepository emailconnectRepository)
        {
            _emailconnectRepository = emailconnectRepository;
        }

        #region Email Connect
        /// <summary>
        /// Getting Email Connect by Business ID.
        /// UI Reffered - Email Connect Grid
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetEmailConnectbyBusinessID(int BusinessId)
        {
            var emails = await _emailconnectRepository.GetEmailConnectbyBusinessID(BusinessId);
            return Ok(emails);
        }
        /// <summary>
        /// Insert Insert Or Update into Email Connect .
        /// UI Reffered -Email Connect
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateEmailConnect(DO_EmailConnect obj)
        {
            var msg = await _emailconnectRepository.InsertOrUpdateEmailConnect(obj);
            return Ok(msg);

        }
        /// <summary>
        /// Active Or De Email Connect.
        /// UI Reffered - Email Connect
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ActiveOrDeActiveEmailConnect(DO_EmailConnect obj)
        {
            var res = await _emailconnectRepository.ActiveOrDeActiveEmailConnect(obj);
            return Ok(res);
        }
        #endregion
    }
}
