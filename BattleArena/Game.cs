using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>
    struct Character
    {
        public string name;
        public float health;
        public float attackPower;
        public float defensePower;
    }

    class Game
    {
        bool gameOver;
        int currentScene;
        Character player;
        Character SmallFrog;
        Character StackedFrog;
        Character MegaFrog;
        Character KingFrog;
        Character[] enemies;
        private int currentEnemyIndex = 0;
        private Character currentEnemy;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {

            Start();

            while (!gameOver)
            {
                Update();
            }

            End();
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            //Initilize Eniemes
            SmallFrog.name = "Nice Frog";
            SmallFrog.health = 35.0f;
            SmallFrog.attackPower = 10.0f;
            SmallFrog.defensePower = 5.0f;


            StackedFrog.name = "Triple Dexlux Frog";
            StackedFrog.health = 55.0f;
            StackedFrog.attackPower = 35.0f;
            StackedFrog.defensePower = 13.0f;


            MegaFrog.name = "Captain Frog";
            MegaFrog.health = 100.0f;
            MegaFrog.attackPower = 70.0f;
            MegaFrog.defensePower = 13.0f;            


            KingFrog.name = "The Guardians of Frogs";
            KingFrog.health = 150.0f;
            KingFrog.attackPower = 50.0f;
            KingFrog.defensePower = 5f;
            

        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
            Console.Clear();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("You escaped this time but I'll get you next time!");
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, string option1, string option2)
        {
            string input = "";
            int inputReceived = 0;

            while (inputReceived != 1 && inputReceived != 2)
            {//Print options
                Console.WriteLine(description);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If player selected the first option...
                if (input == "1" || input == option1)
                {
                    //Set input received to be the first option
                    inputReceived = 1;
                }
                //Otherwise if the player selected the second option...
                else if (input == "2" || input == option2)
                {
                    //Set input received to be the second option
                    inputReceived = 2;
                }
                //If neither are true...
                else
                {
                    //...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }

                Console.Clear();
            }
            return inputReceived;
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {
            switch (currentScene)
            {
                case 0:
                    Start();
                    break;
                case 1:
                    CharacterSelection();
                    DisplayStats(Character);
                    break;
                case 2:
                    Battle();
                    break;
                case 3:
                    DisplayMainMenu();
                    Console.ReadKey();
                    break;


                default:
                    Console.WriteLine("Invaild scene index");
                    break;
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int input = GetInput("Do you want to Restart?", "\n 1. Yes", "\n 2. No");

            if (input == 1)
            {
                currentScene = 1;
                gameOver = false;
            }

            if (input == 2)
            {
                    
                gameOver = true;
            }


        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            Console.WriteLine("Welcome to Hell! What's your name? \n >");
            player.name = Console.ReadLine();
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            GetPlayerName();
            int choice = GetInput("Hope you stay for a while. Please pick your character.", "\n 1. Wizard", "\n 2. Knight");
            switch (choice)
            {

                case 0:
                    player.name = "Name: " + player.name;
                    player.health = 50f;
                    player.attackPower = 25f;
                    player.defensePower = 5f;
                    break;

                case 1:
                    player.name = "Name: " + player.name;
                    player.health = 75f;
                    player.attackPower = 15f;
                    player.defensePower = 10f;
                    break;

            }
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="">The character that will have its stats shown</param>
        void DisplayStats(Character character)
        {
            Console.WriteLine("Name: " + player.name);
            Console.WriteLine("Health: " + player.health);
            Console.WriteLine("Attack: " + player.attackPower);
            Console.WriteLine("Defense: " + player.defensePower);
        }

        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attackPower, float defensePower)
        {
            float damage = attackPower - defensePower;
            if (attackPower - defensePower <= 0)
            {
                damage = 0;
            }

            return damage;
        }

        /// <summary>
        /// Deals damage to a character based on an attacker's attack power
        /// </summary>
        /// <param name="attacker">The character that initiated the attack</param>
        /// <param name="defender">The character that is being attacked</param>
        /// <returns>The amount of damage done to the defender</returns>
        public float Attack(ref Character attacker, ref Character defender)
        {
            return attacker.attackPower - defender.defensePower;
        }



        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {

        }

    }
}
