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
using System.Windows.Shapes;

namespace BubllyChat.MVVM.View
{
    /// <summary>
    /// Interaction logic for GroupCallView.xaml
    /// </summary>
    public partial class GroupCallView : Window
    {
        public GroupCallView(string roomID)
        {
            InitializeComponent();
            DataContext = new GroupCallVM(roomID);
        }
    }
}
