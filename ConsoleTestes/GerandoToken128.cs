using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleTestes
{
    class GerandoToken128
    {
        static void sMain(string[] args)
        {


            //byte[] linkBytes = new byte[128]; 
            //System.Security.Cryptography.RNGCryptoServiceProvider rngCrypto = new System.Security.Cryptography.RNGCryptoServiceProvider(); 
            //rngCrypto.GetBytes(linkBytes); 
            //string text128 = Convert.ToBase64String(linkBytes);
            //string text128Enc = Uri.EscapeDataString(text128);

            //int numeroBytes = System.Text.Encoding.UTF8.GetByteCount(text128);
            //numeroBytes = System.Text.Encoding.UTF8.GetByteCount(text128Enc);

            byte[] linkBytes = new byte[128];
            var rngCrypto = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rngCrypto.GetBytes(linkBytes);
            var text128 = Convert.ToBase64String(linkBytes);
            var text128Enc = Uri.EscapeDataString(text128);

            int numeroBytes = System.Text.Encoding.UTF8.GetByteCount(text128);
            numeroBytes = System.Text.Encoding.UTF8.GetByteCount(text128Enc);

            var bytes = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            // and if you need it as a string... 
            string hash1 = BitConverter.ToString(bytes);
            // or maybe... 
            string hash2 = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            numeroBytes = System.Text.Encoding.UTF8.GetByteCount(hash2);

            var res = RandomString(128);
            numeroBytes = System.Text.Encoding.UTF8.GetByteCount(res);

            var cont = 1000000;

            var lista = new List<string>();

            while (cont > 0)
            {
                var resp = RandomString(128);

                if (lista.Exists(p => p.Equals(resp)))
                {
                    Console.WriteLine("Erro Chave duplicada");
                    Console.ReadLine();
                }
                lista.Add(resp);

                cont--;
            }


            Console.WriteLine("Fim");
            Console.ReadLine();
        }
        static string RandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            // Guid.NewGuid and System.Random are not particularly random. By using a
            // cryptographically-secure random number generator, the caller is always
            // protected, regardless of use.
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }
    }
}
