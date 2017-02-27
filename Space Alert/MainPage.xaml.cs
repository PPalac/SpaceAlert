using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Space_Alert
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        ConnectToServer send = new ConnectToServer("45454");
        

        public MainPage()
        {
            this.InitializeComponent();
            checkID();
            //send.SendMessage("1;asd;aasd;sds;ds;sd");
            temperature.Text = Parameters.temperature.ToString();
            bloodPress.Text = Parameters.systolicBloodPressure.ToString() + "/" + Parameters.diastolicBloodPressure.ToString();
            Pulse.Text = Parameters.pulse.ToString();
            if (Parameters.spaceSuitCondition == 1)
                spaceSuit.Text = "tight";
            else
                spaceSuit.Text = "untight";

            oxyLevel.Value = Parameters.oxygenStatus;
            oxyPercent.Text = Parameters.oxygenStatus.ToString();

            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                await SetParamText();
            });

        }
        private async Task SetParamText()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string value = localSettings.Values["ID"].ToString();
            
            while (true)
            {
                await Task.Delay(2000);
                GetOxyLevel();
                if (Parameters.pulse < 3)
                    return;
                temperature.Text = Parameters.temperature.ToString();
                bloodPress.Text = Parameters.systolicBloodPressure.ToString() + "/" + Parameters.diastolicBloodPressure.ToString();
                Pulse.Text = Parameters.pulse.ToString();
                ID.Text = value;
                if (Parameters.spaceSuitCondition == 1)
                    spaceSuit.Text = "tight";
                else
                    spaceSuit.Text = "untight";
                ConnectToServer connect = new ConnectToServer("45455");
                connect.SendMsg("2;" + value + ";" + Pulse.Text + ";" + Parameters.systolicBloodPressure.ToString() + ";" + Parameters.diastolicBloodPressure.ToString() + ";" + Parameters.temperature.ToString() + ";" + Parameters.spaceSuitCondition.ToString() + ";" + Parameters.oxygenStatus.ToString());
            }
        }

        private void GetOxyLevel()
        {
            Random rand = new Random();
            double sum = 0.0;
            if (Parameters.pulse < 100)
                sum = 0.1;
            //oxyLevel.Value -= 0.1;
            else if (Parameters.pulse >= 100 && Parameters.pulse < 140)
                sum = 0.4;
            //oxyLevel.Value -= 0.4;
            else
                sum = 0.7;
            //oxyLevel.Value -= 0.8;
            if (Parameters.spaceSuitCondition == 0)
                sum += 1.2;
            if (oxyLevel.Value >= 0)
                oxyLevel.Value -= sum;
            else
                oxyLevel.Value = 0;
            if (Parameters.oxygenStatus == 0 && Parameters.pulse > 0) 
            {
                Parameters.pulse -= rand.Next(6, 8);
                Parameters.systolicBloodPressure -= rand.Next(5, 8);
                Parameters.diastolicBloodPressure -= rand.Next(4, 7);
                Parameters.temperature -= Math.Round((rand.NextDouble() / 4), 1);
            }
            Parameters.oxygenStatus = Math.Round(oxyLevel.Value, 1); ;
            oxyPercent.Text = Math.Round(oxyLevel.Value,1).ToString() + "%";
            if (oxyLevel.Value < 30.0 && App.temp == 0)
            {
                MessageDialog caution = new MessageDialog("Oxygen level low!!", "Caution!");
                App.temp = 1;
                caution.ShowAsync();
                oxyLevel.Foreground = new SolidColorBrush(Colors.Red);
            }




        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Alerts));
        }

        private void checkID()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            //localSettings.Values["ID"] = null;
            if (localSettings.Values["ID"] == null)
            {
                var template = "<toast>"
                               + "<visual version='1'>"
                               + "<binding template='ToastText04'>"
                               + "<text id='1'>Select ID:</text>"
                               + "</binding>"
                               + "</visual>"
                               + "<actions>"
                               + "<input id='ID' type='selection' defaultInput = '1'>"
                               + "<selection id='1' content='1'/>"
                               + "<selection id='2' content='2'/>"
                               + "<selection id='3' content='3'/>"
                               + "<selection id='4' content='4'/>"
                               + "</input>"
                               + "<action activationType='foreground' content='OK' arguments = 'ID' hint-inputId='ID'/>"
                               + "</actions>"
                               + "</toast>";


                var xml = new XmlDocument();
                xml.LoadXml(template);
                var toast = new ToastNotification(xml);
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
            else
            {
                object value = localSettings.Values["ID"];
                string id = value.ToString();
            }


        }


    }
}
