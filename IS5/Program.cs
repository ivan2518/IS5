﻿using System;
using System.IO;
using System.Numerics;

namespace IB5
{
    internal class Program
    {
        public static void Main()
        {
            // Исходные данные
            int P = 3307;
            int Q = 5669;
            BigInteger N = P * Q;
            BigInteger phi = (P - 1) * (Q - 1);
            BigInteger E = ChooseE(phi);
            BigInteger D = ModInverse(E, phi);

            // Чтение входного файла
            string inputText = File.ReadAllText("Input.txt");

            // Шифрование
            string encryptedText = Encrypt(inputText, E, N);

            // Запись в выходной файл
            File.WriteAllText("Out.txt", encryptedText);

            Console.WriteLine("Text Encrypted.");
        }

        static BigInteger ChooseE(BigInteger phi)
        {
            return 65537;
        }

        static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            // Расширенный алгоритм Евклида для нахождения обратного по модулю
            BigInteger m0 = m, t, q;
            BigInteger x0 = 0, x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                q = a / m;
                t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0) x1 += m0;
            return x1;
        }

        static string Encrypt(string inputText, BigInteger e, BigInteger n)
        {
            // Шифрование текста
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(inputText);
            string encrypted = "";
            foreach (var b in bytes)
            {
                BigInteger bi = new BigInteger(b);
                bi = BigInteger.ModPow(bi, e, n);
                encrypted += bi.ToString() + " ";
            }


            M();
            return encrypted.Trim();
        }



        public static void M()
        {
            // Исходные данные
            int P = 3307;
            int Q = 5669;
            BigInteger N = P * Q;
            BigInteger phi = (P - 1) * (Q - 1);
            BigInteger E = ChooseE(phi);
            BigInteger D = ModInverse(E, phi);

            // Чтение зашифрованного файла
            string encryptedText = File.ReadAllText("Out.txt");

            // Расшифровка
            string decryptedText = Decrypt(encryptedText, D, N);

            // Запись в выходной файл
            File.WriteAllText("Rezult.txt", decryptedText);

            Console.WriteLine("Text Decrypted.");
        }

        static string Decrypt(string encryptedText, BigInteger d, BigInteger n)
        {
            // Расшифровка текста
            string[] parts = encryptedText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                BigInteger bi = BigInteger.Parse(parts[i]);
                bi = BigInteger.ModPow(bi, d, n);
                bytes[i] = (byte)(bi % 256);
            }
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
