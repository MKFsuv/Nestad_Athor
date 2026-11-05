using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Nestad_Athor
{
    public class Game1 : Game
    {
        //Level variables
        public int Level = 1;


        Player player = new Player();
        CancerousCell cancerousCell = new CancerousCell();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteBatch spriteBatch;
        Texture2D LevelWalls;
        Texture2D playerTexture;
        Texture2D cancerousCellTexture;
        Texture2D Door;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1800;
            _graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // List of the creating of Textures
            playerTexture = new Texture2D(GraphicsDevice, 1, 1);
            playerTexture.SetData(new[] { Color.White });

            cancerousCellTexture = new Texture2D(GraphicsDevice, 1, 1);
            cancerousCellTexture.SetData(new[] { Color.White });

            LevelWalls = new Texture2D(GraphicsDevice, 1, 1);
            LevelWalls.SetData(new[] { Color.White });

            Door = new Texture2D(GraphicsDevice, 1, 1);
            Door.SetData(new[] { Color.White });



            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            //Debug switching between levels using NumPad keys
            if (keyboardState.IsKeyDown(Keys.NumPad1))
            {
                Level = 1;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad2))
            {
                Level = 2;
            }

            // Call the appropriate level update method based on the current level
            switch (Level)
            {
                case 1:
                    Level1.Level1Update(gameTime, player, cancerousCell);
                    break;
                case 2:
                    Level2.Level2Update(gameTime, player, cancerousCell);
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            // Call the appropriate level draw method based on the current level
            switch (Level)
            {
                case 1:
                    Level1.Level1Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture);
                    break;
                case 2:
                    Level2.Level2Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture);
                    break;
            }



            base.Draw(gameTime);
        }
    }
}
