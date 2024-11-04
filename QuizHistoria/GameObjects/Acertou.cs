using System;

namespace QuizHistoria
{
    class Acertou:GameObject
    {
        int tempo0 = 10, tempo1 = 45;

        public override void Render()
        {
            if(tempo0 > 0)
            {
                tempo0--;
                spr.image_index = 0;
            }
            else if(tempo1 > 0)
            {
                tempo1--;
                spr.image_index = 1;
            }
            else
            {
                Program.deletedGameObjects.Add(this);
                Program.perguntas[0].Delete();
                Program.perguntas.RemoveAt(0);
                Program.correções[0].Start();
            }
            Window.Render(this);
        }
    }
}
