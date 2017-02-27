using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;


namespace Space_Alert
{
    class BTScan
    {
        
        
        private BluetoothLEAdvertisementPublisher publisher;
        public void Emit()
        {
            publisher = new BluetoothLEAdvertisementPublisher();
            var manufacturerData = new BluetoothLEManufacturerData();
            manufacturerData.CompanyId = 0xFFFE;
            var writer = new DataWriter();
            UInt16 uuidData = 0x1234;
            writer.WriteUInt16(uuidData);
            manufacturerData.Data = writer.DetachBuffer();
            publisher.Advertisement.ManufacturerData.Add(manufacturerData);
            publisher.Start();

        }
    }
}
