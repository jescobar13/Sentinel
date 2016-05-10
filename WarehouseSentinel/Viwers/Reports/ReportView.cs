using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseSentinel.Controllers;

namespace WarehouseSentinel.Viwers.Reports
{
    public partial class ReportView : Form
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            //// TODO: esta línea de código carga datos en la tabla 'dataSetSentinel.comanda' Puede moverla o quitarla según sea necesario.
            //this.comandaTableAdapter.FillByCIFClient(this.dataSetSentinel.comanda, "S3959337A");
            //com

            comandaBindingSource.DataSource = new MainWindowController().donemComandesByCif("S3959337A");

            this.reportViewer1.RefreshReport();
        }
    }
}
