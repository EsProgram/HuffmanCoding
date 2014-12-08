using System;
using System.Collections.Generic;
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
        private Dictionary<TNodeElement, float> data;
        private List<dynamic> nodes;
        private dynamic root;

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
            nodes = new List<dynamic>();

            ////////////////////////////////////////////////
            //ToDo

            //与えられたデータからリーフノードを作成
            //個々のリーフノードに出現確率を割り当てる
            CreateLeafs(this.data);
            //リーフノードを降順ソート
            nodes.Sort((a, b) => a.prob.CompareTo(b.prob));
            //木の生成
            root = CreateTree(nodes);
            //木生成時に出来たノードを拡張
            ////////////////////////////////////////////////
        }

        /// <summary>
        /// データリストからリーフを作成し、リーフにノード出現確率を割り当てる
        /// </summary>
        /// <param name="data">データを格納した辞書</param>
        private void CreateLeafs(Dictionary<TNodeElement, float> data)
        {
            foreach(var item in data.Select((p, i) => new { p.Key, p.Value, i }))
            {
                nodes.Add(new Node<TNodeElement>(item.Key));
                nodes[item.i].prob = item.Value;
            }
        }

        private dynamic CreateTree(List<dynamic> leafs)
        {
            Stack<dynamic> s = new Stack<dynamic>(leafs);
            //ノードの数が1になったら終了
            //出現確率の低い2つのノードをpop
            //2つのノードの親を作成する
            //親に新しいprob(出現確率)を与える
            //親をpushする

            //最後に残った木のルートを返す
            return null;
        }
    }
}