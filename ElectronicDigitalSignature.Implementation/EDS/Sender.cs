using System.Numerics;
using ElectronicDigitalSignature.Implementation.Hash;

namespace ElectronicDigitalSignature.Implementation.EDS
{
    public class Sender : ElectronicSignature
    {
        public Sender(string? text) : base(text, Modes.Sending) { }
        
        public dynamic SignText()
        {
            return CalculateByFormula(
                new BigInteger(FNV1A.ConvertText(this.Text!)),
                this.Key.Item1,
                this.Key.Item2
            );
        }
    }
}