using SDL2;
using System;
using System.Collections.Generic;

namespace QuizHistoria
{
    class bOpções : GameObject
    {
        public bool correta;
        public string text = "text";
        bool mouseDown = false, apertado = false, erro = false;
        int posBut = 0;

        public override void Update()
        {
            int mouse_x, mouse_y;
            SDL.SDL_GetMouseState(out mouse_x, out mouse_y);

            bool certoErrado = false;

            foreach(var obj in Program.gameObjects)
            {
                if(obj.GetType() == typeof(Acertou) || obj.GetType() == typeof(Errou))
                {
                    certoErrado = true;
                }
            }


            if(!erro && !certoErrado)
            {
                if (MyMath.pointRect(mouse_x, mouse_y, (pos.x + 8) * Window.PIXEL_SIZE, (pos.y + 8) * Window.PIXEL_SIZE, (spr.drawFrame.w - 20) * Window.PIXEL_SIZE, (spr.drawFrame.h - 20) * Window.PIXEL_SIZE))
                {
                    if (mouseDown)
                    {
                        spr.image_index = 2;
                        posBut = 16;
                        if (!apertado)
                            Window.sounds["button"].Play(0);
                        apertado = true;
                    }
                    else if (apertado)
                    {
                        if (correta)
                        {
                            posBut = 0;
                            spr.image_index = 4;

                            int windX, windY, totalX = 640, totalY = 640, sobrouX, sobrouY;
                            SDL.SDL_GetWindowSize(Window.window, out windX, out windY);
                            sobrouX = windX - totalX;
                            sobrouY = windY - totalY;
                            Sprite acertou = Sprite.CreateSprite("Asset/Resposta_Certa.png", new Vector2(640, 640), 1);
                            CreateGameObject(new Vector2(sobrouX / 2, sobrouY / 2), acertou, typeof(Acertou));
                        }
                        else
                        {
                            posBut = 0;
                            spr.image_index = 3;
                            erro = true;

                            int windX, windY, totalX = 640, totalY = 640, sobrouX, sobrouY;
                            SDL.SDL_GetWindowSize(Window.window, out windX, out windY);
                            sobrouX = windX - totalX;
                            sobrouY = windY - totalY;
                            Sprite acertou = Sprite.CreateSprite("Asset/Resposta_Errada.png", new Vector2(640, 640), 1);
                            CreateGameObject(new Vector2(sobrouX / 2, sobrouY / 2), acertou, typeof(Errou));
                        }
                        apertado = false;
                    }
                    else
                    {
                        posBut = 8;
                        spr.image_index = 1;
                    }
                }
                else
                {
                    posBut = 0;
                    spr.image_index = 0;
                    apertado = false;
                }
            } 
        }

        public override void Render()
        {
            Window.Render(this);
            SDL.SDL_Color color;
            color.a = 0;
            color.r = 0;
            color.g = 0;
            color.b = 0;

            string[] lText = text.Split(new char[1] {'\n' },StringSplitOptions.RemoveEmptyEntries);

            int size = 36, y = -(lText.Length - 1) * size / 2;

            foreach (var line in lText)
            {
                Window.DrawTextExt(line, posBut + (int)(pos.x + spr.drawFrame.w / 2) * Window.PIXEL_SIZE, y + posBut + (int)(pos.y + spr.drawFrame.h / 2) * Window.PIXEL_SIZE, size, color, "micross.ttf", true);
                y += size;
            }
        }

        public override void Input(SDL.SDL_Event _event)
        {
            if (_event.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                if (_event.button.button == SDL.SDL_BUTTON_LEFT)
                    mouseDown = true;
            }
            if (_event.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                if (_event.button.button == SDL.SDL_BUTTON_LEFT)
                    mouseDown = false;
            }
            base.Input(_event);
        }
    }
}
