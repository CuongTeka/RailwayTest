using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ThietBiDienTu.Areas.Admin.Models;

namespace ThietBiDienTu.Api
{
    public class TestController : ApiController
    {
        private ThietBiDienTuEntities1 db = new ThietBiDienTuEntities1();

        [HttpGet]
        [Route("api/test")]
        public IHttpActionResult Get()
        {
            return Ok("Api functional correctly");
        }

        // GET api/test/{id}
        //[HttpGet]
        //[Route("api/test/{id:int}")]
        //public IHttpActionResult Get(int id)
        //{
        //    return Ok($"You requested item {id}");
        //}
    }
}
