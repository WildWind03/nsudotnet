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
            var textBytes = File.ReadAllBytes(encryptorConfiguartion.InputFilename);

            var symmetricAlgorithm =
                AlgorithmFactory.CreateAlghorithm(encryptorConfiguartion.AlgorithmName);

            var iCryptoTransform = symmetricAlgorithm.CreateEncryptor();

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, iCryptoTransform, CryptoStreamMode.Write);
                cryptoStream.Write(textBytes, 0, textBytes.Length);
                cryptoStream.Dispose();

                File.WriteAllBytes(encryptorConfiguartion.OutputFilename, memoryStream.ToArray());
            }

            File.WriteAllText(GetKeyFilename(encryptorConfiguartion.InputFilename), Convert.ToBase64String(symmetricAlgorithm.Key));
            File.AppendAllText(GetKeyFilename(encryptorConfiguartion.InputFilename), Convert.ToBase64String(symmetricAlgorithm.IV));

            iCryptoTransform.Dispose();
            symmetricAlgorithm.Dispose();
        }

        private static string GetKeyFilename(string outputFilename)
        {
            var index = outputFilename.LastIndexOf(".", StringComparison.Ordinal);

            if (-1 == index)
            {
                return outputFilename + ".key";
            }

            return Concat(outputFilename.Substring(0, index), ".key", outputFilename.Substring(index));
        }

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
