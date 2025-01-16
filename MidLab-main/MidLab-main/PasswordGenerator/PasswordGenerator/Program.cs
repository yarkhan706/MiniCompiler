using System;
using System.Text;
using System.Text.RegularExpressions;

public class PasswordGenerator
{
    public static void Main()
    {
        string registrationNumber = "35"; 
        string firstName = "Asfand";              
        string lastName = "Yarkhan";              
        string favoriteMovie = "Inception";       

        string password = GeneratePassword(registrationNumber, firstName, lastName, favoriteMovie);
        Console.WriteLine("Generated Password: " + password);
    }

    private static string GeneratePassword(string regNo, string fName, string lName, string favMovie)
    {
        // Regular expression to ensure password meets the criteria
        string pattern = @"^(?=.*\d{2})(?=.*[a-zA-Z]{2})(?=.*[^\w#])(?=.{14})";
        Regex regex = new Regex(pattern);

        Random rnd = new Random();
        string password;

        do
        {
            // Extract 2 digits from the registration number
            string regDigits = ExtractTwoDigits(regNo);

            // Get the second letters from first name and last name
            string nameLetters = (fName.Length > 1 ? fName[1].ToString() : "") +
                                 (lName.Length > 1 ? lName[1].ToString() : "");

            // Get first 2 characters from the favorite movie
            string movieChars = favMovie.Substring(0, 2);

            // Define allowed special characters excluding '#'
            string specialChars = "@$%&*!";

            // Pick two random special characters
            char specialChar1 = specialChars[rnd.Next(specialChars.Length)];
            char specialChar2 = specialChars[rnd.Next(specialChars.Length)];

            // Combine all parts
            StringBuilder passwordBuilder = new StringBuilder();
            passwordBuilder.Append(regDigits);
            passwordBuilder.Append(nameLetters);
            passwordBuilder.Append(movieChars);
            passwordBuilder.Append(specialChar1);
            passwordBuilder.Append(specialChar2);

            // Add random letters to meet length requirement of 14
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            while (passwordBuilder.Length < 14)
            {
                passwordBuilder.Append(alphabet[rnd.Next(alphabet.Length)]);
            }

            password = passwordBuilder.ToString();

        } while (!regex.IsMatch(password)); // Repeat until password matches criteria

        return password;
    }

    private static string ExtractTwoDigits(string input)
    {
        // Helper function to extract first two digits found in a string
        StringBuilder digits = new StringBuilder();
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                digits.Append(c);
                if (digits.Length == 2)
                {
                    break;
                }
            }
        }
        return digits.ToString().PadRight(2, '0'); // Pads with '0' if fewer than 2 digits are found
    }
}
