﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Belt_Assault
{
    class PlayerManager
    {
        public Sprite playerSprite;
        private float playerSpeed = 200.0f;
        private Rectangle playerAreaLimit;

        float angle = 0;

        public long PlayerScore = 0;
        public int LivesRemaining = 2;
        public bool Destroyed = false;

        private Vector2 gunOffset = new Vector2(25, 10);
        private float shotTimer = 0.0f;
        public float minShotTimer = 0.2f;
        private int playerRadius = 15;
        public ShotManager PlayerShotManager;

        private float PowerupTimer = 0;
        private float PowerupTime = 10000f;
        public bool PowerupOn = false;

        public PlayerManager(
            Texture2D texture,  
            Rectangle initialFrame,
            int frameCount,
            Rectangle screenBounds)
        {
            playerSprite = new Sprite(
                new Vector2(500, 500),
                texture,
                initialFrame,
                Vector2.Zero);

            PlayerShotManager = new ShotManager(
                texture,
                new Rectangle(0, 300, 5, 5),
                4,
                2,
                250f,
                screenBounds);

            playerAreaLimit =
                new Rectangle(
                    0,
                    0,
                    screenBounds.Width,
                    screenBounds.Height);

            for (int x = 1; x < frameCount; x++)
            {
                playerSprite.AddFrame(
                    new Rectangle(
                        initialFrame.X + (initialFrame.Width * x),
                        initialFrame.Y,
                        initialFrame.Width,
                        initialFrame.Height));
            }
            playerSprite.CollisionRadius = playerRadius;
        }

        private void FireShot()
        {
            if (shotTimer >= minShotTimer)
            {
                Vector2 vel = TrigHelper.AngleToVector(playerSprite.Rotation);

                PlayerShotManager.FireShot(
                    playerSprite.Center + vel * 25f,
                    vel,
                    true);
                shotTimer = 0.0f;
            }
        }


        private void HandleKeyboardInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                Vector2 vel = TrigHelper.AngleToVector(playerSprite.Rotation);

                playerSprite.Velocity += vel;
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                Vector2 vel = TrigHelper.AngleToVector(playerSprite.Rotation);

                playerSprite.Velocity += -vel;
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                Vector2 vel = TrigHelper.AngleToVector(playerSprite.Rotation - MathHelper.PiOver2);

                playerSprite.Velocity = vel;
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                Vector2 vel = TrigHelper.AngleToVector(playerSprite.Rotation + MathHelper.PiOver2);

                playerSprite.Velocity = vel;
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                FireShot();
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                angle += -.05f;
                playerSprite.Rotation = angle;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                angle += .05f;
                playerSprite.Rotation = angle;
            }
            
        }

        private void HandleGamepadInput(GamePadState gamePadState)
        {
            playerSprite.Velocity +=
                new Vector2(
                    gamePadState.ThumbSticks.Left.X,
                    -gamePadState.ThumbSticks.Left.Y);

            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                FireShot();
            }
        }

        private void imposeMovementLimits()
        {
            Vector2 location = playerSprite.Location;

            if (location.X < playerAreaLimit.X)
                location.X = playerAreaLimit.X;

            if (location.X >
                (playerAreaLimit.Right - playerSprite.Source.Width))
                location.X =
                    (playerAreaLimit.Right - playerSprite.Source.Width);

            if (location.Y < playerAreaLimit.Y)
                location.Y = playerAreaLimit.Y;

            if (location.Y >
                (playerAreaLimit.Bottom - playerSprite.Source.Height))
                location.Y =
                    (playerAreaLimit.Bottom - playerSprite.Source.Height);

            playerSprite.Location = location;
        }

        public void Update(GameTime gameTime)
        {
            PlayerShotManager.Update(gameTime);

            if (!Destroyed)
            {
                playerSprite.Velocity = Vector2.Zero;

                shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (PowerupOn)
                {
                    PowerupTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    if (PowerupTimer > PowerupTime)
                    {
                        PowerupTimer = 0;
                        minShotTimer = 0.2f;
                        PowerupOn = false;
                    }
                }

                HandleKeyboardInput(Keyboard.GetState());
                HandleGamepadInput(GamePad.GetState(PlayerIndex.One));

                playerSprite.Velocity.Normalize();
                playerSprite.Velocity *= playerSpeed;

                playerSprite.Update(gameTime);
                imposeMovementLimits();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerShotManager.Draw(spriteBatch);

            if (!Destroyed)
            {
                playerSprite.Draw(spriteBatch);
            }
        }

    }
}
