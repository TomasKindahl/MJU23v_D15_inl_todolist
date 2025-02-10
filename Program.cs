using System;
using System.Collections.Generic;

public static class MyIO
{
    // Läs inkommando från användaren
    public static string ReadCommand(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    // Jämför om kommandot är lika med förväntat kommando
    public static bool CommandEquals(string input, string expected)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        var parts = input.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
        return string.Equals(parts[0], expected, StringComparison.OrdinalIgnoreCase);
    }

    // Hämta antalet argument i kommandot
    public static int NumArguments(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? 0 : input.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries).Length - 1;
    }

    // Hämta ett specifikt argument från kommandot
    public static string Argument(string input, int index)
    {
        var parts = input.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
        return (index < parts.Length) ? parts[index] : string.Empty;
    }

    // Kontrollera om kommandot har ett specifikt argument
    public static bool HasArgument(string input, string argument)
    {
        return !string.IsNullOrWhiteSpace(input) && input.IndexOf(argument, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}

public class Todo
{
    public enum TodoItem { Active, Ready, Waiting }

    private static Dictionary<int, TodoItem> todoList = new Dictionary<int, TodoItem>();

    public static void ReadListFromFile()
    {
        // Simulerar inläsning från en fil
        todoList[1] = TodoItem.Waiting;
        todoList[2] = TodoItem.Active;
        todoList[3] = TodoItem.Ready;
    }

    public static void PrintHelp()
    {
        Console.WriteLine("Kommandon:");
        Console.WriteLine("hjälp - Visa hjälp");
        Console.WriteLine("aktivera /nummer/ - Aktivera ett objekt");
        Console.WriteLine("klar /nummer/ - Markera ett objekt som klart");
        Console.WriteLine("vänta /nummer/ - Sätt ett objekt på vänt");
        Console.WriteLine("lista - Visa listan");
        Console.WriteLine("sluta - Avsluta programmet");
    }

    public static void TrySetStatus(string argument, TodoItem status)
    {
        if (int.TryParse(argument, out int itemId) && todoList.ContainsKey(itemId))
        {
            todoList[itemId] = status;
            Console.WriteLine($"Objekt {itemId} har nu status: {status}");
        }
        else
        {
            Console.WriteLine("Fel: Ange ett giltigt nummer som finns i listan.");
        }
    }

    public static void PrintTodoList(bool verbose)
    {
        Console.WriteLine("Att-göra-lista:");
        foreach (var item in todoList)
        {
            Console.WriteLine($"- {item.Key}: {item.Value}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Välkommen till att-göra-listan!");
        Todo.ReadListFromFile();
        Todo.PrintHelp();
        string command;

        do
        {
            command = MyIO.ReadCommand("> ").Trim();

            if (MyIO.CommandEquals(command, "hjälp"))
            {
                Todo.PrintHelp();
            }
            else if (MyIO.CommandEquals(command, "aktivera"))
            {
                Todo.TrySetStatus(MyIO.Argument(command, 1), Todo.TodoItem.Active);
            }
            else if (MyIO.CommandEquals(command, "klar"))
            {
                Todo.TrySetStatus(MyIO.Argument(command, 1), Todo.TodoItem.Ready);
            }
            else if (MyIO.CommandEquals(command, "vänta"))
            {
                Todo.TrySetStatus(MyIO.Argument(command, 1), Todo.TodoItem.Waiting);
            }
            else if (MyIO.CommandEquals(command, "lista"))
            {
                Todo.PrintTodoList(MyIO.HasArgument(command, "allt"));
            }
            else if (MyIO.CommandEquals(command, "sluta"))
            {
                Console.WriteLine("Hej då!");
                break;
            }
            else
            {
                Console.WriteLine($"Okänt kommando: {command}");
            }
        }
        while (true);
    }
}
