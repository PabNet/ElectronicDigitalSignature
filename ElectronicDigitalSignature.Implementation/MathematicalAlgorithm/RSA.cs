using System;
using System.Numerics;
using System.Security.Cryptography;

namespace ElectronicDigitalSignature.Implementation.MathematicalAlgorithm
{
    public static class RSA
    {
        private const byte MinByteValue = 64, MaxByteValue = 128;

        public static BigInteger PublicKey, PrivateKey, SecretDigitMultiplication;


        static RSA()
        {
            GetSecretDigit(out var firstDigit);
            GetSecretDigit(out var secondDigit);
            
            SecretDigitMultiplication = firstDigit*secondDigit;
            var eulerFunction = (firstDigit - 1) * (secondDigit - 1);
            
            GetPublicKey(in eulerFunction, out PublicKey);
            GetPrivateKey(in eulerFunction, in PublicKey, out PrivateKey);
        }
        
        private static void GetSecretDigit(out BigInteger bigInteger)
        {
            do
            {
                bigInteger = new BigInteger(GetBytes());
            } while (!IsPrime(bigInteger)
                     || bigInteger < 0);
        }
        
        private static void GetPublicKey(in BigInteger eulerFunction, out BigInteger publicKey)
        {
            do
            {
                publicKey = new BigInteger(GetBytes());
            } while (publicKey <= 0 || publicKey >= eulerFunction ||
                     ExecuteExtendedEuclideanAlgorithm(publicKey, eulerFunction, out _) != 1);
        }
        
        private static void GetPrivateKey(in BigInteger eulerFunction, in BigInteger publicKey, 
            out BigInteger privateKey)
        {
            ExecuteExtendedEuclideanAlgorithm(eulerFunction, publicKey, out privateKey);
            if (privateKey < 0)
            {
                privateKey += eulerFunction;
            }
        }
        
        private static byte[] GetBytes()
        {
            return RandomNumberGenerator.GetBytes(MinByteValue
                                                       + new Random().Next()
                                                       % (MaxByteValue - MinByteValue));
        }
        
        private static BigInteger ExecuteExtendedEuclideanAlgorithm(BigInteger a, BigInteger b, out BigInteger y)
        {
            BigInteger d0 = a,  d1 = b, x0 = 1, x1 = 0, y0 = 0, y1 = 1;
            while (d1 > 1)
            {
                BigInteger q = d0 / d1, d2 = d0 % d1, x2 = x0 - q * x1, y2 = y0 - q * y1;
                d0 = d1;
                d1 = d2;
                x0 = x1;
                x1 = x2;
                y0 = y1;
                y1 = y2;
            }
            y = y1;
            return d1;
        }
        
        private static bool IsPrime(BigInteger digit)
        {
            if (digit == 2 || digit == 3)
            {
                return true;
            }

            if (digit < 2 || digit % 2 == 0)
            {
                return false;
            }
   
            BigInteger t = digit - 1;

            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }
            
            for (byte i = 0; i < 10; i++)
            {

                byte[] byteArray = new byte[digit.ToByteArray().LongLength];

                BigInteger a;

                do
                {
                    byteArray = RandomNumberGenerator.GetBytes(byteArray.Length);
                    a = new BigInteger(byteArray);
                } while (a < 2 || a >= digit - 2);
                
                BigInteger x = BigInteger.ModPow(a, t, digit);

                if (x == 1 || x == digit - 1)
                {
                    continue;
                }

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, digit);

                    if (x == 1)
                    {
                        return false;
                    }
                    
                    if (x == digit - 1)
                    {
                        break;
                    }
                }

                if (x != digit - 1)
                {
                    return false;
                }
                
            }
            
            return true;
            
        }
    }
}