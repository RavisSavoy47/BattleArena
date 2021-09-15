using System;

namespace BattleArena
{

    class Game
    {
        private bool _gameOver;
        private int _currentScene;
        private Entity _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private string _playerName;
        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {

            Start();

            while (!_gameOver)
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
            _gameOver = false;
            _currentScene = 0;
            _currentEnemyIndex = 0;

            Entity SmallFrog = new Entity("Nice Frog", 35, 10, 5);

            Entity StackedFrog = new Entity("Delux Frog", 55, 35, 13);

            Entity MegaFrog = new Entity("Captain Frog", 100, 70, 13);

            Entity KingFrog = new Entity("The Guardians of Frogs", 150, 55, 5);

            //enemies array
            _enemies = new Entity[] { SmallFrog, StackedFrog, MegaFrog, KingFrog };

            _currentEnemy = _enemies[_currentEnemyIndex];
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
            switch (_currentScene)
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
                _currentScene = 0;
                _currentEnemyIndex = 0;
                _currentEnemy = _enemies[_currentEnemyIndex];
            }

            else if (Choice == 2)
            {  
                _gameOver = true;
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
            _playerName = Console.ReadLine();

            Console.Clear();
            //Checks if the player wants to keep their name
            int Choice = GetInput("You've entered " + _playerName + ". Are you sure you want to keep this name?", "Yes", "No");

            if(Choice == 1)
            {
                _currentScene++;
            }
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Hope you survive" + _playerName + ". Please pick your character.", "Wizard", "Knight");

            if (choice == 1)
            {
                _player = new Entity(_playerName, 200, 45, 35);
                _currentScene++;
            }

            else if (choice == 2)
            {
                _player = new Entity(_playerName, 100, 75, 40);
                _currentScene++;
            }
        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {
            Console.WriteLine("Name: " + character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack: " + character.AttackPower);
            Console.WriteLine("Defense: " + character.DefensePower);
            Console.WriteLine();
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            float damageDealth = 0;

            //Print PLayer stats
            DisplayStats(_player);
            //Print Enemy stats
            DisplayStats(_currentEnemy);

            int choice = GetInput("A " + _currentEnemy.Name + " approaches you!", "Attack", "Dodge");
            if (choice == 1)
            {
                    //player attaks enemy 
                    damageDealth = _player.Attack(_currentEnemy);
                    Console.WriteLine("You dealt " + damageDealth + " damage!");
            }
             else if (choice == 2)
            {
                Console.WriteLine("you dodged the enemy's attack!");
                Console.ReadKey();
                Console.Clear();
            }

            damageDealth = _currentEnemy.Attack(_player);
            Console.WriteLine("The " + _currentEnemy.Name + " dealt " + damageDealth + " damage!");

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
            if (_player.Health <= 0)
            {
                Console.WriteLine("You died! Get Gud!");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = 3;
            }
            //check if the enemy dies
            else if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You Defeated " + _currentEnemy.Name);
                Console.ReadKey();
                _currentEnemyIndex++;

                //If all enemies have died continue to next scene
                if (_currentEnemyIndex >= _enemies.Length)
                {
                    _currentScene = 3;
                    return;
                }
                _currentEnemy = _enemies[_currentEnemyIndex];                
            }

        }

    }
}
