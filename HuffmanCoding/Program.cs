using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<char, float> data = new Dictionary<char, float>() { { '_', 18.59f }, { 'A', 6.42f } };
            Huffman<char> huf = new Huffman<char>(data);
        }
    }
}