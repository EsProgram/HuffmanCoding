using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
	/// <summary>
	/// 要素拡張が可能なノードオブジェクトを表現する
	/// 標準要素の取得はElementプロパティにて行う
	/// 任意にプロパティを動的拡張可能
	/// </summary>
	/// <typeparam name="TElement">ノードに割り当てられる要素の型</typeparam>
	internal class Node<TElement> : DynamicObject
	{
		private Dictionary<string, dynamic> dynamic_field;//scalable element
		private Node<TElement> left;
		private Node<TElement> right;

		public TElement Element { get; private set; }//normal element

		/// <summary>
		/// 自身の動的ラッパオブジェクトを返す
		/// </summary>
		public dynamic Mine
		{
			get { return (dynamic)this; }
		}

		public dynamic Left
		{
			get { return (dynamic)left; }
			set { left = value; }
		}

		public dynamic Right
		{
			get { return (dynamic)right; }
			set { right = value; }
		}

		public Node(TElement element, Node<TElement> left = null, Node<TElement> right = null)
			: base()
		{
			dynamic_field = new Dictionary<string, dynamic>();

			Element = element;
			Left = left;
			Right = right;
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			if(dynamic_field.ContainsKey(binder.Name))
				result = dynamic_field[binder.Name];
			else
				result = null;
			return true;
		}

		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			dynamic_field[binder.Name] = value;
			return true;
		}
	}
}