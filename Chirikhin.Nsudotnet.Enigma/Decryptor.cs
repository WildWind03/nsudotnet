using System;
using System.IO;
using System.Security.Cryptography;

namespace Chirikhin.Nsudotnet.Enigma
{
    internal class Decryptor
    {
        public static void Decrypt(DecryptorConfiguration decryptorConfiguration)
        {
            using (var symmetricAlgorithm =
                AlgorithmFactory.CreateAlghorithm(decryptorConfiguration.AlghorithmName))
            {

                var key = new byte[symmetricAlgorithm.KeySize / 8];
                var iv = new byte[symmetricAlgorithm.BlockSize / 8];

                var keyAndIvString = File.ReadAllText(decryptorConfiguration.KeyFilename);
                var keyLengthInString = (int) ((float) key.Length / (key.Length + iv.Length) * keyAndIvString.Length);
                var keyString = keyAndIvString.Substring(0, keyLengthInString);
                var ivString = keyAndIvString.Substring(keyLengthInString);

                var cipcheBytes = File.ReadAllBytes(decryptorConfiguration.InputFilename);

                symmetricAlgorithm.Key = Convert.FromBase64String(keyString);
                symmetricAlgorithm.IV = Convert.FromBase64String(ivString);

                using (var iCryptoTransform = symmetricAlgorithm.CreateDecryptor())
                {
                    using (var mem = new MemoryStream())
                    {
                        using (var crypto = new CryptoStream(mem, iCryptoTransform, CryptoStreamMode.Write))
                        {
                            crypto.Write(cipcheBytes, 0, cipcheBytes.Length);
                            crypto.FlushFinalBlock();
                        }

                        File.WriteAllBytes(decryptorConfiguration.OutputFilename, mem.ToArray());
                    }
                }
            }
        }
    }
}
