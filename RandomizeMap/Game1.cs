using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RandomizeMap
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        //Art assests
        Texture2D lava, floor, exit, playerStart, player;

        //Random map elements
        public int[,] mapGenerator;
        
        //Position based
        Vector2 playerPosition;

        public Game1() : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            GenerateMap();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            lava = Content.Load<Texture2D>("50x50lava");
            floor = Content.Load<Texture2D>("50x50gray");
            player = Content.Load<Texture2D>("50x50player");
            exit = Content.Load<Texture2D>("50x50exit");
            playerStart = Content.Load<Texture2D>("50x50start");
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

             _spriteBatch.Begin();

            Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void Draw()
        {
           //_spriteBatch.Draw(floor, new Vector2(150, 50), Color.White);  //repalce with GenerateMap()
            DrawMap();
            _spriteBatch.Draw(player, new Vector2(150, 50), Color.White);   
        }
        public void DrawMap()
        {   
            var tilePositionX = 150;
            var x = 0;
            for (int iX = 1; iX <= 10; iX++)
            {
                var tilePositionY = 50;
                var y = 0;
                for (int iY = 1; iY <= 10; iY++)
                {
                    if (mapGenerator[x, y] <= 80)
                    {
                        _spriteBatch.Draw(floor, new Vector2(tilePositionX, tilePositionY), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(lava, new Vector2(tilePositionX, tilePositionY), Color.White);
                    }
                    tilePositionY += 50;
                    y++;
                }
            tilePositionX += 50;
            x++;
            }
        }
        public void GenerateMap()
        {
            //NOTE array index starts at 0!
            Random _r = new Random();
            mapGenerator = new int[10, 10];

            //Create a random array; used for parsing for map tile generation.
            var x = 0;
            for (int iX = 1; iX <= 10; iX++)
            {
                var y = 0;
                for (int iY = 1; iY <= 10; iY++)
                {
                    int _random = _r.Next(100);
                    mapGenerator[x, y] = _random;
                    y++;
                }
                x++;                    
            }
        }
    }
}
