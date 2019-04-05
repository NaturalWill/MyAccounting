using MyAccounting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAccounting
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        AssetShowContext db;
        List<Account> AccountList;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            db = new AssetShowContext();
            cbAccount.ItemsSource = AccountList = db.Accounts.OrderBy(x => x.Priority).ToList();

            if (cbAccount.Items.Count > 0)
                cbAccount.SelectedIndex = 0;
        }
        private void cbAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FreshAccountItems();

        }

        private void FreshAccountItems()
        {
            if (cbAccount.SelectedIndex < 0) return;

            dgRecord.ItemsSource = GetDisplayRecord((cbAccount.SelectedItem as Account).Records);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtAsset.Text = txtDebt.Text = txtLimit.Text = txtUsable.Text = txtInfo.Text = "";

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (cbAccount.SelectedIndex < 0) return;

            decimal.TryParse(txtAsset.Text.Trim(), out var asset);
            decimal.TryParse(txtDebt.Text.Trim(), out var debt);
            decimal.TryParse(txtLimit.Text.Trim(), out var limit);
            decimal.TryParse(txtUsable.Text.Trim(), out var usable);
            if (debt == 0 && (usable > 0 && limit > 0))
                debt = limit - usable;
            var record = new Record
            {
                RecordDate = dpDate.SelectedDate == null ? DateTime.Now : dpDate.SelectedDate.Value,
                AccountId = (cbAccount.SelectedItem as Account).AccountId,
                Asset = asset,
                Debt = debt,
                Info = txtInfo.Text.Trim(),
            };

            db.Records.Add(record);
            db.SaveChanges();

            dgRecord.ItemsSource = GetDisplayRecord((cbAccount.SelectedItem as Account).Records);

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = dgRecord.SelectedItem;
            if (item is DispalyRecord record &&
                MessageBox.Show($"确认要删除记录 {record.RecordId}", "删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var toRemove = db.Records.Where(x => x.RecordId == record.RecordId).First();
                db.Records.Remove(toRemove);
                db.SaveChangesAsync();
                FreshAccountItems();
            }
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            var item = dgRecord.SelectedItem;
            if (item is DispalyRecord record &&
                MessageBox.Show($"确认要修改记录 {record.RecordId}", "警告", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var toChange = db.Records.Where(x => x.RecordId == record.RecordId).First();
                if (toChange != null)
                {

                    toChange.RecordDate = record.RecordDate;
                    toChange.Asset = record.Asset;
                    toChange.Debt = record.Debt;
                    toChange.Info = record.Info;
                    db.SaveChanges();
                    MessageBox.Show("change success.");
                    FreshAccountItems();
                }

            }

        }
        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            var a = txtAccount.Text.Trim();
            var c = db.Accounts.Where(x => x.Name == a).Count();
            if (c == 0)
            {
                db.Accounts.Add(new Account { Name = a });
                db.SaveChanges();
                cbAccount.ItemsSource = db.Accounts.ToList();
            }
        }

        private void AccountDetail_Click(object sender, RoutedEventArgs e)
        {
            new AccountDetail().Show();

        }

        private void AccountTotal_Click(object sender, RoutedEventArgs e)
        {
            var list = GetTotalDisplayRecords();
            dgRecord.ItemsSource = list;
        }

        private List<TotalDispalyRecord> GetTotalDisplayRecords()
        {
            List<TotalDispalyRecord> record = new List<TotalDispalyRecord>();
            TotalDispalyRecord total = new TotalDispalyRecord
            {
                AccountName = "总计",
                RecordDate = DateTime.Now,
                Asset = 0,
                Debt = 0,
            };
            if (db.Records == null || db.Records.Count() == 0) return record;
            var s = db.Records.GroupBy(x => x.AccountId);
            var list = new List<Record>();
            s.ToList().ForEach((x) =>
            {
                var r = x.OrderByDescending(y => y.RecordDate).First();
                list.Add(r);

            });
            list.OrderBy(x => x.Account.Priority).ToList().ForEach(r =>
            {
                total.Asset += r.Asset;
                total.Debt += r.Debt;
                record.Add(new TotalDispalyRecord
                {
                    RecordDate = r.RecordDate,
                    Asset = r.Asset,
                    Debt = r.Debt,
                    Info = r.Info,
                    AccountName = r.Account.Name
                });
            }
            );
            record.Add(total);
            return record;
        }
        private List<TotalDispalyRecord> GetTotalDisplayRecords(DateTime dt)
        {
            List<TotalDispalyRecord> rec = new List<TotalDispalyRecord>();
            TotalDispalyRecord total = new TotalDispalyRecord
            {
                AccountName = "总计",
                RecordDate = dt,
                Asset = 0,
                Debt = 0,
            };
            if (db.Records == null || db.Records.Count() == 0) return rec;
            List<TotalDispalyRecord> record = new List<TotalDispalyRecord>();
            var s = db.Records.Where(x => dt.CompareTo(x.RecordDate) >= 0).GroupBy(x => x.AccountId);
            s.ToList().ForEach((x) =>
            {
                var r = x.OrderByDescending(y => y.RecordDate).First();
                total.Asset += r.Asset;
                total.Debt += r.Debt;
                record.Add(new TotalDispalyRecord
                {
                    RecordDate = r.RecordDate,
                    Asset = r.Asset,
                    Debt = r.Debt,
                    Info = r.Info,
                    AccountName = r.Account.Name
                });
            });
            record.Add(total);

            record.OrderBy(x => x.AccountName);

            // 排序
            db.Accounts.OrderBy(x => x.Priority).ToList().ForEach(x =>
            {
                rec.Add(record.Find(y => x.Name == y.AccountName));
            });

            return rec;
        }

        List<DispalyRecord> GetDisplayRecord(List<Record> record)
        {
            if (record == null || record.Count() == 0) return new List<DispalyRecord>();
            var arr = record.OrderBy(x => x.RecordDate).Select(x => new DispalyRecord
            {
                RecordId = x.RecordId,
                RecordDate = x.RecordDate,
                Asset = x.Asset,
                Debt = x.Debt,
                Info = x.Info,
            }).ToList();
            DispalyRecord ai = null;
            foreach (var a in arr)
            {
                if (ai == null) ai = a;
                a.AssetOffset = a.Asset - ai.Asset;
                a.DebtOffset = a.Debt - ai.Debt;
                ai = a;
            }
            return arr;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var dt = sender as DatePicker;


            dgRecord.ItemsSource = GetTotalDisplayRecords(dt.SelectedDate.Value);
        }

        private void CompareTotal_Click(object sender, RoutedEventArgs e)
        {
            //if (dpCountStart.SelectedDate is null || dpCountEnd.SelectedDate is null) return;

            var dtEnd = (dpCountEnd.SelectedDate is null) ? DateTime.Now : dpCountEnd.SelectedDate.Value.Date.AddDays(1).AddSeconds(-1);
            var dtSatrt =(dpCountStart.SelectedDate is null)? dtEnd.Date: dpCountStart.SelectedDate.Value;
            var tr1 = GetTotalDisplayRecords(dtSatrt);
            var tr2 = GetTotalDisplayRecords(dtEnd);
            var record = new List<TotalCompareDispalyRecord>();
            int i = 0;
            tr2.ForEach((y) =>
            {
                var x = tr1[i];

                var r = new TotalCompareDispalyRecord
                {
                    RecordDate = y.RecordDate,
                    Asset = y.Asset,
                    Debt = y.Debt,
                    Info = y.Info,
                    AccountName = y.AccountName,
                };

                if (x.AccountName.Equals(y.AccountName))
                {
                    r.LastRecordDate = x.RecordDate;
                    r.LastAsset = x.Asset;
                    r.LastDebt = x.Debt;

                    r.AssetOffset = y.Asset - x.Asset;
                    r.DebtOffset = y.Debt - x.Debt;

                    i++;
                }
                record.Add(r);
            });

            var recTotal = new TotalCompareDispalyRecord
            {
                AccountName = "总计",
                LastRecordDate = record.Max(x => x.LastRecordDate),
                RecordDate = record.Max(x => x.RecordDate),

                Asset = record.Sum(x => x.Asset),
                Debt = record.Sum(x => x.Debt),
                LastAsset = record.Sum(x => x.LastAsset),
                LastDebt = record.Sum(x => x.LastDebt),
                AssetOffset = record.Sum(x => x.AssetOffset),
                DebtOffset = record.Sum(x => x.DebtOffset),
            };
            record.Add(recTotal);

            dgRecord.ItemsSource = record;
        }


    }
}
