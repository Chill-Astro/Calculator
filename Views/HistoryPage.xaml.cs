using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.Generic;

namespace Calculator.Views
{
    public sealed partial class HistoryPage : Page
    {
        private List<string> _history;

        public HistoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is List<string> history)
            {
                _history = history;
                HistoryListView.ItemsSource = _history;
            }
        }
        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the history list
            HistoryListView.ItemsSource = null;
        }
        public void SetHistory(List<string> history)
        {
            _history = history;
            if (HistoryListView != null)
            {
                HistoryListView.ItemsSource = _history;
            }
        }

    }
}