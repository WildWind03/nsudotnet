using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chirikhin.Nsudotnet.Enigma
{
    class Decryptor
    {
        public static void Decrypt(DecryptorConfiguration decryptorConfiguration)
        {
            SymmetricAlgorithm symmetricAlgorithm =
            AlghorithmFactory.CreateAlghorithm(decryptorConfiguration.AlghorithmName);

            byte[] key = new byte[symmetricAlgorithm.KeySize / 8];
            byte[] iv = new byte[symmetricAlgorithm.BlockSize / 8];

            string keyAndIvString = File.ReadAllText(decryptorConfiguration.KeyFilename);
            int keyLengthInString = (int) ((float) key.Length / (key.Length + iv.Length) * keyAndIvString.Length);
            Console.WriteLine(keyLengthInString);
            string keyString = keyAndIvString.Substring(0, keyLengthInString);
            string ivString = keyAndIvString.Substring(keyLengthInString);

            byte[] cipcheBytes = File.ReadAllBytes(decryptorConfiguration.InputFilename);

            symmetricAlgorithm.Key = Convert.FromBase64String(keyString);
            symmetricAlgorithm.IV = Convert.FromBase64String(ivString);

            using (MemoryStream mem = new MemoryStream())
            {
                CryptoStream crypto = new CryptoStream(mem, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
                crypto.Write(cipcheBytes, 0, cipcheBytes.Length);
                crypto.FlushFinalBlock();
                File.WriteAllBytes(decryptorConfiguration.OutputFilename,mem.ToArray());
            }
        }
    }
}
