using System;
using Shapes;
using BinaryTreeDemo;

namespace FullDemo
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Бінарне дерево прямокутників (postorder) ===");

            var tree = new BinaryTree<Rectangle>();
            tree.Insert(new Rectangle("червоний", "чорний", 2, 3));
            tree.Insert(new Rectangle("синій", "білий", 5, 2));
            tree.Insert(new Rectangle("зелений", "жовтий", 4, 6));
            tree.Insert(new Rectangle("оранжевий", "сірий", 3, 3));

            Console.WriteLine("\nPostorder-обхід дерева:");
            foreach (var rect in tree)
            {
                Console.WriteLine(rect);
            }
        }
    }
}