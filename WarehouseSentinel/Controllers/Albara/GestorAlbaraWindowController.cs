using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WarehouseSentinel.Models;
using WarehouseSentinel.Viwers.Albara;

namespace WarehouseSentinel.Controllers.Albara
{
    public class GestorAlbaraWindowController
    {
        /// <summary>
        /// Context de la base de dades.
        /// </summary>
        SentinelDBEntities context;
        /// <summary>
        /// Referencia a la vista Gestor Albara.
        /// </summary>
        GestorAlbaraWindow gestorAlbaraWindow;

        /// <summary>
        /// Referencia a la taula Comanda.
        /// </summary>
        TComanda tComanda;

        /// <summary>
        /// Constructor. Controller de la vista Gestor Albara
        /// </summary>
        /// <param name="gestorAlbaraWindow">vista gestor albara.</param>
        public GestorAlbaraWindowController(GestorAlbaraWindow gestorAlbaraWindow)
        {
            context = new SentinelDBEntities();
            this.gestorAlbaraWindow = gestorAlbaraWindow;
            this.gestorAlbaraWindow.Show();

            tComanda = new TComanda(context);
        }

        List<string> valors;

        public void tactamentPesBascula(string data)
        {
            string[] pesos;
            valors = new List<string>();

            Console.WriteLine("\r");
            Console.WriteLine(valors.Count);
            Console.WriteLine("\r");

            //textBox_detall.Text = Data + "\r \n";
            pesos = data.Split(new[] { '\r', '\u0002', '\u0003' });

            foreach (string s in pesos)
            {
                if (s.Length > 6)
                {
                    string pes = s.Remove(0, 1);
                    pes = pes.Remove(pes.Length - 1);
                    pes = pes.Replace(" ", "");
                    valors.Add(pes);
                    Console.WriteLine(pes);
                }
            }

            
            Console.WriteLine("\r");
            Console.WriteLine(valors.Count);
            Console.WriteLine("\r");
        }

        public void comencaPesar()
        {
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 100;
            aTimer.Enabled = true;
        }

        string primerValor = "5";

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (!valors[0].Equals("0"))
                primerValor = valors[0];

            bool iguals = valors.TrueForAll(EndsWithSaurus);

            if (iguals)
                gestorAlbaraWindow.label_pesBascula.Content = primerValor;

            Console.WriteLine("======== {0} ========", primerValor);
            Console.WriteLine("======== {0} ========", iguals);
        }

        private bool EndsWithSaurus(string s)
        {
            return s.ToLower().EndsWith(primerValor);
        }
    }
}
