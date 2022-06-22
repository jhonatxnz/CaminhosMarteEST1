using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaminhos
{
    public partial class FrmCaminhos : Form
    {
        ListaDupla<Cidade> cidades = new ListaDupla<Cidade>();
        ListaDupla<Ligacao> caminhos = new ListaDupla<Ligacao>();
        GrafoBackTracking oGrafo;
        public FrmCaminhos()
        {
            InitializeComponent();
        }

        private void FrmAgenda_Load(object sender, EventArgs e)
        {
            int indice = 0;
            Caminhos.ImageList = imlBotoes;
            foreach (ToolStripItem item in Caminhos.Items)
                if (item is ToolStripButton)
                    (item as ToolStripButton).ImageIndex = indice++;

            //abrindo arquivo de cidades
            if (dlgAbrir.ShowDialog() == DialogResult.OK) {
                //le os dados do arquivo de cidades
                cidades.LerDados(dlgAbrir.FileName);
                //exibe  os dados nos cbxDestino
                cidades.ExibirDados(cbxOrigem);
                //exibe  os dados nos cbxDestino
                cidades.ExibirDados(cbxDestino);

                cbxOrigem.Text = "Cidade";
                cbxDestino.Text = "Destino";
            }

            //abrindo arquivo de caminhos entre cidades
            if (dlgAbrirDois.ShowDialog() == DialogResult.OK)
            {
                //le os dados do arquivo de caminhos entre cidades
                caminhos.LerDados(dlgAbrirDois.FileName);
                oGrafo = new GrafoBackTracking(cidades,caminhos);

            }

        }

        private void pbMarte_Paint(object sender, PaintEventArgs e)
        {
            //define a font como Arial,tamanho 10 (utilizado para passar de parâmetro no e.Graphics.DrawString)
            Font font = new Font("Arial", 10);

            cidades.PosicionarNoPrimeiro();
            
            //percorre a lista
            while (cidades.DadoAtual() != null)
            {
                Cidade cidade = cidades.DadoAtual();
                //multiplica o valor de X pela largura do pictureBox
                //multiplica o valor de Y pela altura do pictureBox
                int x = (int)(cidade.X * pbMarte.Width); 
                int y = (int)(cidade.Y * pbMarte.Height);
                //pinta X e Y no pictureBox
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(x, y, 10, 10));
                //escreve o nome da cidade no pictureBox(y + 10 para o nome da cidade não ficar em cima da bolinha
                e.Graphics.DrawString(cidade.Nome, font, Brushes.Black, x, y + 10);
                //udX.Text = x.ToString();
                //udY.Text = y.ToString();
                Pen redPen = new Pen(Color.Red, 1);
                e.Graphics.DrawLine(redPen, (int)(cidade.X), (int)(cidade.Y), x,y);

                cidades.AvancarPosicao(); //avança posicão
            }
            // posiciona no primeiro novamente por conta de um erro que tivemos 
            cidades.PosicionarNoPrimeiro();
        }

        private void btnCaminhos_Click(object sender, EventArgs e)
        {
            int origem = int.Parse(cbxOrigem.Text.Substring(0, 3));
            int destino = int.Parse(cbxDestino.Text.Substring(0, 3));


            var pilhaCaminho = oGrafo.BuscarCaminho(origem, destino, dgvMelhorCaminho,
            dgvCaminhosEncontrados);
            if (pilhaCaminho.EstaVazia)
            {
                MessageBox.Show("Não achou caminho");
            }
            else
            {
                MessageBox.Show("Achou caminho");
                oGrafo.Exibir(dgvCaminhosEncontrados);
                while (!pilhaCaminho.EstaVazia)
                {
                    var mov = pilhaCaminho.Desempilhar();
                }
            }
        }
    }
}
