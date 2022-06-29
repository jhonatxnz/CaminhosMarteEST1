using System;
using System.IO;

namespace apCaminhos
{
    class Cidade : IComparable<Cidade>, IRegistro<Cidade>
    {
        const int tamCodigo = 3,
                  tamNome = 15,
                  tamX = 7,
                  tamY = 7;

        const int iniCodigo = 0,
                  iniNome = iniCodigo + tamCodigo,
                  iniX = iniNome + tamNome,
                  iniY = iniX + tamX;

        string codigo, nome;
        float x, y;

        public string Codigo { get => codigo; set => codigo = value.PadLeft(tamCodigo, '0').Substring(0, tamCodigo); }
        public string Nome { get => nome; set => nome = value.PadRight(tamCodigo, ' ').Substring(0, tamNome); }
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public Cidade(string codigo, string nome, float x, float y)
        {
            Codigo = codigo;
            Nome = nome;
            X = x;
            Y = y;
        }
        //construtor vazio
        public Cidade() { }

        public int CompareTo(Cidade outro)
        {
            return nome.ToUpperInvariant().CompareTo(outro.nome.ToUpperInvariant());
        }

        public Cidade LerRegistro(StreamReader arquivo)
        {
            if (arquivo != null) // arquivo aberto?
            {
                string linha = arquivo.ReadLine();
                Codigo = linha.Substring(iniCodigo, tamCodigo);
                Nome = linha.Substring(iniNome, tamNome);
                X = float.Parse(linha.Substring(iniX, tamX));
                Y = float.Parse(linha.Substring(iniY));
                return this; // retorna o próprio objeto Contato, com os dados
            }
            return default(Cidade);
        }

        public void GravarRegistro(StreamWriter arq)
        {
            if (arq != null)  // arquivo de saída aberto?
            {
                arq.WriteLine(ParaArquivo());
            }
        }
        public string ParaArquivo()
        {
            return Codigo + Nome + X.ToString() + Y.ToString();
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
