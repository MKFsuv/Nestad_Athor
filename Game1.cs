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
        CancerousCell cancerousCell = new CancerousCell(1200, 400, 200);
        CancerousCell cancerousCell2 = new CancerousCell(1000, 400, 200);
        CancerousCell cancerousCell3 = new CancerousCell(1400, 400, 300);
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteBatch spriteBatch;
        Texture2D LevelWalls;
        Texture2D playerTexture;
        Texture2D cancerousCellTexture;
        Texture2D Door;
        Texture2D loadingScreenTexture;
        Texture2D PlayerAttackTexture;
        Texture2D PlayerHitboxTexture;


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

            PlayerAttackTexture = new Texture2D(GraphicsDevice, 1, 1);
            PlayerAttackTexture.SetData(new[] { Color.White });

            loadingScreenTexture = Content.Load<Texture2D>("LoadingScreen//LoadingScreen");



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
            else if (keyboardState.IsKeyDown(Keys.NumPad0))
            {
                Level = 0;
            }

            // Call the appropriate level update method based on the current level
            switch (Level)
            {
                case 1:
                    Level1.Level1Update(this, gameTime, player, cancerousCell);
                    break;
                case 2:
                    Level2.Level2Update(this, gameTime, player, cancerousCell2, cancerousCell3);
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
                case 0:
                    LoadingScreen.LoadingScreenDrawn(this, spriteBatch, loadingScreenTexture);
                    break;
                case 1:
                    Level1.Level1Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture, PlayerAttackTexture);
                    break;
                case 2:
                    Level2.Level2Drawn(this, spriteBatch, playerTexture, PlayerAttackTexture, LevelWalls, player, cancerousCell2, cancerousCell3, cancerousCellTexture, 
                        PlayerHitboxTexture);
                    break;
            }


            base.Draw(gameTime);
        }
    }
}
