/*
 *  Author: Kaiser Slocum
 *  Last edited: 3/19/2019
 *  All rights reserved by God who made me.
 */

#include "Dealer.h"

// Sets theDeck to a Playing Card Deck
Dealer::Dealer() : Player()
{
    theDeck = new PlayingCardDeck();
}
// Sets theDeck to a Playing Card Deck and shuffles it numShuffle times
Dealer::Dealer(int numShuffle) : Player()
{
    theDeck = new PlayingCardDeck;
    theDeck->shuffle(numShuffle);
}
// Deletes the PayingCardDeck
Dealer::~Dealer()
{
    delete theDeck;
}

// Shows the Dealer hand except for the first card represented by XX
string Dealer::showHand()
{
    /*
     * There are two ways of doing this.  You can either call the base class' showHand method and
     * return the modified string, or you can make the "theHand" variable in the base class protected
     * and directly use it.  I chose to do the latter, since I am learning about protected.
     */
    //string d = this->Player::showHand();
    string d = theHand->getAllCardCodes();
    d[0] = 'X';
    d[1] = 'X';
    return d;

}
// Shows every card in the Dealer's hand
string Dealer::fullHand()
{
    /*
     * There are two ways of doing this.  You can either return the base class' showHand method,
     * or you can make the "theHand" variable in the base class protected and directly use it.
     * I chose to do the latter, since I am learning about protected.
     */
    //return this->Player::showHand();
    return theHand->getAllCardCodes();
}
// Deals a card from theDeck
PlayingCard * Dealer::dealCard()
{
    return theDeck->dealCard();
}
// Returns the number of cards left in the deck
int Dealer::cardsLeft()
{
    return theDeck->getCountRemain();
}
// Resets the cards in theDeck and shuffles them thrice.
void Dealer::shuffle()
{
    theDeck->reset();
    theDeck->shuffle(3);
}
