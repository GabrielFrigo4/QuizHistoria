using SDL2;
using System;

namespace QuizHistoria
{
    class bStart : GameObject
    {
        bool mouseDown = false, apertado = false;

        public override void Update()
        {
            int mouse_x, mouse_y;
            SDL.SDL_GetMouseState(out mouse_x, out mouse_y);

            if (MyMath.pointRect(mouse_x, mouse_y, (pos.x + 8) * Window.PIXEL_SIZE, (pos.y + 8) * Window.PIXEL_SIZE, (spr.drawFrame.w - 20) * Window.PIXEL_SIZE, (spr.drawFrame.h - 20) * Window.PIXEL_SIZE))
            {
                if (mouseDown)
                {
                    spr.image_index = 2;
                    if (!apertado)
                        Window.sounds["button"].Play(0);
                    apertado = true;
                }
                else if (apertado)
                {
                    Program.perguntas[0].Start();
                    apertado = false;
                }
                else
                {
                    spr.image_index = 1;
                }
            }
            else
            {
                spr.image_index = 0;
                apertado = false;
            }
        }

        public override void Input(SDL.SDL_Event _event)
        {
            if (_event.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                if(_event.button.button == SDL.SDL_BUTTON_LEFT)
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