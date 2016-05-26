using System;
using System.Windows;
using System.IO.Ports;
using System.Threading;
using System.Collections;
using System.Timers;
using System.Collections.Generic;
using static WarehouseSentinel.Controllers.Albara.GestorAlbaraWindowController;

namespace WarehouseSentinel.Controllers.Albara
{
    public class BasculaR232
    {
        public SerialPort serialPort;
        GestorAlbaraWindowController controller;

        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Handshake Handshake { get; set; }
        public Parity Parity { get; set; }
        public string PortName { get; set; }
        public StopBits StopBits { get; set; }


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
            if (serialPort != null && serialPort.IsOpen) close();

            serialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
            Handshake = Handshake.None;
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
            serialPort.RtsEnable = true;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            serialPort.Open();
        }

        internal void close()
        {
            try
            {
                serialPort.Close();
                serialPort = null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
