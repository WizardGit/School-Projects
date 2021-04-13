#include <iostream>
#include <iomanip>
#include <cmath>
#include <limits>
#include <cstdlib>
#include <ctime>

using namespace std;

const char SCISSORS = 's';
const char PAPER = 'p';
const char ROCK = 'r';

// Randomly returns ROCK, PAPER, or SCISSORS
char chooseObject();
// Displays the Rock, Paper, Scissors instructions
void displayInstructions();
// Returns the input which is ROCK, PAPER, or SCISSORS that the user entered
char getMove();
// Returns true if the user wants to play again; false if not
bool playAgain();
// Returns 1 if the user won
// Returns 2 if the computer won
// Returns 3 if they tied or there was an error
int getWinner(char user, char computer);
// Displays the user's and computer's moves
void displayResults(char user, char comp);

int main()
{
    char userMove = 'x';
    char compMove = 'x';
    int winner = -1;

    displayInstructions();

    do
    {
        // Resets the variables
        userMove = 'x';
        compMove = 'x';
        winner = -1;

        // Gets the user and computer moves
        userMove = getMove();
        compMove = chooseObject();

        // Declares the winner
        winner = getWinner(userMove, compMove);

        if (winner == 1)
        {
            cout << "\n  The User Won!";
            displayResults(userMove, compMove);
        }
        else if (winner == 2)
        {
            cout << "\nThe Computer Won!";
            displayResults(userMove, compMove);
        }
        else
        {
            cout << "\n  It was a tie!";
            displayResults(userMove, compMove);
        }

    // Asks if the user wants to play again
    }while (playAgain() == true);

    return 0;
}

char chooseObject()
{
    int choice = -1;
    srand(static_cast<unsigned>(time(0)));

    // Assigns 1,2, or 3 randomly to "choice"
    choice = rand() % 3;

    // Returns the corresponding character to each random number
    if (choice == 0)
        return ROCK;
    else if (choice == 1)
        return PAPER;
    else if (choice == 2)
        return SCISSORS;
    else
        return 'e';
}

void displayInstructions()
{
    cout << "Welcome to Rock, Paper, Scissors!" << endl;
    cout << "To play, simply input " << endl;
    cout << "\tr : (for rock)" << endl;
    cout << "\tp : (for paper)" << endl;
    cout << "\ts : (for scissors)\n" << endl;
    cout << "Then, the computer will choose one." << endl;
    cout << "Afterwards, the winner will be decided depending on the following:" << endl;
    cout << "\tPaper beats rock" << endl;
    cout << "\tRock beats scissors" << endl;
    cout << "\tScissors beats paper\n" << endl;
}

char getMove()
{
    char object = ' ';

    do
    {
        // Inputs the user's choice
        cout << "Please input (r)ock, (p)aper, or (s)cissors: ";
        cin >> object;
        cin.clear();
        cin.ignore(numeric_limits<streamsize>::max(), '\n');

        // If invalid input was entered, a message is displayed
        if ((cin.fail() == true) || ((tolower(object) != ROCK) && (tolower(object) != PAPER) && (tolower(object) != SCISSORS)))
            cout << "Invalid input. Please try again!\n" << endl;
    }
    while ((cin.fail() == true) || ((tolower(object) != ROCK) && (tolower(object) != PAPER) && (tolower(object) != SCISSORS)));

    cout << endl;

    return tolower(object);
}

bool playAgain()
{
    char answer = 'x';
    do
    {
        // Gets the user's choice
        cout << "Do you want to play again?(y/n): ";
        cin >> answer;
        cin.clear();
        cin.ignore(numeric_limits<streamsize>::max(), '\n');

        // If the input is invalid, an error message is displayed
        if ((cin.fail() == true) || ((tolower(answer) != 'y') && (tolower(answer) != 'n')))
            cout << "Invalid input, please try again!\n" << endl;
    }
    while ((cin.fail() == true) || ((tolower(answer) != 'y') && (tolower(answer) != 'n')));

    cout << endl;
    answer = tolower(answer);

    // returns true if answer is yes; false if answer is no
    if (answer == 'y')
        return true;
    else
        return false;
}

// Returns an integer corresponding to who won
int getWinner(char user, char computer)
{
    if (
             ((user == ROCK) && (computer == PAPER)) ||
             ((user == PAPER) && (computer == SCISSORS)) ||
             ((user == SCISSORS) && (computer == ROCK))
             )
        return 2;
    else if (
             ((user == ROCK) && (computer == SCISSORS)) ||
             ((user == PAPER) && (computer == ROCK)) ||
             ((user == SCISSORS) && (computer == PAPER))
             )
        return 1;

    else
        return 3;
}

// Displays the results of the game
void displayResults(char user,  char comp)
{
    cout << endl;
    cout << "  -" << setw(3);
    for (int c = 0; c < 4; c++)
    {
        cout << "---" << setw(3);
    }
    cout << "\n";

        cout << setw(2) << "|" ;
        cout << setw(10) << "User: " << user << setw(3);
        cout << "|" << setw(3) << "\n";

        cout << setw(2) << "|" ;
        cout << setw(10)<< "V.S." << setw(4);
        cout << "|" << setw(3) << "\n";

        cout << setw(2) << "|" ;
        cout << setw(10) << "Computer: " << comp << setw(3);
        cout << "|" << setw(3) << "\n";


    cout << "  -" << setw(3);
    for (int c = 0; c < 4; c++)
    {
        cout << "---" << setw(3);
    }
    cout <<  "\n";
    cout << endl;
}

