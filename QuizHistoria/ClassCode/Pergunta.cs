using SDL2;
using System;
using System.Collections.Generic;

namespace QuizHistoria
{
    class Pergunta
    {
        string pergunta;
        List<Opção> opções = new List<Opção>();
        Sprite sprite1, sprite2;
        int tempo, maxTempo = 4;
        bool start = false;

        public Pergunta(string pergunta)
        {
            this.pergunta = pergunta;
        }

        public void AddOpção(string text, bool correta)
        {
            Opção opção;
            opção.text = text;
            opção.correta = correta;
            opções.Add(opção);
        }

        public void Start()
        {
            Program.menu = false;
            start = true;
            tempo = maxTempo;

            int windX, windY, totalX = 640, totalY = 588, sobrouX, sobrouY;
            SDL.SDL_GetWindowSize(Window.window, out windX, out windY);
            sobrouX = windX / 2 - totalX;
            sobrouY = windY - totalY;

            sprite1 = Sprite.CreateSprite("Asset/Pergunta1.png", new Vector2(1408, 196), 3);
            sprite2 = Sprite.CreateSprite("Asset/Pergunta2.png", new Vector2(1408, 196), 2);

            List<int> randomItens = new List<int>() { 0, 1, 2, 3 };

            Random random = new Random();

            Program.deletedGameObjects.AddRange(Program.gameObjects);

            Sprite sprClose = Sprite.CreateSprite("Asset/Sair.png", new Vector2(64, 64), 2);
            GameObject.CreateGameObject(new Vector2(windX - 64, 0), sprClose, typeof(bClose));
            Sprite sprMenu = Sprite.CreateSprite("Asset/Volta_Menu.png", new Vector2(64, 64), 2);
            GameObject.CreateGameObject(new Vector2(0, 0), sprMenu, typeof(bMenu));

            for (int i = 0; i < 4; i ++)
            {
                if (i == 0)
                {
                    Sprite spr = Sprite.CreateSprite("Asset/Reposta.png", new Vector2(640, 196), 4);
                    bOpções obj = GameObject.CreateGameObject(new Vector2(sobrouX / 2, 196 + 64 + sobrouY / 4), spr, typeof(bOpções)) as bOpções;
                    int valorRam = random.Next(0, randomItens.Count);
                    int valor = randomItens[valorRam];
                    randomItens.Remove(valor);
                    obj.text = opções[valor].text;
                    obj.correta = opções[valor].correta;
                }
                if (i == 1)
                {
                    Sprite spr = Sprite.CreateSprite("Asset/Reposta.png", new Vector2(640, 196), 4);
                    bOpções obj = GameObject.CreateGameObject(new Vector2(sobrouX / 2, 196*2 + 64 + sobrouY / 2), spr, typeof(bOpções)) as bOpções;
                    int valorRam = random.Next(0, randomItens.Count);
                    int valor = randomItens[valorRam];
                    randomItens.Remove(valor);
                    obj.text = opções[valor].text;
                    obj.correta = opções[valor].correta;
                }
                if (i == 2)
                {
                    Sprite spr = Sprite.CreateSprite("Asset/Reposta.png", new Vector2(640, 196), 4);
                    bOpções obj = GameObject.CreateGameObject(new Vector2(sobrouX / 2 + windX / 2, 196 + 64 + sobrouY / 4), spr, typeof(bOpções)) as bOpções;
                    int valorRam = random.Next(0, randomItens.Count);
                    int valor = randomItens[valorRam];
                    randomItens.Remove(valor);
                    obj.text = opções[valor].text;
                    obj.correta = opções[valor].correta;
                }
                if (i == 3)
                {
                    Sprite spr = Sprite.CreateSprite("Asset/Reposta.png", new Vector2(640, 196), 4);
                    bOpções obj = GameObject.CreateGameObject(new Vector2(sobrouX / 2 + windX / 2, 196 * 2 + 64 + sobrouY / 2), spr, typeof(bOpções)) as bOpções;
                    int valorRam = random.Next(0, randomItens.Count);
                    int valor = randomItens[valorRam];
                    randomItens.Remove(valor);
                    obj.text = opções[valor].text;
                    obj.correta = opções[valor].correta;
                }
            }
        }

        public void Render()
        {
            if (start)
            {
                int windX, windY, totalX = 1408, totalY = 588, sobrouX, sobrouY;
                SDL.SDL_GetWindowSize(Window.window, out windX, out windY);
                sobrouX = windX - totalX;
                sobrouY = windY - totalY;

                if (tempo > 0 && sprite1.image_index < sprite1.ind_max)
                {
                    tempo--;
                    Window.RenderSpr(sprite1, sobrouX / 2, 64);
                }
                else if (sprite1.image_index < sprite1.ind_max)
                {
                    sprite1.image_index++;
                    tempo = maxTempo;
                    Window.RenderSpr(sprite1, sobrouX / 2, 64);
                }
                else if (tempo > 0 && sprite2.image_index < sprite1.ind_max)
                {
                    tempo--;
                    Window.RenderSpr(sprite2, sobrouX / 2, 64);
                }
                else if (sprite2.image_index < sprite2.ind_max)
                {
                    sprite2.image_index++;
                    tempo = maxTempo;
                    Window.RenderSpr(sprite2, sobrouX / 2, 64);
                }
                else
                {
                    Window.RenderSpr(sprite2, sobrouX / 2, 64);

                    SDL.SDL_Color color;
                    color.a = 0;
                    color.r = 0;
                    color.g = 0;
                    color.b = 0;

                    string[] lText = pergunta.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    int size = 36, y = -(lText.Length - 1) * size / 2;

                    foreach (var line in lText)
                    {
                        Window.DrawTextExt(line, windX / 2, y + 64 + 196 / 2, size, color, "micross.ttf", true);
                        y += size;
                    }
                }
            }  
        }

        public void Delete()
        {
            sprite1.Delete();
            sprite2.Delete();
        }
    }
}
