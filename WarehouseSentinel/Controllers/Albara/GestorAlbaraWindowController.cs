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

        TAlbara tAlbara;

        TLiniaAlbara tLiniaAlbara;

        private comanda comandaActual;
        private liniacomanda liniaComandaActual;
        private producte producteSeleccionat;
        private albara albaraActual;
        private liniaalbara liniaAlbaraActual;

        public delegate void SetTextDeleg(string text);

        BasculaR232 basculaR232;
        List<string> valors;

        private bool zeroPesat { get; set; }

        private int QuantitatProducteActual
        {
            get { return quantitatFetTotal; }
            set
            {
                quantitatFetTotal = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }



        private int codiCaixa { get; set; }

        private int quantitatFaltaCaixa;
        private int quantitatFetCaixa;
        private float pesTotalCaixa;

        private int quantitatPendentTotal;
        private int quantitatFetTotal;
        private float pesTotalTotal;

        private int caixesFetesTotals;
        private int caixesPendentsTotals;

        /// <summary>
        /// Quantitat de productes que falten pesar per caixa.
        /// </summary>
        public int QuantitatFaltaCaixa
        {
            get { return quantitatFaltaCaixa; }
            set
            {
                quantitatFaltaCaixa = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Quantitat de produtes que s'han pesat per caixa.
        /// </summary>
        private int QuantitatFetCaixa
        {
            get { return quantitatFetCaixa; }
            set
            {
                quantitatFetCaixa = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Pes de la caixa en total.
        /// </summary>
        private float PesTotalCaixa
        {
            get { return pesTotalCaixa; }
            set
            {
                pesTotalCaixa = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Total de caixes pendents que falta per fer.
        /// </summary>
        public int CaixesPendentsTotals
        {
            get { return caixesPendentsTotals; }
            set
            {
                caixesPendentsTotals = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Total de caixes fetes.
        /// </summary>
        public int CaixesFetesTotals
        {
            get { return caixesFetesTotals; }
            set
            {
                caixesFetesTotals = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Pes total de les caixes.
        /// </summary>
        public float PesTotalTotal
        {
            get { return pesTotalTotal; }
            set
            {
                pesTotalTotal = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }

        Timer aTimer;

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
            tAlbara = new TAlbara(context);
            tLiniaAlbara = new TLiniaAlbara(context);
        }


        internal void closeGestorAlbaraWindow()
        {
            gestorAlbaraWindow.Close();
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

        internal static IEnumerable actualitzaCapcaleresComandes()
        {
            TComanda tComanda = new TComanda(new SentinelDBEntities());
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

        internal void comandaSelec(int comandaID)
        {
            comandaActual = tComanda.getByIDComanda(comandaID);

            albaraActual = tAlbara.getAlbara(comandaActual);

            if (albaraActual == null)
            {
                albaraActual = new albara();
                albaraActual.comanda = comandaActual;
                albaraActual.dataAlbara = DateTime.Now;
                albaraActual.dataEntrega = comandaActual.dataEntrega;
                tAlbara.add(albaraActual);
            }
        }

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
            producteSeleccionat = new TProducte(context).getByID(liniaComandaSeleccionada.Producte_id);

            codiCaixa = tLiniaAlbara.getCodiCaixaBuida(albaraActual, producteSeleccionat).GetValueOrDefault(); //Retorna 0 si no nhi ha cap
            if (codiCaixa == 0)
                codiCaixa = tLiniaAlbara.getMaxIdCaixa().GetValueOrDefault() + 1;

            QuantitatFetCaixa = tLiniaAlbara.getQuantitatProductesByCodiCaixa(codiCaixa, producteSeleccionat.id).GetValueOrDefault();
            QuantitatProducteActual = liniaComandaActual.quantitat.GetValueOrDefault() - tLiniaAlbara.getQuantitatProductesByAlbara(producteSeleccionat.id).GetValueOrDefault();

            if (QuantitatProducteActual == 0)
            {
                System.Windows.MessageBox.Show("This line item is finished.", "Information", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                gestorAlbaraWindow.dataGrid_LiniesComandes.IsEnabled = true;

                return 0;
            }


            PesTotalCaixa = tLiniaAlbara.sumaTotsPesos(codiCaixa).GetValueOrDefault();

            gestorAlbaraWindow.lbl_nomProducte.Content = producteSeleccionat.nom.ToUpper();

            if (producteSeleccionat.unitatCaixa < QuantitatProducteActual)
                QuantitatFaltaCaixa = producteSeleccionat.unitatCaixa.GetValueOrDefault() - QuantitatFetCaixa;
            else
                QuantitatFaltaCaixa = QuantitatProducteActual;

            obreBascula();

            return 0;
        }

        internal void obreBascula()
        {
            basculaR232 = new BasculaR232("COM6", 9600, Parity.None, 8, StopBits.One, this);
            basculaR232.connect();
        }

        /// <summary>
        /// Executa el tractament del string que envia per serial la balança.
        /// </summary>
        /// <param name="data"></param>
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
                float pesada = (float)(Convert.ToDouble(primerValor) / 1000);

                novaPesada(pesada);
            }



            Console.WriteLine("======== {0} ========", primerValor);
            Console.WriteLine("======== {0} ========", iguals);
        }

        private void novaPesada(float pes)
        {
            if (pes == 0)
            {
                if (QuantitatProducteActual == 0)
                {
                    gestorAlbaraWindow.dataGrid_LiniesComandes.IsEnabled = true;
                    System.Windows.MessageBox.Show("You closed this command line. Select another line please.", "Information",
                        System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    QuantitatProducteActual = -1;

                    int numProductesComanda = tLiniaComanda.sumTotesLesQuantitats(comandaActual.codi).GetValueOrDefault();
                    int numProductesAlbara = tLiniaAlbara.getQuantitatProductesByComanda(comandaActual.codi);

                    if (numProductesAlbara == numProductesComanda)
                    {
                        gestorAlbaraWindow.dataGrid_capcaleraComandes.IsEnabled = true;
                        gestorAlbaraWindow.dataGrid_LiniesComandes.IsEnabled = false;

                        comandaActual.estat = "acabada";
                        tComanda.modify(comandaActual);

                        gestorAlbaraWindow.dataGrid_capcaleraComandes.ItemsSource = donemCapcaleresComandes();

                        System.Windows.MessageBox.Show("The order has been successfully completed. Please select another command.", "Information",
                            System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }

                    return;
                }

                gestorAlbaraWindow.label_pesBascula.Content = string.Format("- {0} -", pes);
                zeroPesat = true;
                return;
            }

            if (zeroPesat)
            {
                gestorAlbaraWindow.label_pesBascula.Content = string.Format("{0}", pes);

                liniaAlbaraActual = new liniaalbara();

                if (QuantitatFetCaixa == producteSeleccionat.unitatCaixa)
                {
                    QuantitatFetCaixa = 0;
                    PesTotalCaixa = 0;
                    codiCaixa = tLiniaAlbara.getMaxIdCaixa().GetValueOrDefault() + 1;
                    liniaAlbaraActual.caixa = codiCaixa;

                    if (producteSeleccionat.unitatCaixa < QuantitatProducteActual)
                        QuantitatFaltaCaixa = producteSeleccionat.unitatCaixa.GetValueOrDefault();
                    else
                        QuantitatFaltaCaixa = QuantitatProducteActual;
                }
                else
                {
                    liniaAlbaraActual.caixa = codiCaixa;
                }

                liniaAlbaraActual.albara = albaraActual;
                liniaAlbaraActual.producteNom = producteSeleccionat.nom;
                liniaAlbaraActual.idProducte = producteSeleccionat.id;
                liniaAlbaraActual.pes = pes;

                QuantitatFetCaixa++;
                QuantitatFaltaCaixa--;
                QuantitatProducteActual--;
                PesTotalCaixa += pes;
                PesTotalTotal += pes;

                try
                {
                    tLiniaAlbara.add(liniaAlbaraActual);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                zeroPesat = false;
            }
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

            gestorAlbaraWindow.lbl_t_unitatsPendents.Content = QuantitatProducteActual.ToString();
            gestorAlbaraWindow.lbl_t_pesTotal.Content = PesTotalTotal.ToString();
            //gestorAlbaraWindow.lbl_t_caixesFetes.Content = CaixesFetesTotals.ToString();
            //gestorAlbaraWindow.lbl_t_caixesPendents.Content = CaixesPendentsTotal.ToString();

            gestorAlbaraWindow.lbl_caixa_unitatsPendents.Content = QuantitatFaltaCaixa.ToString();
            gestorAlbaraWindow.lbl_caixa_pesTotal.Content = PesTotalCaixa.ToString();
            gestorAlbaraWindow.lbl_caixa_unitatsFetes.Content = QuantitatFetCaixa.ToString();
        }
    }
}
