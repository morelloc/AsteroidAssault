using System;
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
            if (activeTimer >= 10000)
            {
                Stop = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            activeTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }

    class PowerupManager
    {
        Texture2D spriteSheet;
        private PlayerManager playerManager;
        public List<Powerup> Powerups = new List<Powerup>();

        public PowerupManager(Texture2D spriteSheet, PlayerManager playerManager)
        {
            this.spriteSheet = spriteSheet;
            this.playerManager = playerManager;

            
            /*if (playerManager.PlayerScore = 3000)
            {
                SpawnPowerup();
            }*/

            SpawnPowerup();
        }

        public void SpawnPowerup()
        {
            Powerups.Add(new Powerup(new Vector2(300, 300), spriteSheet, new Rectangle(379, 202, 55, 55), Vector2.Zero));
        }

        public void Update(GameTime gameTime)
        {
            for (int i = Powerups.Count - 1; i >= 0; i--)
                Powerups[i].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = Powerups.Count - 1; i >= 0; i--)
                Powerups[i].Draw(spriteBatch);
        }
    }
    
}


