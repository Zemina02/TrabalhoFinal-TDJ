using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TiledSharp;
using Apos.Gui;
using FontStashSharp;


namespace nd_trabalho.src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RenderTarget2D renderTarget;

        public static float screenWidth;
        public static float screenHeigth;

        #region Managers
        private GameManager gameManager;
        private bool gameOver = false;
        #endregion

        #region Tilemaps
        private TmxMap map;
        private TilemapManager tilemapManager;
        private TilemapManager propmapManager;
        private Texture2D tileset;
        private Texture2D props;
        private List<Rectangle> collisionsRects;
        private Rectangle startRect;
        private Rectangle endRect;
        #endregion


        #region Player
        private Player player;
        public static Texture2D pixel;
        private int points = 0;
        private int health = 10;
        private int timeEntHurt = 5;
        private int hit_counter = 0;

        #endregion

        #region Enemy
        private Enemy inimigo1;
        private Enemy inimigo2;
        private List<Enemy> inimigos;
        private List<Rectangle> inimigosPath;
        #endregion

        #region Camara
        private Camara camara;
        private Matrix transformMatrix;
        #endregion

        #region Coins
        private List<Coins> coins;
        private List<Rectangle> moedas;
        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 800; //definição da altura
            _graphics.PreferredBackBufferWidth = 1024;//definição da largura
            _graphics.ApplyChanges();

            screenHeigth = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;
            //var windowSize = new Vector2(screenWidth, screenHeigth);
            //var mapSize = new Vector2(64,42);//tamanho do tile map
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Tilemap
            map = new TmxMap("Content\\nivel\\level1.tmx");


            tileset = Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            props = Content.Load<Texture2D>(map.Tilesets[1].Name.ToString());

            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeigth = map.Tilesets[0].TileHeight;
            int tilesetTileWidth = tileset.Width / tileWidth;

            int propSetWidth = map.Tilesets[1].TileWidth;
            int propSetHeigth = map.Tilesets[1].TileHeight;
            int propSetTileWidth = props.Width / propSetWidth;
            int propSetTileHeigth = props.Height / propSetHeigth;


            tilemapManager = new TilemapManager(map, tileset, tilesetTileWidth, tilesetTileWidth , tileWidth, tileHeigth,0);

           propmapManager = new TilemapManager(map, props, propSetTileWidth, propSetTileHeigth, propSetWidth, propSetHeigth, 1);

            #endregion

            #region Colisoens
            collisionsRects = new List<Rectangle>();

            foreach (var o in map.ObjectGroups["colisions"].Objects) 
            {
                if(o.Name == "") 
                {
                    collisionsRects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));

                }
                if(o.Name == "SP")
                {
                    startRect = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
                if (o.Name == "EP") 
                {
                    endRect = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);//falta adicionar mais niveis ou um EXIT
                }
            }
            #endregion

            #region GameManager

            gameManager = new GameManager(endRect);

            #endregion

            #region EnemiesAndPaths
            inimigosPath = new List<Rectangle>();

            foreach(var o in map.ObjectGroups["EnemyPaths"].Objects)
            {
                inimigosPath.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }
            
            inimigos = new List<Enemy>();
            inimigo1 = new Enemy(Content.Load<Texture2D>("inimigo2"), inimigosPath[0]);
            inimigo2 = new Enemy(Content.Load<Texture2D>("inimigo2"), inimigosPath[1]);

            inimigos.Add(inimigo1);
            inimigos.Add(inimigo2);

            #endregion

            #region TOOLS
            //caixa para hiboxes
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            #endregion

            #region Player
            player = new Player(
               new Vector2(startRect.X, startRect.Y),
               Content.Load<Texture2D>("defouth_pose"),
               Content.Load<Texture2D>("defouth_correr"),
               Content.Load<Texture2D>("defouth_salto"),
               Content.Load<Texture2D>("defouth_FALL"),
               Content.Load<Texture2D>("attack1"),
               pixel
               );
            #endregion

            #region Camara

            camara = new Camara();

            #endregion

            #region Coins
            coins = new List<Coins>();
            moedas = new List<Rectangle>();

            foreach(var o in map.ObjectGroups["Coins"].Objects)
            {
                moedas.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            for (int i = 0; i < moedas.Count; i++)
            {
                coins.Add(new Coins(Content.Load<Texture2D>("Coins"), new Vector2(moedas[i].X - 13, moedas[i].Y - 10)));
            }


            #endregion

            renderTarget = new RenderTarget2D(GraphicsDevice, 1024, 800);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            #region enimigos
            foreach (var ini in inimigos)
            {
                ini.Update();

                if (ini.Hit(player.hitbox)) 
                {
                    hit_counter++;
                    if (hit_counter > timeEntHurt) 
                    {
                        Console.WriteLine($"health:{health}");
                        health--;
                        hit_counter = 0;
                    }
                }

            }
            #endregion

            #region Camara
            transformMatrix = camara.Follow(player.hitbox);
            #endregion

            #region GameManager

            if (gameManager.hasGameEnded(player.hitbox))
            {
                Console.WriteLine("Acabou Playboy");
                gameOver = true;
            }
            if (health == 0) 
            {
                Console.WriteLine("Acabou Playboy");
                gameOver = true;

            }
            #endregion

            #region Player Collisions

            player.Update();
            var initPos = player.position;

            //  y 
            foreach (var rect in collisionsRects) 
            {
                if (!player.isJumping && !rect.Intersects(player.playerFallRect)) 
                {
                    player.isFalling = true;
                    // Console.WriteLine("fall");
                }


                if (rect.Intersects(player.playerFallRect) && !player.isJumping && player.playerFallRect.Contains(player.playerFallRect.X + 8, rect.Top))
                {
                    //Console.WriteLine("true");
                    player.isFalling = false;
                    player.position.Y = rect.Top - player.hitbox.Height;
                    player.velocity.Y = 0;
                    
                    break;
                }

                if (rect.Contains(player.hitbox.X + 12, player.hitbox.Top))
                {
                    player.velocity.Y = 0;
                    player.isJumping = false;
                    player.isFalling = true;
                }


            }

            // X
            foreach (var rect in collisionsRects) 
            {


                if (rect.Intersects(player.hitbox) && player.velocity.X < 0 && rect.Contains(player.playerFallRect.Left,player.playerFallRect.Y))    
                {
                    player.position.X += 2;
                   player.velocity.X = 0;
                    break;
                }

                if (rect.Intersects(player.hitbox) && player.velocity.X > 0 && rect.Contains(player.playerFallRect.Right, player.playerFallRect.Y))
                {
                    player.position.X -= 2;
                    player.velocity.X = 0;
                    break;
                }


            }

            #endregion

            #region Coins

            for (int i = coins.Count - 1; i >= 0; i--)
            {
                if (player.hitbox.Contains(coins[i].position)) // ou coins[i].position, dependendo da sua classe
                {
                    coins.RemoveAt(i);
                    points++;
                }
            }


            #endregion

            DrawLevel(gameTime);

            base.Update(gameTime);
        }
        
        public void DrawLevel(GameTime gameTime) 
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            tilemapManager.Draw(_spriteBatch);
            propmapManager.Draw(_spriteBatch);

            #region inimigos
            foreach (var enemy in inimigos)
            {
                enemy.Draw(_spriteBatch, gameTime);
            }
            #endregion

            #region Coins
            foreach (var coins in coins)
            {
                coins.Draw(_spriteBatch, gameTime);
            }
            #endregion

            player.Draw(_spriteBatch, gameTime);


            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(renderTarget,new Vector2(0,0), null, Color.White,0f,new Vector2(),1.5f,SpriteEffects.None,0);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
