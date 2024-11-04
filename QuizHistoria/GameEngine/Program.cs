using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuizHistoria
{
    class Program
    {
        public static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> newGameObjects = new List<GameObject>();
        public static List<GameObject> deletedGameObjects = new List<GameObject>();

        public static List<Pergunta> perguntas = new List<Pergunta>();
        public static List<Correção> correções = new List<Correção>();
        public static bool menu = true, agradecimentoBool = false;

        static void Main()
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            Window.Init("Historia", resolution.Width, resolution.Height, true);
            IntPtr fundo = Window.LoadTexture("Asset/Fundo.png");
            IntPtr titulo = Window.LoadTexture("Asset/Titulo.png");
            IntPtr agradecimento = Window.LoadTexture("Asset/Agradecimentos.png");

            LoadGame();
            StartGame();

            SDL.SDL_Event Event;
            while (Window.running)
            {
                while (SDL.SDL_PollEvent(out Event) == 1)
                {
                    switch (Event.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            Window.CloseGame();
                            break;
                        default:
                            foreach (var obj in gameObjects)
                            {
                                obj.Input(Event);
                            }
                            break;
                    }
                }

                Window.Clear();

                int windX, windY;
                SDL.SDL_GetWindowSize(Window.window, out windX, out windY);
                Window.RenderTexExt(fundo, 0, 0, windX, windY);

                if (menu)
                {
                    int texW = 0;
                    int texH = 0;
                    SDL.SDL_QueryTexture(titulo, out _, out _, out texW, out texH);

                    Window.RenderTex(titulo, windX / 2 - texW / 2, 32);
                }

                if (agradecimentoBool)
                {
                    int texW = 0;
                    int texH = 0;
                    SDL.SDL_QueryTexture(agradecimento, out _, out _, out texW, out texH);

                    Window.RenderTex(agradecimento, windX / 2 - texW / 2, 32);

                    string tituloStr = "Sala: 2u3\n \nSoftware: Gabriel Frigo n10\n \nDesign: Davi Braga de Freitas n8\n \nTextos: Henrique Sturline Martins n15";

                    //titulo
                    SDL.SDL_Color color;
                    color.a = 0;
                    color.r = 0;
                    color.g = 0;
                    color.b = 0;

                    string[] lText = tituloStr.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    int size = 72, y = -(lText.Length - 1) * size / 2;

                    foreach (var line in lText)
                    {
                        Window.DrawTextExt(line, windX / 2, y + (texH + windY) / 2, size, color, "calibrib.ttf", true);
                        y += size;
                    }
                }

                if (perguntas.Count < correções.Count)
                {
                    correções[0].Render();
                }
                else if (perguntas.Count == correções.Count && perguntas.Count > 0)
                {
                    perguntas[0].Render();
                }

                foreach (var obj in newGameObjects)
                {
                    gameObjects.Add(obj);
                    obj.Start();
                }
                newGameObjects.Clear();

                foreach (var obj in gameObjects)
                {
                    obj.Update();
                }
                foreach (var obj in gameObjects)
                {
                    obj.Render();
                }

                foreach (var obj in deletedGameObjects)
                {
                    gameObjects.Remove(obj);
                    obj.Destroy();
                }
                deletedGameObjects.Clear();

                Window.DrawColor(135, 206, 235, 255);
                Window.Display();
            }
            SDL.SDL_DestroyTexture(fundo);
            SDL.SDL_DestroyTexture(titulo);
            SDL.SDL_DestroyTexture(agradecimento);
            Window.Quit();
        }

        static void LoadGame()
        {
            if (false)
            {
                Sound sound = Sound.CreateSound("Asset/Sound/MusicaFundo.wav");
                Window.sounds.Add("menu", sound);
                Window.sounds["menu"].SetVolume(4);
                Window.sounds["menu"].Play(-1);
            }

            Sound sound2 = Sound.CreateSound("Asset/Sound/Button.wav");
            Window.sounds.Add("button", sound2);
            Window.sounds["button"].SetVolume(4);
        }

        public static void StartGame()
        {
            menu = true;
            agradecimentoBool = false;
            deletedGameObjects.AddRange(gameObjects);
            perguntas.Clear();
            correções.Clear();

            int windX, windY;
            SDL.SDL_GetWindowSize(Window.window, out windX, out windY);

            Sprite spr = Sprite.CreateSprite("Asset/Play.png", new Vector2(990, 414), 2);
            GameObject.CreateGameObject(new Vector2((windX - 960) / 2, (windY - 384) / 2 + 128), spr, typeof(bStart));

            spr = Sprite.CreateSprite("Asset/Sair.png", new Vector2(64, 64), 2);
            GameObject.CreateGameObject(new Vector2(windX - 64, 0), spr, typeof(bClose));

            string pergunta, resposta1, resposta2, resposta3, resposta4, titulo, conteudo;

            pergunta = "1- Qual foi o território espanhol que deu origem a \nArgentina, Paraguai e Uruguai?";
            resposta1 = "Vice-Reino da Nova Espanha";
            resposta2 = "Vice-Reino do Peru";
            resposta3 = "Vice-Reino da Nova Granada";
            resposta4 = "Vice-Reino do Rio da Prata";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "O Vice-Reino do Rio da Prata era um território colonial que corresponde atualmente a \nArgentina, Paraguai, Uruguai e uma região da Bolívia";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/1.png"));

            pergunta = "2- O termo ''criollo'' se refere a que grupo social\n na América Espanhola?";
            resposta1 = "Indígenas da américa espanhola";
            resposta2 = "Europeus que vieram a colônia";
            resposta3 = "Escravos vindos da África";
            resposta4 = "Descendentes de europeus \nnascidos na colônia";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "Na américa espanhola, eram chamados de crioulos os brancos descendentes de espanhóis,\n enquanto os peninsulares ou chapetones eram os brancos nascidos na Metrópole.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/2.png"));

            pergunta = "3- Qual província foi incorporada pelo Império Português\n em 1821?";
            resposta1 = "Paraguai";
            resposta2 = "Bolívia";
            resposta3 = "Argentina";
            resposta4 = "Uruguai";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "Em 1821, o território que hoje corresponde ao Uruguai foi anexado pelo Império\n Português com o nome de Cisplatina. Essa ocupação ocorreu principalmente\n pelas riquezas minerais da região.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/3.png"));

            pergunta = "4- Qual foi o evento que deu início a Guerra de Independência da Argentina?";
            resposta1 = "Congresso de Tucumán";
            resposta2 = "Grito de Dolores";
            resposta3 = "Bloqueio Continental";
            resposta4 = "Revolução de Maio";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "A Revolução de Maio aconteceu em 1810, na Argentina. O episódio consistiu na\n proclamação de independência por meio das milícias coloniais, que depuseram \no vice-rei e estabeleceram o governo da Primeira Junta.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/4.png"));

            pergunta = "5- Qual foi o principal líder responsável pela independência\n da Argentina?";
            resposta1 = "Simón Bolívar";
            resposta2 = "Miguel Hidalgo";
            resposta3 = "Tupac Amaru II";
            resposta4 = "José de San Martín";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "José de San Martin participou ativamente do processo de independência argentino\n além de outras repúblicas da América Espanhola, como Chile e Peru";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/5.png"));

            pergunta = "6- Qual território da América Espanhola teve sua\n independência anterior aos seus vizinhos?";
            resposta1 = "Argentina";
            resposta2 = "Venezuela";
            resposta3 = "Chile";
            resposta4 = "Paraguai";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "Com a declaração de independência do Paraguai em 1811, o estado se separa do\n restante do Vice-Reino do Rio da Prata e se isola politicamente.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/6.png"));

            pergunta = "7- Que evento permitiu que os movimentos de independência\n na América Espanhola ocorressem?";
            resposta1 = "Bloqueio Continental";
            resposta2 = "Congresso de Viena";
            resposta3 = "Independência do Brasil";
            resposta4 = "Invasão napoleônica na Espanha";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "Com a invasão de Napoleão pela Europa, José Bonaparte foi nomeado Rei da Espanha,\n o que posteriormente gerou uma guerra civil no país. Esse período foi visto como uma oportunidade\n para surgirem movimentos de emancipação nas colônias espanholas.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/7.png"));

            pergunta = "8- Qual foi o general que liderou a Guerra de Independência\n do Chile?";
            resposta1 = "Miguel Hidalgo";
            resposta2 = "Toussaint-Louverture";
            resposta3 = "Simón Bolívar";
            resposta4 = "Bernardo O'Higgins";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = " Com as notícias da invasão da Espanha em 1808, a elite criolla se viu com uma oportunidade\n de tomar o poder através de uma junta de governo. Mas o fim do processo de independência apenas\n ocorreu em 1818, com Bernardo O'Higgins liderando o exército chileno e\n passando a controlar o governo do país como Diretor Supremo.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/8.png"));

            pergunta = "9- Qual foi o primeiro país a se tornar independente e abolir\n a escravidão nas américas?";
            resposta1 = "Estados Unidos";
            resposta2 = "México";
            resposta3 = "Bolívia";
            resposta4 = "Haiti";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "O Haiti foi o pioneiro na abolição da escravidão nas américas, se tornando a primeira república negra\n do mundo. A Revolução do Haiti serviu de inspiração para as outras lutas de independência\n apesar de provocar grande preocupação na elite colonial por seus ideais\n abolicionistas.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/9.png"));

            pergunta = "10- Qual era o grupo que defendia a permanência do sistema colonial?";
            resposta1 = "Patriotas";
            resposta2 = "Indígenas";
            resposta3 = "Escravos";
            resposta4 = "Realistas";
            CreatePergunta(pergunta, resposta1, resposta2, resposta3, resposta4);
            titulo = pergunta;
            conteudo = "Os realistas ou monarquistas eram aqueles que defendiam a dependência em relação à Metrópole.";
            CreateCorreção(titulo, conteudo, Window.LoadTexture("Asset/Respostas/10.png"));
        }

        static void CreatePergunta(string pergunta, string resposta1, string resposta2, string resposta3, string resposta4)
        {
            Pergunta pre = new Pergunta(pergunta);
            pre.AddOpção(resposta1, false);
            pre.AddOpção(resposta2, false);
            pre.AddOpção(resposta3, false);
            pre.AddOpção(resposta4, true);
            perguntas.Add(pre);
        }

        static void CreateCorreção(string titulo, string conteudo, IntPtr image)
        {
            Correção co = new Correção();
            co.titulo = titulo;
            co.conteudo = conteudo;
            co.image = image;
            correções.Add(co);
        }
    }
}