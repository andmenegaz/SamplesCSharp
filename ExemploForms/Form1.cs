using PdfiumViewer;
using S7.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExemploForms
{
    public partial class Form1 : Form
    {
        S7.Net.Plc _plc;
        int modelo = 0;
        int modeloAnt = 0;
        int passo = 0;
        int passoAnt = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            OpenDocument(@"Modelo 0.pdf");
            pdfViewer1.VerticalScroll.Visible = false;
            pdfViewer1.HorizontalScroll.Visible = false;

            Task.Run(() => MonitorarClp());
        }

        public async Task MonitorarClp()
        {
            try
            {

                S7.Net.Plc _plc = new Plc(CpuType.S71200, "192.168.1.212", 0, 1);
                _plc.Open();
                while (_plc.IsConnected)
                {
                    modelo = Convert.ToInt32(_plc.Read("DB35.DBW0"));

                    if (modelo != modeloAnt)
                    {
                        modeloAnt = modelo;

                        OpenDocument($@"Modelo {modelo}.pdf");
                    }
                    passo = Convert.ToInt32(_plc.Read("DB35.DBW2"));
                    if (passo != passoAnt && passo > 0)
                    {
                        passoAnt = passo;
                        BuscaPagina(passo);
                    }
                    Thread.Sleep(100);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        delegate void BuscaPaginatCallback(int pagina);
        public void BuscaPagina(int pagina)
        {
            if (InvokeRequired)
            {
                BuscaPaginatCallback callback = BuscaPagina;
                Invoke(callback, pagina);
            }
            else
            {
                pdfViewer1.Renderer.Page = pagina - 1;
            }
        }

        delegate void OpenDocumentCallback(string fileName);
        private void OpenDocument(string fileName)
        {
            if (InvokeRequired)
            { 
                OpenDocumentCallback callback = OpenDocument;
                Invoke(callback, fileName);
            }
            else
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        pdfViewer1.Document = PdfDocument.Load(this, fileName);
                    }
                    else
                    {
                        pdfViewer1.Document = PdfDocument.Load(this, "Sem_Modelo.pdf");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
