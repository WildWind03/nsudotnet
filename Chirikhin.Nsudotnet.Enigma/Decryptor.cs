using System;
using System.IO;
using System.Security.Cryptography;

namespace Chirikhin.Nsudotnet.Enigma
{
    internal class Decryptor
    {
        public static void Decrypt(DecryptorConfiguration decryptorConfiguration)
        {
            using (var inputStream = new FileStream(decryptorConfiguration.InputFilename, FileMode.Open))
            {
                using (var outputStream = new FileStream(decryptorConfiguration.OutputFilename, FileMode.Create))
                {
                    using (var symmetricAlgorithm =
                        AlgorithmFactory.CreateAlghorithm(decryptorConfiguration.AlghorithmName))
                    {
                        using (var keyStream =
                            new StreamReader(new FileStream(decryptorConfiguration.KeyFilename, FileMode.Open)))
                        {
                            var keyString = keyStream.ReadLine();
                            var ivString = keyStream.ReadLine();
                            symmetricAlgorithm.Key = Convert.FromBase64String(keyString);
                            symmetricAlgorithm.IV = Convert.FromBase64String(ivString);
                        }

                        using (var iCryptoTransform = symmetricAlgorithm.CreateDecryptor())
                        {
                            using (var crypto =
                                new CryptoStream(outputStream, iCryptoTransform, CryptoStreamMode.Write))
                            {
                                inputStream.CopyTo(crypto);
                                crypto.FlushFinalBlock();
                            }
                        }
                    }
                }
            }
        }
    }
}