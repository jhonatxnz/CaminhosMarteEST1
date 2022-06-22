using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace apCaminhos
{
    class GrafoBackTracking
    {
        const int tamanhoDistancia = 4;
        char tipoGrafo;
        int qtasCidades;
        int[,] matriz;
        public GrafoBackTracking(string nomeArquivo, int qtasCidades)
        {
            var arquivo = new StreamReader(nomeArquivo);
            tipoGrafo = arquivo.ReadLine()[0]; // acessa primeiro caracter com tipo do grafo
            qtasCidades = qtasCidades;
            //qtasCidades = LeLinhas();
            matriz = new int[qtasCidades, qtasCidades];
            for (int linha = 0; linha < qtasCidades; linha++)
            {
                string arestas = arquivo.ReadLine();
                for (int coluna = 0; coluna < qtasCidades; coluna++)
                    matriz[linha, coluna] =

                    int.Parse(arestas.Substring(coluna * tamanhoDistancia, tamanhoDistancia));


            }
            //int LeLinhas()
            //{
            //    TextReader Leitor = new StreamReader(nomeArquivo, true);//Inicializa o Leitor
            //    int Linhas = 0;
            //    while (Leitor.Peek() != -1) dos crias
            //    {//Enquanto o arquivo não acabar, o Peek não retorna -1 sendo adequando para o loop while...
            //        Linhas++;//Incrementa 1 na contagem
            //        Leitor.ReadLine();//Avança uma linha no arquivo
            //    }
            //    Leitor.Close(); //Fecha o Leitor, dando acesso ao arquivo para outros programas....
            //    return Linhas;
            //}
        }
        public GrafoBackTracking(ListaDupla<Cidade> cidade, ListaDupla<Ligacao> lig)
        {
            matriz = new int[cidade.Tamanho, cidade.Tamanho];

            QtasCidades = cidade.Tamanho;

            lig.PosicionarNoPrimeiro();

            while (lig.DadoAtual() != null)
            {
                matriz[int.Parse(lig.DadoAtual().IdCidadeOrigem),
                       int.Parse(lig.DadoAtual().IdCidadeDestino)] = lig.DadoAtual().Distancia;

                lig.AvancarPosicao();
            }

        }

        public char TipoGrafo { get => tipoGrafo; set => tipoGrafo = value; }
        public int QtasCidades { get => qtasCidades; set => qtasCidades = value; }
        public int[,] Matriz { get => matriz; set => matriz = value; }
        /* public void Exibir(DataGridView dgv)
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
        } */

        public PilhaVetor<Movimento> BuscarCaminho(int origem, int destino,
          DataGridView dgvGrafo,
          DataGridView dgvPilha)

        {
            int cidadeAtual, saidaAtual;
            bool achouCaminho = false,
            naoTemSaida = false;
            bool[] passou = new bool[qtasCidades];
            // inicia os valores de “passou” pois ainda não foi em nenhuma cidade
            for (int indice = 0; indice < qtasCidades; indice++)
                passou[indice] = false;
            cidadeAtual = origem;
            saidaAtual = 0;
            var pilha = new PilhaVetor<Movimento>(qtasCidades);

            while (!achouCaminho && !naoTemSaida)
            {
                naoTemSaida = (cidadeAtual == origem && saidaAtual == qtasCidades && pilha.EstaVazia);

                if (!naoTemSaida)
                {
                    while ((saidaAtual < qtasCidades) && !achouCaminho)
                    {
                        // se não há saída pela cidade testada, verifica a próxima

                        if (matriz[cidadeAtual, saidaAtual] == 0)
                            saidaAtual++;
                        else
                        // se já passou pela cidade testada, vê se a próxima cidade permite saída
                        if (passou[saidaAtual])
                            saidaAtual++;
                        else
                        // se chegou na cidade desejada, empilha o local
                        // e termina o processo de procura de caminho
                        if (saidaAtual == destino)

                        {

                            Movimento movim = new Movimento(cidadeAtual, saidaAtual);
                            pilha.Empilhar(movim);
                            achouCaminho = true;

                        }
                        else
                        {
                            Movimento movim = new Movimento(cidadeAtual, saidaAtual);
                            pilha.Empilhar(movim);
                            passou[cidadeAtual] = true;
                            cidadeAtual = saidaAtual; // muda para a nova cidade
                            saidaAtual = 0; // reinicia busca de saídas da nova
                                            // cidade a partir da primeira cidade

                        }
                    }
                } /// if ! naoTemSaida
                if (!achouCaminho)
                    if (!pilha.EstaVazia)
                    {
                        var movim = pilha.Desempilhar();
                        saidaAtual = movim.Destino;
                        cidadeAtual = movim.Origem;
                        saidaAtual++;
                    }
            }

            var saida = new PilhaVetor<Movimento>();
            if (achouCaminho)
            { // desempilha a configuração atual da pilha
              // para a pilha da lista de parâmetros
                while (!pilha.EstaVazia)
                {
                    Movimento movim = pilha.Desempilhar();
                    saida.Empilhar(movim);
                }
            }
            return saida;
        }
    }
}