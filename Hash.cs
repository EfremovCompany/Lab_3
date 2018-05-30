using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_3
{
    class Hash
    {
        //https://github.com/lemire/rollinghashjava
        // myn is the length in characters of the blocks you want to hash
        public Hash(int myn)
        {
            n = myn;
            if (n > wordsize)
            {
                //throw new IllegalArgumentException();
            }

        }


        private int fastleftshiftn(int x)
        {
            return (x << n) | (x >> (wordsize - n));
        }

        private static int fastleftshift1(int x)
        {
            return (x << 1) | (x >> (wordsize - 1));
        }

        // add new character (useful to initiate the hasher)
        // to get a strongly universal hash value, you have to ignore the last or first (n-1) bits.
        public int eat(char c)
        {
            hashvalue = fastleftshift1(hashvalue);
            hashvalue ^= hasher.hashvalues[c];
            return hashvalue;
        }

        // remove old character and add new one
        // to get a strongly universal hash value, you have to ignore the last or first (n-1) bits.
        public int update(char outchar, char inchar)
        {
            int z = fastleftshiftn(hasher.hashvalues[outchar]);
            hashvalue = fastleftshift1(hashvalue) ^ z ^ hasher.hashvalues[inchar];
            return hashvalue;
        }

        // this is purely for testing purposes
        public static int nonRollingHash(string s)
        {
            int value = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                char c = s[i];
                int z = hasher.hashvalues[c];
                value = fastleftshift1(value) ^ z;
            }
            return value;
        }

        public static int wordsize = 101;
        public int hashvalue;
        int n;
        int myr;
        static CharacterHash hasher = CharacterHash.getInstance();

    }

    public class CharacterHash
    {
        public int[] hashvalues = new int[1 << 16];

        public CharacterHash()
        {
            Random r = new Random();
            for (int k = 0; k < hashvalues.Length; ++k)
                hashvalues[k] = r.Next();
        }

        public static CharacterHash getInstance()
        {
            return charhash;
        }

        static CharacterHash charhash = new CharacterHash();

    }
}
