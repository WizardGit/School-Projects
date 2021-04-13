using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Example Yahtzee website if you've never played
// https://cardgames.io/yahtzee/

namespace Yahtzee
{
    public partial class yahtzeeForm : Form
    {
        public yahtzeeForm()
        {
            InitializeComponent();
        }

        // you may find these helpful in manipulating the scorecard and in other places in your code
        private const int NONE = -1;
        private const int ONES = 0;
        private const int TWOS = 1;
        private const int THREES = 2;
        private const int FOURS = 3;
        private const int FIVES = 4;
        private const int SIXES = 5;
        private const int THREE_OF_A_KIND = 6;
        private const int FOUR_OF_A_KIND = 7;
        private const int FULL_HOUSE = 8;
        private const int SMALL_STRAIGHT = 9;
        private const int LARGE_STRAIGHT = 10;
        private const int CHANCE = 11;
        private const int YAHTZEE = 12;

        private int rollCount = 0;
        private int uScoreCardCount = 0;        

        // Instance variable for the user's scorecard - an array of 13 ints
        int[] userScoreCard = new int[13];
        // Instance variable for the computer's scorecard -  an array of 13 ints
        int[] computerScoreCard = new int[13];
        // Instance variable for 0 to 5 dice as the user/computer rolls
        List<int> roll = new List<int>();
        // Instance variable for 0 to 5 dice that the user/computer wants to keep 
        List<int> keep = new List<int>();             
        
        // This method rolls numDie and puts the results in the list
        public void Roll(int numDie, List<int> dice)
        {
            dice.Clear();

            Random rand = new Random();

            int counter = 0;

            while (counter < numDie)
            {              
                dice.Add(rand.Next(1, 7));
                counter++;
            }
            while (dice.Count < 5)
            {
                dice.Add(-1);
            }           
        }

        // This method moves all of the rolled dice to the keep dice before scoring.  All of the dice that
        // are being scored have to be in the same list 
        public void MoveRollDiceToKeep(List<int> roll, List<int> keep)
        {           
            CollapseDice(roll);
            CollapseDice(keep);
           
            foreach (int i in roll)
            {
                keep.Add(i);
            }           
        }

        #region Scoring Methods
        //This method returns the number of times the parameter value occurs in the list of dice.
        // Calling it with 5 and the following dice 2, 3, 5, 5, 6 returns 2.
        private int Count(int value, List<int> dice)
        {
            int numValue = 0;

            foreach(int element in dice)
            {
                if (element == value)
                {
                    numValue++;
                }               
            }
            return numValue;
        }

        // This method counts how many 1s, 2s, 3s... 6s there are in a list of ints that represent a set of dice
        // It takes a list of ints as it's parameter.  It should create an array of 6 integers to store the counts.
        // It should then call Count with a value of 1 and store the result in the first element of the array.
        // It should then repeat the process of calling Count with 2 - 6.
        // It returns the array of counts.
        // All of the rest of the scoring methods can be "easily" calculated using the array of counts.
        private int[] GetCounts(List<int> dice)
        {
            int[] counts = new int[6];

            int diNumber = 1; ;

            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = Count(diNumber, dice);
                diNumber++;
            }
            return counts;
        }

        // Each of these methods takes the array of counts as a parameter and returns the score for a dice value.         
        private int ScoreOnes(int[] counts)
        {
            return (counts[0] * 1);
        }

        private int ScoreTwos(int[] counts)
        {
            return (counts[1] * 2);
        }

        private int ScoreThrees(int[] counts)
        {
            return (counts[2] * 3);
        }

        private int ScoreFours(int[] counts)
        {
            return (counts[3] * 4);
        }

        private int ScoreFives(int[] counts)
        {
            return (counts[4] * 5);
        }

        private int ScoreSixes(int[] counts)
        {            
            return (counts[5] * 6);
        }
        // This method calls HasCount(3...) and if there are 3 of a kind calls Sum to calculate the score.        
        private int ScoreThreeOfAKind(int[] counts)
        {
            int whichValue;

            if (HasCount(3, counts, out whichValue) == true)
            {             
                return Sum(counts);
            }
            else
            {          
                return 0;
            }
        }

        private int ScoreFourOfAKind(int[] counts)
        {
            int whichValue;

            if (HasCount(4, counts, out whichValue) == true)
            {              
                return Sum(counts);
            }
            else
            {              
                return 0;
            }
        }

        // This method calls HasCount(2 and HasCount(3 to determine if there's a full house.   
        // It returns 25 if there is a full house
        private int ScoreFullHouse(int[] counts)
        {          
            int whichValueOne;
            int whichValueTwo;

            if (HasCount(2, counts, out whichValueOne) == true && HasCount(3, counts, out whichValueTwo) == true)
            {                
                return 25;
            }
            else
            {            
                return 0;
            }
        }

        private int ScoreSmallStraight(int[] counts)
        {            
            int pastNumber = counts[0];
            if (counts[0] >= 1 &&
                counts[1] >= 1 &&
                counts[2] >= 1 &&
                counts[3] >= 1)
            {               
                return 30;
            }
            else if (counts[1] >= 1 &&
                    counts[2] >= 1 &&
                    counts[3] >= 1 &&
                    counts[4] >= 1)
            {             
                return 30;
            }
            else if (counts[2] >= 1 &&
                counts[3] >= 1 &&
                counts[4] >= 1 &&
                counts[5] >= 1)
            {               
                return 30;
            }
            else
            {               
                return 0;
            }          
        }

        private int ScoreLargeStraight(int[] counts)
        {           
            int pastNumber = counts[0];
            if (counts[1] >= 1 &&
                counts[2] >= 1 &&
                counts[3] >= 1 &&
                counts[4] >= 1)
            {
                if (counts[0] == 0 && counts[5] == 0)
                {                    
                    return 0;
                }
                else
                {                 
                    return 40;
                }
            }
            else
            {               
                return 0;
            }
        }

        private int ScoreChance(int[] counts)
        {           
            return Sum(counts);
        }

        private int ScoreYahtzee(int[] counts)
        {
            int whichValue;

            if (HasCount(5, counts, out whichValue) == true)
            {               
                return 50;
            }
            else
            {             
                return 0;
            }
        }

       // This method can be used to determine if you have 3 of a kind(or 4? or  5?).  The output parameter
       // whichValue tells you which dice value is 3 of a kind.
        private bool HasCount(int howMany, int[] counts, out int whichValue)
        {
            int index = ONES;
            foreach (int count in counts)
            {
                if (howMany <= count)
                {
                    whichValue = index;
                    return true;
                }
            }
            whichValue = NONE;
            return false;
        }

        // This method returns the sum of the dice represented in the counts array.
        // The sum is the # of 1s * 1 + the # of 2s * 2 + the number of 3s * 3 etc          
        private int Sum(int[] counts)
        {
            int sum = 0;

            for (int i = 0; i < counts.Length; i++)
            {
                sum = sum + (counts[i] * (i+1));
            }
           
            return sum;
        }

        // This method makes it "easy" to call the "right" scoring method when you click on an element
        // in the user score card on the UI.
        private int Score(int whichElement, List<int> dice)
        {
            int[] counts = GetCounts(dice);           

            switch (whichElement)
            {
                case ONES:
              
                    return ScoreOnes(counts);
                case TWOS:
                
                    return ScoreTwos(counts);
                case THREES:
                 
                    return ScoreThrees(counts);
                case FOURS:
               
                    return ScoreFours(counts);
                case FIVES:
                
                    return ScoreFives(counts);
                case SIXES:
                    //MessageBox.Show(ScoreSixes(counts).ToString());
                    return ScoreSixes(counts);
                case THREE_OF_A_KIND:
                    return ScoreThreeOfAKind(counts);
                case FOUR_OF_A_KIND:
                    return ScoreFourOfAKind(counts);
                case FULL_HOUSE:
                    return ScoreFullHouse(counts);
                case SMALL_STRAIGHT:
                    return ScoreSmallStraight(counts);
                case LARGE_STRAIGHT:
                    return ScoreLargeStraight(counts);
                case CHANCE:
                    return ScoreChance(counts);
                case YAHTZEE:
                    return ScoreYahtzee(counts);
                default:
                    return 0;
            }
        }
        #endregion

        // set each value to some negative number because 
        // a 0 or a positive number could be an actual score
        private void ResetScoreCard(int[] scoreCard)
        {
            for (int i = 0; i < scoreCard.Length; i++)
            {
                scoreCard[i] = -1;
            }
            uScoreCardCount = 0;
        }        

        // this set has to do with user's scorecard UI
        private void ResetUserUIScoreCard()
        {
            for (int i = 0; i < 13; i++)
            {
                Label lab = (Label)this.scoreCardPanel.Controls["user" + i];
                lab.Text = "";
                lab.Enabled = true;
            }
            for (int i = 0; i < 13; i++)
            {
                Label lab = (Label)this.scoreCardPanel.Controls["computer" + i];
                lab.Text = "";                
            }            
            userBonus.Text = "";
            userSum.Text = "";
            userTotalScore.Text = "";
            computerBonus.Text = "";
            computerSum.Text = "";
            computerTotalScore.Text = "";

            userBonus.Enabled = true;
            userSum.Enabled = true;
            userTotalScore.Enabled = true;
            
            ResetScoreCard(userScoreCard);            
        }

        private void ResetList(List<int> reset)
        {
            reset.Clear();            
            for (int i = 0; i < 5; i++)
            {
                reset.Add(-1);               
            }
        }

        // this method adds the subtotals as well as the bonus points when the user is done playing
        public void UpdateUserUIScoreCard()
        {          
            int sumUser = 0;
            int sumComputer = 0;

            for (int i = 0; i < 6; i++)
            {
                sumUser += userScoreCard[i];
                sumComputer += computerScoreCard[i];
            }
            userSum.Text = sumUser.ToString();
            computerSum.Text = sumComputer.ToString();

            if (sumUser >= 63)
            {
                sumUser += 35;
                userBonus.Text = "35";
            }
            else
            {
                userBonus.Text = "0";
            }
            if (sumComputer >= 63)
            {
                sumComputer += 35;
                computerBonus.Text = "35";
            }
            else
            {
                computerBonus.Text = "0";
            }

            for (int i = 6; i < 13; i++)
            {
                sumUser += userScoreCard[i];
                sumComputer += computerScoreCard[i];
            }
            userTotalScore.Text = sumUser.ToString();
            computerTotalScore.Text = sumComputer.ToString();
        }

        // When I move a die from roll to keep, I put a -1 in the spot that the die used to be in.
        // This method gets rid of all of those -1s in the list.
        private void CollapseDice(List<int> dice)
        {
            int numDice = dice.Count;
            for (int count = 0, i = 0; count < numDice; count++)
            {
                if (dice[i] == -1)
                    dice.RemoveAt(i);
                else
                    i++;
            }
        }

        // When I move a die from roll to keep, I need to know which pb I can use.
        // It's the first spot with a -1 in it
        public int GetFirstAvailablePB(List<int> dice)
        {
            return dice.IndexOf(-1);
        }

        private void ComputerMove()
        {
            //System.Threading.Thread.Sleep(1000);
            if (uScoreCardCount <= 6)
            {
                TestFirstHalf();
            }
            else
            {
                Roll(5, roll);

                HideAllRollDice();
                ShowAllRollDie();

                TestLastHalf();   
                
                MessageBox.Show("Done!");
                HideAllRollDice();
            }

            // when it's the end of the game
            if (uScoreCardCount == 13)
            {
                // enable/disable buttons
                rollButton.Enabled = false;
                // update the sum(s) and bonus parts of the score card
                UpdateUserUIScoreCard();
                // display a message box?
                MessageBox.Show("The game has ended!");
            }
        }

        private void TestFirstHalf()
        {
            Roll(5, roll);
            ShowAllRollDie();
            MessageBox.Show("After I rolled the dice.");

            Label lab;
            int biggest = -1;
            int[] numbers = GetCounts(roll);

            // Finds the first available label and stores its amount of die in the variable biggest
            for (int i = 0; i < numbers.Length; i++)
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + i];
                if (lab.Text == "" && numbers[i] != 0)
                {
                    biggest = numbers[i];
                    break;
                }
            }            

            if (biggest > 0)
            {
                Scoring(2);                
            }
            else
            {
                Roll(5, roll);
                ShowAllRollDie();
                MessageBox.Show("After I rolled the dice.");
                biggest = -1;
                numbers = GetCounts(roll);

                // Finds the first available label and stores its amount of die in the variable biggest
                for (int i = 0; i < numbers.Length; i++)
                {
                    lab = (Label)this.scoreCardPanel.Controls["computer" + i];
                    if (lab.Text == "" && numbers[i] != 0)
                    {
                        biggest = numbers[i];
                        break;
                    }
                }
                if (biggest > 0)
                {
                    Scoring(1);
                }
                else
                {
                    Roll(5, roll);
                    ShowAllRollDie();
                    MessageBox.Show("After I rolled the dice.");
                    biggest = -1;
                    numbers = GetCounts(roll);

                    // Finds the first available label and stores its amount of die in the variable biggest
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        lab = (Label)this.scoreCardPanel.Controls["computer" + i];
                        if (lab.Text == "" && numbers[i] != 0)
                        {
                            biggest = numbers[i];
                            break;
                        }
                    }
                    if (biggest > 0)
                    {
                        Scoring(0);
                    }
                    else
                    {
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            lab = (Label)this.scoreCardPanel.Controls["computer" + i];

                            if (lab.Text == "")
                            {
                                computerScoreCard[i] = 0;
                                lab.Text = computerScoreCard[i].ToString();
                            }
                        }
                    }
                }
            }
            void Scoring(int timesToRoll)
            {
                // Finds the biggest amount of die of one number
                for (int i = 0; i < numbers.Length; i++)
                {
                    lab = (Label)this.scoreCardPanel.Controls["computer" + i];

                    if (numbers[i] > biggest && lab.Text == "")
                    {
                        biggest = numbers[i];
                    }
                }

                // Scores the biggest amount of die of one number
                for (int i = 0; i < numbers.Length; i++)
                {
                    lab = (Label)this.scoreCardPanel.Controls["computer" + i];

                    if (numbers[i] == biggest && lab.Text == "")
                    {
                        for (int counter = 0; counter < timesToRoll; counter++)
                        {
                            RollToComputerKeep(i + 1);
                            MessageBox.Show("After I rolled the dice and kept what I wanted.");

                            int numberNotToRoll = 0;
                            foreach (int value in keep)
                            {
                                if (value != -1)
                                {
                                    numberNotToRoll++;
                                }
                            }
                            Roll((5 - numberNotToRoll), roll);

                            HideAllRollDice();
                            ShowAllRollDie();
                            MessageBox.Show("After I rolled the dice.");
                        }
                        RollToComputerKeep(i + 1);
                        MessageBox.Show("After I rolled the dice and kept what I wanted.");

                        lab.Text = Score(i, keep).ToString();
                        computerScoreCard[i] = int.Parse(lab.Text);
                        HideAllComputerKeepDice();
                        HideAllRollDice();
                        ResetList(roll);
                        ResetList(keep);
                        break;
                    }
                }
            }            
        }

        private void TestLastHalf()
        {
            Label lab = (Label)this.scoreCardPanel.Controls["computer0"];
            int[] array = new int[5];
            array = GetCounts(roll);

            if (ThreeOfAKind(array) == true && computer6.Text == "")
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + 6];
                lab.Text = Score(6, roll).ToString();
                computerScoreCard[6] = int.Parse(lab.Text);
            }
            else if (FourOfAKind(array) == true && computer7.Text == "")
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + 7];
                lab.Text = Score(7, roll).ToString();
                computerScoreCard[7] = int.Parse(lab.Text);
            }
            else if (FullHouse(array) == true && computer8.Text == "")
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + 8];
                lab.Text = Score(8, roll).ToString();
                computerScoreCard[8] = int.Parse(lab.Text);
            }
            else if (SmallStraight(array) == true && computer9.Text == "")
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + 9];
                lab.Text = Score(9, roll).ToString();
                computerScoreCard[9] = int.Parse(lab.Text);
            }
            else if (LargeStraight(array) == true && computer10.Text == "")
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + 10];
                lab.Text = Score(10, roll).ToString();
                computerScoreCard[10] = int.Parse(lab.Text);
            }
            else if (Yahtzee(array) == true && computer12.Text == "")
            {
                lab = (Label)this.scoreCardPanel.Controls["computer" + 12];
                lab.Text = Score(12, roll).ToString();
                computerScoreCard[12] = int.Parse(lab.Text);
            }
            else
            {
                Random rand = new Random();
                int index = 0;
                while (lab.Text != "")
                {
                    index = rand.Next(6, 13);
                    lab = (Label)this.scoreCardPanel.Controls["computer" + index];
                }
                lab.Text = Score(index, roll).ToString();
                computerScoreCard[index] = int.Parse(lab.Text);
            }
            bool ThreeOfAKind(int[] counts)
            {
                int whichValue;

                if (HasAmount(3, counts, out whichValue) == true)
                    return true;
                else
                    return false;
            }
            bool FourOfAKind(int[] counts)
            {
                int whichValue;

                if (HasAmount(4, counts, out whichValue) == true)
                    return true;
                else
                    return false;
            }
            bool FullHouse(int[] counts)
            {
                int whichValueOne;
                int whichValueTwo;

                if (HasAmount(2, counts, out whichValueOne) == true && HasAmount(3, counts, out whichValueTwo) == true)
                    return true;
                else
                    return false;
            }
            bool SmallStraight(int[] counts)
            {
                int pastNumber = counts[0];
                if (counts[0] >= 1 &&
                    counts[1] >= 1 &&
                    counts[2] >= 1 &&
                    counts[3] >= 1)
                {
                    return true;
                }
                else if (counts[1] >= 1 &&
                        counts[2] >= 1 &&
                        counts[3] >= 1 &&
                        counts[4] >= 1)
                {
                    return true;
                }
                else if (counts[2] >= 1 &&
                    counts[3] >= 1 &&
                    counts[4] >= 1 &&
                    counts[5] >= 1)
                {
                    return true;
                }
                else
                    return false;
            }
            bool LargeStraight(int[] counts)
            {
                int pastNumber = counts[0];
                if (counts[1] >= 1 &&
                    counts[2] >= 1 &&
                    counts[3] >= 1 &&
                    counts[4] >= 1)
                {
                    if (counts[0] == 0 && counts[5] == 0)
                        return false;
                    else
                        return true;
                }
                else
                    return false;

            }
            bool Yahtzee(int[] counts)
            {
                int whichValue;

                if (HasAmount(5, counts, out whichValue) == true)
                    return true;
                else
                    return false;
            }
            bool HasAmount(int howMany, int[] counts, out int whichValue)
            {
                int index = ONES;
                foreach (int count in counts)
                {
                    if (howMany <= count)
                    {
                        whichValue = index;
                        return true;
                    }
                }
                whichValue = NONE;
                return false;
            }
        }

        private void RollToComputerKeep(int diceValue)
        {
            // figure out where in the set of keep picture boxes there's a "space"
            int available = GetFirstAvailablePB(keep);

            for (int i = 0; i < roll.Count; i++)
            {
                if (roll[i] == diceValue)
                {
                    available = GetFirstAvailablePB(keep);
                    keep[available] = roll[i];
                    HideRollDie(i);
                    ShowComputerKeepDie(available);
                    roll[i] = -1;
                }
            }
        }

        #region UI Dice Methods
        /* These are all UI methods */
        private PictureBox GetKeepDie(int i)
        {
            PictureBox die = (PictureBox)this.Controls["keep" + i];
            return die;
        }

        public void HideKeepDie(int i)
        {
            GetKeepDie(i).Visible = false;
        }

        public void HideAllKeepDice()
        {
            for (int i = 0; i < 5; i++)
                HideKeepDie(i);
        }

        public void ShowKeepDie(int i)
        {
            PictureBox die = GetKeepDie(i);            
            die.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Dice\\die" + keep[i] + ".png");
            die.Visible = true;
        }

        public void ShowAllKeepDie()
        {
            for (int i = 0; i < 5; i++)
            {
                if (keep[i] > 0)
                {
                    ShowKeepDie(i);
                }               
            }
        }

        private PictureBox GetComputerKeepDie(int i)
        {
            PictureBox die = (PictureBox)this.Controls["computerKeep" + i];
            return die;
        }

        public void HideComputerKeepDie(int i)
        {
            GetComputerKeepDie(i).Visible = false;
        }

        public void HideAllComputerKeepDice()
        {
            for (int i = 0; i < 5; i++)
                HideComputerKeepDie(i);
        }

        public void ShowComputerKeepDie(int i)
        {
            PictureBox die = GetComputerKeepDie(i);
            die.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Dice\\die" + keep[i] + ".png");
            die.Visible = true;          
        }

        public void ShowAllComputerKeepDie()
        {            
            for (int i = 0; i < 5; i++)
            {
                if (keep[i] > 0)
                {
                    ShowComputerKeepDie(i);
                }              
            }
        }

        private PictureBox GetRollDie(int i)
        {
            PictureBox die = (PictureBox)this.Controls["roll" + i];
            return die;
        }

        public void HideRollDie(int i)
        {
            GetRollDie(i).Visible = false;
        }

        public void HideAllRollDice()
        {
            for (int i = 0; i < 5; i++)
                HideRollDie(i);
        }

        public void ShowRollDie(int i)
        {
            PictureBox die = GetRollDie(i);
            die.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Dice\\die" + roll[i] + ".png");
            die.Visible = true;
        }

        public void ShowAllRollDie()
        {
            for (int i = 0; i < roll.Count; i++)
            {
                if (roll[i] > 0)
                {
                    ShowRollDie(i);
                }
            }
        }
        #endregion

        #region Event Handlers
        private void Form1_Load(object sender, EventArgs e)
        {
            ResetList(roll);
            ResetList(keep);
            //* Hide the roll dice
            HideAllRollDice();
            //* Hide the keep dice
            HideAllKeepDice();
            //* Hide the computer's dice
            HideAllComputerKeepDice();
            // reset the user's scorecard
            ResetUserUIScoreCard();            
        }

        private void rollButton_Click(object sender, EventArgs e)
        {            
            // DON'T WORRY ABOUT ROLLING MULTIPLE TIMES UNTIL YOU CAN SCORE ONE ROLL
            // hide all of the keep picture boxes           
            // any of the die that were moved back and forth from roll to keep by the user
            // are "collapsed" in the keep data structure
            // show the keep dice again

            // START HERE
            // clear the roll data structure

            // hide all of the roll picture boxes
            HideAllRollDice();
            // roll the right number of dice 
            int numberO = 0;
            foreach (int value in keep)
            {
                if (value != -1)
                {
                    numberO++;
                }
            }
            Roll((5 - numberO), roll);
            // show the roll picture boxes
            ShowAllRollDie();
            
            // increment the number of rolls
            rollCount++;

            // disable the button if you've rolled 3 times
            if (rollCount == 3)
            {
                rollButton.Enabled = false;                
            }            
        }

        private void userScorecard_DoubleClick(object sender, EventArgs e)
        {
            // move any rolled die into the keep dice
            MoveRollDiceToKeep(roll, keep);

            // hide picture boxes for both roll and keep
            HideAllRollDice();
            HideAllKeepDice();           
            rollCount = 0;

            // determine which element in the score card was clicked
            Label clickedLabel = (Label)sender;
            string type = clickedLabel.Name;
            type = type.Remove(0, 4);

            // score that element            
            // put the score in the scorecard and the UI
            clickedLabel.Text = Score(int.Parse(type), keep).ToString();
            userScoreCard[int.Parse(type)] = int.Parse(clickedLabel.Text);

            // disable this element in the score card
            clickedLabel.Enabled = false;

            // clear the keep dice           
            // reset the roll count
            // increment the number of elements in the score card that are full
            uScoreCardCount++;           

            // enable/disable buttons
            rollButton.Enabled = true;

            ResetList(roll);
            ResetList(keep);           
            ComputerMove();
            ResetList(roll);
            ResetList(keep);
        }

        private void roll_DoubleClick(object sender, EventArgs e)
        {
            // figure out which die you clicked on
            PictureBox clickedLabel = (PictureBox)sender;

            // figure out where in the set of keep picture boxes there's a "space"
            int available = GetFirstAvailablePB(keep);

            // move the roll die value from this die to the keep data structure in the "right place"
            // sometimes that will be at the end but if the user is moving dice back and forth
            // it may be in the middle somewhere
            string type = clickedLabel.Name;
            type = type.Remove(0, 4);
            keep[available] = roll[int.Parse(type)];  
            
            //  hide the picture box
            HideRollDie(int.Parse(type));
            ShowKeepDie(available);

            // clear the die in the roll data structure
            roll[int.Parse(type)] = -1;
        }

        private void keep_DoubleClick(object sender, EventArgs e)
        {
            // figure out which die you clicked on
            PictureBox clickedLabel = (PictureBox)sender;
       
            // figure out where in the set of roll picture boxes there's a "space"
            int available = GetFirstAvailablePB(roll);

            // move the roll die value from this die to the roll data structure in the "right place"
            // sometimes that will be at the end but if the user is moving dice back and forth
            // it may be in the middle somewhere
            string type = clickedLabel.Name;
            type = type.Remove(0, 4);
            roll[available] = keep[int.Parse(type)];
            
            // hide the picture box
            HideKeepDie(int.Parse(type));
            ShowRollDie(available);

            // clear the die in the keep data structure
            keep[int.Parse(type)] = -1;            
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            ResetList(roll);
            ResetList(keep);
            //* Hide the roll dice
            HideAllRollDice();
            //* Hide the keep dice
            HideAllKeepDice();
            //* Hide the computer's dice
            HideAllComputerKeepDice();
            // reset the user's scorecard
            ResetUserUIScoreCard();
            rollButton.Enabled = true;            
        }                
        #endregion       
    }
}
