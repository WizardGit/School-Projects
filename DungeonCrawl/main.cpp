/*
    Program Name: Dungeon Crawl
    Author: Kaiser Slocum
    Date: 1/21/2019
    All Rights Reserved
*/
#include <iostream>
#include <cstdlib> // for random numbers
#include <ctime> // so that the random numbers are different every time
#include <iomanip> // for manipulating the interface
#include <limits> // for random numbers or something

using namespace std;

const int SIZE = 10; // Determines the size of the dungeon SIZE x SIZE
const int NUMTRAPS = 20; // Determines the number of traps you want in your dungeon
const int NUMONSTERS = 10; // Determines the number of monsters you want in your dungeon
bool terminat = false; // Determines if the program should end because you won/lost
bool haveRelic = false; // Determines if you have the relic
const char PLAYER = 'P';
const char TREASURE = 'X';
const char TRAP = 'T';
const char RELIC = 'R';
const char MONSTER = 'M';
const char EMPTYSPACE = '.';
const char UP = 'u';
const char DOWN = 'd';
const char RIGHT = 'r';
const char LEFT = 'l';


// Finds where the specified object is in the dungeon
void findObject(char dungeon[SIZE][SIZE], int rc[2], char object)
{
    for (int r = 0; r < SIZE; r++)
    {
        for (int c = 0; c < SIZE; c++)
        {
            if (dungeon[r][c] == object)
            {
                rc[0] = r;
                rc[1] = c;
            }
        }
    }
}

// Finds where the specified object is in the dungeon
int findEveryMonster(char dungeon[SIZE][SIZE], int monsters[NUMONSTERS][2])
{
    int monster = 0;

    for (int r = 0; r < SIZE; r++)
    {
        for (int c = 0; c < SIZE; c++)
        {
            if (dungeon[r][c] == MONSTER)
            {
                monsters[monster][0] = r;
                monsters[monster][1] = c;
                monster++;
            }
        }
    }
    return monster;
}

// Checks if a monster can move onto the specified square
bool canMonsterMove(char dungeon[SIZE][SIZE], int r, int c)
{
    if ((r >= SIZE) || (r < 0) || (c >= SIZE) || (c < 0))
        return false;
    else if (dungeon[r][c] == EMPTYSPACE)
        return true;
    else
        return false;
}

// Moves monster at row and column do random, adjacent square
void moveMonster(char dungeon[SIZE][SIZE], int r, int c)
{
    bool complete = false;
    int value = -1;

    while(complete == false)
    {
        value = rand() % 5;

        if ((value == 0) && (canMonsterMove(dungeon, r-1, c) == true))
        {
            // Move up
            dungeon[r][c] = EMPTYSPACE;
            dungeon[r-1][c] = MONSTER;
            complete = true;
        }
        else if ((value == 1) && (canMonsterMove(dungeon, r+1, c) == true))
        {
            // Move down
            dungeon[r][c] = EMPTYSPACE;
            dungeon[r+1][c] = MONSTER;
            complete = true;
        }
        else if ((value == 2) && (canMonsterMove(dungeon, r, c-1) == true))
        {
            // Move left
            dungeon[r][c] = EMPTYSPACE;
            dungeon[r][c-1] = MONSTER;
            complete = true;
        }
        else if ((value == 3) && (canMonsterMove(dungeon, r, c+1) == true))
        {
            // Move right
            dungeon[r][c] = EMPTYSPACE;
            dungeon[r][c+1] = MONSTER;
            complete = true;
        }
        else if (value == 4)
        {
            // Don't move
            complete = true;
        }
    }
}

// Moves all the monsters in the specified dungeon
void moveMonsters(char dungeon[SIZE][SIZE])
{
    int monsters[NUMONSTERS][2];
    int monstersLeft = findEveryMonster(dungeon, monsters);

    for (int r = 0; r < monstersLeft; r++)
    {
        moveMonster(dungeon, monsters[r][0], monsters[r][1]);
    }
}

// Creates and places the monsters in the dungeon
void createMonsters(char dungeon[SIZE][SIZE])
{
    int r = -1;
    int c = -1;

    for (int t = 0; t < NUMONSTERS; t++)
    {
        do
        {
            r = rand() % SIZE;
            c = rand() % SIZE;
        }while (dungeon[r][c] != EMPTYSPACE);
        dungeon [r][c] = MONSTER;
    }
}

// Creates the relic and places it in the dungeon
void createRelic(char dungeon[SIZE][SIZE])
{
    int r = -1;
    int c = -1;

    do
    {
        r = rand() % SIZE;
        c = rand() % SIZE;
    }while (dungeon[r][c] != EMPTYSPACE);
    dungeon [r][c] = RELIC;
}

// Creates the traps and places them in the dungeon
void createTraps(char dungeon[SIZE][SIZE])
{
    int r = -1;
    int c = -1;

    for (int t = 0; t < NUMTRAPS; t++)
    {
        do
        {
            r = rand() % SIZE;
            c = rand() % SIZE;
        }while (dungeon[r][c] != EMPTYSPACE);
        dungeon [r][c] = TRAP;
    }
}

// Creates the dungeon
void createDungeon(char dungeon[SIZE][SIZE])
{
    int r = -1;
    int c = -1;

    for (int r = 0; r < SIZE; r++)
    {
        for (int c = 0; c < SIZE; c++)
        {
            dungeon[r][c] = EMPTYSPACE; // First places . everywhere
        }
    }

    dungeon [0][0] = PLAYER; // Always places the explorer at index 0,0

    do
    {
        r = rand() % SIZE;
        c = rand() % SIZE;
    }while (dungeon[r][c] != EMPTYSPACE);
    dungeon [r][c] = TREASURE; // Places the treasure at a random space

    createTraps(dungeon); // Calls the function that creates the traps
    createRelic(dungeon); // Calls the function that creates the relic
    createMonsters(dungeon); // Calls the function that creates the monsters
}

// Displays the current state of the dungeon
void displayDungeon(char dungeon[SIZE][SIZE])
{
    cout << " -" << setw(3);
    for (int c = 0; c < SIZE; c++)
    {
        cout << "---" << setw(3);
    }
    cout << "-  " << setw(3) << "\n";

    for (int r = 0; r < SIZE; r++)
    {
        cout << "|" << setw(3);
        for (int c = 0; c < SIZE; c++)
        {
            cout << dungeon[r][c] << setw(3);
        }
        cout << "|" << setw(3) << "\n";
    }

    cout << " -" << setw(3);
    for (int c = 0; c < SIZE; c++)
    {
        cout << "---" << setw(3);
    }
    cout << "-  " << setw(3) << "\n";
}

// Displays the results of what happens when the explorer moves
bool checkMoveDisplayResult(char dungeon[SIZE][SIZE], int r, int c)
{
    if (r >= SIZE || r < 0 || c >= SIZE || c < 0)
    {
        cout << "\nBecause it is so dark, you ran into a wall and were not able to move!\n" << endl;
        return false;
    }
    else if (dungeon[r][c] == TRAP)
    {
        cout << "\nYou lost the game because you were killed by a trap!\n" << endl;
        terminat = true;
        return true;
    }
    else if (dungeon[r][c] == MONSTER)
    {
        if (haveRelic == true)
        {
            cout << "\nYou have destroyed the monster with your bludgeon and may carry on!\n" << endl;
        }
        else
        {
            cout << "\nYou ran into a monster and were killed!\n" << endl;
            terminat = true;
        }
        return true;
    }
    else if (dungeon[r][c] == TREASURE)
    {
        cout << "\nYou won the game because you found the treasure!\n" << endl;
        terminat = true;
        return true;
    }
    else if (dungeon[r][c] == RELIC)
    {
        cout << "\nYou have found and collected an old relic, a bludgeon in a dungeon!\n" << endl;
        haveRelic = true;
        return true;
    }
    else if (dungeon[r][c] == EMPTYSPACE)
    {
        cout << "\nYou journeyed forward!\n" << endl;
        return true;
    }
    else
    {
        cout << "Something went wrong in checkMoveDisplayResult!" << endl;
        return false;
    }
}

void movePlayer(char dungeon[SIZE][SIZE], int rc[2])
{
    int rco[2];
    findObject(dungeon, rco, PLAYER);

    dungeon[rc[0]][rc[1]] = PLAYER;
    dungeon[rco[0]][rco[1]] = EMPTYSPACE;
}

// Moves the player
void makePlayerMove(char dungeon[SIZE][SIZE], char d)
{
    int rc[2];
    findObject(dungeon, rc, PLAYER);

    if (d == UP)
    {
        rc[0]--;
        if (checkMoveDisplayResult(dungeon, rc[0], rc[1]) == true)
            movePlayer(dungeon, rc);
    }
    else if (d == DOWN)
    {
        rc[0]++;
        if (checkMoveDisplayResult(dungeon, rc[0], rc[1]) == true)
            movePlayer(dungeon, rc);
    }
    else if (d == LEFT)
    {
        rc[1]--;
        if (checkMoveDisplayResult(dungeon, rc[0], rc[1]) == true)
            movePlayer(dungeon, rc);
    }
    else if (d == RIGHT)
    {
        rc[1]++;
        if (checkMoveDisplayResult(dungeon, rc[0], rc[1]) == true)
            movePlayer(dungeon, rc);
    }
    else
        cout << "Unrecognizable direction for player in movePlayer!";
}

// Gets and validates the direction that the explorer wants to move
char getMoveDirection()
{
    char d = 's';

    do
    {
        cout << "Please type in a valid direction to move (U,D,L,R)";
        cin >> d;
        cin.clear();
        cin.ignore(numeric_limits<streamsize>::max(), '\n');
    }while((d != UP) && (d != DOWN) && (d != LEFT) && (d != RIGHT) && (d != 'U') && (d != 'D') && (d != 'L') && (d != 'R'));

    d = tolower(d);
    return d;
}

// Inputs if the user wants to play again and returns y(yes) or n(no)
char playAgain(char answer)
{
    do
    {
        cout << "Do you want to play again? (Y/N)" << endl;
        cin >> answer;
        cin.clear();
        cin.ignore(numeric_limits<streamsize>::max(),'\n');
    }while((answer != 'Y') && (answer!= 'N') && (answer != 'y' ) && (answer!= 'n'));
    answer = tolower(answer);
    return answer;
}

// Displays the game's instructions
void displayInstructions()
{
    cout << "\n\n";
    cout << "Your Journey Has Begun." << endl;
    cout << "You are an intrepid explorer searching for a long lost and forgotten treasure." << endl;
    cout << "As the explorer(P) you must find the treasure(X)." << endl;
    cout << "You must also avoid the traps(T) or monsters(M) or you will die." << endl;
    cout << "There may be a relic(R) that you will find which could help you!" << endl;
    cout << "To move enter in d(down),u(up),l(left),r(right)." << endl;
    cout << "Good luck, explorer!" << endl;
    cout << "\n\n";
}

// Connects all the functions together
int main()
{
    char answer;
    char d;
    char dungeon[SIZE][SIZE];
    srand(static_cast<unsigned>(time(0)));

    do
    {
        terminat = false;
        haveRelic = false;
        d = 'x';
        answer = 'x';

        createDungeon(dungeon);
        displayInstructions();
        displayDungeon(dungeon);

        do
        {
            d = getMoveDirection();
            makePlayerMove(dungeon, d);
            moveMonsters(dungeon);
            displayDungeon(dungeon);
        }while(terminat == false);

        answer = playAgain(answer);
    }while (answer != 'n');

    return 0;
}
