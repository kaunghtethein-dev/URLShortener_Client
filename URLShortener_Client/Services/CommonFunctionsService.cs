using System.Text.RegularExpressions;

namespace URLShortener_Client.Services
{
    public class CommonFunctionsService
    {
        public bool IsEmailValidFormat(string email)
        {
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            
        }
    }
}
