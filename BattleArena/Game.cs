using System;

namespace BattleArena
{
    //Test


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
        bool gameOver = false;
        int currentScene = 1;
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
            //Initilize Enemies 
            SmallFrog.name = "Nice Frog";
            SmallFrog.health = 35.0f;
            SmallFrog.attackPower = 10.0f;
            SmallFrog.defensePower = 5.0f;

            StackedFrog.name = "Dexlux Frog";
            StackedFrog.health = 55.0f;
            StackedFrog.attackPower = 35.0f;
            StackedFrog.defensePower = 13.0f;


            MegaFrog.name = "Captain Frog";
            MegaFrog.health = 100.0f;
            MegaFrog.attackPower = 70.0f;
            MegaFrog.defensePower = 13.0f;            


            KingFrog.name = "The Guardians of Frogs";
            KingFrog.health = 150.0f;
            KingFrog.attackPower = 40.0f;
            KingFrog.defensePower = 5f;

            //enemies array
            enemies = new Character[] { SmallFrog, StackedFrog, MegaFrog, KingFrog };

            ResetCurrentEnemy();
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Fairwell NERD!");
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
            //Goes through each Scene
            switch (currentScene)
            {
                case 0:
                    Start();
                    break;
                case 1:
                    CharacterSelection();
                    break;
                case 2:
                    Battle();

                    Console.ReadKey();
                    Console.Clear();

                    CheckBattleResults();
                    break;
                case 3:
                    DisplayMainMenu();

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
            int input = GetInput("Do you want to Restart your Adventure?", "Yes", "No");

            if (input == 1)
            {
                currentScene = 1;
                ResetCurrentEnemy();
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
            Console.WriteLine("Welcome to Hell! What's your name?");
            Console.Write(">");
            player.name = Console.ReadLine();
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            GetPlayerName();
            int choice = GetInput("Hope you stay for a while. Please pick your character.", "Wizard", "Knight");

            if (choice == 1)
            {
                
                player.health = 200f;
                player.attackPower = 45f;
                player.defensePower = 35f;


            }

            else if (choice == 2)
            {
                
                player.health = 100f;
                player.attackPower = 75f;
                player.defensePower = 40f;
            }
            currentScene = 2;
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="">The character that will have its stats shown</param>
        void DisplayStats(Character character)
        {
            Console.WriteLine("Name: " + character.name);
            Console.WriteLine("Health: " + character.health);
            Console.WriteLine("Attack: " + character.attackPower);
            Console.WriteLine("Defense: " + character.defensePower);

 
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
            float damageTaken = CalculateDamage(attacker.attackPower, defender.defensePower);
            defender.health -= damageTaken;
            return damageTaken;
        }



        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            //Print PLayer stats
            DisplayStats(player);
            //Print Enemy stats
            DisplayStats(currentEnemy);

            int choice = GetInput("A Enemy Approaches you!", "Attack", "Dodge");
            switch(choice)
            {
                case 1:
                    
                    
                    //player attaks enemy 
                    float damageTaken = Attack(ref player, ref currentEnemy);
                    Console.WriteLine(currentEnemy.name + " has taken " + damageTaken);

                    //enemy attcaks player
                    damageTaken = Attack(ref currentEnemy, ref player);
                    Console.WriteLine(player.name + " has taken " + damageTaken);
                    break;
                case 2:
                    Console.WriteLine("You Dodge " + currentEnemy.name);
                    Console.ReadLine();
                    break;

            }
        }

        void ResetCurrentEnemy()
        {
            currentEnemyIndex = 0;
            //Set Starting fighters
            currentEnemy = enemies[currentEnemyIndex];

        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
            Console.Clear();
            //If the player dies they are asked if they want to keep playing or not
            if (player.health <= 0)
            {
                Console.WriteLine("You died! Get Gud!");
                gameOver = true;
                

            }
            //check if the enemy dies
            if (currentEnemy.health <= 0)
            {
                currentEnemyIndex++;
                //If all enemies have died continue to next scene
                if (currentEnemyIndex >= enemies.Length)
                {
                    Console.WriteLine("You Defeated The great Evil!");
                    Console.WriteLine("Congratulations!");
                    Console.ReadKey();
                    currentScene = 3;
                }
                else
                {
                    //..If the enemy dies the next enemy appears 
                    

                    currentEnemy = enemies[currentEnemyIndex];
                }
            }

        }

    }
}
