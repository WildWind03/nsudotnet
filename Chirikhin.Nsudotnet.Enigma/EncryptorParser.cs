using System;
using System.Collections.Generic;
using static System.String;

namespace Chirikhin.Nsudotnet.Enigma
{
    public static class EncryptorParser
    {
        private const int ProperCountOfArguments = 4;

        public static EncryptorConfiguartion GetConfiguration(IReadOnlyList<string> args)
        {
            if (null == args || args.Count != ProperCountOfArguments)
            {
                throw new ArgumentException(Concat("args can not be null and must be equal to ", ProperCountOfArguments));
            }

            var inputFilename = args[1];
            var algorithmName = args[2];
            var outputFilename = args[3];
            return new EncryptorConfiguartion(inputFilename, algorithmName, outputFilename);
        }
    }
}
