using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace nd_trabalho.src
{
    public class Player : Entity
    {
        public Rectangle playerFallRect;
        public SpriteEffects effects;


        public float playerSpeed =(float) 1.7;
        public float fallSpeed = (float) 0.5;
        public float jumpSpeed = -14;//QUndto menor mais rapido
        public float jumpHeigth = 0;//quanto maior mais o tamanho do salto


        public float startY;
        public bool isFalling = true;
        public bool isJumping;

        public Rectangle attack1;

        public Animations[] playerAnimations;
        public currentAnimation playerAnimationController;

        Texture2D pixelBruh;


        public Player(Vector2 position,Texture2D idleSprite, Texture2D runSprite, Texture2D jumpSprite, Texture2D fallingSprite, Texture2D attackSprite, Texture2D pixel)
        {
            pixelBruh = pixel;
            playerAnimations = new Animations[5];

            this.position = position;
            velocity = new Vector2();

            playerAnimations[0] = new Animations(idleSprite);
            playerAnimations[1] = new Animations(runSprite);
            playerAnimations[2] = new Animations(jumpSprite);
            playerAnimations[3] = new Animations(fallingSprite);
            playerAnimations[4] = new Animations(attackSprite);

            hitbox = new Rectangle((int)position.X, (int)position.Y, 25, 32);
            playerFallRect = new Rectangle((int)position.X, (int)position.Y, 16,10);

            attack1 = new Rectangle(hitbox.X + hitbox.Width, hitbox.Y, 32, 32);

        }
        public override void Update()//MOVIMENTO DO PLAYER
        {
            KeyboardState keyboard = Keyboard.GetState();

            playerAnimationController = currentAnimation.Idle;
            

            if (isFalling)
            {
                velocity.Y += fallSpeed;
                playerAnimationController = currentAnimation.Falling;
                if (keyboard.IsKeyDown(Keys.A)) { effects = SpriteEffects.FlipHorizontally; }
                else { effects = SpriteEffects.None; }
            }

            position += velocity;
            
            Move(keyboard);
            Jump(keyboard);

            hitbox.X = (int)position.X + 4;
            hitbox.Y = (int)position.Y;

            attack1.Width = 0;
            attack1.Height = 0;

            Attack1(keyboard);

            playerFallRect.X = (int)position.X + 8;
            playerFallRect.Y = (int)(position.Y + 30);

        }


        private void Move(KeyboardState keyboard)
        {

            if (keyboard.IsKeyDown(Keys.A))
            {
                if (velocity.X > (float)-1.5)
                {
                    velocity.X -= playerSpeed;
                }
                if (!isFalling && !isJumping)
                {
                    playerAnimationController = currentAnimation.Run;
                    effects = SpriteEffects.FlipHorizontally;
                }
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                if (velocity.X < (float)1.5)
                {
                    velocity.X += playerSpeed;
                }
                if (!isFalling && !isJumping)
                {
                    playerAnimationController = currentAnimation.Run;
                    effects = SpriteEffects.None;
                }
            }

            if (keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.D))
            {
                velocity.X = 0;
            }
        }

        private void Jump(KeyboardState keyboard)
        {
            if (isJumping)
            {
                velocity.Y += 1f; // gravidade

                if (velocity.Y >= jumpHeigth)
                {
                    isJumping = false;
                    isFalling = true;
                }

                playerAnimationController = currentAnimation.Jump;
                if (keyboard.IsKeyDown(Keys.A)) { effects = SpriteEffects.FlipHorizontally; }
                else { effects = SpriteEffects.None; }
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.Space) && !isFalling)
                {
                    isJumping = true;
                    isFalling = false;
                    velocity.Y = jumpSpeed; // impulso inicial do salto
                }
            }
        }

        private void Attack1(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.J) && !isFalling && !isJumping && keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.D))
            {

                playerAnimationController = currentAnimation.Attacking;
                if (effects == SpriteEffects.FlipHorizontally) 
                {
                    attack1 = new Rectangle(hitbox.X - 15, hitbox.Y, 15, 32);
                }
                else
                {
                    attack1 = new Rectangle(hitbox.X + hitbox.Width, hitbox.Y, 15, 32);
                }
                
            }

        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)//desenha o jogador na tela
        {

            // Topo
            spriteBatch.Draw(pixelBruh, new Rectangle(hitbox.X, hitbox.Y, hitbox.Width, 1), Color.Red);
            // Fundo
            spriteBatch.Draw(pixelBruh, new Rectangle(hitbox.X, hitbox.Y + hitbox.Height - 1, hitbox.Width, 1), Color.Red);
            // Esquerda
            spriteBatch.Draw(pixelBruh, new Rectangle(hitbox.X, hitbox.Y, 1, hitbox.Height), Color.Red);
            // Direita
            spriteBatch.Draw(pixelBruh, new Rectangle(hitbox.X + hitbox.Width - 1, hitbox.Y, 1, hitbox.Height), Color.Red);

            // Topo
            spriteBatch.Draw(pixelBruh, new Rectangle(playerFallRect.X, playerFallRect.Y, playerFallRect.Width, 1), Color.Red);
            // Fundo
            spriteBatch.Draw(pixelBruh, new Rectangle(playerFallRect.X, playerFallRect.Y + playerFallRect.Height - 1, playerFallRect.Width, 1), Color.Red);
            // Esquerda
            spriteBatch.Draw(pixelBruh, new Rectangle(playerFallRect.X, playerFallRect.Y, 1, playerFallRect.Height), Color.Red);
            // Direita
            spriteBatch.Draw(pixelBruh, new Rectangle(playerFallRect.X + playerFallRect.Width - 1, playerFallRect.Y, 1, playerFallRect.Height), Color.Red);


            // Topo
            spriteBatch.Draw(pixelBruh, new Rectangle(attack1.X, attack1.Y, attack1.Width, 1), Color.Red);
            // Fundo
            spriteBatch.Draw(pixelBruh, new Rectangle(attack1.X, attack1.Y + attack1.Height - 1, attack1.Width, 1), Color.Red);
            // Esquerda
            spriteBatch.Draw(pixelBruh, new Rectangle(attack1.X, attack1.Y, 1, attack1.Height), Color.Red);
            // Direita
            spriteBatch.Draw(pixelBruh, new Rectangle(attack1.X + attack1.Width - 1, attack1.Y, 1, attack1.Height), Color.Red);



            switch (playerAnimationController)
            {
                case currentAnimation.Idle:

                    playerAnimations[0].Draw(spriteBatch, position, gameTime, effects, 150 );

                    break;

                case currentAnimation.Run:

                    playerAnimations[1].Draw(spriteBatch, position, gameTime, effects,100 );

                    break;

                case currentAnimation.Jump:

                    playerAnimations[2].Draw(spriteBatch,position,gameTime, effects, 100);

                    break;

                case currentAnimation.Falling:

                    playerAnimations[3].Draw(spriteBatch,position,gameTime, effects, 500);

                    break;

                case currentAnimation.Attacking:


                    playerAnimations[4].Draw(spriteBatch,position,gameTime, effects, 100);

                break;

                default:
                    break;
            }
        }
    }
}
