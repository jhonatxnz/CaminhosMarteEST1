using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class PilhaLista<Dado> : IStack<Dado> where Dado : IComparable<Dado>
{
    NoDuplo<Dado> topo;
    int tamanho;

    public PilhaLista() // construtor
    {
        topo = null;
        tamanho = 0;
    }
    public int Tamanho { get => tamanho; }
    public bool EstaVazia { get => topo == null; }
    public void Empilhar(Dado o)
    {
        // Instancia um nó, coloca o Dado o nele e o liga ao antigo topo da pilha
        NoDuplo<Dado> novoNo = new NoDuplo<Dado>(o);
        topo = novoNo; // topo passa a apontar o novo nó
        tamanho++; // atualiza número de elementos na pilha
    }
    public Dado OTopo()
    {
        if (EstaVazia)
            throw new Exception("Underflow da pilha");
        return topo.Info;
    }
    public Dado Desempilhar()
    {
        if (EstaVazia)
            throw new Exception("Underflow da pilha");
        Dado o = topo.Info; // obtém o objeto do topo
        topo = topo.Prox; // avança topo para o nó seguinte
        tamanho--; // atualiza número de elementos na pilha
        return o; // devolve o objeto que estava no topo
    }

    public List<Dado> DadosDaPilha()
    {
        List<Dado> lista = new List<Dado>();

        NoDuplo<Dado> atual = topo;
        while (atual != null)
        {
            lista.Add(atual.Info);
            atual = atual.Prox;
        }
        return lista;
    }

    

}