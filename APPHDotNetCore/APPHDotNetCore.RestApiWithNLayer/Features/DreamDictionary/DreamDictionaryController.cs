using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APPHDotNetCore.RestApiWithNLayer.Features.DreamDictionary
{
    [Route("api/[controller]")]
    [ApiController]
    public class DreamDictionaryController : ControllerBase
    {
        private async Task<DreamDictionary> GetDataAsync()
        {
            string DreamjsonStr = await System.IO.File.ReadAllTextAsync("DreamDictionary.json");
            var modelStr = JsonConvert.DeserializeObject<DreamDictionary>(DreamjsonStr);
            return modelStr;
        }
        //api/DreamDictionary/DreamHeader
        [HttpGet("dreamHeader")]
        public async Task<IActionResult> DreamHeader()
        {
            var model = await GetDataAsync();
            return Ok(model.BlogHeader);
        }

        [HttpGet]
        public async Task<IActionResult> DreamDetail()
        {
            var model = await GetDataAsync();
            return Ok(model.BlogDetail);
        }

        [HttpGet("{blogId}/{blogDetailId}")]
        public async Task<IActionResult> Result(int blogId,int blogDetailId)
        {
            var model = await GetDataAsync();
            return Ok(model.BlogDetail.FirstOrDefault(x => x.BlogId == blogId && x.BlogDetailId == blogDetailId));
        }

    }
    public class DreamDictionary
    {
        public Blogheader[] BlogHeader { get; set; }
        public Blogdetail[] BlogDetail { get; set; }
    }

    public class Blogheader
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
    }

    public class Blogdetail
    {
        public int BlogDetailId { get; set; }
        public int BlogId { get; set; }
        public string BlogContent { get; set; }
    }
}
