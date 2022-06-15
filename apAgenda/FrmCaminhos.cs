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
        ListaDupla<Cidade> cidades;     //instanciamos a classe para utilizar seus metódos
        ListaDupla<Ligacao> caminhos;   //instanciamos a classe para utilizar seus metódo
        GrafoBackTracking oGrafo;   //instanciamos a classe para utilizar seus metódo
        public FrmCaminhos()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FrmAgenda_Load(object sender, EventArgs e)
        {
            int indice = 0;
            Caminhos.ImageList = imlBotoes;
            foreach (ToolStripItem item in Caminhos.Items)
                if (item is ToolStripButton)
                    (item as ToolStripButton).ImageIndex = indice++;

            cidades = new ListaDupla<Cidade>();
            if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                cidades.LerDados(dlgAbrir.FileName);    //le os dados do arquivo que o usuario escolheu
                cidades.ExibirDados(cbxOrigem);         //exibe  os dados nos cbxDestino
                cidades.ExibirDados(cbxDestino);        //exibe  os dados nos cbxDestino

            }
            caminhos = new ListaDupla<Ligacao>();
            if (dlgAbrirCaminhos.ShowDialog() == DialogResult.OK)
            {
                caminhos.LerDados(dlgAbrirCaminhos.FileName);    //le os dados do arquivo que o usuario escolheu
                oGrafo = new GrafoBackTracking(dlgAbrir.FileName);

            }

        }

        private void pbMarte_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", 10); //define a font como Arial,tamanho 10 (utilizado para passar de parâmetro no e.Graphics.DrawString)

            cidades.PosicionarNoPrimeiro(); //posiciona o atua no primeiro
            while (cidades.DadoAtual() != null)//percorre a lista
            {
                Cidade cidade = cidades.DadoAtual();
                int x = (int)(cidade.X * pbMarte.Width); //multiplica o valor de X pela largura do pictureBox
                int y = (int)(cidade.Y * pbMarte.Height);//multiplica o valor de Y pela altura do pictureBox
                e.Graphics.FillEllipse(Brushes.Black, new Rectangle(x, y, 10, 10)); //pinta X e Y no pictureBox
                e.Graphics.DrawString(cidade.Nome, font, Brushes.Black, x, y + 10);//escreve o nome da cidade no pictureBox(y + 10 para o nome da cidade não ficar em cima da bolinha)
                //udX.Text = x.ToString();
                //udY.Text = y.ToString();
                cidades.AvancarPosicao(); //avança posicão
            }
            cidades.PosicionarNoPrimeiro();// posiciona no primeiro novamente por conta de um erro que tivemos zz
        }

        private void btnCaminhos_Click(object sender, EventArgs e)
        {
            int origem = int.Parse(cbxOrigem.Text);
            int destino = int.Parse(cbxDestino.Text);
            var pilhaCaminho = oGrafo.BuscarCaminho(origem, destino,  dgvMelhorCaminho,
            dgvCaminhosEncontrados);
            if (pilhaCaminho.EstaVazia)
                MessageBox.Show("Não achou caminho");
            else
            {
                MessageBox.Show("Achou caminho");
                oGrafo.Exibir(dgvMelhorCaminho);
                
                while (!pilhaCaminho.EstaVazia)
                {
                    var mov = pilhaCaminho.Desempilhar();
                }
            }
        }
    }
}