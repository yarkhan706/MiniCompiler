using System;
using System.Collections.Generic;

class Program
{
    // Grammar productions and their SDT actions
    static Dictionary<string, Func<List<string>, string>> SDT = new Dictionary<string, Func<List<string>, string>>()
    {
        { "S → AA", values => values[0] + values[1] },
        { "A → aA", values => "a" + values[0] },
        { "A → b",  values => "b" },
        { "S' → S", values => values[0] }
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Enter input string (e.g., 'ab'):");
        string input = Console.ReadLine();
        Queue<char> inputQueue = new Queue<char>(input);

        Stack<string> stack = new Stack<string>();
        Dictionary<string, string> values = new Dictionary<string, string> { { "S", null }, { "A", null } };

        string currentState = "I0";
        Console.WriteLine($"Initial state: {currentState}");

        while (true)
        {
            Console.WriteLine($"Stack: {string.Join(" ", stack.ToArray())}");
            Console.WriteLine($"Remaining input: {string.Join("", inputQueue)}");
            Console.WriteLine($"Current state: {currentState}");

            if (inputQueue.Count == 0 && currentState == "I5")
            {
                Console.WriteLine("String accepted.");
                Console.WriteLine($"Final value: {values["S"]}");
                return;
            }
            else if (inputQueue.Count == 0)
            {
                Console.WriteLine("Parsing completed with errors.");
                return;
            }

            char currentChar = inputQueue.Count > 0 ? inputQueue.Peek() : '\0';

            switch (currentState)
            {
                case "I0":
                    if (currentChar == 'a')
                    {
                        stack.Push("a");
                        inputQueue.Dequeue();
                        currentState = "I3";
                    }
                    else if (currentChar == 'b')
                    {
                        stack.Push("b");
                        inputQueue.Dequeue();
                        currentState = "I4";
                    }
                    else
                    {
                        Console.WriteLine("Unexpected input. Exiting.");
                        return;
                    }
                    break;

                case "I3":
                    if (currentChar == 'a')
                    {
                        stack.Push("a");
                        inputQueue.Dequeue();
                        currentState = "I3";
                    }
                    else if (currentChar == 'b')
                    {
                        stack.Push("b");
                        inputQueue.Dequeue();
                        currentState = "I4";
                    }
                    else
                    {
                        // Reduce A → aA
                        string aVal = stack.Pop();
                        string a1Val = values["A"] ?? "";
                        values["A"] = SDT["A → aA"](new List<string> { a1Val });
                        Console.WriteLine($"Reduced: A → aA, A.val = {values["A"]}");
                        currentState = "I2";
                    }
                    break;

                case "I4":
                    // Reduce A → b
                    stack.Pop();
                    values["A"] = SDT["A → b"](new List<string>());
                    Console.WriteLine($"Reduced: A → b, A.val = {values["A"]}");
                    currentState = "I2";
                    break;

                case "I2":
                    if (stack.Count >= 1 && values["A"] != null)
                    {
                        // Combine A → AA
                        string a2Val = values["A"];
                        stack.Pop(); // Remove A
                        string a1Val = values["A"] ?? "";
                        values["S"] = SDT["S → AA"](new List<string> { a1Val, a2Val });
                        Console.WriteLine($"Reduced: S → AA, S.val = {values["S"]}");
                        currentState = "I5";
                    }
                    else
                    {
                        Console.WriteLine("Unexpected reduction. Exiting.");
                        return;
                    }
                    break;

                case "I5":
                    // Accept the string
                    if (inputQueue.Count == 0)
                    {
                        Console.WriteLine("String accepted.");
                        Console.WriteLine($"Final value: {values["S"]}");
                        return;
                    }
                    break;

                default:
                    Console.WriteLine("Unexpected state. Exiting.");
                    return;
            }
        }
    }
}
