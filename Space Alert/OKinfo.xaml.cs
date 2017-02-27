using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Space_Alert
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OKinfo : Page
    {
        public OKinfo()
        {
            this.InitializeComponent();
        }

        private void btnReturnToBase_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string value = localSettings.Values["ID"].ToString();
            if (Parameters.pulse > 130)
                Parameters.pulse = 130;
            ConnectToServer connect = new ConnectToServer("45454");
            connect.SendMsg("11;"+value);
        }

        private void btnContinueWorking_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string value = localSettings.Values["ID"].ToString();
            if (Parameters.pulse > 115)
                Parameters.pulse = 115;
            ConnectToServer connect = new ConnectToServer("45454");
            connect.SendMsg("12;" + value);
        }
    }
}
