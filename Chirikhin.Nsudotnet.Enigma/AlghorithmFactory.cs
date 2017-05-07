using System;
using System.Security.Cryptography;

namespace Chirikhin.Nsudotnet.Enigma
{
    public static class AlghorithmFactory
    {
        private static readonly Random Random = new Random();

        public static SymmetricAlgorithm CreateAlghorithm(string algorithmName)
        {
            switch (algorithmName.ToUpper())
            {
                case "AES":
                    SymmetricAlgorithm symmetricAlgorithm = new AesCryptoServiceProvider();
                    symmetricAlgorithm.KeySize = 128;
                    symmetricAlgorithm.BlockSize = 128;
                    symmetricAlgorithm.Key = GenarateByteArray(symmetricAlgorithm.KeySize / 8);
                    symmetricAlgorithm.IV = GenarateByteArray(symmetricAlgorithm.BlockSize / 8);
                    symmetricAlgorithm.Mode = CipherMode.ECB;
                    symmetricAlgorithm.Padding = PaddingMode.PKCS7;
                    return symmetricAlgorithm;
                case "DES":
                    return new DESCryptoServiceProvider();
                case "RC2":
                    return new RC2CryptoServiceProvider();
                case "Rijndael":
                    return new RijndaelManaged();
                default:
                    throw new ArgumentException("There isn't appropriate algorithm");
            }
        }

        private static byte[] GenarateByteArray(int length)
        {
            byte[] byteArray = new byte[length];

            for (int k = 0; k < length; ++k)
            {
                byteArray[k] = (byte) Random.Next(0, 255);
            }

            return byteArray;
        }
    }
}
