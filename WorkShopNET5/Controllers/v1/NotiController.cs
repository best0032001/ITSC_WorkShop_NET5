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
    public class NotiController : ITSCController
    {
        public NotiController(IHttpClientFactory clientFactory, ITSCServer iTSCServer, ILogger<ITSCController> logger, IHrStoreRepository hrStoreRepository)
        {
            this.loadConfig(logger, iTSCServer, clientFactory);


        }
        [HttpGet("v1/noti")]
        public async Task<IActionResult> noti()
        {

            NotiMgsView notiMgsView = new NotiMgsView();
            notiMgsView.appID = "xxxxxxxxxxxxxxxxxxxxxxxx";
            notiMgsView.cmuaccount = new List<string>();
            notiMgsView.cmuaccount.Add("xxxxxxxxx@cmu.ac.th");
            notiMgsView.message = "test lib 3.1.0";

            List<NotiReturnView> test = await this.NotiService(notiMgsView);
            foreach (NotiReturnView notiReturnView in test)
            {

                if (notiReturnView.isEmail)
                {
                    // sendEmail  (notiReturnView.cmuaccount)
                }
            }
            return Ok();
        }
        [HttpGet("v1/notiNoLib")]
        public async Task<IActionResult> notiNoLib()
        {
            NotiMgsViewDemo notiMgsView = new NotiMgsViewDemo();
            notiMgsView.appID = "xxxxxxxxxxxxxxxxxxxxxxxx";
            notiMgsView.cmuaccount = new List<string>();
            notiMgsView.cmuaccount.Add("xxxxxxxxx@cmu.ac.th");
            notiMgsView.message = "test lib 3.1.0";

            String notiAPI = "https://xxxxxxxxxxx/noti/dev/v1/NotiService";
            HttpClient httpClient = this._clientFactory.CreateClient();
            var response = await httpClient.PostAsync(notiAPI, new StringContent(JsonConvert.SerializeObject(notiMgsView)));
            var responseString = await response.Content.ReadAsStringAsync();
            APIModelNotiDemo dataTemp = JsonConvert.DeserializeObject<APIModelNotiDemo>(responseString);
            foreach (NotiReturnViewDemo notiReturnView in dataTemp.data)
            {

                if (notiReturnView.isEmail)
                {
                    // sendEmail  (notiReturnView.cmuaccount)
                }
            }
            return Ok();
        }
    }
    public class NotiReturnViewDemo  //ถ้าไม่ได้lib สร้างclass ตามนี้ 
    {
        
        public string cmuaccount { get; set; }
        public bool isEmail { get; set; }
    }
    public class APIModelNotiDemo //ถ้าไม่ได้lib สร้างclass ตามนี้
    {
     

        public List<NotiReturnViewDemo> data { get; set; }
        public string message { get; set; }
    }
    public class NotiMgsViewDemo //ถ้าไม่ได้lib สร้างclass ตามนี้
    {
 

        public string appID { get; set; }
        public List<string> cmuaccount { get; set; }
        public string message { get; set; }
    }
}
