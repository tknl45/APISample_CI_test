using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace APISample.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : BaseController
    {

        [HttpGet("demo_message")]
        public void demo_message(string message)
        {
            this.Message = message;
        }

        [HttpPost("demo_data")]
        public void demo_data(string message, [FromBody]Object pushForm)
        {
            this.Message = message;
            this.Data = pushForm;
        }

        [HttpGet("demo_error")]
        public void demo_error(string message)
        {
            throw new ArgumentException("ERROR");
        }



    }


    
}
