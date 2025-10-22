using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTreeDemo
{
    public class BinaryTreeNode<T>
    {
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }
    }

    public class BinaryTree<T> : IEnumerable<T> where T : class, IComparable<T>
    {
        private BinaryTreeNode<T> root;

        public void Insert(T value)
        {
            root = InsertRecursive(root, value);
        }

        private BinaryTreeNode<T> InsertRecursive(BinaryTreeNode<T> node, T value)
        {
            if (node == null)
            {
                return new BinaryTreeNode<T>(value);
            }

            if (value.CompareTo(node.Data) < 0)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            else
            {
                node.Right = InsertRecursive(node.Right, value);
            }

            return node;
        }
        
        private void PostOrder(BinaryTreeNode<T> node, List<T> list)
        {
            if (node == null) return;
            PostOrder(node.Left, list);
            PostOrder(node.Right, list);
            list.Add(node.Data);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var list = new List<T>();
            PostOrder(root, list);
            foreach (var item in list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}