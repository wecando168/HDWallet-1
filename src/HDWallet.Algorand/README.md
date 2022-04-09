### Notes  

Create Algorand wallet from extended private key (xprv)  
```csharp
string xprv = "xprv9s21ZrQH143K3xUTDYV4BjfSvuVscczTMw7LtNWgjYBLLGHPqagsJm9YyQihApsr8UFEP9ydzVzTdVezhaDVFDciCGMLhV5Yp8s2fRT7qh9";

IAccount<AlgorandWallet> account = new Account<AlgorandWallet>(xprv);
var wallet = account.GetExternalWallet(1);
Assert.AreEqual(expected: "5RKLKOVRU4WRWEKKI5I5Z6HTRNYA3XD6HGU34ZDRCDLJJ3DYQEOAOEWODY", wallet.Address);
```