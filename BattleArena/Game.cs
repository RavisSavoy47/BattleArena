using System;

namespace BattleArena
{

    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }

    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
    }

    class Game
    {
        private bool _gameOver;
        private int _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex;
        private Entity _currentEnemy;
        private string _playerName;
        private Item[] _wizardItems;
        private Item[] _knightItems;
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

        public void InitializeItems()
        {
            //Wizard Items
            Item bigWand = new Item { Name = "Big Wand", StatBoost = 20, ItemType = 1 };
            Item bigSheild = new Item { Name = "Big Sheild", StatBoost = 15, ItemType = 0};

            //Knight Items
            Item stick = new Item { Name = "Large Stick", StatBoost = 25, ItemType = 1 };
            Item shoes = new Item { Name = "The Drip", StatBoost = 1000, ItemType = 0 };

            //Initialize arrays
            _wizardItems = new Item[] { bigWand, bigSheild };
            _knightItems = new Item[] { stick, shoes };
        }

        public void InitializeEnimes()
        {
            Entity SmallFrog = new Entity("Nice Frog", 35, 10, 5);

            Entity StackedFrog = new Entity("Delux Frog", 55, 35, 13);

            Entity MegaFrog = new Entity("Captain Frog", 100, 70, 13);

            Entity KingFrog = new Entity("The Guardians of Frogs", 150, 55, 5);

            //enemies array
            _enemies = new Entity[] { SmallFrog, StackedFrog, MegaFrog, KingFrog };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            _gameOver = false;
            _currentScene = 0;
            InitializeEnimes();
            InitializeItems();
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
        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                //Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + " " + options[i]);
                }
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        //Set input received to be the default value
                        inputReceived = -1;
                        //Display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                    Console.Clear();
                }
                //If the player didn't type an int
                else
                {
                    //set inpurt recieved to be default value
                    inputReceived = -1;
                    Console.WriteLine("Invalid Input Bro!");
                    Console.ReadKey(true);
                }


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

            if (Choice == 0)
            {
                _currentScene = 0;
                _currentEnemyIndex = 0;
                _currentEnemy = _enemies[_currentEnemyIndex];
            }

            else if (Choice == 1)
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

            if(Choice == 0)
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

            if (choice == 0)
            {
                _player = new Player(_playerName, 200, 45, 35, _wizardItems);
                _currentScene++;
            }

            else if (choice == 1)
            {
                _player = new Player(_playerName, 100, 75, 40, _knightItems);
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

        public void DisplayEquipItemMenu()
        {
            //Get items index
            int choice = GetInput("Select an item to equip.", _player.GetItemNames());

            //Equip item at given index
            if(!_player.TryEquipItem(choice))
                Console.WriteLine("Sorry bro I failed u....");
            //Print Feedback
            Console.WriteLine(" You equipped " + _player.CurrentItem.Name + "!!");
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

            int choice = GetInput("A " + _currentEnemy.Name + " approaches you!", "Attack", "Equip Item", "Remove Current Item");
            if (choice == 0)
            {
                    //player attaks enemy 
                    damageDealth = _player.Attack(_currentEnemy);
                    Console.WriteLine("You dealt " + damageDealth + " damage!");
            }
             else if (choice == 1)
            {
                DisplayEquipItemMenu();
                Console.ReadKey(true);
                Console.Clear();
                return;
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
