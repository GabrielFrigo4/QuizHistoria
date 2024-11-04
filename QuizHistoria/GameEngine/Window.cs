using SDL2;
using System;
using System.Collections.Generic;

namespace QuizHistoria
{
    static class Window
    {
        public const int PIXEL_SIZE = 1;

        static public IntPtr window, renderer;

        public static Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
        public static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        public static bool running = true;

        public static void CloseGame()
        {
            running = false;
        }

        public static void Init(string title, int height, int width, bool full_window = false)
        {
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            SDL.SDL_Init(SDL.SDL_INIT_AUDIO);
            SDL.SDL_WindowFlags windowFlags;
            if (full_window)
                windowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
            else
                windowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;
            window = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, height, width, windowFlags);
            renderer = SDL.SDL_CreateRenderer(window,
                                      -1,
                                      SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                                      SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            IMG.IMG_Init(IMG.IMG_InitFlags.IMG_INIT_PNG);
            TTF.TTF_Init();

            int audio_rate = 44100, channels = 2, audio_buffer = 4096;
            UInt16 audio_format = SDL.AUDIO_S16SYS;
            MIX.Mix_OpenAudio(audio_rate, audio_format, channels, audio_buffer);
        }

        public static void Clear()
        {
            SDL.SDL_RenderClear(renderer);
        }

        public static void Quit()
        {
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
            TTF.TTF_Quit();
            IMG.IMG_Quit();
        }

        public static IntPtr LoadTexture(string path)
        {
            IntPtr tex = IMG.IMG_LoadTexture(renderer, path);
            if (tex == IntPtr.Zero)
                Console.WriteLine("erro load texture");
            return tex;
        }

        public static void Render(GameObject obj)
        {
            SDL.SDL_Rect srcrect;
            srcrect.x = obj.spr.GetFrame().x;
            srcrect.y = obj.spr.GetFrame().y;
            srcrect.w = obj.spr.GetFrame().w;
            srcrect.h = obj.spr.GetFrame().h;

            SDL.SDL_Rect dstrect;
            dstrect.x = (int)obj.pos.x * PIXEL_SIZE;
            dstrect.y = (int)obj.pos.y * PIXEL_SIZE;
            dstrect.w = obj.spr.drawFrame.w * PIXEL_SIZE;
            dstrect.h = obj.spr.drawFrame.h * PIXEL_SIZE;

            SDL.SDL_Point origen;
            origen.x = 0 * PIXEL_SIZE;
            origen.y = 0 * PIXEL_SIZE;

            SDL.SDL_RenderCopyEx(renderer, obj.spr.tex, ref srcrect, ref dstrect, obj.spr.ang, ref origen, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public static void RenderSpr(Sprite spr, int x, int y)
        {
            SDL.SDL_Rect srcrect;
            srcrect.x = spr.GetFrame().x;
            srcrect.y = spr.GetFrame().y;
            srcrect.w = spr.GetFrame().w;
            srcrect.h = spr.GetFrame().h;

            SDL.SDL_Rect dstrect;
            dstrect.x = x * PIXEL_SIZE;
            dstrect.y = y * PIXEL_SIZE;
            dstrect.w = spr.drawFrame.w * PIXEL_SIZE;
            dstrect.h = spr.drawFrame.h * PIXEL_SIZE;

            SDL.SDL_Point origen;
            origen.x = 0 * PIXEL_SIZE;
            origen.y = 0 * PIXEL_SIZE;

            SDL.SDL_RenderCopyEx(renderer, spr.tex, ref srcrect, ref dstrect, spr.ang, ref origen, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public static void RenderTex(IntPtr tex, int x, int y)
        {
            int texW = 0;
            int texH = 0;
            SDL.SDL_QueryTexture(tex, out _, out _, out texW, out texH);

            SDL.SDL_Rect dstrect;
            dstrect.x = x;
            dstrect.y = y;
            dstrect.w = texW;
            dstrect.h = texH;

            SDL.SDL_Rect srcrect;
            srcrect.x = 0;
            srcrect.y = 0;
            srcrect.w = texW;
            srcrect.h = texH;

            SDL.SDL_RenderCopy(renderer, tex, ref srcrect, ref dstrect);
        }

        public static void RenderTexExt(IntPtr tex, int x, int y, int w, int h)
        {
            int texW = 0;
            int texH = 0;
            SDL.SDL_QueryTexture(tex, out _, out _, out texW, out texH);

            SDL.SDL_Rect dstrect;
            dstrect.x = x;
            dstrect.y = y;
            dstrect.w = w;
            dstrect.h = h;

            SDL.SDL_Rect srcrect;
            srcrect.x = 0;
            srcrect.y = 0;
            srcrect.w = texW;
            srcrect.h = texH;

            SDL.SDL_RenderCopy(renderer, tex, ref srcrect, ref dstrect);
        }

        public static void DrawColor(byte r, byte g, byte b, byte a)
        {
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, a);
        }

        public static void DrawText(string text, int x, int y, int size)
        {
            IntPtr _font = TTF.TTF_OpenFont("C:/Windows/Fonts/arial.ttf", size);

            SDL.SDL_Color color;
            color.r = color.g = color.b = color.a = 0;

            IntPtr surfaceMessage = TTF.TTF_RenderText_Solid(_font, text, color);

            IntPtr Message = SDL.SDL_CreateTextureFromSurface(renderer, surfaceMessage);

            int texW = 0;
            int texH = 0;
            uint format = 0;
            int access = 0;
            SDL.SDL_QueryTexture(Message, out format, out access, out texW, out texH);

            SDL.SDL_Rect dstrect;
            dstrect.x = x;
            dstrect.y = y;
            dstrect.w = texW;
            dstrect.h = texH;

            SDL.SDL_Rect srcrect;
            srcrect.x = 0;
            srcrect.y = 0;
            srcrect.w = texW;
            srcrect.h = texH;

            SDL.SDL_RenderCopy(renderer, Message, ref srcrect, ref dstrect);
            TTF.TTF_CloseFont(_font);

            SDL.SDL_FreeSurface(surfaceMessage);
            SDL.SDL_DestroyTexture(Message);
        }

        public static void DrawTextExt(string text, int x, int y, int size, SDL.SDL_Color color, string font, bool center)
        {
            IntPtr _font = TTF.TTF_OpenFont("C:/Windows/Fonts/" + font, size);
            IntPtr surfaceMessage = TTF.TTF_RenderText_Solid(_font, text, color);

            IntPtr Message = SDL.SDL_CreateTextureFromSurface(renderer, surfaceMessage);

            int texW = 0;
            int texH = 0;
            uint format = 0;
            int access = 0;
            SDL.SDL_QueryTexture(Message, out format, out access, out texW, out texH);

            SDL.SDL_Rect dstrect;
            dstrect.x = x;
            dstrect.y = y;
            dstrect.w = texW;
            dstrect.h = texH;

            SDL.SDL_Rect srcrect;
            srcrect.x = 0;
            srcrect.y = 0;
            srcrect.w = texW;
            srcrect.h = texH;

            if (!center)
            {
                dstrect.x = x;
                dstrect.y = y;
                dstrect.w = texW;
                dstrect.h = texH;
            }
            else
            {
                dstrect.x = x - (texW / 2);
                dstrect.y = y - (texH / 2);
                dstrect.w = texW;
                dstrect.h = texH;
            }

            SDL.SDL_RenderCopy(renderer, Message, ref srcrect, ref dstrect);
            TTF.TTF_CloseFont(_font);

            SDL.SDL_FreeSurface(surfaceMessage);
            SDL.SDL_DestroyTexture(Message);
        }

        public static void DrawRect(int x, int y, int w, int h)
        {
            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;

            SDL.SDL_RenderFillRect(renderer, ref rect);
        }

        public static void Display()
        {
            SDL.SDL_RenderPresent(renderer);
        }

        public static int GetRefreshRate()
        {
            int displayIndex = SDL.SDL_GetWindowDisplayIndex(window);

            SDL.SDL_DisplayMode mode;

            SDL.SDL_GetDisplayMode(displayIndex,0, out mode);

            return mode.refresh_rate;
        }
    }
}
