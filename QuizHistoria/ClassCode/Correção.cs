using SDL2;
using System;

namespace QuizHistoria
{
    class Correção
    {
        public string titulo, conteudo;
        public IntPtr image = IntPtr.Zero;

        public void Start()
        {
            int windX, windY;
            SDL.SDL_GetWindowSize(Window.window, out windX, out windY);

            Program.deletedGameObjects.AddRange(Program.gameObjects);
            Sprite spr = Sprite.CreateSprite("Asset/Passar.png", new Vector2(128, 64), 4);
            GameObject.CreateGameObject(new Vector2(windX - 128, windY - 64), spr, typeof(bNext));

            Sprite sprClose = Sprite.CreateSprite("Asset/Sair.png", new Vector2(64, 64), 2);
            GameObject.CreateGameObject(new Vector2(windX - 64, 0), sprClose, typeof(bClose));
            Sprite sprMenu = Sprite.CreateSprite("Asset/Volta_Menu.png", new Vector2(64, 64), 2);
            GameObject.CreateGameObject(new Vector2(0, 0), sprMenu, typeof(bMenu));
        }

        public void Render()
        {
            int windX, windY, totalX = 1408, totalY = 588, sobrouX, sobrouY;
            SDL.SDL_GetWindowSize(Window.window, out windX, out windY);
            sobrouX = windX - totalX;
            sobrouY = windY - totalY;

            //titulo
            SDL.SDL_Color color;
            color.a = 0;
            color.r = 0;
            color.g = 0;
            color.b = 0;

            string[] lText = titulo.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int size = 48, y = -(lText.Length - 1) * size / 2;

            foreach (var line in lText)
            {
                Window.DrawTextExt(line, windX / 2, y + 98, size, color, "calibrib.ttf", true);
                y += size;
            }

            //corpo
            color.a = 0;
            color.r = 0;
            color.g = 0;
            color.b = 0;

            lText = conteudo.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int size2 = 36, y2 = -(lText.Length - 1) * size2 / 2;

            int texW = 0;
            int texH = 32;

            if(image != IntPtr.Zero)
                SDL.SDL_QueryTexture(image, out _, out _, out texW, out texH);

            foreach (var line in lText)
            {
                Window.DrawTextExt(line, windX / 2, y2 + (y + 16 + (windY - texH))/2, size2, color, "micross.ttf", true);
                y2 += size;
            }

            Window.RenderTex(image, windX / 2 - texW / 2, windY - texH - 32);
        }

        public void Delete()
        {
            SDL.SDL_DestroyTexture(image);
        }
    }
}
