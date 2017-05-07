namespace Chirikhin.Nsudotnet.Enigma
{
    public class DecryptorConfiguration
    {
        public DecryptorConfiguration(string inputFilename, string alghorithmName, string keyFilename, string outputFilename)
        {
            InputFilename = inputFilename;
            AlghorithmName = alghorithmName;
            KeyFilename = keyFilename;
            OutputFilename = outputFilename;
        }

        public string InputFilename { get; }
        public string AlghorithmName { get; }
        public string KeyFilename { get; }
        public string OutputFilename { get; }
    }
}
