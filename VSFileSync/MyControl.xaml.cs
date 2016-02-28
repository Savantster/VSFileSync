using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Raydude.VSFileSync
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentUICulture, "We are inside {0}.button1_Click()", this.ToString()),
                            "FileSync Control Window");

        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            // When we're being loaded, we need to go get all the Solutions/Projects from our table, and show the flag
            // for if this is being tracked. We'll need to come up with a way to let them add current solutions/projects
            // to our tracking system.. or, perhaps, we'll scan based on Solution location.. if we identify which items
            // are "found" but not in our system, we can let them click the row and "track" or "ignore"?


        }
    }
}