using System.Numerics;
using ElectronicDigitalSignature.Implementation.Hash;

namespace ElectronicDigitalSignature.Implementation.EDS
{
    public class Recipient : ElectronicSignature
    {
        public Recipient(string? text) : base(text, Modes.Receiving) {}
        
        public bool VerifySignature(BigInteger signature)
        {
            return new BigInteger(FNV1A.ConvertText(this.Text)) 
                   == 
                   CalculateByFormula(signature, this.Key.Item1, this.Key.Item2);
        }
      
    }
}