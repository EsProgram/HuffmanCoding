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
            Node<char, float> root = new Node<char, float>('a');
            Node<char, float> el1 = new Node<char, float>('b');
            root.Left = el1;

            Console.WriteLine(root.Left.Element);
        }
    }
}