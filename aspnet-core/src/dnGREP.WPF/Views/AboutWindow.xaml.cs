﻿using System.Windows.Navigation;

namespace dnGREP.WPF
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : ThemedWindow
    {
        public AboutWindow()
        {
            InitializeComponent();

            DataContext = new AboutViewModel();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
