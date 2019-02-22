
using System;

public class BankAccount
{
    public string FullName { get; set; }
    public Int64 AccountNumber { get; set; }
    public Int64 CardNumber { get; set; }
    public Int64 PinCode { get; set; }
    public decimal Balance { get; set; }

    public bool isLocked { get; set; }
}

