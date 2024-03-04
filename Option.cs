using System;

namespace Cryptid
{
    public class Option
    {
        public string Algorithm { get; set; }
        public string Key { get; set; }
        public string FilePath { get; set; }
        public bool Encrypt { get; set; }
    }
}
