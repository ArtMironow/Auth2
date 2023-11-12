using Flurl;
using Flurl.Http;
using ImgbbApi.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImgbbApi
{
    public class ImgbbApiClient : IImgbbApiClient
    {
        private readonly ImgbbApiClientConfiguration _imgbbConfiguration;

        public ImgbbApiClient(IOptions<ImgbbApiClientConfiguration> options)
        {
            _imgbbConfiguration = options.Value;
        }

        public async Task<string> UploadImage(string image)
        {
            var url = _imgbbConfiguration.ImgbbApiEndPoint.SetQueryParam("key", _imgbbConfiguration.ImgbbApiSecret);
            var result = await url.PostMultipartAsync(mp => mp.AddString("image", image)).ReceiveJson();
           
            string resultJson = JsonConvert.SerializeObject(result);

            JObject jObject = JObject.Parse(resultJson);
            JToken imageUrl = jObject["data"]["url"];
            
            return imageUrl.ToString();  
        }
    }
}
