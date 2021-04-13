/*
 *  Author: Kaiser Slocum
 *  Last edited: 3/19/2019
 *  All rights reserved by God who made me.
 */

#include "Player.h"

// sets theHand to a BlackJackHand
Player::Player()
{
    theHand = new BlackJackHand();
    stake = 0;
}
// sets theHand to a BlackJackHand and the stake to the parameter
Player::Player(int s)
{
    theHand = new BlackJackHand();
    stake = s;

}
// Deletes theHand
Player::~Player()
{
    delete theHand;
}

// Adds a card to the player hand
bool Player::takeCard(PlayingCard * c)
{
    return theHand->addCard(c);
}
// Shows the player's hand
string Player::showHand()
{
    return theHand->getAllCardCodes();
}
// Returns the player's lowest possible score
int Player::getLowScore()
{
    return theHand->getLowScore();
}
// Returns the player's highest possible score
int Player::getHighScore()
{
    return theHand->getHighScore();
}
// Returns the player's score
int Player::getScore()
{
    if ((theHand->getHighScore()) > 21)
        return theHand->getLowScore();
    else
        return theHand->getHighScore();
}
// Returns true if the player is busted, false if not
bool Player::busted()
{
    return theHand->isBust();
}
// Returns true if the player's score is less than 16
bool Player::wantCard()
{
    if (getScore() < 16)
        return true;
    else
        return false;
}
// Clears the hand of the player
void Player::clearHand()
{
    theHand->clearHand();
}
// Sets the player's sake according to the parameter
void Player::setStake(int s)
{
    stake = s;
}
// Returns the player's stake
int Player::getStake()
{
    return stake;
}
// Makes the player's bet
bool Player::makeBet(int b)
{
    if (b <= stake)
    {
        bet = b;
        return true;
    }
    else
    {
        bet = 0;
        return false;
    }
}
// Adds the player's bet to his/her stake
void Player::won()
{
    stake += bet;
    bet = 0;
}
// Subracts the player's bet from his/her stake
void Player::lost()
{
    stake -= bet;
    bet = 0;
}
