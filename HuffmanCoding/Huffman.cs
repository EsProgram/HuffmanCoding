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
            nodes.OrderBy(n => n.prob);
            //木の生成
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
    }
}