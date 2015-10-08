using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KineticCamp
{
    public class PlayerManager
    {
        private Entity Player_Entity;
        private DisplayBar HealthBar;
        //private DisplayBar ManaBar; 
        private int Health;
        private int Mana;
        private int Total_XP;
        private int Current_XP;
        private const int Total_Health = 100;
        private int HealthCoolDown;

        #region Constructors
        public PlayerManager(Entity Player_Entity, int Health, int Mana, int Total_XP, int Current_XP, DisplayBar HealthBar)
        {
            this.Player_Entity = Player_Entity;
            this.Health = Health;
            this.Mana = Mana;
            this.Total_XP = Total_XP;
            this.Current_XP = Current_XP; 
            this.HealthBar = HealthBar;
            HealthCoolDown = 0; 
        }

        public PlayerManager(Entity Player_Entity, DisplayBar HealthBar)
        {
            this.Player_Entity = Player_Entity;
            this.Health = 100;
            this.Mana = 100; 
            this.Total_XP = 0;
            this.Current_XP = 0;
            this.HealthBar = HealthBar;
            HealthCoolDown = 0; 
        }
        #endregion

        #region get Methods

        public Entity getPlayerEntity()
        {
            return Player_Entity;
        }

        public int getHealth()
        {
            return Health;
        }

        public int getMana()
        {
            return Mana;
        }

        public int getTotalExperience()
        {
            return Total_XP;
        }

        public int getCurrentExperience()
        {
            return Current_XP;
        }

        public DisplayBar getHealthBar()
        {
            return HealthBar; 
        }

        public int getHealthCoolDown()
        {
            return HealthCoolDown; 
        }

        #endregion

        #region set Methods

        public void setPlayerEntity(Entity Player_Entity)
        {
            this.Player_Entity = Player_Entity;
        } 

        public void setHealth(int Health)
        {
            this.Health = Health; 
        }

        public void setMana(int Mana)
        {
            this.Mana = Mana; 
        }

        public void setTotalExperience(int Total_XP)
        {
            this.Total_XP = Total_XP;
        }

        public void setCurrentExperience(int Current_XP)
        {
            this.Current_XP = Current_XP;
        }
        
        public void setHealthBar(DisplayBar HealthBar)
        {
            this.HealthBar = HealthBar; 
        }

        #endregion

        #region Player Management

        public void damagePlayer(int damage)
        {
            this.Health = this.Health - damage;
            double HealthBar_Ratio = ((double)200 / (double)Total_Health);  
            Rectangle newBar = HealthBar.getDisplayBar();
            newBar.Width = (int) (newBar.Width-((int)(damage*HealthBar_Ratio)));
            this.HealthBar.setDisplayBar(newBar);
        }

        public void healthRegen()
        {
            if(this.Health<Total_Health)
            {
                this.Health += 1;
                double HealthBar_Ratio = ((double)200 / (double)Total_Health);
                Rectangle newBar = HealthBar.getDisplayBar();
                newBar.Width = (int)(newBar.Width + ((int)(1 * HealthBar_Ratio)));
                this.HealthBar.setDisplayBar(newBar);
            }
        }

        public void updateHealthCooldown()
        {
            HealthCoolDown += 1;
            if (HealthCoolDown > 35)
            {
                HealthCoolDown = 0;
            }
        }



        #endregion
    }
}
