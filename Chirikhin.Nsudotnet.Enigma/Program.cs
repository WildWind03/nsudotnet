﻿using System;
using System.CodeDom.Compiler;

namespace Chirikhin.Nsudotnet.Enigma
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (DecryptEncryptDetector.IsEncrypt(args))
                {
                    Encryptor.Encrypt(EncryptorParser.GetConfiguration(args));
                }
                else
                {
                    Decryptor.Decrypt(DecryptorParser.GetConfiguration(args));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
