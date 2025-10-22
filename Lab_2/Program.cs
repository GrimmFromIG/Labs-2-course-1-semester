using System;
using System.Collections;
using System.Collections.Generic;
using Shapes;
using BinaryTreeDemo;

namespace FullDemo

{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var r1 = new Rectangle("червоний", "чорний", 2, 3);
            var r2 = new Rectangle("синій", "білий", 5, 2);
            var r3 = new Rectangle("зелений", "жовтий", 4, 6);
            var r4 = new Rectangle("оранжевий", "сірий", 3, 3);

            
            Console.WriteLine("--- Завдання 2.1: Робота з Масивом (Rectangle[]) ---");
            Rectangle[] rectArray = new Rectangle[4];
            rectArray[0] = r1;
            rectArray[1] = r2;
            rectArray[2] = r3;
            Console.WriteLine("Масив (ітерація):");
            foreach (var rect in rectArray) { if (rect != null) Console.WriteLine(rect); }
            
            Console.WriteLine("\nМасив (оновлення [1]):");
            rectArray[1] = r4; 
            Console.WriteLine($"Нове значення: {rectArray[1]}");

            Console.WriteLine("\nМасив (пошук 'червоний' r1):");
            for (int i = 0; i < rectArray.Length; i++)
            {
                if (rectArray[i] != null && rectArray[i].ColorFill == "червоний")
                {
                    Console.WriteLine($"Знайдено на позиції {i}: {rectArray[i]}");
                    break;
                }
            }

            Console.WriteLine("\nМасив (видалення r1 з індексом 0):");
            rectArray[0] = null; 
            Console.WriteLine("Масив після видалення:");
            foreach (var rect in rectArray) 
            { 
                Console.WriteLine(rect?.ToString() ?? "NULL"); 
            }


            Console.WriteLine("\n--- Завдання 2.2: Неузагальнена колекція (ArrayList) ---");
            ArrayList arrayList = new ArrayList();
            arrayList.Add(r1);
            arrayList.Add(r2);
            arrayList.Add(r3);
            Console.WriteLine("ArrayList (ітерація):");
            foreach (object obj in arrayList) { Console.WriteLine((Rectangle)obj); }

            Console.WriteLine("\nArrayList (оновлення [1]):");
            arrayList[1] = r4; 
            Console.WriteLine($"Нове значення: {(Rectangle)arrayList[1]}");

            Console.WriteLine("\nArrayList (пошук r3):");
            if (arrayList.Contains(r3)) { Console.WriteLine($"Знайдено на позиції {arrayList.IndexOf(r3)}"); }

            Console.WriteLine("\nArrayList (видалення r1):");
            arrayList.Remove(r1);
            Console.WriteLine("ArrayList після видалення:");
            foreach (object obj in arrayList) { Console.WriteLine((Rectangle)obj); }


            Console.WriteLine("\n--- Завдання 2.3: Узагальнена колекція (List<Rectangle>) ---");
            List<Rectangle> list = new List<Rectangle>();
            list.Add(r1);
            list.Add(r2);
            list.Add(r3);
            Console.WriteLine("List (ітерація):");
            foreach (Rectangle rect in list) { Console.WriteLine(rect); }

            Console.WriteLine("\nList (оновлення [1]):");
            list[1] = r4; 
            Console.WriteLine($"Нове значення: {list[1]}");

            Console.WriteLine("\nList (пошук площі 24):");
            Rectangle foundRectList = list.Find(rect => rect.GetArea() == 24);
            if (foundRectList != null) { Console.WriteLine($"Знайдено: {foundRectList}"); }

            Console.WriteLine("\nList (видалення r1):");
            list.Remove(r1);
            Console.WriteLine("List після видалення:");
            foreach (Rectangle rect in list) { Console.WriteLine(rect); }
            
            Console.WriteLine("\n--- Завдання 3: Бінарне дерево (узагальнене, <T : class>) ---");
            
            var tree = new BinaryTree<Rectangle>();
            tree.Insert(new Rectangle("червоний", "чорний", 2, 3));
            tree.Insert(new Rectangle("синій", "білий", 5, 2));
            tree.Insert(new Rectangle("зелений", "жовтий", 4, 6));
            tree.Insert(new Rectangle("оранжевий", "сірий", 3, 3));

            Console.WriteLine("\nДемонстрація обходу дерева (Postorder - Варіант 5):");
            foreach (var rect in tree)
            {
                Console.WriteLine(rect);
            }
        }
    }
}