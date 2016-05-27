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

        public delegate void SetTextDeleg(string text);

        BasculaR232 basculaR232;
        List<string> valors;

        private bool zeroPesat { get; set; }
        private int caixesTotals { get; set; }
        private int quantitatProducteActual { get; set; }

        private comanda comandaActual;
        private liniacomanda liniaComandaActual;
        private producte producteSeleccionat;
        private albara albaraActual;
        private liniaalbara liniaAlbaraActual;

        private int codiCaixa { get; set; }
        private int quantitatProducteCaixa { get; set; }

        private int caixaQuantitatFalta;
        private int caixaQuantitatFet;
        private decimal caixa_pesTotal;

        private int total_QuantitatPendent;
        private int total_QuantitatFet;
        private decimal total_PesTotal;
        private int total_CaixesFetes;
        private int total_CaixesPendents;

        /// <summary>
        /// Quantitat de productes que falten pesar per caixa.
        /// </summary>
        public int Caixa_QuantitatFalta
        {
            get { return caixaQuantitatFalta; }
            set
            {
                caixaQuantitatFalta = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Quantitat de produtes que s'han pesat per caixa.
        /// </summary>
        private int Caixa_QuantitatFet
        {
            get { return caixaQuantitatFet; }
            set
            {
                caixaQuantitatFet = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Pes de la caixa en total.
        /// </summary>
        private decimal Caixa_pesTotal
        {
            get { return caixa_pesTotal; }
            set
            {
                caixa_pesTotal = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Total de caixes pendents que falta per fer.
        /// </summary>
        public int Total_CaixesPendents
        {
            get { return total_CaixesPendents; }
            set
            {
                total_CaixesPendents = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Total de caixes fetes.
        /// </summary>
        public int Total_CaixesFetes
        {
            get { return total_CaixesFetes; }
            set
            {
                total_CaixesFetes = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Pes total de les caixes.
        /// </summary>
        public decimal Total_PesTotal
        {
            get { return total_PesTotal; }
            set
            {
                total_PesTotal = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Quantitat d'unitats fetes en total.
        /// </summary>
        public int Total_QuantitatFet
        {
            get { return total_QuantitatFet; }
            set
            {
                total_QuantitatFet = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Quantitat d'unitats pendents en total.
        /// </summary>
        public int Total_QuantitatPendent
        {
            get { return total_QuantitatPendent; }
            set
            {
                total_QuantitatPendent = value;
                OnValorsGestorAlbaraChanged(new EventArgs());
            }
        }
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

        internal IEnumerable donemLiniesComanda(int codiComanda)
        {
            return tLiniaComanda.getAll(codiComanda);
        }

        internal void comandaSelec(int comandaID)
        {
            comandaActual = tComanda.getByIDComanda(comandaID);

            albaraActual = new albara();
            albaraActual.comanda = comandaActual;
            albaraActual.dataAlbara = DateTime.Now;
            albaraActual.dataEntrega = comandaActual.dataEntrega;

            tAlbara.add(albaraActual);
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
            quantitatProducteActual = liniaComandaActual.quantitat.GetValueOrDefault();

            codiCaixa = tLiniaAlbara.getCodiCaixaBuida(albaraActual, producteSeleccionat).GetValueOrDefault(); //Retorna 0 si no nhi ha cap
            if (codiCaixa == 0)
                codiCaixa = tLiniaAlbara.getMaxIdCaixa().GetValueOrDefault() + 1;

            Caixa_QuantitatFet = tLiniaAlbara.getQuantitatProductesByCodiCaixa(codiCaixa).GetValueOrDefault();

            gestorAlbaraWindow.lbl_nomProducte.Content = producteSeleccionat.nom.ToUpper();

            #region Calcul valors de les pantalles.
            ////Totals

            //Total_QuantitatFet = 0;
            //Total_QuantitatPendent = quantitatProducteActual;
            //Total_PesTotal = 0;
            //Total_CaixesFetes = 0;

            if (quantitatProducteActual % producteSeleccionat.unitatCaixa == 0)
            {
                Total_CaixesPendents = quantitatProducteActual / producteSeleccionat.unitatCaixa.GetValueOrDefault();
                caixesTotals = Total_CaixesPendents;
            }
            else
            {
                Total_CaixesPendents = quantitatProducteActual / producteSeleccionat.unitatCaixa.GetValueOrDefault() + 1;
                caixesTotals = Total_CaixesPendents;
            }

            ////Caixa

            //Caixa_QuantitatFet = 0;

            //if (producteSeleccionat.unitatCaixa <= quantitatProducteActual)
            //{
            //    Caixa_QuantitatFalta = producteSeleccionat.unitatCaixa.GetValueOrDefault();
            //}
            //else
            //{
            //    Caixa_QuantitatFalta = quantitatProducteActual;
            //}

            //Caixa_pesTotal = 0;
            #endregion

            obreBascula();

            return 0;
        }

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
                decimal pesada = (Convert.ToDecimal(primerValor) / 1000);

                novaPesada(pesada);
            }



            Console.WriteLine("======== {0} ========", primerValor);
            Console.WriteLine("======== {0} ========", iguals);
        }

        private void novaPesada(decimal pes)
        {
            if (pes == 0)
            {
                zeroPesat = true;
                gestorAlbaraWindow.label_pesBascula.Content = string.Format("- {0} -", pes.ToString());

                //Si la quantitat de caixes que falten per fer de la línia == 0
                if (Caixa_QuantitatFalta == 0)
                {
                    System.Windows.MessageBox.Show("Change Box", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

                    //Reset a Quantitat de Caixes Fetes i Pes total 
                    Caixa_QuantitatFet = 0;
                    Caixa_pesTotal = 0;
                    //TODO --> Revisat !!!
                    //Torna a calcular la quantitat que falta per caixa     //Comprovar que funcioni be perque no se si conta les fetes o no;
                    if (producteSeleccionat.unitatCaixa <= liniaComandaActual.quantitat.GetValueOrDefault())
                        Caixa_QuantitatFalta = producteSeleccionat.unitatCaixa.GetValueOrDefault();
                    else
                        Caixa_QuantitatFalta = liniaComandaActual.quantitat.GetValueOrDefault();


                    Total_CaixesFetes++;

                    //Si el total de caixes pendents és 0 s'ha acabat la liniaComanda
                    if (Total_CaixesPendents == 0)
                    {
                        System.Windows.MessageBox.Show("The line item has been successfully completed.", "Stop", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        //Proces de tencar el serial port correctament i seleccionar una altre linia de comanda.
                        //basculaR232.close();
                        gestorAlbaraWindow.dataGrid_LiniesComandes.IsEnabled = true;
                    }
                }

                return;
            }

            if (zeroPesat)
            {
                liniaAlbaraActual = new liniaalbara();

                if (Caixa_QuantitatFet == producteSeleccionat.unitatCaixa)
                {
                    codiCaixa = tLiniaAlbara.getMaxIdCaixa().GetValueOrDefault() + 1;
                    liniaAlbaraActual.caixa = codiCaixa;
                }
                else
                {
                    liniaAlbaraActual.caixa = codiCaixa;
                }

                liniaAlbaraActual.albara = albaraActual;
                liniaAlbaraActual.producteNom = producteSeleccionat.nom;
                liniaAlbaraActual.idProducte = producteSeleccionat.id;
                quantitatProducteActual--;

                try
                {
                    tLiniaAlbara.add(liniaAlbaraActual);
                }
                catch(Exception ex) { }

                Caixa_QuantitatFet++;
                Total_QuantitatFet++;

                Caixa_pesTotal += pes;
                Total_PesTotal += pes;

                gestorAlbaraWindow.label_pesBascula.Content = string.Format("{0}", pes.ToString());

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
