using System;

namespace Chirikhin.Nsudotnet.Enigma
{
    public static class DecryptorParser
    {
        public static DecryptorConfiguration GetConfiguration(string[] args)
        {
            if (null == args || args.Length != 5)
            {
                throw new ArgumentException("args can not be null and must be equal to 5");
            }

            var inputFilename = args[1];
            var algorithmName = args[2];
            var keyFilename = args[3];
            var outputFilename = args[4];
            return new DecryptorConfiguration(inputFilename, algorithmName, keyFilename, outputFilename);
        }
    }
}
