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
using System.Windows.Shapes;

namespace MyAccounting
{
    /// <summary>
    /// AccountDetail.xaml 的交互逻辑
    /// </summary>
    public partial class AccountDetail : Window
    {
        public AccountDetail()
        {
            InitializeComponent();
        }

        private AssetShowContext db;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            db = new AssetShowContext();
            cbAccount.ItemsSource = db.Accounts.ToList();
            if (cbAccount.Items.Count > 0)
                cbAccount.SelectedIndex = 0;
        }


        private void cbAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbAccount.SelectedIndex < 0) return;

            dgRecord.ItemsSource = GetDisplayRecord((cbAccount.SelectedItem as Account).Records);

        }
        List<DispalyRecord> GetDisplayRecord(List<Record> record)
        {
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
        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (cbAccount.SelectedIndex < 0) return;
            var record = new Record
            {
                RecordDate = dpDate.SelectedDate == null ? DateTime.Now : dpDate.SelectedDate.Value,
                AccountId = (cbAccount.SelectedItem as Account).AccountId,
                Asset = Convert.ToDecimal(txtAsset.Text.Trim()),
                Debt = Convert.ToDecimal(txtDebt.Text.Trim()),
                Info = txtInfo.Text.Trim(),
            };

            db.Records.Add(record);
            db.SaveChanges();

            dgRecord.ItemsSource = GetDisplayRecord((cbAccount.SelectedItem as Account).Records);

        }
    }
}
