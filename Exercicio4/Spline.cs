using CG_Biblioteca;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcgcg
{
    internal class Spline : Objeto
    {
        private IList<Ponto> pontoSelecionados;
        private IList<Ponto4D> pontos;
        private Ponto4D pontoSelecionado;

        private Ponto4D pontoInferiorEsquerdo;
        private Ponto4D pontoSuperiorEsquerdo;
        private Ponto4D pontoInferiorDireito;
        private Ponto4D pontoSuperiorDireito;

        private SegReta AB;
        private SegReta BC;
        private SegReta CD;

        private int indexPonto = 0;

        private int qtdPontosSpline = 9;

        public Spline(Objeto _paiRef, ref char _rotulo, Objeto objetoFilho = null) : base(_paiRef, ref _rotulo, objetoFilho)
        {
            PrimitivaTipo = OpenTK.Graphics.OpenGL4.PrimitiveType.LineLoop;
            ShaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag");
            PrimitivaTamanho = 20;

            pontoInferiorEsquerdo = new Ponto4D(-0.5, -0.5);
            pontoSuperiorEsquerdo = new Ponto4D(-0.5, 0.5);
            pontoInferiorDireito  = new Ponto4D(0.5, -0.5);
            pontoSuperiorDireito  = new Ponto4D(0.5, 0.5);

            pontos = [pontoInferiorDireito, pontoSuperiorDireito, pontoSuperiorEsquerdo, pontoInferiorEsquerdo];

            pontoSelecionados = [
                new Ponto(this, ref _rotulo, pontoInferiorDireito),
                new Ponto(this, ref _rotulo, pontoSuperiorDireito),
                new Ponto(this, ref _rotulo, pontoSuperiorEsquerdo),
                new Ponto(this, ref _rotulo, pontoInferiorEsquerdo)];
            pontoSelecionado = pontos[indexPonto];

            AB = new SegReta(this, ref _rotulo, pontoInferiorDireito, pontoSuperiorDireito);
            BC = new SegReta(this, ref _rotulo, pontoSuperiorDireito, pontoSuperiorEsquerdo);
            CD = new SegReta(this, ref _rotulo, pontoSuperiorEsquerdo, pontoInferiorEsquerdo);

            AB.ShaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shaderCiano.frag");
            BC.ShaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shaderCiano.frag");
            CD.ShaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shaderCiano.frag");

            Atualizar();
        }

        public void PegarProximoPonto()
        {
            indexPonto++;
            if (indexPonto == 4)
                indexPonto = 0;

            pontoSelecionado = pontos[indexPonto];
        }

        public void AtualizarPontoSelecionado()
        {
            //pontos[indexPonto] = pontoSelecionado;
            Atualizar();
        }

        void CalcularPontosSpline()
        {
            base.pontosLista.Clear();

            for (var idxPonto = 1; idxPonto < qtdPontosSpline; idxPonto++)
            {
                var tempo = (1.0 / qtdPontosSpline) * idxPonto;

                var p1p2 = PontoIntermediario(pontoInferiorEsquerdo, pontoSuperiorEsquerdo, tempo);
                var p2p3 = PontoIntermediario(pontoSuperiorEsquerdo, pontoSuperiorDireito, tempo);
                var p3p4 = PontoIntermediario(pontoSuperiorDireito, pontoInferiorDireito, tempo);
            
                var p1p2p3 = PontoIntermediario(p1p2, p2p3, tempo);
                var p2p3p4 = PontoIntermediario(p2p3, p3p4, tempo);
            
                var p1p2p3p4 = PontoIntermediario(p1p2p3, p2p3p4, tempo);
                base.PontosAdicionar(p1p2p3p4);
            }

            base.PontosAdicionar(pontoInferiorDireito);
            base.PontosAdicionar(pontoSuperiorDireito);
            base.PontosAdicionar(pontoSuperiorEsquerdo);
            base.PontosAdicionar(pontoInferiorEsquerdo);

            base.ObjetoAtualizar();
        }

        Ponto4D PontoIntermediario(Ponto4D pontoA, Ponto4D pontoB, double tempo)
        {
            double X = pontoA.X + (pontoB.X - pontoA.X) * tempo;
            double Y = pontoA.Y + (pontoB.Y - pontoA.Y) * tempo;
            return new Ponto4D(X, Y);
        }

        public void AlterarPonto(KeyboardState key)
        {
            if (key.IsKeyPressed(Keys.Space))
                PegarProximoPonto();
            if (key.IsKeyPressed(Keys.C))
            {
                pontoSelecionado.Y += 0.1;
            }
            if (key.IsKeyPressed(Keys.B))
            {
                pontoSelecionado.Y -= 0.1;
            }
            if (key.IsKeyPressed(Keys.E))
            {
                pontoSelecionado.X -= 0.1;
            }
            if (key.IsKeyPressed(Keys.D))
            {
                pontoSelecionado.X += 0.1;
            }
            if (key.IsKeyPressed(Keys.C))
            {
                pontoSelecionado.Y += 0.1;
            }
            if (key.IsKeyPressed(Keys.Equal))
            {
                // validar
                ++qtdPontosSpline;
            }
            if (key.IsKeyPressed(Keys.Comma))
            {
                // validar
                --qtdPontosSpline;
            }

            Atualizar();
        }

        void Atualizar()
        {
            int indexAnterior = 0;
            if (indexPonto == 0)
                indexAnterior = pontos.Count - 1;
            else
                indexAnterior = indexPonto - 1;

            pontoSelecionados[indexAnterior].ShaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shaderBranca.frag");
            pontoSelecionados[indexPonto].ShaderObjeto = new Shader("Shaders/shader.vert", "Shaders/shaderVermelha.frag");

            pontoSelecionados[indexPonto].PontosAlterar(pontos[indexPonto], 0);

            AB.ObjetoAtualizar();
            BC.ObjetoAtualizar();
            CD.ObjetoAtualizar();

            CalcularPontosSpline();
        }
    }
}
