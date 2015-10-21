using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;

namespace KineticCamp
{
    public class DeathManager
    {
        private InputManager inputManager;
        //Player Values

        private Level level; 
       
        private Vector2 PlayerLocation;
        private int PlayerTotalExp;
        private int PlayerCurrentExp;
        private int PlayerHealth;
        private int PlayerMana;
        private int PlayerTotalMana;
        //Level Values
        private int levelMode;
        private List<Npc> Npcs;
        private List<Vector2> NpcLocations; 
        private List<Vector2> ObjectLocations;
        private List<bool> DoorUnlocked;

        public DeathManager(InputManager inputManager)
        {
            this.inputManager = inputManager;
            PlayerLocation = inputManager.getPlayer().getLocation();
            PlayerTotalExp = inputManager.getPlayerManager().getTotalExperience();
            PlayerCurrentExp = inputManager.getPlayerManager().getCurrentExperience();
            PlayerHealth = inputManager.getPlayerManager().getHealth();
            PlayerMana = inputManager.getPlayerManager().getMana();
            PlayerTotalMana = inputManager.getPlayerManager().getTotalMana();

            levelMode = inputManager.getLevel().getMode();
            Npcs = new List<Npc>();
            NpcLocations = new List<Vector2>();
            populateNpcList();
            ObjectLocations = new List<Vector2>();
            populateObjectLocationsList();
            DoorUnlocked = new List<bool>();
            populateDoorsUnlockedList(); 

        }

        public void populateNpcList()
        {
            for(int i = 0; i<inputManager.getLevel().getNpcs().Count; i++)
            {
                Npcs.Add(inputManager.getLevel().getNpcs()[i]);
                NpcLocations.Add(inputManager.getLevel().getNpcs()[i].getLocation());
            }
        }

        public void populateObjectLocationsList()
        {
            for (int i = 0; i < inputManager.getLevel().getObjects().Count; i++)
            {
                ObjectLocations.Add(inputManager.getLevel().getObjects()[i].getLocation());
            }
        }

        public void populateDoorsUnlockedList()
        {
            for (int i = 0; i < inputManager.getLevel().getDoors().Count; i++)
            {
                DoorUnlocked.Add(inputManager.getLevel().getDoors()[i].isUnlocked());
            }
        }

        public void resetGame()
        {
            resetPlayer();
            resetLevel();   
        }

        public void resetPlayer()
        {
            inputManager.getPlayer().setLocation(PlayerLocation);
            inputManager.getPlayerManager().setTotalExp(PlayerTotalExp);
            inputManager.getPlayerManager().setCurrentExp(PlayerCurrentExp);
            inputManager.getPlayerManager().setHealth(PlayerHealth);
            inputManager.getPlayerManager().setMana(PlayerMana);
            inputManager.getPlayerManager().setTotalMana(PlayerTotalMana);
        }

        public void resetLevel()
        {
            inputManager.getLevel().resetNpcs(Npcs, NpcLocations);
            inputManager.getLevel().resetObjects(ObjectLocations);
            inputManager.getLevel().resetDoors(DoorUnlocked);
            inputManager.getLevel().resetTokens();
        }



    }
}
