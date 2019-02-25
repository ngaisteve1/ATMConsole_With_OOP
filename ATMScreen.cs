using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;

public enum SecureMenu
{
    // Value 1 is needed because menu starts with 1 while enum starts with 0 if no value given.

    [Description("Check balance")]
    CheckBalance = 1,

    [Description("Place Deposit")]
    PlaceDeposit = 2,

    [Description("Make Withdrawal")]
    MakeWithdrawal = 3,

    [Description("Third Party Transfer")]
    ThirdPartyTransfer = 4,
    
    [Description("Transaction")]
    ViewTransaction = 5,

    [Description("Logout")]
    Logout = 6
}

public static class ATMScreen
{
    internal static string cur = "RM ";

    #region ATM UI Forms
    public static VMThirdPartyTransfer ThirdPartyTransferForm(){
        var vMThirdPartyTransfer = new VMThirdPartyTransfer();

        ///Console.Write("\nRecipient's account number: ");
        //vMThirdPartyTransfer.RecipientBankAccountNumber = Convert.ToInt64(Console.ReadLine()); // no validation here yet.
        vMThirdPartyTransfer.RecipientBankAccountNumber = Utility.GetValidIntInputAmt("recipient's account number");

        //Console.Write($"\nTransfer amount: {cur}");
        vMThirdPartyTransfer.TransferAmount = Utility.GetValidDecimalInputAmt("amount");

        //Console.Write("\nRecipient's account name: ");
        vMThirdPartyTransfer.RecipientBankAccountName = Utility.GetRawInput("recipient's account name");
        // no validation here yet.

        return vMThirdPartyTransfer;
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
        Console.WriteLine("| 5. Transactions            |");
        Console.WriteLine("| 6. Logout                  |");
        Console.WriteLine("|                            |");
        Console.WriteLine(" ---------------------------");
    }
    #endregion

    




}