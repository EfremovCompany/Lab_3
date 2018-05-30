/* Ефремов С.В. ПСм-12
 * Лабораторная 3
 * 
 * Требуется найти все вхождения любого из образцов в текст. Результаты поиска не
должны зависеть от регистра букв, то есть каждая буква в образце и тексте может быть
как строчной, так и прописной.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputLines = File.ReadAllLines(args[0], Encoding.UTF8);
            int patternsCount = Int32.Parse(inputLines[0]);
            List<string> patterns = new List<string>();

            for (int i = 0; i < patternsCount; i++)
            {
                patterns.Add(inputLines[i + 1]);
            }

            string text = File.ReadAllText(inputLines[patternsCount + 1], Encoding.UTF8);
            Stopwatch stopwatch = Stopwatch.StartNew();
            RK rk = new RK(patterns);
            rk.find(text);
            stopwatch.Stop();
            Console.WriteLine("TIME IS : " + stopwatch.ElapsedMilliseconds);

            if (!File.Exists("./OUTPUT.txt"))
            {
                // Create a file to write to.
                string createText = "Hello and Welcome" + Environment.NewLine;
                File.WriteAllText("./OUTPUT.txt", createText);
            }

            rk.WriteResultToFile();

            Console.ReadKey();
        }
    }
}
