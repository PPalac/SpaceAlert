using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Space_Alert
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Alerts : Page
    {
        public Alerts()
        {
            this.InitializeComponent();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string value = localSettings.Values["ID"].ToString();
            Random rnd = new Random();
            ConnectToServer connect = new ConnectToServer("45454");
            string temperature = Convert.ToString(Parameters.temperature);
            if (Parameters.spaceSuitCondition != 0)
                Parameters.spaceSuitCondition = rnd.Next(8, 19) / 10;
            Parameters.pulse = 140;
            string spaceSuitCondition = Convert.ToString(Parameters.spaceSuitCondition);
            string pulse = Convert.ToString(Parameters.pulse);
            string systolicBloodPressure = Convert.ToString(Parameters.systolicBloodPressure);
            string diastolicBloodPressure = Convert.ToString(Parameters.diastolicBloodPressure);
            string oxygenStatus = Parameters.oxygenStatus.ToString();
            string allStrings;
            allStrings = "0;" + value + ";" + pulse + ";" + systolicBloodPressure + ";" + diastolicBloodPressure + ";" + temperature + ";" + spaceSuitCondition + ";" + oxygenStatus;

            connect.SendMsg(allStrings);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OKinfo));
        }
    }
}
