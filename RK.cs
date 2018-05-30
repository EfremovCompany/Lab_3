using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LAB_3
{
    public class RK
    {
        private List<string> result = new List<string>();
        Dictionary<string, int> patterns = new Dictionary<string, int>();

        public RK(List<string> patterns)
        {
            foreach (string pattern in patterns)
            {
                this.patterns.Add(pattern, CreateHash(pattern));
            }
        }

        public void WriteResultToFile()
        {
            System.IO.File.WriteAllLines("./OUTPUT.txt", result);
        }

        private int CreateHash(string input)
        {
            int hash = 0;
            foreach (char ch in input.ToLower())
            {
                if (ch == '\n')
                    hash += ' ';
                else
                    hash += ch;
            }
            return hash;
        }

        private int GetHashOfSymbol(char symbol)
        {
            if (symbol == '\n')
                return ' ';
            return Char.ToLower(symbol);
        }

        private int RollHash(int prev, char first, string data)
        {
            return prev - GetHashOfSymbol(first) + GetHashOfSymbol(data[data.Length - 1]);
        }

        public void find(string text)
        {
            text = text.Replace("\r\n", "\n");

            int lineNumber = 1;
            int charNumber = 1;
            int[] prevHash = new int[patterns.Count];
            int[] currentHash = new int[patterns.Count];
            
            for (int i = 0; i < text.Length - 1; i++)
            { 
                int param = 0;
                foreach (KeyValuePair<string, int> pattern in patterns)
                {
                    int length = pattern.Key.Length;
                   
                    if (i + length < text.Length)
                    {
                        if (i == 0)
                        {
                            currentHash[param] = CreateHash(text.Substring(0, length));
                        }

                        if (text[i] == '\n' && param == 0)
                        {
                            lineNumber++;
                            charNumber = 0;
                        }

                        if (currentHash[param] == pattern.Value && check(text.Substring(i, length), pattern.Key))
                        {
                            result.Add("Line " + lineNumber.ToString() + ", Position " + (charNumber).ToString() + ": " + pattern.Key);
                        }

                        prevHash[param] = currentHash[param];
                        currentHash[param] = RollHash(prevHash[param], text[i], text.Substring(i + 1, length).Replace('\n', ' '));
                        
                        param++;
                    }
                }
                charNumber++;
            }
        }
                


        private bool check(string txt, string pattern)
        {
            txt = txt.ToLower().Replace('\n', ' ');
            pattern = pattern.ToLower().Replace('\n', ' ');
            for (int j = 0; j < pattern.Length; j++)
                if (pattern[j] != txt[j])
                    return false;
            return true;
        }
    }
}
