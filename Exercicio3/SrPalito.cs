using CG_Biblioteca;
using System;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class SrPalito : Objeto
    {
        public SrPalito(Objeto _paiRef, ref char _rotulo) : base(_paiRef, ref _rotulo)
        {
            Atualizar();
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }
    }
}
