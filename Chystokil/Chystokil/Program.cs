﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Chystokil
{
    class Program
    {
        static void Main(string[] args)
        {
            string MainTxt = @"C:\Users\PC\source\repos\Chystokil\Chystokil\txt.txt";
            string EncryptTxt = @"C:\Users\PC\source\repos\Chystokil\Chystokil\encrypt.txt";
            string DecryptTxt = @"C:\Users\PC\source\repos\Chystokil\Chystokil\decrypt.txt";
            using (StreamReader sr = new StreamReader(MainTxt))
            {
                var text = sr.ReadToEnd().ToString();
                Stopwatch stopWatch = new Stopwatch();

                stopWatch.Start();
                string str = encrypt(text, 4);
                stopWatch.Stop();

                using (FileStream fstream = new FileStream(EncryptTxt, FileMode.OpenOrCreate))
                {
                    byte[] ArrayLast = System.Text.Encoding.Default.GetBytes(str);
                    fstream.Write(ArrayLast, 0, ArrayLast.Length);
                }

                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elapsedTime);
            }
            using (StreamReader srd = new StreamReader(EncryptTxt))
            {
                var encodeText = srd.ReadToEnd().ToString();
                Stopwatch DecrypClock = new Stopwatch();

                DecrypClock.Start();
                string strg = decrypt(encodeText, 4);
                DecrypClock.Stop();

                using (FileStream fstream = new FileStream(DecryptTxt, FileMode.OpenOrCreate))
                {
                    byte[] ArrayLast = System.Text.Encoding.Default.GetBytes(strg);
                    fstream.Write(ArrayLast, 0, ArrayLast.Length);
                }

                TimeSpan tsn = DecrypClock.Elapsed;

                string elTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    tsn.Hours, tsn.Minutes, tsn.Seconds,
                    tsn.Milliseconds / 10);
                Console.WriteLine("RunTime: " + elTime);


            }
        }

        public static string encrypt(string str, int key)
        {
            string result = "";
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; i + j < str.Length; j += key)
                {
                    result += str[i + j];
                }
            }
            return result;
        }
        public static string decrypt(string str, int key)
        {
            string result = "";
            char[] helper = str.ToCharArray();
            char[] temp = str.ToCharArray();


            int k = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; i + j < str.Length; j += key)
                {
                    int v = i + j;
                    helper[v] = temp[k++];
                }
            }
            for (int i = 0; i < str.Length; i++)
            {
                result += helper[i];
            }
            return result;
        }
        public static string encryptBlock(string str, int key, int block)
        {
            string[] res = new string[str.Length / block + 1];
            string result = "";
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = str.Substring(i, block);
            }
            foreach (var item in res)
            {
                result += encrypt(item, key);
            }
            return result;
        }
        public static string decryptBlock(string str, int key, int block)
        {
            string result = "";
            try
            {
                string[] res = new string[str.Length / block];
                for (int i = 0; i < res.Length; i++)
                {
                    res[i] = str.Substring(i, block);
                }
                foreach (var item in res)
                {
                    result += decrypt(item, key);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
            return result;

        }

    }
}
