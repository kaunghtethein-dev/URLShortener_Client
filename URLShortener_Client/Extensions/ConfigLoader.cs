namespace URLShortener_Client.Extensions
{
    public static class ConfigLoader
    {
        public static async Task<HttpResponseMessage> LoadConfigFileAsync(string filePath)
        {
            using var http = new HttpClient();
            return await http.GetAsync(filePath);
        }
    }
}
