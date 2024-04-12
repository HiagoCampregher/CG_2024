using CG_Biblioteca;
using System;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        private double raio = 0;

        public Circulo(Objeto _paiRef, ref char _rotulo, int _quantidadePontos, int _tamanhoPonto, double _raio) : base(_paiRef, ref _rotulo)
        {
            // Quantidade pontos
            // raio
            // tamanho 5

            PrimitivaTipo = PrimitiveType.Points;
            PrimitivaTamanho = _tamanhoPonto;
            raio = _raio;



            double anguloCalculado = Convert.ToDouble(360 / _quantidadePontos);

            double anguloDeslocado = 0;
            for (int idx = 0; idx < _quantidadePontos; ++idx)
            {
                anguloDeslocado += anguloCalculado;
                Ponto4D ponto = Matematica.GerarPtosCirculo(anguloDeslocado, raio);

                PontosAdicionar(ponto);
            }

            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }
    }
}
