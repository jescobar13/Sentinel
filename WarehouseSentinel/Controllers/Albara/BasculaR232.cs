using System;
using System.Windows;
using System.IO.Ports;
using System.Threading;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

namespace WarehouseSentinel.Controllers.Albara
{
    public class BasculaR232
    {
        private SerialPort serialPort;
        GestorAlbaraWindowController controller;

        private int BaudRate { get; set; }
        private int DataBits { get; set; }
        private Handshake Handshake { get; set; }
        private Parity Parity { get; set; }
        private string PortName { get; set; }
        private StopBits StopBits { get; set; }

        public delegate void SetTextDeleg(string text);

        public string Data { get; set; }

        public BasculaR232(string nomPort, int baudRate, Parity parity,
            int dataBits, StopBits stopBits, GestorAlbaraWindowController controller)
        {
            PortName = nomPort;
            BaudRate = baudRate;
            Parity = parity;
            DataBits = dataBits;
            StopBits = stopBits;
            this.controller = controller;
        }

        /// <summary>
        /// Connecta el serial port configurat.
        /// </summary>
        public void connect()
        {
            serialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
            Handshake = Handshake.None;
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            serialPort.RtsEnable = true;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            serialPort.Open();
            controller.comencaPesar();
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(500);
                Data = serialPort.ReadExisting();

                Application.Current.Dispatcher.Invoke(new SetTextDeleg(controller.tactamentPesBascula), Data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
