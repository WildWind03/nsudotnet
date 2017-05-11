using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirikhin.Nsudotnet.Enigma
{
    class InvalidKeyIvFile : Exception
    {
        public InvalidKeyIvFile(string reason) : base(reason)
        {
            
        }
    }
}
