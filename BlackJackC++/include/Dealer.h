/*
 *  Author: Kaiser Slocum
 *  Last edited: 3/19/2019
 *  All rights reserved by God who made me.
 */

#ifndef DEALER_H
#define DEALER_H
#include "Player.h"
#include "PlayingCardDeck.h"
#include <iostream>

using namespace std;

class Dealer : public Player
{
    protected:

    private:
        PlayingCardDeck * theDeck;

    public:
        Dealer();
        Dealer(int numShuffle);
        virtual ~Dealer();

        virtual string showHand();
        string fullHand();
        PlayingCard * dealCard();
        int cardsLeft();
        void shuffle();
};

#endif // DEALER_H
