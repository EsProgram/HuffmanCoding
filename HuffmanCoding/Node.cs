using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    internal class Node<Tkey, Tval> : DynamicObject
    {
        private Dictionary<string, object> dynamic_field;//scalable element
        private Node<Tkey, Tval> left;
        private Node<Tkey, Tval> right;

        public char Element { get; private set; }//normal element

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

        public Node(char code, Node<Tkey, Tval> left = null, Node<Tkey, Tval> right = null)
            : base()
        {
            dynamic_field = new Dictionary<string, object>();

            Element = code;
            Left = left;
            Right = right;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(dynamic_field.ContainsKey(binder.Name))
            {
                result = dynamic_field[binder.Name];
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            dynamic_field[binder.Name] = value;
            return true;
        }
    }
}