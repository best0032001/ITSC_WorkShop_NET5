using ITSC_API_GATEWAY_LIB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public TestController(IHttpClientFactory clientFactory, ITSCServer iTSCServer, ILogger<ITSCController> logger, IHrStoreRepository hrStoreRepository)
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

        [HttpPost("v1/TestPost")]
        public async Task<IActionResult> AddTest([FromBody] string body)
        {
            DateTime _date = _IItscServer.GetDateITSC();
            String action = "TestController.AddTest #clickXXXXXX";
            this.beginActionITSC(action);
            try
            {
                if (!await this.checkApp())
                {
                    return this.UnauthorizedITSC(action);
                }

                dynamic dBody = JsonConvert.DeserializeObject<dynamic>(body);
                APIModel aPIModel = new APIModel();
                aPIModel.data = ""+dBody.name;

                return OkITSC(aPIModel, action);
            }
            catch (Exception ex)
            {
                return this.StatusErrorITSC(action, ex);
            }
        }

        [HttpPost("v1/TestFile")]
        public async Task<IActionResult> AddTestFile()
        {
            DateTime _date = _IItscServer.GetDateITSC();
            String action = "TestController.AddTest #clickXXXXXX";


            this.insertLog(action, "xxxxxx");
            this.debugLog(action, "xxxxxx");
            this.beginActionITSC(action);
            try
            {
                if (!await this.checkApp())
                {
                    return this.UnauthorizedITSC(action);
                }
                string data = Request.Form["data"];
                dynamic dBody = JsonConvert.DeserializeObject<dynamic>(data);
                int countFiles = Request.Form.Files.Count;
                if (countFiles > 0)
                {
                    for (int filenameCount = 1; filenameCount <= countFiles; filenameCount++)
                    {
                        IFormFile file01 = Request.Form.Files["filename" + filenameCount];
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "upload");

                        FileModel fileModel1d = this.SaveFile(path, file01, 5);
                        if (fileModel1d.isSave)
                        {
                            String fileName = fileModel1d.fileName;
                            String dbPath = fileModel1d.dbPath;
                            String fullPath =fileModel1d.fullPath;

                        }
                        else
                        {
                            this.debugLog(action, fileModel1d.error);
                            return this.StatusCodeITSC(action, 503);
                        }
                    }
                }
                    APIModel aPIModel = new APIModel();
                aPIModel.data = "" + dBody.name;

                return OkITSC(aPIModel, action);
            }
            catch (Exception ex)
            {
                return this.StatusErrorITSC(action, ex);
            }
        }

        [HttpGet("v1/mail")]
        public async Task<IActionResult> mail()
        {
            DateTime _date = _IItscServer.GetDateITSC();
            String action = "TestController.mail #clickXXXXXX";
            this.beginActionITSC(action);
            try
            {
                if (!await this.checkApp())
                {
                    return this.UnauthorizedITSC(action);
                }

                List<IFormFile> Attachment = new List<IFormFile>();
                Attachment = null;

                String Title = "CMUMIS : xxxxxx";
                String Subject = "แจ้งสถานะxxxxxxx";
                String Message = "xxxxxx";

                await SendEmailAsync(Title, "xxx@cmu.ac.th", Subject, Message, Attachment);
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
