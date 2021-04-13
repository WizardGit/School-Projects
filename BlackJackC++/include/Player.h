/*
 *  Author: Kaiser Slocum
 *  Last edited: 3/19/2019
 *  All rights reserved by God who made me.
 */

#ifndef PLAYER_H
#define PLAYER_H
#include "BlackJackHand.h"
#include <iostream>

using namespace std;


class Player
{
    protected:
        BlackJackHand * theHand;

    private:

        int stake;
        int bet;

    public:
        Player();
        Player(int s);
        virtual ~Player();

        bool takeCard(PlayingCard*c);
        string showHand();
        int getLowScore();
        int getHighScore();
        int getScore();
        bool busted();
        bool wantCard();
        void clearHand();
        void setStake(int s);
        int getStake();
        bool makeBet(int bet);
        void won();
        void lost();
};

#endif // PLAYER_H
