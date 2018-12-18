using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            RulePrint();
            Console.Write("Please choose difficulty level (recomended 1-4): ");
            int size = 2 * Convert.ToInt32(Console.ReadLine());
            int PairCounter = 0;
            if (size > 4)
            {
                Console.WriteLine("I wonder are you brave or just a fool? well it's you funeral.");
            }
            
            string[,] gameBoard = BoardMaker(size, ref PairCounter);
            char[,] printBoard = PrintMaker(size);
            BoardAlign(gameBoard);
            Console.WriteLine("The board is now ready.");
            Console.Write("Press any key to start.");
            Console.ReadKey();
            bool whichPlayer = true; // if "true" player 1, if "false" player 2.
            int player1Points = 0;
            int player2Points = 0;
            while (PairCounter > 1)
            {
                if (whichPlayer)
                {
                    player1Points += GameRound(ref whichPlayer, ref PairCounter, gameBoard, printBoard);
                }
                else
                {
                    player2Points += GameRound(ref whichPlayer,ref PairCounter, gameBoard, printBoard);
                }
            }
            if (player1Points > player2Points)
            {
                Console.WriteLine("Congratulations Player 1!!!! you won this game!");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Congratulations Player 2!!!! you won this game!");
                Console.WriteLine();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }

        
        // Prints the rules for the game

        private static void RulePrint()
        {
            Console.WriteLine("Welcome to the Memory Cards game, 2 player edition.");
            Console.WriteLine();
            Console.WriteLine("Stage One: Setting the Board");
            Console.WriteLine("Choose difficulty level 1-4, the difficulty level will determine the game board's size.");
            Console.WriteLine("Input as many strings as neccesary to make cards, each card may contain up to nine characters.");
            Console.WriteLine("Each card will have 2 copies placed in a random spot on the board.");
            Console.WriteLine();
            Console.WriteLine("Stage Two: Game Time");
            Console.WriteLine("On your turn choose which card to flip, you will then see the current board.");
            Console.WriteLine(@"on the printed borad you will see cards marked with 'X' these cards are flipped down,");
            Console.WriteLine(@"cards marked with '0' are those alreday pulled out of the board.");
            Console.WriteLine(@"the card you revealed will show which strings is on this card.");
            Console.WriteLine("following the first pick choose another card to flip. The board will be printed again with both cards revealed.");
            Console.WriteLine("If the cards match they will be pulled from the board, you will receive a point and get to play again.");
            Console.WriteLine("If the cards dont match no points will be given, the cards will be flipped back down and the other player will now play.");
            Console.WriteLine();
            Console.WriteLine("The game ends when there are 2 cards remaining in the board, the winner is the one with the highest score.");
            Console.WriteLine();
            Console.WriteLine("NOTE: This game is not zero based.");
            Console.WriteLine();
            Console.WriteLine("press any key to begin the game.");
            Console.ReadKey();
        }

        // receives string from the user and places 2 copies of it in random null cells inside array

        static string[,] BoardMaker(int size, ref int PairCounter)
        {
            string[,] newBoard = new string[size, size];
            for (int rowCount = 0; rowCount < newBoard.GetLength(0); rowCount++)
            {
                for (int colCount = 0; colCount < (newBoard.GetLength(1) / 2); colCount++)
                {
                    Console.Write($"Please enter the string for card set number {PairCounter + 1} (NOTE: maximum length is nine characters): ");
                    newBoard[rowCount, colCount] = Console.ReadLine();
                    if (newBoard[rowCount, colCount].Length > 10)
                    {
                        newBoard[rowCount, colCount] = newBoard[rowCount, colCount].Substring(0, 9);
                    }
                    
                    int reverseCol = (size) - 1 - colCount;
                    newBoard[rowCount, reverseCol] = newBoard[rowCount, colCount];
                    PairCounter++;
                }
            }
            int loopCounter = size * 10;
            newBoard = BoardShuffle(newBoard, loopCounter);
            return newBoard;
        }

        //randomizes the order of cells in the game board

        private static string[,] BoardShuffle(string[,] newBoard, int loopCounter)
        {
            Random randomGen = new Random();
            if (loopCounter > 0)
            {
                int row1 = randomGen.Next(0, newBoard.GetLength(0));
                int row2 = randomGen.Next(0, newBoard.GetLength(0));
                int col1 = randomGen.Next(0, newBoard.GetLength(1));
                int col2 = randomGen.Next(0, newBoard.GetLength(1));
                string temp = newBoard[row1, col1];
                newBoard[row1, col1] = newBoard[row2, col2];
                newBoard[row2, col2] = temp;
                loopCounter--;
                BoardShuffle(newBoard, loopCounter);
            }
            return newBoard;
        }

        // Creates the Array used for printing on screen

        static char[,] PrintMaker(int size)
        {
            char[,] PrintArray = new char[size * 2, size * 2];
            for (int rowCounter = 0; rowCounter < PrintArray.GetLength(0); rowCounter++)
            {
                for (int colCounter = 0; colCounter < PrintArray.GetLength(1); colCounter++)
                {
                    PrintArray[rowCounter, colCounter] = 'x';
                }
            }
            return PrintArray;
        }

        // Aligns the gameBoard strings

        private static void BoardAlign(string[,] gameBoard)
        {
            for (int rowCounter = 0; rowCounter < gameBoard.GetLength(0); rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < gameBoard.GetLength(1); columnCounter++)
                {
                    switch (gameBoard[rowCounter,columnCounter].Length)
                    {
                        case '1':
                            gameBoard[rowCounter, columnCounter] = "    gameBoard[rowCounter,columnCounter]    ";
                            break;
                        case '2':
                            gameBoard[rowCounter, columnCounter] = "    gameBoard[rowCounter,columnCounter]   ";
                            break;
                        case '3':
                            gameBoard[rowCounter, columnCounter] = "   gameBoard[rowCounter,columnCounter]   ";
                            break;
                        case '4':
                            gameBoard[rowCounter, columnCounter] = "   gameBoard[rowCounter,columnCounter]  ";
                            break;
                        case '5':
                            gameBoard[rowCounter, columnCounter] = "  gameBoard[rowCounter,columnCounter]  ";
                            break;
                        case '6':
                            gameBoard[rowCounter, columnCounter] = "  gameBoard[rowCounter,columnCounter] ";
                            break;
                        case '7':
                            gameBoard[rowCounter, columnCounter] = " gameBoard[rowCounter,columnCounter] ";
                            break;
                        case '8':
                            gameBoard[rowCounter, columnCounter] = " gameBoard[rowCounter,columnCounter]";
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        // Main function for a single round of game, returns 1 if cards matched or 0 if not.

        private static int GameRound(ref bool whichPlayer, ref int PairCounter, string[,] gameBoard, char[,] printBoard)
        {
            Console.WriteLine("This is " + ((whichPlayer) ? "Player 1's turn." : "Player 2's turn."));
            bool flag = true;
            int card1Row, card1Column, card2Row, card2Column;
            do
            {
                Console.Write("Please choose the first card's row: ");
                card1Row = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Write("Please choose the first card's column: ");
                card1Column = Convert.ToInt32(Console.ReadLine()) - 1;
                flag = DoesCellExist(gameBoard, card1Column, card1Row) ;
            }
            while (!flag);
            PrintBoard(card1Row, card1Column, gameBoard, printBoard);
            do
            {
                Console.Write("Please choose the second card's row: ");
                card2Row = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Write("Please choose the second card's column: ");
                card2Column = Convert.ToInt32(Console.ReadLine()) - 1;
                flag = DoesCellExist(gameBoard, card2Column, card2Row);
                if (card1Row == card2Row && card1Column == card2Column)
                {
                    Console.WriteLine("You can't pick the same card twice, please choose another");
                    flag = false;
                }
            }
            while (!flag);
            PrintBoard(card1Row, card1Column, gameBoard, printBoard, card2Row, card2Column);
            if (gameBoard[card1Row, card1Column] == gameBoard[card2Row, card2Column])
            {
                PairCounter--;
                gameBoard[card1Row, card1Column] = null;
                gameBoard[card2Row, card2Column] = null;
                printBoard[card1Row, card1Column] = '0';
                printBoard[card2Row, card2Column] = '0';
                return 1;
            }
            else
            {
                whichPlayer = !whichPlayer;
                return 0;
            }
        }

        // prints the board after a card is chosen

        private static void PrintBoard(int card1Row, int card1Column, string[,] gameBoard, char[,] printBoard, int card2Row = -1, int card2Column = -1)
        {
            for (int rowCounter = 0; rowCounter < gameBoard.GetLength(0); rowCounter++)
            {
                for (int columnCounter = 0; columnCounter < gameBoard.GetLength(1); columnCounter++)
                {
                    if (card1Row != rowCounter && card1Column != columnCounter && card2Row != rowCounter && card2Column != columnCounter)
                    {
                        Console.Write($"     {printBoard[rowCounter, columnCounter]}     ");
                    }
                    else
                    {
                        Console.Write(gameBoard[rowCounter, columnCounter]);
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        // Checks if the chosen cell is within bounds of game board and if card was already taken (taken cards are 'null' in array)

        private static bool DoesCellExist(string[,] gameBoard, int cardColumn, int cardRow)
        {
            if (cardRow < 0 || cardColumn < 0)
            {
                Console.WriteLine("desired cell is out of bounds, pick another");
                return false;
            }
            else if (cardRow > gameBoard.GetLength(0) || cardColumn > gameBoard.GetLength(1))
            {
                Console.WriteLine("desired cell is out of bounds, pick another");
                return false;
            }
            else if (gameBoard[cardRow, cardColumn] == null)
            {
                Console.WriteLine("desired cell was already taken, pick another");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
