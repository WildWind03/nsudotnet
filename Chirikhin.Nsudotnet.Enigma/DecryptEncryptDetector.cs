namespace Chirikhin.Nsudotnet.Enigma
{
    public static class DecryptEncryptDetector
    {
        public static bool IsEncrypt(string[] args)
        {
            switch (args[0])
            {
                case "encrypt":
                    return true;
                case "decrypt":
                    return false;
                default:
                    throw new ParserException("Invalid mode");
            }
        }
    }
}
