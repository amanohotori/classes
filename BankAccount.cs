using System;
using System.Collections.Generic;

namespace classes
{
    public class BankAccount
    {
        // プロパティ（口座番号の初期値、口座番号、名義、残高）
        public static int accountNumberSeed = 1234567890;
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                // 残高を0で初期化して、トランザクションログから現在の残高をget
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        // 口座開設のコンストラクター（クラス呼び出しに使うクラスと同名のメソッド）
        public BankAccount(string name, decimal initialBalance)
        {
            // 口座番号を割り振りして、次の口座のために accountNumberSeed を1繰り上げておく
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            // 名義に第1引数を設定
            this.Owner = name;
            // 口座開設時の振り込みを処理
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        // トランザクションオブジェクトList<T>を宣言
        private List<Transaction> allTransactions = new List<Transaction>();

        // 振り込み処理
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            // 振込金額が負の金額であれば例外をthrow
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amout of deposit must be positive");
            }
            // 振込金額が正数であればトランザクションログを追加
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        // 引き出し処理
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            // 引き出し金額が負の金額であれば例外をthroww
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amout of withdrawal must be positive");
            }
            // 残高から引き出し金額を引いてマイナスになる（残高不足）なら例外をthrow
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            // 正常な引き出しであればトランザクションログを追加
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        // トランザクション履歴のstringを作成するGetAccountHistoryメソッド
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            // ※この "\t" は "タブ（インデント）" のエスケープ記号
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }
    }
}