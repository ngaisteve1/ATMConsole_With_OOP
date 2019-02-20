using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

public enum SecureMenu
{
    [Description("Check balance")]
    CheckBalance = 1,

    [Description("Place Deposit")]
    PlaceDeposit = 2,

    [Description("Make Withdrawal")]
    MakeWithdrawal = 3,

    [Description("Third Party Transfer")]
    ThirdPartyTransfer = 4,

    [Description("Logout")]
    Logout = 5
}
public static class ATMScreen
{
    private static CultureInfo culture = new CultureInfo("ms-MY");
    internal static string cur = "RM ";



    #region Input - Validation
    public static int ValidateInputInt(string input)
    {
        int myInt = 0;

        if (!String.IsNullOrWhiteSpace(input))
        {
            if (int.TryParse(input, out myInt))
                return myInt;
            else
                return -1;

        }
        else
            return -1;
    }

    public static decimal ValidateInputAmount(string input)
    {
        decimal myInt = 0;

        if (!String.IsNullOrWhiteSpace(input))
        {
            if (decimal.TryParse(input, out myInt))
                return myInt;
            else
                return -1;
        }
        else
            return -1;
    }
    #endregion

    #region Input - Security
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
    #endregion

    #region UIOutput - ATM Menu

    public static void ShowMenu1()
    {
        Console.Clear();
        Console.WriteLine(" ------------------------");
        Console.WriteLine("| Meybank ATM Main Menu  |");
        Console.WriteLine("|                        |");
        Console.WriteLine("| 1. Insert ATM card     |");
        Console.WriteLine("| 2. Exit                |");
        Console.WriteLine("|                        |");
        Console.WriteLine(" ------------------------");
        Console.Write("Enter your option: ");
    }

    public static void ShowMenu2()
    {
        Console.Clear();
        Console.WriteLine(" ---------------------------");
        Console.WriteLine("| Meybank ATM Secure Menu    |");
        Console.WriteLine("|                            |");
        Console.WriteLine("| 1. Balance Enquiry         |");
        Console.WriteLine("| 2. Cash Deposit            |");
        Console.WriteLine("| 3. Withdrawal              |");
        Console.WriteLine("| 4. Third Party Transfer    |");
        Console.WriteLine("| 5. Logout                  |");
        Console.WriteLine("|                            |");
        Console.WriteLine(" ---------------------------");
        Console.Write("Enter your option: ");
    }
    #endregion

    #region UIOutput - UX and output format
    public static void printDotAnimation()
    {
        for (var x = 0; x < 10; x++)
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