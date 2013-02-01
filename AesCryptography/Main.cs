namespace AesCryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    class MainClass
    {
        public static void Main(string[] args)
        {

            using (Aes aes = Aes.Create()) 
            {
              try 
              {
//                    string keyString = "GZLGs08uYPwtiJARVi0wSOCOKajulSpKi8k2dLk9BbI=";
//                    string ivString = "+Rd4Lrv11T21k5aeVvaYpQ==";
//                    
//                    byte[] key = Convert.FromBase64String(keyString);
//                    byte[] iv = Convert.FromBase64String(ivString);                   
//
//                    aes.Key = key;
//                    aes.IV = iv;

                    aes.Key = CreateRandomByteArrayOfSize(32);
                    aes.IV = CreateRandomByteArrayOfSize(16);
                    
                    Console.WriteLine("Key: " + Convert.ToBase64String(aes.Key));
                    Console.WriteLine ("IV: " + Convert.ToBase64String(aes.IV));

                   // byte[] key = Convert.FromBase64String("/FAs9Kqg0cuomsUtpdyzGF6o2lEwdDwCpBIBAt5r82E=");
                    
                    string original = "qulatrak";
                    byte[] encrypted = Encrypt(original, aes.Key, aes.IV);

                    //byte[] encrypted = Convert.FromBase64String("dNNWqUkx2hPhAbrlzE5ErA==");

                    //string perlDecrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    
                    string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                    
                    Console.WriteLine("Encrypted string: " + Convert.ToBase64String(encrypted));
                    Console.WriteLine("Decrypted string: " + decrypted);   
              } 
              catch (Exception ex) 
              {
                  
              }
            }
        }

        private static byte[] Encrypt(string text, byte[] key, byte[] iv)
        {
            byte[] result;

            using (Aes aes = Aes.Create()) 
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryEncrypt = new MemoryStream())
                {
                    using (CryptoStream cryptoEncrypt = new CryptoStream(memoryEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writeEncrypt = new StreamWriter(cryptoEncrypt))
                        {
                            writeEncrypt.Write(text);
                        }

                        result = memoryEncrypt.ToArray();
                    }
                }

            }

            return result;
        }

        private static string Decrypt(byte[] encryptedText, byte[] key, byte[] iv)
        {
            string result;

            using (Aes aes = Aes.Create()) 
            {
                aes.Key = key;
                aes.IV = iv;
                
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                
                using (MemoryStream memoryDecrypt = new MemoryStream(encryptedText))
                {
                    using (CryptoStream cryptoDecrypt = new CryptoStream(memoryDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader readDecrypt = new StreamReader(cryptoDecrypt))
                        {
                            result = readDecrypt.ReadToEnd();
                        }
                    }
                }
                
            }

            return result;
        }

             
        private static byte[] CreateRandomByteArrayOfSize (int sizeInBytes)
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider ();
            byte[] result = new byte[sizeInBytes];
            rngCsp.GetBytes (result);
            
            return result;
        }             
    }
}