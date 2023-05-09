using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for EncryptDecrypt
/// </summary>
public static class EncryptDecrypt
{
    public static string ConvertStringToHex(String input, System.Text.Encoding encoding)
    {
        Byte[] stringBytes = encoding.GetBytes(input);
        StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
        foreach (byte b in stringBytes)
        {
            sbBytes.AppendFormat("{0:X2}", b);
        }
        return sbBytes.ToString();
    }

    public static string ConvertHexToString(String hexInput, System.Text.Encoding encoding)
    {
        int numberChars = hexInput.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
        }
        return encoding.GetString(bytes);
    }



    public static String Encrypt(string source)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] Key = { 12, 13, 14, 15, 16, 17, 18, 19 };
        byte[] IV = { 12, 13, 14, 15, 16, 17, 18, 19 };

        ICryptoTransform encryptor = des.CreateEncryptor(Key, IV);

        try
        {
            byte[] IDToBytes = ASCIIEncoding.ASCII.GetBytes(source);
            byte[] encryptedID = encryptor.TransformFinalBlock(IDToBytes, 0, IDToBytes.Length);
            return Convert.ToBase64String(encryptedID);
        }
        catch (FormatException)
        {
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static string Decrypt(string encrypted)
    {
        byte[] Key = { 12, 13, 14, 15, 16, 17, 18, 19 };
        byte[] IV = { 12, 13, 14, 15, 16, 17, 18, 19 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        ICryptoTransform decryptor = des.CreateDecryptor(Key, IV);

        try
        {
            byte[] encryptedIDToBytes = Convert.FromBase64String(encrypted.Replace(" ", "+"));
            byte[] IDToBytes = decryptor.TransformFinalBlock(encryptedIDToBytes, 0, encryptedIDToBytes.Length);
            return ASCIIEncoding.ASCII.GetString(IDToBytes);
        }
        catch (FormatException)
        {
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

}