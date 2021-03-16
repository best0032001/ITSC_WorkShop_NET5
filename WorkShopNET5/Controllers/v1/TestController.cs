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
    }
}
