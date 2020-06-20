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

namespace FinanceWpf.Terminal
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
            
        }

        private void Cbox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            int i = 0;
            foreach(TreeViewItem tn in tvMain.Items)
            {
                if (i < 1)
                    tn.IsExpanded = true;
                else
                    break;
                i++;
            }
        }
    }
}
