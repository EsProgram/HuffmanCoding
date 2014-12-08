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
            Node root = new Node('a');
            Node el1 = new Node('b');
            root.Left = el1;

            Console.WriteLine(root.Left.Element);
        }
    }
}