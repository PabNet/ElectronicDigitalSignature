using System;
using ElectronicDigitalSignature.Implementation.EDS;

namespace ElectronicDigitalSignature.Implementation
{
    static class Program
    {
        public static void Main()
        {
            Console.Write("Введите текст для подписи: ");
            var signature = new Sender(Console.ReadLine()).SignText();
            Console.Write("Введите текст для проверки подписи: ");
            Console.WriteLine("Подпись: \n'" 
                              + signature + "'\n" +
                              (new Recipient(Console.ReadLine()).VerifySignature(signature)
                                  ? "Действительна" : "Не действительна"));
        }
    }
}