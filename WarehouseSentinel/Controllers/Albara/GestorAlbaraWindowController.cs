using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
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
        /// Referencia a la taula Linia Comanda.
        /// </summary>
        TLiniaComanda tLiniaComanda;

        public delegate void SetTextDeleg(string text);

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
            tLiniaComanda = new TLiniaComanda(context);
        }

        internal void closeGestorAlbaraWindow()
        {
            gestorAlbaraWindow.Close();
        }

        BasculaR232 basculaR232;

        internal void obreBascula()
        {
            if (basculaR232 == null)
            {
                basculaR232 = new BasculaR232("COM6", 9600, Parity.None, 8, StopBits.One, this);
                basculaR232.connect();
            }
            else
            {
                basculaR232.close();
                basculaR232 = null;
                obreBascula();
            }
        }

        internal void tancaBascula()
        {
            basculaR232.close();
        }

        internal IEnumerable donemCapcaleresComandes()
        {
            List<CapComanda> capcaleresComanda = new List<CapComanda>();

            foreach (comanda c in tComanda.getAll())
            {
                if (c.estat.Equals("pendent"))
                {
                    capcaleresComanda.Add(new CapComanda(c));
                }
            }

            return capcaleresComanda;
        }

        internal IEnumerable donemLiniesComanda(int codiComanda)
        {
            return tLiniaComanda.getAll(codiComanda);
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

            primerValor = valors[0];

            bool iguals = valors.TrueForAll(EndsWithSaurus);

            if (iguals)
            {
                decimal pesada = (Convert.ToDecimal(primerValor) / 1000);

                novaPesada(pesada);
            }



            Console.WriteLine("======== {0} ========", primerValor);
            Console.WriteLine("======== {0} ========", iguals);
        }

        private bool zeroPesat { get; set; }
        private decimal ultimPes { get; set; }

        private int caixesTotals { get; set; }

        private void novaPesada(decimal pes)
        {
            if (pes == 0)
            {
                zeroPesat = true;
                gestorAlbaraWindow.label_pesBascula.Content = string.Format("- {0} -", pes.ToString());
                return;
            }

            if (zeroPesat)
            {
                if (Caixa_QuantitatFalta == 0)
                {
                    
                }
                else
                {
                    Caixa_QuantitatFalta--;
                    Caixa_QuantitatFet++;
                    Caixa_pesTotal += pes;
                }

                


                Total_PesTotal += pes;

                gestorAlbaraWindow.label_pesBascula.Content = string.Format("{0}", pes.ToString());

                ultimPes = pes;
                zeroPesat = false;
            }


        }

        private liniacomanda liniaComandaActual;

        /// <summary>
        /// Actua sobre la Linia de comanda Seleccionada i comença el proces de pesatge; També actualitza
        /// els labels de la pantalla.
        /// </summary>
        /// <param name="liniaComandaSeleccionada">Linia de comanda Seleccionada per executar.</param>
        internal int processaLiniaComanda(liniacomanda liniaComandaSeleccionada)
        {
            if (liniaComandaSeleccionada == null) return 1;

            gestorAlbaraWindow.dataGrid_LiniesComandes.IsEnabled = false;

            liniaComandaActual = liniaComandaSeleccionada;

            calculaValors(liniaComandaSeleccionada.quantitat, liniaComandaSeleccionada.Producte_id);

            obreBascula();

            return 0;
        }

        private int caixaQuantitatFalta;
        private int caixaQuantitatFet;
        private decimal caixa_pesTotal;
        private int caixa_unitatsFalta;

        public int Caixa_QuantitatFalta
        {
            get { return caixaQuantitatFalta; }
            set
            {
                caixaQuantitatFalta = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        private int Caixa_QuantitatFet
        {
            get { return caixaQuantitatFet; }
            set
            {
                caixaQuantitatFet = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        private decimal Caixa_pesTotal
        {
            get { return caixa_pesTotal; }
            set
            {
                caixa_pesTotal = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        private int Caixa_unitatsFalta
        {
            get { return caixa_unitatsFalta; }
            set
            {
                caixa_unitatsFalta = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        private int total_QuantitatPendent;
        private int total_QuantitatFet;
        private decimal total_PesTotal;
        private int total_CaixesFetes;
        private int total_CaixesPendents;

        public int Total_CaixesPendents
        {
            get { return total_CaixesPendents; }
            set
            {
                total_CaixesPendents = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        public int Total_CaixesFetes
        {
            get { return total_CaixesFetes; }
            set
            {
                total_CaixesFetes = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        public decimal Total_PesTotal
        {
            get { return total_PesTotal; }
            set
            {
                total_PesTotal = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        public int Total_QuantitatFet
        {
            get { return total_QuantitatFet; }
            set
            {
                total_QuantitatFet = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        public int Total_QuantitatPendent
        {
            get { return total_QuantitatPendent; }
            set
            {
                total_QuantitatPendent = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        private void calculaValors(int? quantitat, int producte_id)
        {
            producte p = new TProducte(context).getByID(producte_id);

            gestorAlbaraWindow.lbl_nomProducte.Content = p.nom.ToUpper();

            //Totals

            Total_QuantitatFet = 0;
            Total_QuantitatPendent = quantitat.GetValueOrDefault();
            Total_PesTotal = 0;
            Total_CaixesFetes = 0;

            if (quantitat % p.unitatCaixa == 0)
            {
                Total_CaixesPendents = quantitat.GetValueOrDefault() / p.unitatCaixa.GetValueOrDefault();
                caixesTotals = Total_CaixesPendents;
            }
            else
            {
                Total_CaixesPendents = quantitat.GetValueOrDefault() / p.unitatCaixa.GetValueOrDefault() + 1;
                caixesTotals = Total_CaixesPendents;
            }

            //Caixa

            if (p.unitatCaixa < quantitat.GetValueOrDefault())
            {
                Caixa_QuantitatFalta = p.unitatCaixa.GetValueOrDefault();
            }
            else
            {
                Caixa_QuantitatFalta = quantitat.GetValueOrDefault();
            }

            Caixa_QuantitatFet = 0;

            Caixa_pesTotal = 0;
        }

        string primerValor = "5";

        private bool EndsWithSaurus(string s)
        {
            return s.ToLower().EndsWith(primerValor);
        }

        /// <summary>
        /// Es produeix quan els valors de la linia de comanda canvien.
        /// </summary>
        public event EventHandler ValorsGestorAlbaraChanged;

        /// <summary>
        /// Genera el event ValorsGestorAlbaraChanged
        /// </summary>
        protected virtual void OnValorsGestorAlbaraChanged(EventArgs e)
        {
            ValorsGestorAlbaraChanged?.Invoke(this, e);

            gestorAlbaraWindow.lbl_t_unitatsPendents.Content = Total_QuantitatPendent.ToString();
            gestorAlbaraWindow.lbl_t_unitatsFetes.Content = Total_QuantitatFet.ToString();
            gestorAlbaraWindow.lbl_t_pesTotal.Content = Total_PesTotal.ToString();
            gestorAlbaraWindow.lbl_t_caixesFetes.Content = Total_CaixesFetes.ToString();
            gestorAlbaraWindow.lbl_t_caixesPendents.Content = Total_CaixesPendents.ToString();

            gestorAlbaraWindow.lbl_caixa_unitatsPendents.Content = Caixa_QuantitatFalta.ToString();
            gestorAlbaraWindow.lbl_caixa_pesTotal.Content = Caixa_pesTotal.ToString();
            gestorAlbaraWindow.lbl_caixa_unitatsFetes.Content = Caixa_QuantitatFet.ToString();
        }
    }
}
