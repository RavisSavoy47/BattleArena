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
        int currentScene = 0;
        Character player;
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
            Character SmallFrog = new Character { name = "Nice Frog", health = 35, attackPower = 10, defensePower = 5 };

            Character StackedFrog = new Character { name = "Delux Frog", health = 55, attackPower = 35, defensePower = 13 };

            Character MegaFrog = new Character { name = "Captain Frog", health = 100, attackPower = 70, defensePower = 13 };

            Character KingFrog = new Character { name = "The Guardians of Frogs", health = 150, attackPower = 40, defensePower = 5 };

            //enemies array
            enemies = new Character[] { SmallFrog, StackedFrog, MegaFrog, KingFrog };

            currentEnemy = enemies[currentEnemyIndex];
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
            Console.ReadKey(true);
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
                    GetPlayerName();
                    break;
                case 1:
                    CharacterSelection();
                    break;
                case 2:
                    Battle();
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
            int Choice = GetInput("Do you want to Restart your Adventure?", "Yes", "No");

            if (Choice == 1)
            {
                currentScene = 1;
                currentEnemyIndex = 0;
                currentEnemy = enemies[currentEnemyIndex];
            }

            else if (Choice == 2)
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

            Console.Clear();
            //Checks if the player wants to keep their name
            int Choice = GetInput("You've entered " + player.name + ". Are you sure you want to keep this name?", "Yes", "No");

            if(Choice == 1)
            {
                currentScene++;
            }
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Hope you survive" + player.name + ". Please pick your character.", "Wizard", "Knight");

            if (choice == 1)
            {
                player.health = 200;
                player.attackPower = 45;
                player.defensePower = 35;
                currentScene++;
            }

            else if (choice == 2)
            {
                player.health = 100;
                player.attackPower = 75;
                player.defensePower = 40;
                currentScene++;
            }
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
            Console.WriteLine();
        }

        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attackPower, float defensePower)
        {
            float damageTaken = attackPower - defensePower;

            if (damageTaken < 0)
            {
                damageTaken = 0;
            }

            return damageTaken;
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

            if (defender.health < 0)
            {
                defender.health = 0;
            }

            return damageTaken;
        }



        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealth = 0;

            //Print PLayer stats
            DisplayStats(player);
            //Print Enemy stats
            DisplayStats(currentEnemy);

            int choice = GetInput("A " + currentEnemy.name + " approaches you!", "Attack", "Dodge");
            if (choice == 1)
            {
                    //player attaks enemy 
                    damageDealth = Attack(ref player, ref currentEnemy);
                    Console.WriteLine("You dealth " + damageDealth + " damage!");
            }
             else if (choice == 2)
            {
                Console.WriteLine("you dodged the enemy's attack!");
                Console.ReadKey();
                Console.Clear();
            }

            damageDealth = Attack(ref currentEnemy, ref player);
            Console.WriteLine("The " + currentEnemy.name + " dealt" + damageDealth, " damage!");

            Console.ReadKey(true);
            Console.Clear();
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
                Console.ReadKey(true);
                Console.Clear();
                currentScene = 3;
            }
            //check if the enemy dies
            else if (currentEnemy.health <= 0)
            {
                Console.WriteLine("You Defeated " + currentEnemy.name);
                Console.ReadKey();
                currentEnemyIndex++;

                //If all enemies have died continue to next scene
                if (currentEnemyIndex >= enemies.Length)
                {
                    currentScene = 3;
                    return;
                }
                currentEnemy = enemies[currentEnemyIndex];                
            }

        }

    }
}
