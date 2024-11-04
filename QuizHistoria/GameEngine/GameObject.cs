using System;
using SDL2;

namespace QuizHistoria
{
    class GameObject
    {
        public Vector2 pos;
        public Sprite spr;

        public static GameObject CreateGameObject(Vector2 pos, Sprite spr, Type type)
        {
            var obj = Activator.CreateInstance(type) as GameObject;
            obj.pos = pos;
            obj.spr = spr;

            Program.newGameObjects.Add(obj);
            return obj;
        }

        public virtual void Start()
        {

        }

        public virtual void Destroy()
        {
            spr.Delete();
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {
            Window.Render(this);
        }

        public virtual void Input(SDL.SDL_Event _event)
        {

        }
    }
}
