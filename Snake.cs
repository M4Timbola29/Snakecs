using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        // Define the size of the game field
        const int width = 80;
        const int height = 20;

        // Define the initial position and direction of the snake
        static int x = width / 2;
        static int y = height / 2;
        static string direction = "RIGHT";

        // Define the initial length and body of the snake
        static int length = 5;
        static List<int> xBody = new List<int>();
        static List<int> yBody = new List<int>();

        // Define the position and score of the food
        static int xFood = 0;
        static int yFood = 0;
        static int score = 0;

        // Define a random number generator
        static Random random = new Random();

        static void Main(string[] args)
        {
            // Set the console window size and title
            Console.SetWindowSize(width + 8, height + 8);
            Console.Title = "Snake Game";

            // Hide the cursor
            Console.CursorVisible = false;

            // Spawn the first food
            SpawnFood();

            // Ask the user for his name
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();

            // Start the game loop
            while (true)
            {
                // Draw the game field
                DrawField();

                // Draw the snake
                DrawSnake();

                // Draw the food
                DrawFood();

                // Draw the score
                //DrawScore();

                // Check the user input
                Input();

                // Move the snake
                Move();

                // Check the collision
                Collision();

                // Slow down the game
                Thread.Sleep(100);
            }
        }

        // A method to draw the game field
        static void DrawField()
        {
            // Clear the console
            Console.Clear();

            // Draw the top border
            for (int i = 0; i < width + 2; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
            }

            // Draw the bottom border
            for (int i = 0; i < width + 2; i++)
            {
                Console.SetCursorPosition(i, height + 1);
                Console.Write("-");
            }

            // Draw the left border
            for (int i = 0; i < height + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }

            // Draw the right border
            for (int i = 0; i < height + 2; i++)
            {
                Console.SetCursorPosition(width + 1, i);
                Console.Write("|");
            }
        }

        // A method to draw the snake
        static void DrawSnake()
        {
            // Draw the head of the snake
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O");

            // Draw the body of the snake
            for (int i = 0; i < xBody.Count; i++)
            {
                Console.SetCursorPosition(xBody[i], yBody[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("o");
            }
        }

        // A method to draw the food
        static void DrawFood()
        {
            // Draw the food
            Console.SetCursorPosition(xFood, yFood);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");
        }

        // A method to draw the score
        static void DrawScore()
        {
            // Draw the score
            Console.SetCursorPosition(0, height + 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Score: " + score);
        }

        // A method to check the user input
        static void Input()
        {
            // Check if a key is pressed
            if (Console.KeyAvailable)
            {
                // Get the pressed key
                ConsoleKey key = Console.ReadKey(true).Key;

                // Change the direction according to the pressed key
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (direction != "DOWN")
                            direction = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        if (direction != "UP")
                            direction = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != "RIGHT")
                            direction = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != "LEFT")
                            direction = "RIGHT";
                        break;
                }
            }
        }

        // A method to move the snake
        static void Move()
        {
            // Add the current head position to the body
            xBody.Add(x);
            yBody.Add(y);

            // Remove the last body part if the snake is too long
            if (xBody.Count > length)
            {
                xBody.RemoveAt(0);
                yBody.RemoveAt(0);
            }

            // Change the head position according to the direction
            switch (direction)
            {
                case "UP":
                    y--;
                    break;
                case "DOWN":
                    y++;
                    break;
                case "LEFT":
                    x--;
                    break;
                case "RIGHT":
                    x++;
                    break;
            }
        }

        // A method to check the collision
        static void Collision()
        {
            // Check if the snake hits the border
            if (x < 1 || x > width || y < 1 || y > height)
            {
                // End the game
                GameOver();
            }

            // Check if the snake eats the food
            if (x == xFood && y == yFood)
            {
                // Increase the score and the length
                score++;
                length++;

                // Spawn a new food
                SpawnFood();
            }

            // Check if the snake hits itself
            for (int i = 0; i < xBody.Count; i++)
            {
                if (x == xBody[i] && y == yBody[i])
                {
                    // End the game
                    GameOver();
                }
            }
        }

        // A method to spawn a new food
        static void SpawnFood()
        {
            // Generate a random position for the food
            xFood = random.Next(1, width);
            yFood = random.Next(1, height);

            // Check if the food overlaps with the snake
            for (int i = 0; i < xBody.Count; i++)
            {
                if (xFood == xBody[i] && yFood == yBody[i])
                {
                    // Spawn a new food
                    SpawnFood();
                }
            }
        }

        // A method to end the game
        static void GameOver()
        {
            // Clear the console
            Console.Clear();

            // Display the game over message and the final score
            Console.SetCursorPosition(width / 2 - 4, height / 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Game Over");
            Console.SetCursorPosition(width / 2 - 4, height / 2 + 1);
            Console.Write("Score: " + score);

            // Wait for a key press
            Console.ReadKey();

            // Exit the program
            Environment.Exit(0);
        }
    }
}