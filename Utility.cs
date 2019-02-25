using System;
using System.Text;
using System.Threading;
using System.Globalization;

public static class Utility
{
    private static CultureInfo culture = new CultureInfo("ms-MY");
    public static decimal GetValidDecimalInputAmt(string input)
    {
        bool valid = false;
        string rawInput;
        decimal amount = 0;

        // Get user's input input type is valid
        while (!valid)
        {
            rawInput = GetRawInput(input);
            valid = decimal.TryParse(rawInput, out amount);
            if (!valid)
                PrintMessage("Invalid input. Try again.", false);
        }

        return amount;
    }

    public static Int64 GetValidIntInputAmt(string input)
    {
        bool valid = false;
        string rawInput;
        Int64 amount = 0;

        // Get user's input input type is valid
        while (!valid)
        {
            rawInput = GetRawInput(input);
            valid = Int64.TryParse(rawInput, out amount);
            if (!valid)
                PrintMessage("Invalid input. Try again.", false);
        }

        return amount;
    }

    // public static Object GetInput(string input, int validationLength, DataType _dataType)
    // {
    //     bool valid = false;
    //     string rawInput;
    //     Object amount1 = null;

    //     // If required validation length more than 0, only validate
    //     if (validationLength > 0)
    //         // Validate until input length pass
    //         while (!valid)
    //         {
    //             rawInput = GetRawInput(input);
    //             valid = ValidateLength(rawInput, validationLength);
    //             if (!valid)
    //             {
    //                 ATMScreen.PrintMessage("Invalid input length.", false);
    //                 continue;
    //             }
    //             switch (_dataType)
    //             {
    //                 case DataType.typeInt64:
    //                     valid = decimal.TryParse(rawInput, out amount1);
    //                     amount1 = Convert.ToInt64(rawInput);

    //                     break;
    //                 case DataType.typeDecimal:
    //                     amount1 = Convert.ToDecimal(rawInput);
    //                     // decimal amount2 = 0;
    //                     // valid = decimal.TryParse(rawInput, out amount2) && amount2 > 0;
    //                     break;
    //                 default:
    //                     break;
    //             }
    //             //Check for valid data type
    //             // if (rawInput.GetType().Equals(typeof(Int64)))
    //             // {
    //             //     Int64 amount = 0;
    //             //     valid = Int64.TryParse(rawInput, out amount) && amount > 0;
    //             //    // amount1 = amount;
    //             // }
    //             // else if (rawInput.GetType().Equals(typeof(decimal)))
    //             // {
    //             //     decimal amount = 0;
    //             //     valid = decimal.TryParse(rawInput, out amount) && amount > 0;
    //             //    // amount1 = amount;
    //             // }
    //             // else if (rawInput.GetType().Equals(typeof(String)))
    //             // {
    //             //     valid = true;
    //             //    // amount1 = rawInput;
    //             // }
    //             // else
    //             // {
    //             //     ATMScreen.PrintMessage("Unhandle input type validation. Please contact the bank.", false);
    //             //     valid = false;
    //             // }
    //         }
    //     else
    //     {
    //         rawInput = GetRawInput(input);
    //     }

    //     return amount1;
    // }

    // private static bool ValidateNumber(string message, DataType _dataType, out Int64 amount)
    // {
    //     //Console.WriteLine(message);
    //     return Int64.TryParse(message, out amount) && amount > 0;
    // }

    // private static bool ValidateLength(string message, int validationLength = 0)
    // {
    //     return (message.Length == validationLength) ? true : false;
    // }

    public static string GetRawInput(string message)
    {
        Console.Write($"Enter {message}: ");
        return Console.ReadLine();
    }

    public static string GetHiddenConsoleInput()
    {
        StringBuilder input = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
            else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
        }
        return input.ToString();
    }

    #region UIOutput - UX and output format
    public static void printDotAnimation(int timer = 10)
    {
        for (var x = 0; x < timer; x++)
        {
            System.Console.Write(".");
            Thread.Sleep(100);
        }
        Console.WriteLine();
    }

    public static string FormatAmount(decimal amt)
    {
        return String.Format(culture, "{0:C2}", amt);
    }

    public static void PrintMessage(string msg, bool success)
    {
        if (success)
            Console.ForegroundColor = ConsoleColor.Yellow;
        else
            Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine(msg);
        Console.ResetColor();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    #endregion
}