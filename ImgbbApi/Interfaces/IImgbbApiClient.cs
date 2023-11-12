namespace ImgbbApi.Interfaces
{
    public interface IImgbbApiClient
    {
        Task<string> UploadImage(string image);
    }
}
