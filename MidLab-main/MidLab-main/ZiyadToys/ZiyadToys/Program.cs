using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Define the initial and final states
        string initialState = "S0";
        string finalState = "S4";  // Reaching S4 means the sequence is valid.

        // State transition dictionary
        var stateTransitions = new Dictionary<string, Dictionary<string, string>>
        {
            { "S0", new Dictionary<string, string> { { "start", "S1" } } },
            { "S1", new Dictionary<string, string> { { "accelerate", "S2" }, { "stop", "S4" } } },
            { "S2", new Dictionary<string, string> { { "right", "S3" }, { "brake", "S4" } } },
            { "S3", new Dictionary<string, string> { { "stop", "S4" } } },
            { "S4", new Dictionary<string, string>() } // No more transitions from S4
        };

        // Prompt user to enter commands
        Console.WriteLine("Enter car commands (separated by spaces) or 'exit' to quit:");
        while (true)
        {
            Console.Write("\nInput: ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "exit")
                break;

            string[] commands = input.Split(' ');
            string currentState = initialState;

            bool isValid = true;
            foreach (string command in commands)
            {
                // Check if "left" is used (unsupported feature)
                if (command == "left")
                {
                    Console.WriteLine("ERROR: This car does not support left turns!");
                    isValid = false;
                    break;
                }

                // Check for valid state transitions
                if (stateTransitions[currentState].ContainsKey(command))
                {
                    currentState = stateTransitions[currentState][command];
                }
                else
                {
                    Console.WriteLine($"ERROR: Invalid command '{command}' in state '{currentState}'!");
                    isValid = false;
                    break;
                }
            }

            // Display result based on final state
            if (isValid)
            {
                if (currentState == finalState)
                    Console.WriteLine("RESULT OKAY: Commands executed successfully!");
                else
                    Console.WriteLine("ERROR: Incomplete command sequence!");
            }
        }
    }
}