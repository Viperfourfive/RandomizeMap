using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace RandomizeMap
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        //Art assests
        Texture2D lava, floor, exit, playerStart, player;
        SpriteFont font;

        //Random map elements
        public int[,] mapGenerator;
        Random _r;
        
        //Position based
        Input _input = new Input();
        Vector2 boardStart;
        Rectangle boardSize;
        Vector2 playerPosition;
        int xP = 0;
        int yP = 0;
        int startTile, exitTile;

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
            boardStart = new Vector2(150, 50);
            boardSize = new Rectangle((int)boardStart.X, (int)boardStart.Y, 500, 500);

            _r = new Random();
            PlaceStart();
            playerPosition = new Vector2(startTile, 50);
            PlaceExit();
        

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
            font = Content.Load<SpriteFont>("Template");
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playerPosition = _input.PlayerMovement(playerPosition, boardSize);
            

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

             _spriteBatch.Begin();
            Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public void Draw()
        {
            DrawMap();
            _spriteBatch.Draw(player, playerPosition, Color.White);
            DebugHUD();
        }
        public void DebugHUD()
        {
            _spriteBatch.DrawString(font, "x: " + playerPosition.X, new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(font, "y: " + playerPosition.Y, new Vector2(10, 40), Color.White);
            _spriteBatch.DrawString(font, "["+(xP+1)+","+(yP+1)+"]: " + mapGenerator[xP, yP], new Vector2(10, 550), Color.White);
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
            //Draw START
            _spriteBatch.Draw(playerStart, new Vector2(startTile, 50), Color.White);
            //Draw EXIT
            _spriteBatch.Draw(exit, new Vector2(exitTile, 500),Color.White);
        }
        public int PlaceStart()  //Creates start ALWAYS on top row, but random column.
        {
            do
            {
                startTile = _r.Next(0, 9);
            }
            while (mapGenerator[9, startTile] <= 75);

            return startTile = (startTile * 50) + 150;
        }
        public int PlaceExit()  //Creates exit ALWAYS on bottom row, but random column.
        {
            do
            {
                exitTile = _r.Next(0, 9);
            }
            while(mapGenerator[9,exitTile] <= 75 && exitTile != startTile);

            return exitTile = (exitTile * 50) + 150;
        }

        //  OBSOLETE since adding PlaceExit() && PlaceStart()
        //public void PlacePlayer() //starting place for the player on launch
        //{
        //    do
        //    {
        //        //Check for lava before placing the player
        //        Random _r = new Random();
        //        do
        //        {
        //            xP = _r.Next(0, 9);
        //            yP = _r.Next(0, 9);
        //        }
        //        while (mapGenerator[xP, yP] != 1);

        //        //Assign to grid pixels
        //        if (xP == 0)
        //        {
        //            xP = 3;
        //        }
        //        if (yP == 0)
        //        {
        //            yP = 1;
        //        }
        //        playerPosition.X = xP * 50;
        //        playerPosition.X += 150;
        //        playerPosition.Y = yP * 50;
        //        playerPosition.Y += 50;
        //    }
        //    //verify that position is inside game board.
        //    while (playerPosition.X <= 149 || playerPosition.X >= 651 && playerPosition.Y <= 99 || playerPosition.Y >= 501);
        //}
        public int[,] GenerateMap()
        {
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
            return mapGenerator;
        }
    }
}
