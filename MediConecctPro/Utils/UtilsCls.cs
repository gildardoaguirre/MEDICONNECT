using System.Text;

namespace MediConecctPro.Utils
{
    public class UtilsCls
    {
        public string Encript(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}
