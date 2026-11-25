using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nestad_Athor.Items;
using System.Collections.Generic;

//Hi

namespace Nestad_Athor
{
    public class Game1 : Game
    {
        //Level variables
        public int Level = 0;

        Tears tears = new Tears(0, 0);
        Player player = new Player();
        Damage_up_items damage = new Damage_up_items();

        TumorBoss tumor = new TumorBoss();

        Spitters spitter = new Spitters(1000, 400);
        
        CancerousCell cancerousCell = new CancerousCell(1200, 400, 200);
        CancerousCell cancerousCell2 = new CancerousCell(1000, 400, 200);
        CancerousCell cancerousCell3 = new CancerousCell(1400, 400, 300);
        CancerousCell cancerousCell4 = new CancerousCell(20, 20, 300);
        CancerousCell cancerousCell5 = new CancerousCell(1800, 400, 500);
        CancerousCell cancerousCell6 = new CancerousCell(1400, 400, 300);

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteBatch spriteBatch;
        Texture2D LevelWalls;
        public Texture2D tumorBossTexture;
        Texture2D playerTexture;
        public Texture2D cancerousCellTexture;
        Texture2D Door;
        Texture2D loadingScreenTexture;
        Texture2D PlayerAttackTexture;
        Texture2D PlayerHitboxTexture;
        public Texture2D SpittersTexture;
        public Texture2D TearsTexture;
        public Texture2D HealthBarTexture;
        public Texture2D EnemyHealthBarTexture;
        public SpriteFont StartScreenText;
        public Texture2D chunkofflesh;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowAltF4 = true;

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

            cancerousCellTexture = Content.Load<Texture2D>("BadGuys//CancerousCell");

            LevelWalls = Content.Load<Texture2D>("Assets//Walls");

            Door = new Texture2D(GraphicsDevice, 1, 1);
            Door.SetData(new[] { Color.White });

            PlayerAttackTexture = new Texture2D(GraphicsDevice, 1, 1);
            PlayerAttackTexture.SetData(new[] { Color.White });

            loadingScreenTexture = Content.Load<Texture2D>("LoadingScreen//LoadingScreen");

            HealthBarTexture = new Texture2D(GraphicsDevice, 1, 1);
            HealthBarTexture.SetData(new[] { Color.White });

            EnemyHealthBarTexture = new Texture2D(GraphicsDevice, 1, 1);
            EnemyHealthBarTexture.SetData(new[] { Color.White });

            TearsTexture = new Texture2D(GraphicsDevice, 1, 1);
            TearsTexture.SetData(new[] { Color.White });

            SpittersTexture = new Texture2D(GraphicsDevice, 1, 1);
            SpittersTexture.SetData(new[] { Color.White });

            chunkofflesh = new Texture2D(GraphicsDevice, 1, 1);
            chunkofflesh.SetData(new[] { Color.White });

            tumorBossTexture = new Texture2D(GraphicsDevice, 1, 1);
            tumorBossTexture.SetData(new[] { Color.White });


            StartScreenText = Content.Load<SpriteFont>("Text//StartScreenText");

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            KeyboardState keyboardState = Keyboard.GetState();

            LoadingScreen.LoadingScreenUpdate(this, gameTime);

            //Debug switching between levels using NumPad keys
            if (keyboardState.IsKeyDown(Keys.NumPad1))
            {
                Level = 1;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad2))
            {
                Level = 2;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad3))
            {
                Level = 3;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad4))
            {
                Level = 4;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad0))
            {
                Level = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad5))
            {
                Level = 5;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad6))
            {
                Level = 8;
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
                case 3:
                    Level3.Level3Update(this, gameTime, player, cancerousCell4, cancerousCell5, cancerousCell6);
                    break;
                case 4:
                    Level4.Level4Update(this, gameTime, player, cancerousCell, spitter, tears);
                    break;
                case 5:
                    Level5.Level5Update(this, gameTime, player, cancerousCell, damage);
                    break;
                case 6:
                    Level6.Level6Update(this, gameTime, player, cancerousCell, damage);
                    break;
                case 7:
                    Level7.Level7Update(this, gameTime, player, cancerousCell, damage);
                    break;
                case 8:
                    Level8.Level8Update(this, gameTime, player, cancerousCell, damage, tumor);
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
                    Level1.Level1Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture, PlayerAttackTexture, Door);
                    break;
                case 2:
                    Level2.Level2Drawn(this, spriteBatch, playerTexture, PlayerAttackTexture, LevelWalls, player, cancerousCell2, cancerousCell3, cancerousCellTexture, 
                        PlayerHitboxTexture, Door);
                    break;
                case 3:
                    Level3.Level3Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell4, cancerousCell5, cancerousCell6, cancerousCellTexture, PlayerAttackTexture,
                        Door);
                    break;
                case 4:
                    Level4.Level4Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, PlayerAttackTexture, tears, spitter, Door);
                        break;
                case 5:
                    Level5.Level5Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture, PlayerAttackTexture, damage, Door);
                    break;
                case 6:
                    Level6.Level6Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture, PlayerAttackTexture, damage, Door);
                    break;
                case 7:
                    Level7.Level7Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture, PlayerAttackTexture, damage, Door);
                    break;
                case 8:
                    Level8.Level8Drawn(this, spriteBatch, playerTexture, LevelWalls, player, cancerousCell, cancerousCellTexture, PlayerAttackTexture, damage, Door, tumorBossTexture, tumor);
                    break;
            }
            

            base.Draw(gameTime);
        }
    }
}
