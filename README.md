# ATMConsole_With_OOP
<img class="emoji" alt="atm" height="20" width="20" src="https://github.githubassets.com/images/icons/emoji/unicode/1f3e7.png"> Implemented Object-Oriented Programming like interface, class and object. Use cases: Login. Check Balance. Place Deposit. Make Withdrawal. Third party transfer. View transactions.

### Software Development Summary
- Technology: C#
- Framework: .NET Core 2.2
- Project Type: Console
- IDE: Visual Studio Code (Version 1.31.1)
- Paradigm or pattern of programming: Object-Oriented Programming (OOP)
- Data: Data of this demo program (Bank Account and Transaction data) are stored using List objects. No database is used on purpose for this demo version.

### Features / Use Cases:
- [x] Check account balance
- [x] Place deposit
- [x] Make withdraw
- [x] Check card number and pin against bank account list object (Note: No database is used on purpose to demo the use of list object)
- [x] Make third-party-transfer (Transfer within the same bank but different account number)
- [x] View bank transactions

#### Business Rules:
- User is not allow to withdraw or transfer more than the balance amount. A minimum RM20 is needed to maintain the bank account.
- If user key in the wrong pin more than 3 times, the bank account will be locked.

#### Assumption:
- All bank account are the from the same bank

### OOP principles and C# features implemented:
- class
- object
- Object and collection initializer
- encapsulation: private, internal and public
- LINQ to object (LINQ Query syntax)
- List
- static
- this
- field
- property
- const
- switch case
- string interpolation
- while loop
- enum
- region
- System.Globalization for local currency symbol and format
- ternary operator ?

### Video Demo
- [ATM Program Demo Video - Part 1](http://www.youtube.com/watch?v=sn3S3-FYbA0)
- [ATM Program Demo Video - Part 2](http://www.youtube.com/watch?v=wUR7553mbyo)

[If this content is helpful to you, consider to support and buy me a cup of coffee :) ](https://ko-fi.com/V7V2PN67)
