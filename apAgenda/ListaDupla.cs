//Jhonatan Willian dos Santos Silva 21686
//Yngrid Vitória Sa Baeta           21694
using System;
using System.IO;
using System.Windows.Forms;

class ListaDupla<Dado> : IDados<Dado>
                where Dado : IComparable<Dado>, IRegistro<Dado>, new()
{
    NoDuplo<Dado> primeiro, ultimo, atual;
    int quantosNos;
    private Situacao situacaoAtual;

    public ListaDupla()
    {
        primeiro = ultimo = atual = null;
        quantosNos = 0;
    }

    public Situacao SituacaoAtual
    {
        get => this.situacaoAtual;          //retorna situacaoAtual
        set => this.situacaoAtual = value;  //seta situacaoAtual
    }
    public int PosicaoAtual
    {
        get
        {
            if (Existe(DadoAtual(), out int ondeEsta)) //se a posicao atual existir
            {
                return ondeEsta; // retorna onde ela esta 
            }
            else 
                return -1;       // retorna posição inexistente
        }
        set => PosicionarEm(value); //muda posição passada como parâmetro
    }
    public bool EstaNoInicio
    {
        get => atual == primeiro; // se atual for igual ao primeiro, significa que o nó está no inicio da lista
    }
    public bool EstaNoFim
    {
        get => atual == ultimo;  // se atual for igual ao último, significa que o nó está no fim da lista
    }
    public bool EstaVazio        // (bool) Verificar se está vazia
    {
        get => primeiro == null; // se o primeiro está null significa que não tem ngm na lista
    }
    public int Tamanho
    {
        get => quantosNos;      //retorna quantidade de nós
    }

    public void LerDados(string nomeArquivo)    // fará a leitura e armazenamento dos dados do arquivo cujo nome é passado por parâmetro
    {
        if (nomeArquivo != null) // se arquivo for diferente de nulo
        {
            var arquivo = new StreamReader(nomeArquivo); //novo obj do tipo StreamReader

            while (!arquivo.EndOfStream)  //enquanto não for fim de arquivo
            {
                var dado = new Dado(); //cria obj do tipo Dado

                dado.LerRegistro(arquivo);  //le o dado

                IncluirAposFim(dado);  //inclui o dado
            }

            arquivo.Close(); //fecha o arquivo
        }
    }


    public void GravarDados(string nomeArquivo)  // gravará sequencialmente, no arquivo cujo nome é passado por parâmetro, os dados armazenados na lista
    {
        if (nomeArquivo != null)  // se arquivo for diferente de nulo
        {
            var arq = new StreamWriter(nomeArquivo);    //novo obj do tipo StreamWriter
            PosicionarNoPrimeiro(); //posicionamos no primeiro dado para iniciar a leitura

            while (atual != null)  //enquanto o atual for diferente de nulo
            {
                var dado = atual.Info; //dado recebe as informações do dadoAtual()
                dado.GravarRegistro(arq); //grava o dado no arquivo passado por parâmetro
                AvancarPosicao(); // vai para o prox dado
            }
            arq.Close(); //fecha o arquivo
        }
    }
    public void PosicionarNoPrimeiro()        // Posicionar atual no primeiro nó para ser acessado
    {
        if (!EstaVazio)         //se a lista no está vazia
            atual = primeiro;   //posiciona atual no primeiro nó
    }
    public void RetrocederPosicao()        // Retroceder atual para o nó anterior para ser acessado
    {   
        if (!EstaVazio)         //se a lista no está vaziaz
            atual = atual.Ant;  //posiciona atual no nó anterior do atual
    }
    public void AvancarPosicao()
    {
        if (!EstaVazio)         //se a lista no está vazia
            atual = atual.Prox; //posiciona atual no próximo nó 
    }
    public void PosicionarNoUltimo()        // posicionar atual no último nó para ser acessado
    {
        if (!EstaVazio)         //se a lista no está vazia
            atual = ultimo;     //posiciona atual no último nó
    }
    public void PosicionarEm(int posicaoDesejada)
    {
        PosicionarNoPrimeiro(); // usamos o método posicionar no primeiro

        for (int i = 0; i < posicaoDesejada; i++) // fazemos um for, até a posição desejada
        {
            if (atual != null) //se atual for diferente de nulo
            {
                AvancarPosicao(); // usamos o método avançar posição
            }
        }
    }
    // (bool) Pesquisar Dado procurado em ordem crescente; a pesquisa
    // posicionará o ponteiro atual no nó procurado quando este
    // or encontrado ou, se não achar, no nó seguinte a local
    // onde deveria estar o nó procurado
    public bool Existe(Dado procurado, out int ondeEsta)
    {
        PosicionarNoPrimeiro();
        // Em seguida, é verificado se a lista está vazia. Caso esteja, é
        // retornado false ao local de chamada, indicando que a chave não foi
        // encontrada, e atual e anterior ficam valendo null
        if (EstaVazio)
        {
            ondeEsta = -1; //retorna posição inexistente
            return false;
        }
        // a lista não está vazia, possui nós
        // dado procurado é menor que o primeiro dado da lista:
        // portanto, dado procurado não existe
        if (procurado.CompareTo(primeiro.Info) < 0)
        {
            ondeEsta = -1; //retorna posição inexistente
            return false;
        }
        // dado procurado é maior que o último dado da lista:
        // portanto, dado procurado não existe
        if (procurado.CompareTo(ultimo.Info) > 0)
        {
            PosicionarNoUltimo();
            ondeEsta = -1; //retorna posição inexistente
            return false;
        }
        // caso não tenha sido definido que a chave está fora dos limites de 
        // chaves da lista, vamos procurar no seu interior
        // o apontador atual indica o primeiro nó da lista e consideraremos que
        // ainda não achou a chave procurada nem chegamos ao final da lista
        bool achou = false;
        bool fim = false;
        ondeEsta = 0;
        // repete os comandos abaixo enquanto não achou o RA nem chegou ao
        // final da lista
        while (!achou && !fim)
            // se o apontador atual vale null, indica final da lista
            if (atual == null)
                fim = true;
            // se não chegou ao final da lista, verifica o valor da chave atual
            else
            // verifica igualdade entre chave procurada e chave do nó atual
            
            if (procurado.CompareTo(atual.Info) == 0)
            {
                achou = true;
            }
            else
            // se chave atual é maior que a procurada, significa que
            // a chave procurada não existe na lista ordenada e, assim,
            // termina a pesquisa indicando que não achou. Anterior
            // aponta o anterior ao atual, que foi acessado por
            // último
            if (atual.Info.CompareTo(procurado) > 0)
                fim = true;
            else
            {
                // se não achou a chave procurada nem uma chave > que ela,
                // então a pesquisa continua, de maneira que o apontador
                // anterior deve apontar o nó atual e o apontador atual
                // deve seguir para o nó seguinte
                AvancarPosicao(); // avança posição do nó
                ondeEsta++;
            }
        // por fim, caso a pesquisa tenha terminado, o apontador atual
        // aponta o nó onde está a chave procurada, caso ela tenha sido
        // encontrada, ou o nó onde ela deveria estar para manter a
        // ordenação da lista. O apontador anterior aponta o nó anterior
        // ao atual
        return achou; // devolve o valor da variável achou, que indica
    }                   // se a chave procurada foi ou não encontrado
    public bool Excluir(Dado dadoAExcluir)  //CORRIGIR
    {
        

        if (Existe(dadoAExcluir, out _))
        {
            if (EstaNoInicio) //se o atual for o primeiro
            {
                primeiro = atual.Prox; // primeiro apontara para o proximo nó

                if (primeiro != null)
                {
                    primeiro.Ant = null;
                }
            }
            else
            {
                atual.Ant.Prox = atual.Prox; //o proximo no do anterior aponta o proximo nó do atual

                if (EstaNoFim) //se o nó atual for o ultimo 
                {
                    ultimo = atual.Ant; //o ultimo apontará para seu nó antecessor
                    // ultimo.Prox = null; // ultimo.Prox será igual a nulo
                }

                else
                {
                    // atual.Prox = atual.Ant; 
                    atual.Prox.Ant = atual.Ant; // o proximo no do anterior aponta para o anterior do atual
                }
            }
               
            quantosNos--; //decrementa a quantidade de nós
            AvancarPosicao(); //posiciona no ultimo
            return true; //exclusão bem sucedida 
        }
        return false;
    }

    public bool IncluirNoInicio(Dado novoValor)
    {
      
        if (novoValor == null)  // se o novoValor for nulo
            return false;       //retorna falso

        var novoNo = new NoDuplo<Dado>(novoValor); //cria novoNo

        if (EstaVazio)//                         z                 x               x
            ultimo = novoNo;// o último elemento da lista receberá o novoNo
        else
        {
            novoNo.Prox = primeiro; // o novoNo.Prox recebido apontara para o primeiro
            novoNo = primeiro.Ant; //novo no apontara para o no anterior 
        }

        primeiro = novoNo; // o primeiro apontara para o novo no
        quantosNos++; //a quantidade de nós aumentará
        return true;
    }
    public bool IncluirAposFim(Dado novoValor)
    {
        
        if (novoValor == null) // se o novoValor for nulo
            return false;      //retorna falso

        var novoNo = new NoDuplo<Dado>(novoValor);// cria novoNo

        if (EstaVazio) //                  x                 x               z
            primeiro = novoNo;// o primeiro elemento da lista receberá o novoNo
        else
        {
            ultimo.Prox = novoNo; // o sucessor do ultimo no recebe o novoNo
            novoNo.Ant = ultimo; // o novoNo recebido apontara para o ultimo 
        }

        ultimo = novoNo; // ultimo recebe novoNo
        quantosNos++; //aumenta a quantidade de nós
        return true; //retorna true
    }

    public bool Incluir(Dado novoValor)         // (bool) Inserir nó com Dado em ordem crescente //CORRIGIR
    {
        if (!Existe(novoValor, out int ondeEsta)) // existeChave configura anterior e atual
        {                       // aqui temos certeza de que a chave não existe
                                // guarda dados no novo nó

            if (EstaVazio)      // se a lista está vazia, então o
                IncluirNoInicio(novoValor);  // novo nó é o primeiro da lista
            else
                if (EstaNoFim && ondeEsta == -1)
                IncluirAposFim(novoValor);
            else
            // testa se nova chave > última chave

            if (atual.Ant == null && atual != null)
                IncluirNoInicio(novoValor); // liga novo antes do primeiro
            else
            {
                var novo = new NoDuplo<Dado>(novoValor);
                atual.Ant.Prox = novo;  //atual.ant.prox aponta para o novo no
                novo.Ant = atual.Ant;   //novoNo.ant  aponta para o atual.ant
                atual.Ant = novo;        // atual.ant recebe o novo nó
                novo.Prox = atual;       //novo.prox recebe o atual
                atual = novo;           // atual aponta para o novo
                quantosNos++;            // incrementa número de nós da lista         
                return true;
            }
            return true;
        }
        return false;
    }
    public bool Incluir(Dado novoValor, int posicaoDeInclusao)  // inclui novo nó na posição indicada da lista
    {
        if (novoValor == null || posicaoDeInclusao < 0)  // se o novoValor for nulo
            return false;       //retorna falso

        var novoNo = new NoDuplo<Dado>(novoValor); //cria novoNo

        if (EstaVazio)      // se a lista está vazia, então o
            IncluirNoInicio(novoValor);  // novo nó é o primeiro da lista
        else if (!Existe(novoValor, out int ondeEsta)) //se o nó não existe
        {
            PosicionarEm(posicaoDeInclusao); //posiciona na posicao desejada pelo usuário
            atual.Ant.Prox = novoNo;  // proximo do anterior recebe novoNo
            novoNo.Prox = atual; //Proximo do novoNo recebe atual
            novoNo.Ant = atual.Ant; // Anterior do novoNO recebe Anterior do nó atual
            atual.Ant = novoNo; //Anterior do atual recebe novoNo
            atual = novoNo; //atual recebe novoNo
            quantosNos++; //aumenta a quantidade de nós
            return true; // inclusão realizada 
        }

        return false;
    }
    public Dado this[int indice]
    {

        get
        {
            if (indice < 0 || indice > quantosNos - 1) // se a posição for negativa ou menor que a quantidade de nós menos um
            {
                throw new Exception("Indice fora dos valores permitidos"); //dispara exceção
            }

            else 
            {
                PosicionarEm(indice); //posiciona no valor do índice 
                return DadoAtual();//Retorna o DadoAtual 
            }
        }

        set
        {
            {
                if (indice < 0 || indice > quantosNos - 1) // se a posição for negativa ou menor que a quantidade de nós menos um
                {
                    throw new Exception("Indice fora dos valores permitidos"); //dispara exceção
                }
                else
                {
                    PosicionarEm(indice); //posiciona no valor do índice 
                    atual.Info = value;   //muda dado atual
                }
            }
        }
    }
    public Dado DadoAtual()  // retorna o dado atualmente visitado
    {
        if (atual != null) //se atual for diferente de null
            return atual.Info; //sera retornado as informações do nó atual

        return default(Dado);//caso o atual seja nulo, nada sera retornado


    }
    public void ExibirDados()   // lista os dados armazenados na lista em modo console
    {
        Console.Clear(); //limpa a tela do console
        PosicionarNoPrimeiro(); //posiciona atual no primeiro
        while (atual != null) //se o atual for diferente de nulo, ou seja a lista não está vazia
        {
            Console.WriteLine(atual.Info + "\n"); //escreverá as informações do no atual no console
            atual = atual.Prox; //no atual recebe proximo no
        }
        PosicionarNoPrimeiro(); //posiciona atual no primeiro
    }
    public void ExibirDados(ListBox lista)  // lista os dados armazenados na lista no listbox passado como parâmetro
    {
        lista.Items.Clear(); //limpa o conteúdo da lista
        atual = primeiro;    //posiciona atual no primeiro
        while (atual != null)//se o atual for diferente de nulo, ou seja a lista não está vazia
        {
            lista.Items.Add(atual.Info);//irá ser adicionada na lista a informação do atual
            atual = atual.Prox;//nó atual recebe proximo no
        }
        PosicionarNoPrimeiro(); //posiciona atual no primeiro
    }
    public void ExibirDados(ComboBox lista) // lista os dados armazenados na lista no combobox passado como parâmetro
    {
        lista.Items.Clear(); //limpa o conteúdo do combobox
        atual = primeiro;   //posiciona atual no primeiro
        while (atual != null)//se o atual for diferente de nulo, ou seja a lista não está vazia
        {
            lista.Items.Add(atual.Info);//irá ser adicionada no combobox informação do atual
            atual = atual.Prox;//nó atual recebe proximo no
        }
        PosicionarNoPrimeiro(); //posiciona atual no primeiro
    }
    public void ExibirDados(TextBox lista)
    {
        lista.Text = ""; //limpa o conteúdo do textbox
        PosicionarNoPrimeiro();//posiciona atual no primeiro

        while (atual != null)  //se o atual for diferente de nulo, ou seja a lista não está vazia
        {
            lista.Text += $"{atual.Info}"; //escreve no textbox a informação contida no atual
            atual = atual.Prox;//nó atual recebe proximo no
        }
        PosicionarNoPrimeiro(); //posiciona atual no primeiro
    }
    public void Ordenar()
    {
        ListaDupla<Dado> ordenada = new ListaDupla<Dado>(); //cria nova lista ordenada

        PosicionarNoPrimeiro(); // posicionamos no primeiro dado/no

        while (atual != null) //enquanto não está vazia
        {
            ordenada.Incluir(atual.Info);// lista ordenada recebe as informações do dado atual
            this.AvancarPosicao();//avança posição na lista
            // achar o menor de todos
            // remover o menor de todos
            // incluir o menor de todos já removido na lista ordenada
        }

        this.primeiro = ordenada.primeiro;
        this.ultimo = ordenada.ultimo;
        this.quantosNos = ordenada.quantosNos;
    }
}
