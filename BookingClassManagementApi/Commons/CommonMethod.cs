using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BookingClassManagementApi.Commons
{
    public class CommonMethod
    {
        
            public static string DecryptPassword(string password)
            {
                string decrypt = Decrypt(password, "$3cUr37iNk");
                return decrypt;
            }

            public static string EncryptPassword(string password)
            {
               
                string encryptedPassword = Encrypt(password, "$3cUr37iNk");
                return encryptedPassword;
            }
            private static string Encrypt(string strText, string strEncrypt)
            {
                byte[] byKey = new byte[20];
                byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                try
                {
                    byKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strText);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);

                    cs.Write(inputArray, 0, inputArray.Length);
                    cs.FlushFinalBlock();

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    //throw ex;
                }
                return string.Empty;
            }

            private static string Decrypt(string strText, string strEncrypt)
            {
                byte[] bKey = new byte[20];
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

                try
                {
                    bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);

                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                    return encoding.GetString(ms.ToArray());
                }

                catch
                {
                    //throw ex;                
                }
                return string.Empty;
            }

        }        

    
    
}
