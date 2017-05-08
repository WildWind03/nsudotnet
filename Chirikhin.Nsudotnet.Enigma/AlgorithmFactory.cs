using System;
using System.Security.Cryptography;

namespace Chirikhin.Nsudotnet.Enigma
{
    public static class AlgorithmFactory
    {
        private static readonly Random Random = new Random();

        public static SymmetricAlgorithm CreateAlghorithm(string algorithmName)
        {
            switch (algorithmName.ToUpper())
            {
                case "AES":
                    SymmetricAlgorithm aesCryptoServiceProvider = new AesCryptoServiceProvider();
                    aesCryptoServiceProvider.KeySize = 128;
                    aesCryptoServiceProvider.BlockSize = 128;
                    aesCryptoServiceProvider.Key = GenarateByteArray(aesCryptoServiceProvider.KeySize / 8);
                    aesCryptoServiceProvider.IV = GenarateByteArray(aesCryptoServiceProvider.BlockSize / 8);
                    aesCryptoServiceProvider.Mode = CipherMode.ECB;
                    aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    return aesCryptoServiceProvider;
                case "DES":
                    SymmetricAlgorithm desCryptoServiceProvider = new DESCryptoServiceProvider();
                    desCryptoServiceProvider.KeySize = 64;
                    desCryptoServiceProvider.BlockSize = 64;
                    desCryptoServiceProvider.Key = GenarateByteArray(desCryptoServiceProvider.KeySize / 8);
                    desCryptoServiceProvider.IV = GenarateByteArray(desCryptoServiceProvider.BlockSize / 8);
                    desCryptoServiceProvider.Mode = CipherMode.ECB;
                    desCryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    return desCryptoServiceProvider;
                case "RC2":
                    SymmetricAlgorithm rc2CryptoServiceProvider = new RC2CryptoServiceProvider();
                    rc2CryptoServiceProvider.KeySize = 64;
                    rc2CryptoServiceProvider.BlockSize = 64;
                    rc2CryptoServiceProvider.Key = GenarateByteArray(rc2CryptoServiceProvider.KeySize / 8);
                    rc2CryptoServiceProvider.IV = GenarateByteArray(rc2CryptoServiceProvider.BlockSize / 8);
                    rc2CryptoServiceProvider.Mode = CipherMode.ECB;
                    rc2CryptoServiceProvider.Padding = PaddingMode.PKCS7;
                    return rc2CryptoServiceProvider;
                case "RIJNDAEL":
                    SymmetricAlgorithm  rijndaelManaged = new RijndaelManaged();
                    rijndaelManaged.KeySize = 128;
                    rijndaelManaged.BlockSize = 128;
                    rijndaelManaged.Key = GenarateByteArray(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = GenarateByteArray(rijndaelManaged.BlockSize / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    return rijndaelManaged;
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
