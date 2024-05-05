using CG_Biblioteca;
using System;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Circulo : Objeto
    {
        private double _angulo = Convert.ToDouble(360 / 72);

        public Circulo(Objeto paiRef, ref char rotulo, double raio, Ponto4D ptoDeslocamento) : base(paiRef, ref rotulo)
        {
            PrimitivaTipo = PrimitiveType.LineLoop;
            Raio = raio;

            Deslocar(ptoDeslocamento);
        }

        public Circulo(Objeto paiRef, ref char rotulo, double raio) : this(paiRef, ref rotulo, raio, new Ponto4D()) { }

        public Ponto4D PontoCentral { get; private set; }
        public double Raio { get; private set; }

        public void Deslocar(Ponto4D ptoCentral)
        {
            PontoCentral = ptoCentral;

            pontosLista.Clear();

            double anguloDeslocado = 0;
            for (int idx = 0; idx < 72; ++idx)
            {
                anguloDeslocado += _angulo;
                Ponto4D ponto = Matematica.GerarPtosCirculo(anguloDeslocado, Raio);
                ponto.X += ptoCentral.X;
                ponto.Y += ptoCentral.Y;

                PontosAdicionar(ponto);
            }
        }
    }
}
