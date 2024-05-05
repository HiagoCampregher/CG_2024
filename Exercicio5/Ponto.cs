#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Ponto : Objeto
    {
        public Ponto(Objeto _paiRef, ref char _rotulo, Ponto4D pto) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = 20;
            Deslocar(pto);
            Atualizar();
        }

        public Ponto(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, new Ponto4D()) { }

        public Ponto4D PontoElemento { get; private set; }

        public void Deslocar(Ponto4D novoPonto)
        {
            pontosLista.Clear();
            PontoElemento = novoPonto;
            PontosAdicionar(PontoElemento);
        }

        public void Atualizar()
        {
            base.ObjetoAtualizar();
        }

#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Ponto _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return (retorno);
        }
#endif

    }
}
