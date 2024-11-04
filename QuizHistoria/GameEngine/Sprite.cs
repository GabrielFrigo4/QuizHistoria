using System;
using SDL2;

namespace QuizHistoria
{
    class Sprite
    {
        public IntPtr tex;
        public SDL.SDL_Point origin;
        public SDL.SDL_Rect frame, drawFrame;
        public uint image_index, ind_max;
        public float ang = 0;

        Sprite(IntPtr tex, SDL.SDL_Point origin, Vector2 size, Vector2 sizeDraw, uint ind_max)
        {
            this.tex = tex;
            this.origin = origin;
            this.frame.x = 0;
            this.frame.y = 0;
            this.frame.w = size.x;
            this.frame.h = size.y;
            this.drawFrame.x = 0;
            this.drawFrame.y = 0;
            this.drawFrame.w = sizeDraw.x;
            this.drawFrame.h = sizeDraw.y;
            this.image_index = 0;
            this.ind_max = ind_max;
        }

        public static Sprite CreateSprite(string path, SDL.SDL_Point origin, Vector2 size, uint ind_max)
        {
            IntPtr tex = Window.LoadTexture(path);
            Sprite spr;
            spr = new Sprite(tex, origin, size, size, ind_max);
            return spr;
        }

        public static Sprite CreateSprite(string path, Vector2 size, uint ind_max)
        {
            IntPtr tex = Window.LoadTexture(path);
            Sprite spr;
            SDL.SDL_Point origin;
            origin.x = 0; 
            origin.y = 0;
            spr = new Sprite(tex, origin, size, size, ind_max);
            return spr;
        }

        public static Sprite CreateSprite(string path, SDL.SDL_Point origin, Vector2 size, Vector2 drawSize, uint ind_max)
        {
            IntPtr tex = Window.LoadTexture(path);
            Sprite spr;
            spr = new Sprite(tex, origin, size, drawSize, ind_max);
            return spr;
        }

        public static Sprite CreateSprite(string path, Vector2 size, Vector2 drawSize, uint ind_max)
        {
            IntPtr tex = Window.LoadTexture(path);
            Sprite spr;
            SDL.SDL_Point origin;
            origin.x = 0;
            origin.y = 0;
            spr = new Sprite(tex, origin, size, drawSize, ind_max);
            return spr;
        }

        public SDL.SDL_Rect GetFrame()
        {
            SDL.SDL_Rect thisFrame;

            int theInd = (int)image_index;
            if (theInd >= ind_max)
            {
                theInd = (int)ind_max - 1;
            }
            if (theInd < 0)
            {
                theInd = 0;
            }
            thisFrame.x = frame.x + frame.w * ((int)image_index);
            thisFrame.y = frame.y;
            thisFrame.w = frame.w;
            thisFrame.h = frame.h;

            return thisFrame;
        }

        public void Delete()
        {
            SDL.SDL_DestroyTexture(tex);
        }
    }
}
