using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Dictionary<char, float> data = new Dictionary<char, float>()
			{
				{'_', 18.59f },{'A', 6.42f },{'B',1.27f},{'C',2.18f},{'D',3.17f},
				{'E',10.31f},{'F',2.08f},{'G',1.52f},{'H',4.67f},{'I',5.75f},
				{'J',0.08f},{'K',0.49f},{'L',3.21f},{'M',1.98f},{'N',5.72f},
				{'O',6.32f},{'P',1.52f},{'Q',0.08f},{'R',4.84f},{'S',5.14f},
				{'T',7.96f},{'U',2.28f},{'V',0.83f},{'W',1.75f},{'X',0.13f},
				{'Y',1.64f},{'Z',0.05f},{'*',0.02f}
			};

			Huffman<char> huf = new Huffman<char>(data);

			Console.WriteLine(huf['*']);
		}
	}
}