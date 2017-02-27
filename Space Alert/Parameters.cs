using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Alert
{
    static class Parameters
    {
        public static int pulse { get; set; }
        public static int systolicBloodPressure { get; set; }
        public static int diastolicBloodPressure { get; set; }
        public static double temperature { get; set; }
        public static int spaceSuitCondition { get; set; }
        public static double oxygenStatus { get; set; }

        public static void SetStartParameters()
        {
            Random rnd = new Random();
            temperature = Convert.ToDouble(rnd.Next(360, 370)) / 10;
            //temperature += rnd.NextDouble();
            spaceSuitCondition = 1;
            pulse = rnd.Next(65, 85);
            systolicBloodPressure = rnd.Next(110, 130);
            diastolicBloodPressure = rnd.Next(70, 90);
            oxygenStatus = 100.0;
        }
        public static async Task updateParameters()
        {
            Random rnd = new Random();

            while (true)
            {
                await Task.Delay(3000);

                if (pulse <= 60)
                    pulse += rnd.Next(0, 4);
                else if (pulse >= 180)
                    pulse -= rnd.Next(0, 5);
                else
                    pulse += rnd.Next(-3, 5);

                if (systolicBloodPressure <= 100)
                    systolicBloodPressure += rnd.Next(0, 4);
                else if (systolicBloodPressure >= 180)
                    systolicBloodPressure -= rnd.Next(0, 5);
                else
                    systolicBloodPressure += rnd.Next(-4, 5);

                if (diastolicBloodPressure <= 50)
                    diastolicBloodPressure += rnd.Next(0, 3);
                else if (diastolicBloodPressure >= 110)
                    diastolicBloodPressure -= rnd.Next(0, 4);
                else
                    diastolicBloodPressure += rnd.Next(-3, 4);

                if (temperature <= 35.0)
                    temperature += Math.Round(rnd.NextDouble()*2/10,1);
                else if (temperature >= 38.5)
                    temperature -= Math.Round(rnd.NextDouble()*2/10,1);
                else
                    temperature += Math.Round((rnd.NextDouble()-0.5)*1.8/10,1);
            }

        }


    }
}
