using System;

public static class ConnectionTokensUtil
{

    public static byte[] newToken => Guid.NewGuid().ToByteArray();

    public static int HashToken(byte[] token) => new Guid(token).GetHashCode();

    public static string TokenToString(byte[] token) => new Guid(token).ToString();

}
