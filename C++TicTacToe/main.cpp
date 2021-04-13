/*
    Tic Tac Toe Program
    Kaiser Slocum
    1/21/2019
*/

#include <iostream>
#include <iomanip>
#include <cmath>
#include <limits>

using namespace std;

const int ROW = 3;
const int COL = 3;

const char PLAYER1SYMBOL = 'O';
const char PLAYER2SYMBOL = 'X';

// Checks that a particular column is a win
bool ColumnWin(char board[ROW][COL], int c, char symbol)
{
    for (int r = 0; r < ROW; r++)
    {
        if (board[r][c] != symbol)
            return false;
    }
    return true;
}

// Checks that any column is a win
bool AnyColumnWin(char board[ROW][COL], char symbol)
{
    for (int c = 0; c < COL; c++)
    {
        if (ColumnWin(board, c, symbol) == true)
            return true;
    }
    return false;
}

//Checks that a particular row is a win
bool RowWin(char board[ROW][COL], int r, char symbol)
{
    for (int c = 0; c < COL; c++)
    {
        if (board[r][c] != symbol)
            return false;
    }
    return true;
}

// Checks that any row is a win
bool AnyRowWin(char board[ROW][COL], char symbol)
{
    for (int r = 0; r < ROW; r++)
    {
        if (RowWin(board, r, symbol) == true)
            return true;
    }
    return false;
}

// Checks to see if the left to right diagonal is a win
bool LRDiagonalWin(char board[ROW][COL], char symbol)
{
    for (int r = 0, c = 0; r < ROW && c < COL; r++, c++)
    {
        if (board[r][c] != symbol)
            return false;
    }
    return true;
}

// Checks to see if the right to left diagonal is a win
bool RLDiagonalWin(char board[ROW][COL], char symbol)
{
   for (int r = 0, c = (COL - 1); r < ROW && c >= 0; r++, c--)
    {
        if (board[r][c] != symbol)
            return false;
    }
    return true;
}

// Checks if any diagonal has a win
bool AnyDiagonalWin(char board[ROW][COL], char symbol)
{
    if(LRDiagonalWin(board, symbol) == true)
       return true;
    else if(RLDiagonalWin(board, symbol) == true)
        return true;
    else
        return false;
}

// Checks if there is a win for the specified symbol
bool CheckWin(char board[ROW][COL], char symbol)
{
    if (AnyColumnWin(board, symbol) == true)
    {
        cout << "\n" << "Player-" << symbol << "- won!" << "\n";
        return true;
    }
    else if (AnyRowWin(board, symbol) == true)
    {
        cout << "\n" << "Player-" << symbol << "- won!" << "\n";
        return true;
    }
    else if (AnyDiagonalWin(board, symbol) == true)
    {
        cout << "\n" << "Player-" << symbol << "- won!" << "\n";
        return true;
    }
    else
    {
        return false;
    }
}

// Checks if the game is a draw
bool CheckDraw(char board[ROW][COL])
{
    for (int r = 0; r < ROW; r++)
    {
         for (int c = 0; c < COL; c++)
        {
            if (board[r][c] != PLAYER1SYMBOL && board[r][c] != PLAYER2SYMBOL)
                return false;
        }
    }
    cout << "\nDraw!\n";
    return true;
}

// Gets and makes the move in the 2d array
 void GetMove(char board[ROW][COL], char playerSymbol)
{
    int rowNumber = -1;
    int colNumber = -1;
    bool failed = true;

    cout << "Player-" << playerSymbol << "-'s turn" << endl;
    do
    {
        do
        {
            cout << "Please type in a valid row number: ";
            cin >> rowNumber;

            if(cin.fail())
            {
                cin.clear();
                cin.ignore(numeric_limits<streamsize>::max(),'\n');

                failed = true;

            }
            else if (rowNumber < 0)
                failed = true;
            else if (rowNumber >= ROW)
                failed = true;
            else
                failed = false;

        }while(failed == true);

        do
        {
            cout << "Please type in a valid column number: ";
            cin >> colNumber;

            if(cin.fail())
            {
                cin.clear();
                cin.ignore(numeric_limits<streamsize>::max(),'\n');

                failed = true;
            }
            else if (colNumber < 0)
                failed = true;
            else if (colNumber >= COL)
                failed = true;
            else
                failed = false;

        }while(failed == true);

    }while ((board[rowNumber][colNumber] == PLAYER1SYMBOL) || (board[rowNumber][colNumber] == PLAYER1SYMBOL));

    board[rowNumber][colNumber] = playerSymbol;
}

// Shows the current characters in the board
void ShowBoard(char board[ROW][COL])
{
    for (int r = 0; r < ROW; r++)
    {
         for (int c = 0; c < COL; c++)
        {
                cout << " [" << board[r][c] << "] ";
        }
        cout << "\n";
    }
    cout << "\n";
}

// Sets the board to a 2d array of space characters
// Should always be called first.
void ResetBoard(char board[ROW][COL])
{
    for (int r = 0; r < ROW; r++)
    {
         for (int c = 0; c < COL; c++)
        {
            board[r][c] = ' ';
        }
    }
}

// Displays the Instructions
void DisplayInstructions()
{
    cout << "\n\n";
    cout << "This is a 3x3 Tic Tac Toe, without a computer player." << endl;
    cout << "To make a move, simply type in the row number (0,1,2) " << endl;
    cout << "and the column number (0,1,2) when you are prompted." << endl;
    cout << "Then, press enter(or return) and repeat." << endl;
    cout << "If you try to place your symbol on an already occupied square you will be" << endl;
    cout << "required to re-type in your column and row numbers.";
    cout << "\n\n";
}

int main()
{
    char answer;
    char board[ROW][COL];

    do
    {
        ResetBoard(board);
        answer = 'a';
        DisplayInstructions();

        do
        {
            ShowBoard(board);
            GetMove(board, PLAYER1SYMBOL);

            if ((CheckWin(board, PLAYER1SYMBOL) == true) || (CheckDraw(board) == true))
                break;

            ShowBoard(board);
            GetMove(board, PLAYER2SYMBOL);
        } while ((CheckWin(board, PLAYER2SYMBOL) != true) && (CheckDraw(board) != true));

        ShowBoard(board);

        do
        {
            cout << "Do you want to play again? (Y/N)" << endl;
            cin >> answer;
            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(),'\n');
        }while((answer != 'Y') && (answer!= 'N') && (answer != 'y' ) && (answer!= 'n'));
        answer = tolower(answer);
    }
    while(answer == 'y');

    return 0;
}
