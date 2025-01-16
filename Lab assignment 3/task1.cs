#include <iostream>
#include <regex>
#include <string>

int main()
{
    // Regular expression for a floating-point number with a max length of 6
    std::regex floatRegex("^[-+]?\\d{1,5}(\\.\\d{1,4})?$");

    // Test input
    std::string input;

    std::cout << "Enter a number: ";
    std::cin >> input;

    // Check if the input matches the regex
    if (input.length() <= 6 && std::regex_match(input, floatRegex))
    {
        std::cout << input << " is a valid floating-point number of length <= 6.\n";
    }
    else
    {
        std::cout << input << " is NOT a valid floating-point number or it exceeds length 6.\n";
    }

    return 0;
}
