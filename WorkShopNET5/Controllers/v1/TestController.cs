using ITSC_API_GATEWAY_LIB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkShopNET5.Model.Interface;

namespace WorkShopNET5.Controllers.v1
{
    [Route("api/")]
    [ApiController]
    public class TestController : ITSCController
    {
        private IHrStoreRepository _hrStoreRepository;
        public TestController(IHttpClientFactory clientFactory, ITSCServer iTSCServer,  ILogger<ITSCController> logger, IHrStoreRepository hrStoreRepository)
        {
            this.loadConfig(logger, iTSCServer, clientFactory);
            _hrStoreRepository = hrStoreRepository;

        }

        public async Task<IActionResult> temp()
        {
            DateTime _date = _IItscServer.GetDateITSC();
            String action = "Consent/self";
            this.beginActionITSC(action);
            try
            {
                APIModel aPIModel = new APIModel();
                return OkITSC(aPIModel, action);
            }
            catch (Exception ex)
            {
                return this.StatusErrorITSC(action, ex);
            }
        }

        [HttpGet("v1/Test")]
        public async Task<IActionResult> Test()
        {
            DateTime _date = _IItscServer.GetDateITSC();
            String action = "TestController.Test #clickXXXXXX";
            this.beginActionITSC(action);
            try
            {
                if (!await this.checkApp())
                {
                    return this.UnauthorizedITSC(action);
                }

                APIModel aPIModel = new APIModel();
                aPIModel.data = this.cmuaccount;
            
                return OkITSC(aPIModel, action);
            }
            catch (Exception ex)
            {
                return this.StatusErrorITSC(action, ex);
            }
        }
    }
}
