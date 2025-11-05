using System;
using System.Collections.Generic;
using System.Linq;

public delegate int StringDigitCounter(string text);

public class SimpleStack
{
    private List<object> _items = new List<object>();

    public event EventHandler StackCleared;

    public SimpleStack()
    {
    }

    public void Push(object item)
    {
        _items.Add(item);
        Console.WriteLine($"Додано в стек: {item}");
    }

    public object Pop()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("Стек порожній.");
            return null;
        }

        int lastIndex = _items.Count - 1;
        object item = _items[lastIndex];
        _items.RemoveAt(lastIndex);
        
        Console.WriteLine($"Видалено зі стеку: {item}");
        return item;
    }

    public void Clear()
    {
        Console.WriteLine("...Виконується очищення стеку...");
        _items.Clear();

        OnStackCleared(EventArgs.Empty);
    }

    protected virtual void OnStackCleared(EventArgs e)
    {
        StackCleared?.Invoke(this, e);
    }

    public int Count => _items.Count;
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Тест Завдання 1 (Делегат + Лямбда) ---");

        StringDigitCounter counter = (text) =>
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    count++;
                }
            }
            return count;
        };

        string testString = "Це тестовий рядок з 123 цифрами і ще 45.";

        int digitCount = counter(testString);

        Console.WriteLine($"Рядок: '{testString}'");
        Console.WriteLine($"Кількість цифр: {digitCount}");


        Console.WriteLine("\n--- Тест Завдання 2 і 3 (Подія) ---");

        SimpleStack stack = new SimpleStack();

        stack.StackCleared += HandleStackCleared;

        stack.Push("Яблуко");
        stack.Push(100);
        stack.Push(true);
        
        Console.WriteLine($"Елементів у стеку: {stack.Count}");
        
        stack.Pop();

        stack.Clear();
        
        Console.WriteLine($"Елементів у стеку після очищення: {stack.Count}");

        Console.ReadKey();
    }

    public static void HandleStackCleared(object sender, EventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("!!! ПОДІЯ: Стек було успішно очищено !!!");
        
        if(sender is SimpleStack s)
        {
            Console.WriteLine($"Об'єкт '{sender.GetType().Name}' надіслав подію.");
        }
        Console.ResetColor();
    }
}