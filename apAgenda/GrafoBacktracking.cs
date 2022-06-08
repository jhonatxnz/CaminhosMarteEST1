using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhos
{
    public class GrafoBacktracking
    {
        const int tamanhoDistancia = 4;
        char tipoGrafo;
        int qtasCidades;
        int[,] matriz;

        public GrafoBacktracking(string nomeArquivo)
        {
            var arquivo = new StreamReader(nomeArquivo);
            tipoGrafo = arquivo.ReadLine()[0]; // acessa primeiro caracter com tipo do grafo
            qtasCidades = int.Parse(arquivo.ReadLine());
            matriz = new int[qtasCidades, qtasCidades];
            for (int linha = 0; linha < qtasCidades; linha++)
            {
                string arestas = arquivo.ReadLine();
                for (int coluna = 0; coluna < qtasCidades; coluna++)
                    matriz[linha, coluna] =

                    int.Parse(arestas.Substring(coluna * tamanhoDistancia, tamanhoDistancia));

            }

        }
        public char TipoGrafo { get => tipoGrafo; set => tipoGrafo = value; }
        public int QtasCidades { get => qtasCidades; set => qtasCidades = value; }
        public int[,] Matriz { get => matriz; set => matriz = value; }

        public void Exibir(DataGridView dgv)
        {
            dgv.RowCount = dgv.ColumnCount = qtasCidades;
            for (int coluna = 0; coluna < qtasCidades; coluna++)
            {
                dgv.Columns[coluna].HeaderText = coluna.ToString();
                dgv.Rows[coluna].HeaderCell.Value = coluna.ToString();
                dgv.Columns[coluna].Width = 30;
            } 
            for (int linha = 0; linha < qtasCidades; linha++)
                for (int coluna = 0; coluna < qtasCidades; coluna++)
                    if (matriz[linha, coluna] != 0)
                        dgv[coluna, linha].Value = matriz[linha, coluna];
        }
    }
}
