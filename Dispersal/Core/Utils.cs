
using System.Text;

namespace Dispersal.Core;

public class Utils
{
    public static int GetCurrentTime()
    {
        return (int)((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public static DateTime UnixtimeToDatetime(int unixTimeStamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
        return dateTimeOffset.DateTime;
    }

    public static string UnixtimeToString(int unixTimeStamp)
    {
        if (unixTimeStamp <= 0)
            return "";

        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
        return dateTimeOffset.DateTime.ToString();
    }

    public static string BytesToHex(byte[] bytes)
    {
        if (bytes.Length == 0)
            return "";

        int length = bytes.Length;
        StringBuilder sBuilder = new(length << 1);

        for (int i = 0; i < length; i++)
            sBuilder.Append(bytes[i].ToString("x2"));

        return sBuilder.ToString();
    }
}
