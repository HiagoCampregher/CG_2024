using CG_Biblioteca;
using System;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        private double raio = 0;

        public Circulo(Objeto _paiRef, ref char _rotulo, double _raio) : base(_paiRef, ref _rotulo)
        {
            PrimitivaTipo = PrimitiveType.Points;
            raio = _raio;

            double anguloCalculado = Convert.ToDouble(360 / 72);

            double anguloDeslocado = 0;
            for (int idx = 0; idx < 72; ++idx)
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
