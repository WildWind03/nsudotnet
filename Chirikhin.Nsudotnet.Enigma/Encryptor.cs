using System;
using System.IO;
using System.Security.Cryptography;
using static System.String;

namespace Chirikhin.Nsudotnet.Enigma
{
    public class Encryptor
    {
        public static void Encrypt(EncryptorConfiguartion encryptorConfiguartion)
        {
            using (var inputReader =
                new FileStream(encryptorConfiguartion.InputFilename, FileMode.Open))
            {
                using (var outputWriter =
                    new FileStream(encryptorConfiguartion.OutputFilename, FileMode.Create))
                {
                    using (var symmetricAlgorithm =
                        AlgorithmFactory.CreateAlghorithm(encryptorConfiguartion.AlgorithmName))
                    {

                        using (var iCryptoTransform = symmetricAlgorithm.CreateEncryptor())
                        {
                                using (var cryptoStream =
                                    new CryptoStream(outputWriter, iCryptoTransform, CryptoStreamMode.Write))
                                {
                                    inputReader.CopyTo(cryptoStream);
                                }

                            File.WriteAllText(GetKeyFilename(encryptorConfiguartion.InputFilename),
                                $"{Convert.ToBase64String(symmetricAlgorithm.Key)}\n{Convert.ToBase64String(symmetricAlgorithm.IV)}");
                        }
                    }
                }
            }
        }

        private static string GetKeyFilename(string outputFilename)
        {
            return outputFilename.Insert(outputFilename.Length - Path.GetExtension(outputFilename).Length, ".key");
        }
    }
}
