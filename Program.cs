using System;

namespace classes
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1000円で口座開設
            var account = new BankAccount("hotori", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");

            // 500円引き出す
            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            // 友だちから100円振り込まれる
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);
            /* 残高600円を超えて引き出してみる（例外のテスト）
            account.MakeWithdrawal(800, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            */

            /* マイナスの残高で口座を開設してtry/catch文で例外をスルーしてみる
            BankAccount invalidAccount;
            try
            {
                invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
                return;
            }
            */
            /* 引き出しで残高がマイナスになる場合のテスト（try/catch文で例外をスルー）
            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }
            */
            Console.WriteLine(account.GetAccountHistory());
        }
    }
}
