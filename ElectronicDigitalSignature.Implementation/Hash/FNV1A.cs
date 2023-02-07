namespace ElectronicDigitalSignature.Implementation.Hash
{
    public class FNV1A
    {
        const uint FnvPrime = 0x1000193;
        const uint FnvOffsetBasic = 0x811C9DC5;
        
        public static uint ConvertText(string? text)
        {
            uint hashValue = FnvOffsetBasic;
            foreach (var symbol in text!)
            {
                hashValue ^= symbol;
                hashValue *= FnvPrime;
            }

            return hashValue;
        }
    }
}