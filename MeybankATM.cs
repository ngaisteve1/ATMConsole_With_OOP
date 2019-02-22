using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
using System.Threading;

namespace MeybankATMSystem
{
    class MeybankATM : IBalance, IDeposit, IWithdrawal, IThirdPartyTransfer, ILogin
    {
        private static int tries;
        private const int maxTries = 3;
        private const decimal minimum_kept_amt = 20;

        //todo: A transaction class with transaction amount can replace these two variable.
        private static decimal transaction_amt;

        private static List<BankAccount> _accountList;
        private static List<Transaction> _listOfTransactions;
        private static BankAccount selectedAccount;
        private static BankAccount inputAccount;



        public void Execute()
        {
            Initialization();
            ATMScreen.ShowMenu1();

            while (true)
            {
                switch (ATMScreen.ValidateInputInt(Console.ReadLine()))
                {
                    case 1:
                        CheckCardNoPassword();

                        while (true)
                        {
                            ATMScreen.ShowMenu2();

                            switch (ATMScreen.ValidateInputInt(Console.ReadLine()))
                            {
                                case (int)SecureMenu.CheckBalance:
                                    CheckBalance(selectedAccount);
                                    break;
                                case (int)SecureMenu.PlaceDeposit:
                                    PlaceDeposit(selectedAccount);
                                    break;
                                case (int)SecureMenu.MakeWithdrawal:
                                    MakeWithdrawal(selectedAccount);
                                    break;
                                case (int)SecureMenu.ThirdPartyTransfer:
                                    var vMThirdPartyTransfer = new VMThirdPartyTransfer();
                                    vMThirdPartyTransfer = ATMScreen.ThirdPartyTransferForm();

                                    PerformThirdPartyTransfer(selectedAccount, vMThirdPartyTransfer);
                                    break;
                                case (int)SecureMenu.Logout:
                                    Console.WriteLine("You have succesfully logout.");
                                    Execute();
                                    break;
                                default:
                                    ATMScreen.PrintMessage("Invalid Option Entered.", false);

                                    break;
                            }
                        }

                    case 2:
                        Console.WriteLine("Please collect your ATM card. Thank you for using Meybank. ");
                        Thread.Sleep(1000);
                        System.Environment.Exit(1);
                        break;
                    default:
                        ATMScreen.PrintMessage("Invalid Option Entered.", false);
                        break;
                }
            }
        }

        private static void LockAccount()
        {
            Console.Clear();
            ATMScreen.PrintMessage("Your account is locked.", true);
            Console.WriteLine("Please go to the nearest branch to unlocked your account.");
            Console.WriteLine("Thank you for using Meybank. ");
            Console.ReadKey();
            System.Environment.Exit(1);
        }

        private static void Initialization()
        {
            transaction_amt = 0;

            _accountList = new List<BankAccount>
            {
                new BankAccount() { FullName = "John", AccountNumber=333111, CardNumber = 123, PinCode = 111111, Balance = 2000.00m, isLocked = false },
                new BankAccount() { FullName = "Mike", AccountNumber=111222, CardNumber = 456, PinCode = 222222, Balance = 1500.30m, isLocked = true },
                new BankAccount() { FullName = "Mary", AccountNumber=888555, CardNumber = 789, PinCode = 333333, Balance = 2900.12m, isLocked = false }
            };
        }

        public void CheckCardNoPassword()
        {
            bool pass = false;

            while (!pass)
            {
                inputAccount = new BankAccount();


                Console.WriteLine("\nNote: Actual ATM system will accept user's ATM card to validate");
                Console.Write("and read card number, bank account number and bank account status. \n\n");
                Console.Write("Enter ATM Card Number: ");
                inputAccount.CardNumber = Convert.ToInt32(Console.ReadLine());
                // for brevity, no extra null, empty, space, data type validation here.

                Console.Write("Enter 6 Digit PIN: ");
                inputAccount.PinCode = Convert.ToInt32(ATMScreen.GetHiddenConsoleInput());
                // for brevity, no extra null, empty, space, data type validation here.


                System.Console.Write("\nChecking card number and password.");
                ATMScreen.printDotAnimation();

                foreach (BankAccount account in _accountList)
                {
                    if (inputAccount.CardNumber.Equals(account.CardNumber))
                    {
                        selectedAccount = account;

                        if (inputAccount.PinCode.Equals(account.PinCode))
                        {
                            if (selectedAccount.isLocked)
                                LockAccount();
                            else
                                pass = true;


                        }
                        else
                        {

                            pass = false;
                            tries++;

                            if (tries >= maxTries)
                            {
                                selectedAccount.isLocked = true;

                                LockAccount();
                            }

                        }
                    }
                }

                if (!pass)
                    ATMScreen.PrintMessage("Invalid Card number or PIN.", false);

                Console.Clear();
            }
        }





        public void CheckBalance(BankAccount bankAccount)
        {
            ATMScreen.PrintMessage($"Your bank account balance amount is: {ATMScreen.FormatAmount(bankAccount.Balance)}", true);
        }

        public void PlaceDeposit(BankAccount account)
        {

            Console.WriteLine("\nNote: Actual ATM system will just let you ");
            Console.Write("place bank notes into ATM machine. \n\n");
            Console.Write("Enter amount: " + ATMScreen.cur);
            transaction_amt = ATMScreen.ValidateInputAmount(Console.ReadLine());

            System.Console.Write("\nCheck and counting bank notes.");
            ATMScreen.printDotAnimation();

            if (transaction_amt <= 0)
                ATMScreen.PrintMessage("Amount needs to be more than zero. Try again.", false);
            else if (transaction_amt % 10 != 0)
                ATMScreen.PrintMessage($"Key in the deposit amount only with multiply of 10. Try again.", false);
            else
            {
                if (PreviewBankNotesCount(transaction_amt))
                {
                    account.Balance = account.Balance + transaction_amt;

                    ATMScreen.PrintMessage($"You have successfully deposited {ATMScreen.FormatAmount(transaction_amt)}", true);
                }
                else
                    ATMScreen.PrintMessage($"You have cancelled your action.", false);


            }
        }

        public void MakeWithdrawal(BankAccount account)
        {
            Console.WriteLine("\nNote: For GUI or actual ATM system, user can ");
            Console.Write("choose some default withdrawal amount or custom amount. \n\n");

            Console.Write("Enter amount: " + ATMScreen.cur);
            transaction_amt = ATMScreen.ValidateInputAmount(Console.ReadLine());

            if (transaction_amt <= 0)
                ATMScreen.PrintMessage("Amount needs to be more than zero. Try again.", false);
            else if (transaction_amt > account.Balance)
                ATMScreen.PrintMessage($"Withdrawal failed. You do not have enough fund to withdraw {ATMScreen.FormatAmount(transaction_amt)}", false);
            else if ((account.Balance - transaction_amt) < minimum_kept_amt)
                ATMScreen.PrintMessage($"Withdrawal failed. Your account needs to have minimum {ATMScreen.FormatAmount(minimum_kept_amt)}", false);
            else if (transaction_amt % 10 != 0)
                ATMScreen.PrintMessage($"Key in the deposit amount only with multiply of 10. Try again.", false);
            else
            {
                account.Balance = account.Balance - transaction_amt;

                ATMScreen.PrintMessage($"Please collect your money. You have successfully withdraw {ATMScreen.FormatAmount(transaction_amt)}", true);
            }
        }



        public void PerformThirdPartyTransfer(BankAccount bankAccount, VMThirdPartyTransfer vMThirdPartyTransfer)
        {
            Console.ReadKey();
            Console.WriteLine("\nNote: For GUI or actual ATM system, user can ");
            Console.Write("choose some default withdrawal amount or custom amount. \n\n");

            if (vMThirdPartyTransfer.TransferAmount > 0)
            {
                if (vMThirdPartyTransfer.TransferAmount > bankAccount.Balance)
                    ATMScreen.PrintMessage($"Withdrawal failed. You do not have enough fund to withdraw {ATMScreen.FormatAmount(transaction_amt)}", false);
                else if (bankAccount.Balance - vMThirdPartyTransfer.TransferAmount < 20)
                    ATMScreen.PrintMessage($"Withdrawal failed. Your account needs to have minimum {ATMScreen.FormatAmount(minimum_kept_amt)}", false);
                else
                {
                    // Check if the receiver's bank account number is valid.
                    var selectedBankAccountReceiver = (from b in _accountList
                                                       where b.AccountNumber == bankAccount.AccountNumber
                                                       //&& b.Bank == Banks.MBB // Third party is for same bank.
                                                       select b).FirstOrDefault();

                    if (selectedBankAccountReceiver == null)
                        ATMScreen.PrintMessage($"Third party transfer failed. Receiver bank account number is invalid.", false);
                    else
                    {
                        // Update balance amount (Giver)
                        bankAccount.Balance = bankAccount.Balance - vMThirdPartyTransfer.TransferAmount;

                        // Update balance amount (Receiver)
                        selectedBankAccountReceiver.Balance = selectedBankAccountReceiver.Balance + vMThirdPartyTransfer.TransferAmount;

                        // Add transaction record.
                        Transaction transaction = new Transaction()
                        {
                            BankAccountNoFrom = bankAccount.AccountNumber,
                            BankAccountNoTo = vMThirdPartyTransfer.RecipientBankAccountNumber,
                            TransactionAmount = vMThirdPartyTransfer.TransferAmount,
                            TransactionDate = DateTime.Now
                        };
                        _listOfTransactions.Add(transaction);
                        ATMScreen.PrintMessage($"You have successfully transferred out {ATMScreen.FormatAmount(transaction_amt)} to {vMThirdPartyTransfer.RecipientBankAccountName}", true);

                        // Check giver's total transaction amount.                        
                        //     var transferLimitCheck = (from b in _accountList
                        //                               join t in _listOfTransactions
                        //                               on b.AccountNumber equals t.BankAccountNoFrom
                        //                               where
                        //                               //&& b.Bank == Banks.MBB // Third party is for same bank.\
                        //                               t.TransactionDate.Day == DateTime.Now.Day
                        //                               && t.TransactionDate.Month == DateTime.Now.Month
                        //                               && t.TransactionDate.Year == DateTime.Now.Year
                        //                               group t by t.BankAccountNoFrom into groupResult
                        //                               select new
                        //                               {
                        //                                   totalTransactionAmt = groupResult.Sum(s => s.TransactionAmount)
                        //                               }
                        //                               ).FirstOrDefault();

                        //     decimal totalTransactionAmt = transferLimitCheck != null ? transferLimitCheck.totalTransactionAmt : 0;

                        //     if (totalTransactionAmt + vMThirdPartyTransfer.TransferAmount >= 30000)
                        //     {
                        //         ATMScreen.PrintMessage($"Withdrawal failed. Third party transfer failed. Daily limit {ATMScreen.FormatAmount(30000)} reached. ", false);
                        //     }
                        //     else
                        //     {
                        //         // Update balance amount (Giver)
                        //         bankAccount.Balance = bankAccount.Balance - vMThirdPartyTransfer.TransferAmount;

                        //         // Update balance amount (Receiver)
                        //         selectedBankAccountReceiver.Balance = selectedBankAccountReceiver.Balance + vMThirdPartyTransfer.TransferAmount;

                        //         // Add transaction record.
                        //         Transaction transaction = new Transaction()
                        //         {
                        //             BankAccountId = selectedBankAccount.Id,
                        //             BankAccountNoFrom = selectedBankAccount.AccountNumber,
                        //             BankAccountNoTo = thirdParty.BankAccountNumberTo,
                        //             TransactionAmount = thirdParty.TransferAmount,
                        //             TransactionDate = DateTime.Now,
                        //             TransferMode = TransferMode.ThirdPartyTransfer,
                        //             ReceiverBank = selectedBankAccountReceiver.Bank
                        //         };
                        //         db.Transactions.Add(transaction);

                        //         db.SaveChanges();

                        //         ATMScreen.PrintMessage($"You have successfully transferred {ATMScreen.FormatAmount(transaction_amt)}", true);
                        //     }
                        // }
                    }
                }
            }
        }

        private static bool PreviewBankNotesCount(decimal amount)
        {
            int hundredNotesCount = (int)amount / 100;
            int fiftyNotesCount = ((int)amount % 100) / 50;
            int tenNotesCount = ((int)amount % 50) / 10;

            Console.WriteLine("\nSummary");
            Console.WriteLine("-------");
            Console.WriteLine($"{ATMScreen.cur} 100 x {hundredNotesCount} = {100 * hundredNotesCount}");
            Console.WriteLine($"{ATMScreen.cur} 50 x {fiftyNotesCount} = {50 * fiftyNotesCount}");
            Console.WriteLine($"{ATMScreen.cur} 10 x {tenNotesCount} = {10 * tenNotesCount}");
            Console.Write($"Total amount: {ATMScreen.FormatAmount(amount)}");

            Console.Write("\n\nPress 1 to confirm or 0 to cancel: ");
            string opt = Console.ReadLine();

            return (opt.Equals("1")) ? true : false;
        }
    }
}
