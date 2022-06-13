using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apCaminhos
{
    class Movimento : IComparable<Movimento>
    {// onde estou, para onde vou
        private int origem, destino;
        public Movimento(int or, int dest)
        {
            origem = or;
            destino = dest;
        }
        public int CompareTo(Movimento outro) // compatível com ListaSimples e NoLista
        {
            return 0;
        }
        public int Origem { get => origem; set => origem = value; }
        public int Destino { get => destino; set => destino = value; }
        public override String ToString()
        {
            return origem + " " + destino;
        }
        //public Movimento LerRegistro(StreamReader arquivo) {
        //    if (arquivo != null) // arquivo aberto?
        //    {
        //        string linha = arquivo.ReadLine();
        //        Origem = int.Parse(linha.Substring(origem, destino));
                
        //        return this; // retorna o próprio objeto Contato, com os dados
        //    }
        //    return default(Movimento);
        //}
        //public string ParaArquivo()
        //{
        //    return origem + " " + destino;
        //}
        //public void GravarRegistro(StreamWriter arquivo) {
        //    if (arquivo != null)  // arquivo de saída aberto?
        //    {
        //        arquivo.WriteLine(ParaArquivo());
        //    }
        //}
    }
}
