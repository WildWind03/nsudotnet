using System;
using System.Collections.Generic;

namespace Chirikhin.Nsudotnet.Enigma
{
    public static class EncryptorParser
    {
        public static EncryptorConfiguartion GetConfiguration(IReadOnlyList<string> args)
        {
            if (null == args || args.Count != 4)
            {
                throw new ArgumentException("args can not be null and must be equal to 4");
            }

            var inputFilename = args[1];
            var algorithmName = args[2];
            var outputFilename = args[3];
            return new EncryptorConfiguartion(inputFilename, algorithmName, outputFilename);
        }
    }
}
