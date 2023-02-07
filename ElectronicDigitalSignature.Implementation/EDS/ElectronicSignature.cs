using System;
using System.Numerics;
using ElectronicDigitalSignature.Implementation.MathematicalAlgorithm;

namespace ElectronicDigitalSignature.Implementation.EDS
{
    public class ElectronicSignature
    {
        private protected readonly Tuple<BigInteger, BigInteger> Key;
        private protected readonly string? Text;
        
        public ElectronicSignature(string? text, Modes mode)
        {
            this.Text = text;

            var typeKey = mode == Modes.Sending ? RSA.PrivateKey : RSA.PublicKey;

            this.Key = new(typeKey, RSA.SecretDigitMultiplication);

        }
        
        private protected BigInteger CalculateByFormula(BigInteger value, BigInteger exponent, BigInteger modulus)
        {
            return BigInteger.ModPow(value, exponent, modulus);
        }
    }
}