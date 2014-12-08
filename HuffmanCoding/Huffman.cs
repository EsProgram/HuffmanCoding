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
    /// <typeparam name="TID">ノードに付加する一意な識別記号</typeparam>
    internal class Huffman<TID>
    {
        private Dictionary<TID, float> data;

        /// <summary>
        /// データからハフマン符号を生成する
        /// </summary>
        /// <param name="data">
        /// キーにノードの識別子を
        /// 値に符号の出現頻度を格納する辞書
        /// </param>
        public Huffman(Dictionary<TID, float> data)
        {
            this.data = new Dictionary<TID, float>(data);
        }
    }
}