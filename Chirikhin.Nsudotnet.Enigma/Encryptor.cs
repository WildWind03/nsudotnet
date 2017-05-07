using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Chirikhin.Nsudotnet.Enigma
{
    public class Encryptor
    {
        public static void Encrypt(EncryptorConfiguartion encryptorConfiguartion)
        {
            byte[] textBytes = File.ReadAllBytes(encryptorConfiguartion.InputFilename);

            SymmetricAlgorithm symmetricAlgorithm =
                AlghorithmFactory.CreateAlghorithm(encryptorConfiguartion.AlgorithmName);

            ICryptoTransform iCryptoTransform = symmetricAlgorithm.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, iCryptoTransform, CryptoStreamMode.Write);

            cryptoStream.Write(textBytes, 0, textBytes.Length);
            cryptoStream.Close();

            byte[] cipcherText = memoryStream.ToArray();
            memoryStream.Close();

            Console.WriteLine(Convert.ToBase64String(symmetricAlgorithm.Key).Length);
            File.WriteAllText(GetKeyFilename(encryptorConfiguartion.InputFilename), Convert.ToBase64String(symmetricAlgorithm.Key));
            File.AppendAllText(GetKeyFilename(encryptorConfiguartion.InputFilename), Convert.ToBase64String(symmetricAlgorithm.IV));

            iCryptoTransform.Dispose();
            symmetricAlgorithm.Clear();

            File.WriteAllBytes(encryptorConfiguartion.OutputFilename, cipcherText);
        }

        private static string GetKeyFilename(string outputFilename)
        {
            var index = outputFilename.LastIndexOf(".", StringComparison.Ordinal);

            if (-1 == index)
            {
                return outputFilename + ".key";
            }

            return outputFilename.Substring(0, index) + ".key" + outputFilename.Substring(index);
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
