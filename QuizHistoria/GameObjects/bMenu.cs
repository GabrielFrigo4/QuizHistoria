using SDL2;
using System;

namespace QuizHistoria
{
    class bMenu : GameObject
    {
        bool mouseDown = false, apertado = false;

        public override void Update()
        {
            int mouse_x, mouse_y;
            SDL.SDL_GetMouseState(out mouse_x, out mouse_y);

            if (MyMath.pointRect(mouse_x, mouse_y, (pos.x + 1) * Window.PIXEL_SIZE, (pos.y + 1) * Window.PIXEL_SIZE, (spr.drawFrame.w - 2) * Window.PIXEL_SIZE, (spr.drawFrame.h - 2) * Window.PIXEL_SIZE))
            {
                if (mouseDown)
                {
                    if (!apertado)
                        Program.StartGame();
                    apertado = true;
                }
                else
                {
                    apertado = false;
                }
                spr.image_index = 1;
            }
            else
            {
                spr.image_index = 0;
                if (mouseDown)
                {
                    apertado = true;
                }
                else
                {
                    apertado = false;
                }
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