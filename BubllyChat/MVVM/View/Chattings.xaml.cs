using BubllyChat.MVVM.ViewModel;
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

namespace BubllyChat.MVVM.View
{
    /// <summary>
    /// Interaction logic for Chattings.xaml
    /// </summary>
    public partial class Chattings : UserControl
    {
        private ChattingVM _viewModel;
        public Chattings()
        {
            InitializeComponent();
            Loaded += Chattings_Loaded;
            Unloaded += Chattings_Unloaded;
        }

        private async void Chattings_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChattingVM viewModel)
            {
                _viewModel = viewModel;

                try
                {
                    // Khởi tạo dữ liệu trước
                    await viewModel.InitAsync();

                    // Sau đó kết nối SignalR
                    await viewModel.ConnectAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Chattings_Unloaded(object sender, RoutedEventArgs e)
        {
            // Cleanup khi UserControl bị unload
            _viewModel?.Dispose();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Không cần thiết nữa vì đã xử lý trong Chattings_Loaded
        }
    }
}
