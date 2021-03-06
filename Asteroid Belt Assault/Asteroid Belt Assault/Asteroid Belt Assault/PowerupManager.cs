﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroid_Belt_Assault
{
    class Powerup : Sprite
    {

        public float activeTimer;
        public bool Stop = false;

        public Powerup(
          Vector2 location,
          Texture2D texture,
          Rectangle initialFrame,
          Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            activeTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (activeTimer >= 10000)
            {
                location = new Vector2(-500, -500);
                Stop = true;
            }
        }
    }

    class PowerupManager
    {
        Texture2D spriteSheet;
        private PlayerManager playerManager;
        public List<Powerup> Powerups = new List<Powerup>();
        public bool spawn = true;

        public PowerupManager(Texture2D spriteSheet, PlayerManager playerManager)
        {
            this.spriteSheet = spriteSheet;
            this.playerManager = playerManager;  
        }
        //1- rapid fire
        //2- extra life
        public void SpawnPowerup(int powerup)
        {
            if (powerup == 1)
                Powerups.Add(new Powerup(new Vector2(200, 300), spriteSheet, new Rectangle(382, 194, 45, 50), Vector2.Zero));
            if (powerup == 2)
                Powerups.Add(new Powerup(new Vector2(500, 100), spriteSheet, new Rectangle(470, 126, 55, 75), Vector2.Zero));
        }

        public void Update(GameTime gameTime)
        {
            for (int i = Powerups.Count - 1; i >= 0; i--)
                Powerups[i].Update(gameTime);


            if (playerManager.PlayerScore >= 1000 && spawn)
            {
                SpawnPowerup(1);
                spawn = false;

            }
            if (playerManager.PlayerScore >= 1000 && spawn)
            {
                SpawnPowerup(2);
                spawn = false;

            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = Powerups.Count - 1; i >= 0; i--)
                Powerups[i].Draw(spriteBatch);
        }
    }
    
}


