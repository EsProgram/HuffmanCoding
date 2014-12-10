using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
	/// <summary>
	///ハフマン符号を扱うクラス
	/// </summary>
	/// <typeparam name="TNodeElement">ノードの要素の型</typeparam>
	internal class Huffman<TNodeElement>
	{
		private Dictionary<TNodeElement, float> data;//初期データを保存
		private List<dynamic> leafs;//初期データから生成されたリーフを格納
		private dynamic root;//木のルートノード

		public int MaxRank { get; private set; }

		/// <summary>
		/// 符号記号に割り付けられたコードを取得する
		/// </summary>
		/// <param name="element">符号記号</param>
		/// <returns></returns>
		public string this[TNodeElement element]
		{
			get { return GetCode(element); }
		}

		/// <summary>
		/// データからハフマン符号を生成する
		/// </summary>
		/// <param name="data">
		/// キーにノードの要素、値に符号の出現頻度を格納した辞書
		/// </param>
		public Huffman(Dictionary<TNodeElement, float> data)
		{
			//元のデータを保持
			this.data = new Dictionary<TNodeElement, float>(data);
			//木の生成時にそれぞれのノードを保持しておくリストを初期化
			leafs = new List<dynamic>();

			//与えられたデータからリーフノードを作成
			//個々のリーフノードに出現確率を割り当てる
			CreateLeafs(this.data);
			//リーフノードを降順ソート
			leafs.Sort((a, b) => a.prob.CompareTo(b.prob));
			//木の生成
			root = CreateTree(leafs);
			//木生成時に出来たノードを拡張(ランク,符号ビット,親)
			Attach(root);
			//リーフが使いやすいように反転させる
			leafs.Reverse();
		}

		/// <summary>
		/// データリストからリーフを作成し、リーフにノード出現確率を割り当てる
		/// </summary>
		/// <param name="data">データを格納した辞書</param>
		private void CreateLeafs(Dictionary<TNodeElement, float> data)
		{
			foreach(var item in data.Select((p, i) => new { p.Key, p.Value, i }))
			{
				leafs.Add(new Node<TNodeElement>(item.Key));
				leafs[item.i].prob = item.Value;
			}
		}

		/// <summary>
		/// ハフマン木を生成し、ルートノードを返す
		/// </summary>
		/// <param name="leafs">リーフ</param>
		/// <returns></returns>
		private dynamic CreateTree(List<dynamic> leafs)
		{
			Action<List<dynamic>> create_tree = null;
			create_tree = (_s) =>
			{
				//ノードの数が1になったら終了
				if(_s.Count == 1)
					return;

				//出現確率の低い2つのノードをpop
				dynamic d1 = _s[0];
				dynamic d2 = _s[1];
				_s.RemoveRange(0, 2);

				//2つのノードの親を作成する()
				dynamic new_node = new Node<char>('+', right: d1, left: d2);
				new_node.prob = d1.prob + d2.prob;

				//子に親を登録しておく
				d1.parent = new_node;
				d2.parent = new_node;

				//親をpushする
				int index = _s.FindIndex(n => n.prob > new_node.prob);
				_s.Insert(index < 0 ? _s.Count : index, new_node);

				//再帰
				create_tree(_s);
			};

			var nodes = new List<dynamic>(leafs);
			create_tree(nodes);
			//最後に残った木のルートを返す
			return nodes.First();
		}

		/// <summary>
		/// 再帰的にランク、符号ビットを割り当てる
		/// </summary>
		/// <param name="root">木のルート</param>
		private void Attach(dynamic root)
		{
			//コード、ランク、符号ビットを表す文字
			Action<dynamic, int, char> attach = null;
			attach = (n, r, c) =>
			{
				if(n == null)
					return;
				n.code = c;
				n.rank = r;
				if(MaxRank < r)
					MaxRank = r;
				attach(n.Left, r + 1, '0');
				attach(n.Right, r + 1, '1');
			};
			//ルートにはビットR(Root)を割り当てておく
			attach(root, 0, 'R');
		}

		/// <summary>
		/// 標準出力から指定ストリームへのリダイレクトの後、アクションを実行する
		/// アクション実行後、リダイレクト先は標準出力に戻る
		/// </summary>
		/// <param name="direction">リダイレクト先</param>
		/// <param name="action">実行するアクション</param>
		private void RedirectAction(TextWriter direction, Action action)
		{
			//書き込み先へリダイレクト
			Console.SetOut(direction);
			//アクションの実行
			action();
			//標準出力へ戻す
			var c = new StreamWriter(Console.OpenStandardOutput());
			c.AutoFlush = true;
			Console.SetOut(c);
		}

		/// <summary>
		/// ハフマン木を表示する
		/// </summary>
		public void PrintTree()
		{
			bool white_space = false;
			Action<dynamic, bool> print_tree = null;
			print_tree = (n, is_right) =>
			{
				if(n == null)
				{
					if(is_right)
						Console.WriteLine();
					white_space = true;
					return;
				}
				print_tree(n.Left, false);
				print_tree(n.Right, true);
				if(white_space)
				{
					for(int i = n.rank; i < MaxRank; ++i)
					{
						Console.Write("       ");
					}
					white_space = false;
				}
				Console.Write(" - {0}:{1,2}", n.Element, n.rank);
			};

			Console.WriteLine("Code Tree :");
			print_tree(root, false);
			Console.WriteLine();
		}

		/// <summary>
		/// ハフマン木を書き込む
		/// </summary>
		/// <param name="tw">テキスト書き込み先</param>
		public void PrintTree(TextWriter tw)
		{
			RedirectAction(tw, PrintTree);
		}

		public void PrintCode()
		{
			Action<dynamic> write_code = null;
			write_code = l =>
			{
				if(l == null)
					return;
				write_code(l.parent);
				Console.Write(l.code == 'R' ? ' ' : l.code);
			};

			//表示
			Console.WriteLine("Code :");
			foreach(var i in leafs)
			{
				Console.Write(i.Element + " : ");
				write_code(i);
				Console.WriteLine();
			}
		}

		public void PrintCode(TextWriter tw)
		{
			RedirectAction(tw, PrintCode);
		}

		/// <summary>
		/// 符号記号に割り付けられたコードを取得する
		/// </summary>
		/// <param name="element">符号記号</param>
		/// <returns></returns>
		public string GetCode(TNodeElement element)
		{
			StringBuilder sb = new StringBuilder();
			//該当するリーフノードを得る
			dynamic node = leafs.FirstOrDefault(n => n.Element == element);
			Action<dynamic> getcode = null;
			getcode = (n) =>
			{
				if(n == null)
					return;
				getcode(n.parent);
				if(n.code != 'R')
					sb.Append(n.code);
			};

			getcode(node);
			return sb.ToString();
		}
	}
}