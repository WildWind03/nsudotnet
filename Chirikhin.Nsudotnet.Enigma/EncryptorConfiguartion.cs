namespace Chirikhin.Nsudotnet.Enigma
{
    public class EncryptorConfiguartion
    {
        public string AlgorithmName { get;}
        public string InputFilename { get; }
        public string OutputFilename { get; }

        public EncryptorConfiguartion(string inputFilename, string algorithmName, string outputFilename)
        {
            InputFilename = inputFilename;
            AlgorithmName = algorithmName;
            OutputFilename = outputFilename;
        }
    }
}
