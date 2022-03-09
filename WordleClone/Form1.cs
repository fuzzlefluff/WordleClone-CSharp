using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordleClone
{
    public partial class MainForm : Form
    {
        WordleClone game;
        private static MainForm form;
        private static bool isEasy = true;

        public MainForm()
        {
            InitializeComponent();
            form = this;
            
            
            //setup listen events
            character1.KeyPress += character1_Key;
            character2.KeyPress += character2_Key;
            character3.KeyPress += character3_Key;
            character4.KeyPress += character4_Key;
            character5.KeyPress += character5_Key;
            
            //set character alignment to the center for all our letter boxes
            character1.SelectionAlignment = HorizontalAlignment.Center;
            character2.SelectionAlignment = HorizontalAlignment.Center;
            character3.SelectionAlignment = HorizontalAlignment.Center;
            character4.SelectionAlignment = HorizontalAlignment.Center;
            character5.SelectionAlignment = HorizontalAlignment.Center;
            entry1Char1.SelectionAlignment = HorizontalAlignment.Center;
            entry1Char2.SelectionAlignment = HorizontalAlignment.Center;
            entry1Char3.SelectionAlignment = HorizontalAlignment.Center;
            entry1Char4.SelectionAlignment = HorizontalAlignment.Center;
            entry1Char5.SelectionAlignment = HorizontalAlignment.Center;
           
            entry2Char1.SelectionAlignment = HorizontalAlignment.Center;
            entry2Char2.SelectionAlignment = HorizontalAlignment.Center;
            entry2Char3.SelectionAlignment = HorizontalAlignment.Center;
            entry2Char4.SelectionAlignment = HorizontalAlignment.Center;
            entry2Char5.SelectionAlignment = HorizontalAlignment.Center;
            
            entry3Char1.SelectionAlignment = HorizontalAlignment.Center;
            entry3Char2.SelectionAlignment = HorizontalAlignment.Center;
            entry3Char3.SelectionAlignment = HorizontalAlignment.Center;
            entry3Char4.SelectionAlignment = HorizontalAlignment.Center;
            entry3Char5.SelectionAlignment = HorizontalAlignment.Center;
            
            entry4Char1.SelectionAlignment = HorizontalAlignment.Center;
            entry4Char2.SelectionAlignment = HorizontalAlignment.Center;
            entry4Char3.SelectionAlignment = HorizontalAlignment.Center;
            entry4Char4.SelectionAlignment = HorizontalAlignment.Center;
            entry4Char5.SelectionAlignment = HorizontalAlignment.Center;
            
            entry5Char1.SelectionAlignment = HorizontalAlignment.Center;
            entry5Char2.SelectionAlignment = HorizontalAlignment.Center;
            entry5Char3.SelectionAlignment = HorizontalAlignment.Center;
            entry5Char4.SelectionAlignment = HorizontalAlignment.Center;
            entry5Char5.SelectionAlignment = HorizontalAlignment.Center;
            
            entry6Char1.SelectionAlignment = HorizontalAlignment.Center;
            entry6Char2.SelectionAlignment = HorizontalAlignment.Center;
            entry6Char3.SelectionAlignment = HorizontalAlignment.Center;
            entry6Char4.SelectionAlignment = HorizontalAlignment.Center;
            entry6Char5.SelectionAlignment = HorizontalAlignment.Center;

            letterboxA.SelectionAlignment = HorizontalAlignment.Center;
            letterboxB.SelectionAlignment = HorizontalAlignment.Center;
            letterboxC.SelectionAlignment = HorizontalAlignment.Center;
            letterboxD.SelectionAlignment = HorizontalAlignment.Center;
            letterboxE.SelectionAlignment = HorizontalAlignment.Center;
            letterboxF.SelectionAlignment = HorizontalAlignment.Center;
            letterboxG.SelectionAlignment = HorizontalAlignment.Center;
            letterboxH.SelectionAlignment = HorizontalAlignment.Center;
            letterboxI.SelectionAlignment = HorizontalAlignment.Center;
            letterboxJ.SelectionAlignment = HorizontalAlignment.Center;
            letterboxK.SelectionAlignment = HorizontalAlignment.Center;
            letterboxL.SelectionAlignment = HorizontalAlignment.Center;
            letterboxM.SelectionAlignment = HorizontalAlignment.Center;
            letterboxN.SelectionAlignment = HorizontalAlignment.Center;
            letterboxO.SelectionAlignment = HorizontalAlignment.Center;
            letterboxP.SelectionAlignment = HorizontalAlignment.Center;
            letterboxQ.SelectionAlignment = HorizontalAlignment.Center;
            letterboxR.SelectionAlignment = HorizontalAlignment.Center;
            letterboxS.SelectionAlignment = HorizontalAlignment.Center;
            letterboxT.SelectionAlignment = HorizontalAlignment.Center;
            letterboxU.SelectionAlignment = HorizontalAlignment.Center;
            letterboxV.SelectionAlignment = HorizontalAlignment.Center;
            letterboxW.SelectionAlignment = HorizontalAlignment.Center;
            letterboxX.SelectionAlignment = HorizontalAlignment.Center;
            letterboxY.SelectionAlignment = HorizontalAlignment.Center;
            letterboxZ.SelectionAlignment = HorizontalAlignment.Center;

            newGame(isEasy);
        }

        private void newgameBtn_Click(object sender, EventArgs e)
        {
            isEasy = true;
            newGame(isEasy);
        }

        private void lockInputBoxes(bool lockInput) 
        {
            character1.ReadOnly = lockInput;
            character2.ReadOnly = lockInput;
            character3.ReadOnly = lockInput;
            character4.ReadOnly = lockInput;
            character5.ReadOnly = lockInput;
        }

        private void enterInput() 
        {
            string r = "";
            r += character1.Text;
            r += character2.Text;
            r += character3.Text;
            r += character4.Text;
            r += character5.Text;

            
            if (game.isWord(r,isEasy)) 
            {
                string s = game.testString(r);
                
                if (s == "You loose! Sorry!" || s == "You Win!") { game.setGuessCount(-1); updateAttempts(r); lockInputBoxes(true); }
                if (s == form.winBox.Text) { newGame(isEasy); return; }
                form.winBox.Text = s;
                form.winBox.ForeColor = Color.White;
                updateAttempts(r);

            }
            else 
            {
                form.winBox.Text = "Not a valid word!";
                form.winBox.ForeColor = Color.Red;
            }
        }

        private void updateAttempts(string input) 
        {
            List<Tuple<int, char>> matchingChars;
            matchingChars = WordleClone.returnMatchingChars(game.giveCheatString(),input);
            bool printSkip = true;

            for(int i =0; i<input.Length; i++)
            {
                for(int c=0; c<matchingChars.Count; c++)
                {
                    if (matchingChars[c].Item2 == input[i])
                    {
                        printSkip = false;
                        if (matchingChars[c].Item1 != -1)
                        {
                            updateAttemptSquare(game.giveGuessCount()+1, i + 1, 2, input[i].ToString());
                            updateLetter(input[i].ToString(), 2);
                        }
                        if (matchingChars[c].Item1 == -1)
                        {
                            updateAttemptSquare(game.giveGuessCount() + 1, i + 1, 1, input[i].ToString());
                            updateLetter(input[i].ToString(), 1);
                        }
                        matchingChars.Remove(matchingChars[c]);
                    }
                    else
                    {
                        
                    }
                }
                if (printSkip) { updateAttemptSquare(game.giveGuessCount() + 1, i + 1, 0, input[i].ToString()); updateLetter(input[i].ToString(), -1); }
                printSkip = true;

            }
        }

        private void updateAttemptSquare(int indexAttempt, int indexChar,int color,string c) 
        
        {
            Color grey = Color.FromArgb(64,64,64);
            Color yellow = Color.Yellow;
            Color green = Color.Green;
            if(color == 0) 
            {
                if(indexAttempt == 5) 
                {
                    if (indexChar == 1) { form.entry1Char1.BackColor = grey; entry1Char1.Text = c; }
                    if (indexChar == 2) { form.entry1Char2.BackColor = grey; entry1Char2.Text = c; }
                    if (indexChar == 3) { form.entry1Char3.BackColor = grey; entry1Char3.Text = c; }
                    if (indexChar == 4) { form.entry1Char4.BackColor = grey; entry1Char4.Text = c; }
                    if (indexChar == 5) { form.entry1Char5.BackColor = grey; entry1Char5.Text = c; }
                }
                if (indexAttempt == 4)
                {
                    if (indexChar == 1) { form.entry2Char1.BackColor = grey; entry2Char1.Text = c; }
                    if (indexChar == 2) { form.entry2Char2.BackColor = grey; entry2Char2.Text = c; }
                    if (indexChar == 3) { form.entry2Char3.BackColor = grey; entry2Char3.Text = c; }
                    if (indexChar == 4) { form.entry2Char4.BackColor = grey; entry2Char4.Text = c; }
                    if (indexChar == 5) { form.entry2Char5.BackColor = grey; entry2Char5.Text = c; }
                }
                if (indexAttempt == 3)
                {
                    if (indexChar == 1) { form.entry3Char1.BackColor = grey; entry3Char1.Text = c; }
                    if (indexChar == 2) { form.entry3Char2.BackColor = grey; entry3Char2.Text = c; }
                    if (indexChar == 3) { form.entry3Char3.BackColor = grey; entry3Char3.Text = c; }
                    if (indexChar == 4) { form.entry3Char4.BackColor = grey; entry3Char4.Text = c; }
                    if (indexChar == 5) { form.entry3Char5.BackColor = grey; entry3Char5.Text = c; }
                }
                if (indexAttempt == 2)
                {
                    if (indexChar == 1) { form.entry4Char1.BackColor = grey; entry4Char1.Text = c; }
                    if (indexChar == 2) { form.entry4Char2.BackColor = grey; entry4Char2.Text = c; }
                    if (indexChar == 3) { form.entry4Char3.BackColor = grey; entry4Char3.Text = c; }
                    if (indexChar == 4) { form.entry4Char4.BackColor = grey; entry4Char4.Text = c; }
                    if (indexChar == 5) { form.entry4Char5.BackColor = grey; entry4Char5.Text = c; }
                }
                if (indexAttempt == 1)
                {
                    if (indexChar == 1) { form.entry5Char1.BackColor = grey; entry5Char1.Text = c; }
                    if (indexChar == 2) { form.entry5Char2.BackColor = grey; entry5Char2.Text = c; }
                    if (indexChar == 3) { form.entry5Char3.BackColor = grey; entry5Char3.Text = c; }
                    if (indexChar == 4) { form.entry5Char4.BackColor = grey; entry5Char4.Text = c; }
                    if (indexChar == 5) { form.entry5Char5.BackColor = grey; entry5Char5.Text = c; }
                }
                if (indexAttempt == 0)
                {
                    if (indexChar == 1) { form.entry6Char1.BackColor = grey; entry6Char1.Text = c; }
                    if (indexChar == 2) { form.entry6Char2.BackColor = grey; entry6Char2.Text = c; }
                    if (indexChar == 3) { form.entry6Char3.BackColor = grey; entry6Char3.Text = c; }
                    if (indexChar == 4) { form.entry6Char4.BackColor = grey; entry6Char4.Text = c; }
                    if (indexChar == 5) { form.entry6Char5.BackColor = grey; entry6Char5.Text = c; }
                }
            }
            if (color == 1) 
            {
                if (indexAttempt == 5)
                {
                    if (indexChar == 1) { form.entry1Char1.BackColor = yellow; entry1Char1.Text = c; }
                    if (indexChar == 2) { form.entry1Char2.BackColor = yellow; entry1Char2.Text = c; }
                    if (indexChar == 3) { form.entry1Char3.BackColor = yellow; entry1Char3.Text = c; }
                    if (indexChar == 4) { form.entry1Char4.BackColor = yellow; entry1Char4.Text = c; }
                    if (indexChar == 5) { form.entry1Char5.BackColor = yellow; entry1Char5.Text = c; }
                }
                if (indexAttempt == 4)
                {
                    if (indexChar == 1) { form.entry2Char1.BackColor = yellow; entry2Char1.Text = c; }
                    if (indexChar == 2) { form.entry2Char2.BackColor = yellow; entry2Char2.Text = c; }
                    if (indexChar == 3) { form.entry2Char3.BackColor = yellow; entry2Char3.Text = c; }
                    if (indexChar == 4) { form.entry2Char4.BackColor = yellow; entry2Char4.Text = c; }
                    if (indexChar == 5) { form.entry2Char5.BackColor = yellow; entry2Char5.Text = c; }
                }
                if (indexAttempt == 3)
                {
                    if (indexChar == 1) { form.entry3Char1.BackColor = yellow; entry3Char1.Text = c; }
                    if (indexChar == 2) { form.entry3Char2.BackColor = yellow; entry3Char2.Text = c; }
                    if (indexChar == 3) { form.entry3Char3.BackColor = yellow; entry3Char3.Text = c; }
                    if (indexChar == 4) { form.entry3Char4.BackColor = yellow; entry3Char4.Text = c; }
                    if (indexChar == 5) { form.entry3Char5.BackColor = yellow; entry3Char5.Text = c; }
                }
                if (indexAttempt == 2)
                {
                    if (indexChar == 1) { form.entry4Char1.BackColor = yellow; entry4Char1.Text = c; }
                    if (indexChar == 2) { form.entry4Char2.BackColor = yellow; entry4Char2.Text = c; }
                    if (indexChar == 3) { form.entry4Char3.BackColor = yellow; entry4Char3.Text = c; }
                    if (indexChar == 4) { form.entry4Char4.BackColor = yellow; entry4Char4.Text = c; }
                    if (indexChar == 5) { form.entry4Char5.BackColor = yellow; entry4Char5.Text = c; }
                }
                if (indexAttempt == 1)
                {
                    if (indexChar == 1) { form.entry5Char1.BackColor = yellow; entry5Char1.Text = c; }
                    if (indexChar == 2) { form.entry5Char2.BackColor = yellow; entry5Char2.Text = c; }
                    if (indexChar == 3) { form.entry5Char3.BackColor = yellow; entry5Char3.Text = c; }
                    if (indexChar == 4) { form.entry5Char4.BackColor = yellow; entry5Char4.Text = c; }
                    if (indexChar == 5) { form.entry5Char5.BackColor = yellow; entry5Char5.Text = c; }
                }
                if (indexAttempt == 0)
                {                                                                   
                    if (indexChar == 1) { form.entry6Char1.BackColor = yellow; entry6Char1.Text = c; }
                    if (indexChar == 2) { form.entry6Char2.BackColor = yellow; entry6Char2.Text = c; }
                    if (indexChar == 3) { form.entry6Char3.BackColor = yellow; entry6Char3.Text = c; }
                    if (indexChar == 4) { form.entry6Char4.BackColor = yellow; entry6Char4.Text = c; }
                    if (indexChar == 5) { form.entry6Char5.BackColor = yellow; entry6Char5.Text = c; }
                }
            }
            if (color == 2)
            {
                if (indexAttempt == 5)
                {
                    if (indexChar == 1) { form.entry1Char1.BackColor = green; entry1Char1.Text = c; }
                    if (indexChar == 2) { form.entry1Char2.BackColor = green; entry1Char2.Text = c; }
                    if (indexChar == 3) { form.entry1Char3.BackColor = green; entry1Char3.Text = c; }
                    if (indexChar == 4) { form.entry1Char4.BackColor = green; entry1Char4.Text = c; }
                    if (indexChar == 5) { form.entry1Char5.BackColor = green; entry1Char5.Text = c; }
                }
                if (indexAttempt == 4)
                {
                    if (indexChar == 1) { form.entry2Char1.BackColor = green; entry2Char1.Text = c; }
                    if (indexChar == 2) { form.entry2Char2.BackColor = green; entry2Char2.Text = c; }
                    if (indexChar == 3) { form.entry2Char3.BackColor = green; entry2Char3.Text = c; }
                    if (indexChar == 4) { form.entry2Char4.BackColor = green; entry2Char4.Text = c; }
                    if (indexChar == 5) { form.entry2Char5.BackColor = green; entry2Char5.Text = c; }
                }
                if (indexAttempt == 3)
                {
                    if (indexChar == 1) { form.entry3Char1.BackColor = green; entry3Char1.Text = c; }
                    if (indexChar == 2) { form.entry3Char2.BackColor = green; entry3Char2.Text = c; }
                    if (indexChar == 3) { form.entry3Char3.BackColor = green; entry3Char3.Text = c; }
                    if (indexChar == 4) { form.entry3Char4.BackColor = green; entry3Char4.Text = c; }
                    if (indexChar == 5) { form.entry3Char5.BackColor = green; entry3Char5.Text = c; }
                }
                if (indexAttempt == 2)
                {
                    if (indexChar == 1) { form.entry4Char1.BackColor = green; entry4Char1.Text = c; }
                    if (indexChar == 2) { form.entry4Char2.BackColor = green; entry4Char2.Text = c; }
                    if (indexChar == 3) { form.entry4Char3.BackColor = green; entry4Char3.Text = c; }
                    if (indexChar == 4) { form.entry4Char4.BackColor = green; entry4Char4.Text = c; }
                    if (indexChar == 5) { form.entry4Char5.BackColor = green; entry4Char5.Text = c; }
                }
                if (indexAttempt == 1)
                {
                    if (indexChar == 1) { form.entry5Char1.BackColor = green; entry5Char1.Text = c; }
                    if (indexChar == 2) { form.entry5Char2.BackColor = green; entry5Char2.Text = c; }
                    if (indexChar == 3) { form.entry5Char3.BackColor = green; entry5Char3.Text = c; }
                    if (indexChar == 4) { form.entry5Char4.BackColor = green; entry5Char4.Text = c; }
                    if (indexChar == 5) { form.entry5Char5.BackColor = green; entry5Char5.Text = c; }
                }
                if (indexAttempt == 0)
                {
                    if (indexChar == 1) { form.entry6Char1.BackColor = green; entry6Char1.Text = c; }
                    if (indexChar == 2) { form.entry6Char2.BackColor = green; entry6Char2.Text = c; }
                    if (indexChar == 3) { form.entry6Char3.BackColor = green; entry6Char3.Text = c; }
                    if (indexChar == 4) { form.entry6Char4.BackColor = green; entry6Char4.Text = c; }
                    if (indexChar == 5) { form.entry6Char5.BackColor = green; entry6Char5.Text = c; }
                }
            }
        }
        
        private void updateLetter(string letter, int color) 
        {
            Color grey = Color.FromArgb(64, 64, 64);
            Color yellow = Color.Khaki;
            Color green = Color.Green;
            Color black = Color.Black;
            Color white = Color.White;

            letter = letter.ToUpper();
            
            if(letter == "A") 
            {
                if (color == -1) { letterboxA.ForeColor = black; }
                else
                {
                    letterboxA.ForeColor = white;
                    if (color == 0) { letterboxA.BackColor = grey; }
                    if (color == 1) { letterboxA.BackColor = yellow; }
                    if (color == 2) { letterboxA.BackColor = green; }
                }
                return;
            }
            if (letter == "B")
            {
                if (color == -1) { letterboxB.ForeColor = black; }
                else
                {
                    letterboxB.ForeColor = white;
                    if (color == 0) { letterboxB.BackColor = grey; }
                    if (color == 1) { letterboxB.BackColor = yellow; }
                    if (color == 2) { letterboxB.BackColor = green; }
                }
                return;
            }
            if (letter == "C")
            {
                if (color == -1) { letterboxC.ForeColor = black; }
                else
                {
                    letterboxC.ForeColor = white;
                    if (color == 0) { letterboxC.BackColor = grey; }
                    if (color == 1) { letterboxC.BackColor = yellow; }
                    if (color == 2) { letterboxC.BackColor = green; }
                }
                return;
            }
            if (letter == "D")
            {
                if (color == -1) { letterboxD.ForeColor = black; }
                else
                {
                    letterboxD.ForeColor = white;
                    if (color == 0) { letterboxD.BackColor = grey; }
                    if (color == 1) { letterboxD.BackColor = yellow; }
                    if (color == 2) { letterboxD.BackColor = green; }
                }
                return;
            }
            if (letter == "E")
            {
                if (color == -1) { letterboxE.ForeColor = black; }
                else
                {
                    letterboxE.ForeColor = white;
                    if (color == 0) { letterboxE.BackColor = grey; }
                    if (color == 1) { letterboxE.BackColor = yellow; }
                    if (color == 2) { letterboxE.BackColor = green; }
                }
                return;
            }
            if (letter == "F")
            {
                if (color == -1) { letterboxF.ForeColor = black; }
                else
                {
                    letterboxF.ForeColor = white;
                    if (color == 0) { letterboxF.BackColor = grey; }
                    if (color == 1) { letterboxF.BackColor = yellow; }
                    if (color == 2) { letterboxF.BackColor = green; }
                }
                return;
            }
            if (letter == "G")
            {
                if (color == -1) { letterboxG.ForeColor = black; }
                else
                {
                    letterboxG.ForeColor = white;
                    if (color == 0) { letterboxG.BackColor = grey; }
                    if (color == 1) { letterboxG.BackColor = yellow; }
                    if (color == 2) { letterboxG.BackColor = green; }
                }
                return;
            }
            if (letter == "H")
            {
                if (color == -1) { letterboxH.ForeColor = black; }
                else
                {
                    letterboxH.ForeColor = white;
                    if (color == 0) { letterboxH.BackColor = grey; }
                    if (color == 1) { letterboxH.BackColor = yellow; }
                    if (color == 2) { letterboxH.BackColor = green; }
                }
                return;
            }
            if (letter == "I")
            {
                if (color == -1) { letterboxI.ForeColor = black; }
                else
                {
                    letterboxI.ForeColor = white;
                    if (color == 0) { letterboxI.BackColor = grey; }
                    if (color == 1) { letterboxI.BackColor = yellow; }
                    if (color == 2) { letterboxI.BackColor = green; }
                }
                return;
            }
            if (letter == "J")
            {
                if (color == -1) { letterboxJ.ForeColor = black; }
                else
                {
                    letterboxJ.ForeColor = white;
                    if (color == 0) { letterboxJ.BackColor = grey; }
                    if (color == 1) { letterboxJ.BackColor = yellow; }
                    if (color == 2) { letterboxJ.BackColor = green; }
                }
                return;
            }
            if (letter == "K")
            {
                if (color == -1) { letterboxK.ForeColor = black; }
                else
                {
                    letterboxK.ForeColor = white;
                    if (color == 0) { letterboxK.BackColor = grey; }
                    if (color == 1) { letterboxK.BackColor = yellow; }
                    if (color == 2) { letterboxK.BackColor = green; }
                }
                return;
            }
            if (letter == "L")
            {
                if (color == -1) { letterboxL.ForeColor = black; }
                else
                {
                    letterboxL.ForeColor = white;
                    if (color == 0) { letterboxL.BackColor = grey; }
                    if (color == 1) { letterboxL.BackColor = yellow; }
                    if (color == 2) { letterboxL.BackColor = green; }
                }
                return;
            }
            if (letter == "M")
            {
                if (color == -1) { letterboxM.ForeColor = black; }
                else
                {
                    letterboxM.ForeColor = white;
                    if (color == 0) { letterboxM.BackColor = grey; }
                    if (color == 1) { letterboxM.BackColor = yellow; }
                    if (color == 2) { letterboxM.BackColor = green; }
                }
                return;
            }
            if (letter == "N")
            {
                if (color == -1) { letterboxN.ForeColor = black; }
                else
                {
                    letterboxN.ForeColor = white;
                    if (color == 0) { letterboxN.BackColor = grey; }
                    if (color == 1) { letterboxN.BackColor = yellow; }
                    if (color == 2) { letterboxN.BackColor = green; }
                }
                return;
            }
            if (letter == "O")
            {
                if (color == -1) { letterboxO.ForeColor = black; }
                else
                {
                    letterboxO.ForeColor = white;
                    if (color == 0) { letterboxO.BackColor = grey; }
                    if (color == 1) { letterboxO.BackColor = yellow; }
                    if (color == 2) { letterboxO.BackColor = green; }

                }
                return;
            }
            if (letter == "P")
            {
                if (color == -1) { letterboxP.ForeColor = black; }
                else
                {
                    letterboxP.ForeColor = white;
                    if (color == 0) { letterboxP.BackColor = grey; }
                    if (color == 1) { letterboxP.BackColor = yellow; }
                    if (color == 2) { letterboxP.BackColor = green; }
                }
                return;
            }
            if (letter == "Q")
            {
                if (color == -1) { letterboxQ.ForeColor = black; }
                else
                {
                    letterboxQ.ForeColor = white;
                    if (color == 0) { letterboxQ.BackColor = grey; }
                    if (color == 1) { letterboxQ.BackColor = yellow; }
                    if (color == 2) { letterboxQ.BackColor = green; }
                }
                return;
            }
            if (letter == "R")
            {
                if (color == -1) { letterboxR.ForeColor = black; }
                else
                {
                    letterboxR.ForeColor = white;
                    if (color == 0) { letterboxR.BackColor = grey; }
                    if (color == 1) { letterboxR.BackColor = yellow; }
                    if (color == 2) { letterboxR.BackColor = green; }
                }
                return;
            }
            if (letter == "S")
            {
                if (color == -1) { letterboxS.ForeColor = black; }
                else
                {
                    letterboxS.ForeColor = white;
                    if (color == 0) { letterboxS.BackColor = grey; }
                    if (color == 1) { letterboxS.BackColor = yellow; }
                    if (color == 2) { letterboxS.BackColor = green; }
                }
                return;
            }
            if (letter == "T")
            {
                if (color == -1) { letterboxT.ForeColor = black; }
                else
                {
                    letterboxT.ForeColor = white;
                    if (color == 0) { letterboxT.BackColor = grey; }
                    if (color == 1) { letterboxT.BackColor = yellow; }
                    if (color == 2) { letterboxT.BackColor = green; }
                }
                return;
            }
            if (letter == "U")
            {
                if (color == -1) { letterboxU.ForeColor = black; }
                else
                {
                    letterboxU.ForeColor = white;
                    if (color == 0) { letterboxU.BackColor = grey; }
                    if (color == 1) { letterboxU.BackColor = yellow; }
                    if (color == 2) { letterboxU.BackColor = green; }
                }
                return;
            }
            if (letter == "V")
            {
                if (color == -1) { letterboxV.ForeColor = black; }
                else
                {
                    letterboxV.ForeColor = white;
                    if (color == 0) { letterboxV.BackColor = grey; }
                    if (color == 1) { letterboxV.BackColor = yellow; }
                    if (color == 2) { letterboxV.BackColor = green; }
                }
                return;
            }
            if (letter == "W")
            {
                if (color == -1) { letterboxW.ForeColor = black; }
                else
                {
                    letterboxA.ForeColor = white;
                    if (color == 0) { letterboxW.BackColor = grey; }
                    if (color == 1) { letterboxW.BackColor = yellow; }
                    if (color == 2) { letterboxW.BackColor = green; }
                }
                return;
            }
            if (letter == "X")
            {
                if (color == -1) { letterboxX.ForeColor = black; }
                else
                {
                    letterboxX.ForeColor = white;
                    if (color == 0) { letterboxX.BackColor = grey; }
                    if (color == 1) { letterboxX.BackColor = yellow; }
                    if (color == 2) { letterboxX.BackColor = green; }
                }
                return;
            }
            if (letter == "Y")
            {
                if (color == -1) { letterboxY.ForeColor = black; }
                else
                {
                    letterboxA.ForeColor = white;
                    if (color == 0) { letterboxY.BackColor = grey; }
                    if (color == 1) { letterboxY.BackColor = yellow; }
                    if (color == 2) { letterboxY.BackColor = green; }
                }
                return;
            }
            if (letter == "Z")
            {
                if (color == -1) { letterboxZ.ForeColor = black; }
                else
                {
                    letterboxZ.ForeColor = white;
                    if (color == 0) { letterboxZ.BackColor = grey; }
                    if (color == 1) { letterboxZ.BackColor = yellow; }
                    if (color == 2) { letterboxZ.BackColor = green; }
                }
                return;
            }
        }
        private void resetLetters() 
        {
            updateLetter("A", 0);
            updateLetter("B", 0);
            updateLetter("C", 0);
            updateLetter("D", 0);
            updateLetter("E", 0);
            updateLetter("F", 0);
            updateLetter("G", 0);
            updateLetter("H", 0);
            updateLetter("I", 0);
            updateLetter("J", 0);
            updateLetter("K", 0);
            updateLetter("L", 0);
            updateLetter("M", 0);
            updateLetter("N", 0);
            updateLetter("O", 0);
            updateLetter("P", 0);
            updateLetter("Q", 0);
            updateLetter("R", 0);
            updateLetter("S", 0);
            updateLetter("T", 0);
            updateLetter("U", 0);
            updateLetter("V", 0);
            updateLetter("W", 0);
            updateLetter("X", 0);
            updateLetter("Y", 0);
            updateLetter("Z", 0);
        }
        private void newGame(bool isEasy) 
        {
            game = new(form,isEasy);
            this.cheatTxtB.Text = game.giveCheatString();
            character1.Text = "";
            character2.Text = "";
            character3.Text = "";
            character4.Text = "";
            character5.Text = "";
            lockInputBoxes(false);
            character1.Focus();
            winBox.Text = "New Game, Attempts Left:" + (game.giveGuessCount() + 1);
            resetAttemptBoxes();
            resetLetters();
            
        }

        private void newGame(int wordIndex,bool isEasy) 
        {
            game = new(form,wordIndex,isEasy);
            this.cheatTxtB.Text = game.giveCheatString();
            character1.Text = "";
            character2.Text = "";
            character3.Text = "";
            character4.Text = "";
            character5.Text = "";
            lockInputBoxes(false);
            character1.Focus();
            winBox.Text = "New Game, Attempts Left:" + (game.giveGuessCount() + 1);
            resetAttemptBoxes();
            resetLetters();
        }

        private void resetAttemptBoxes() 
        {
            for (int i = 0; i <= 6; i++)
            {
                for (int c = 0; c <= 5; c++)
                {
                    updateAttemptSquare(i, c, 0, "");
                }
            }
        }

        //run out input box logic
        private void character1_Key(object sender, System.Windows.Forms.KeyPressEventArgs e) 
        {
           if(e.KeyChar != (char)Keys.Back) 
            {
                if(character1.Text.Length == 1) { character1.Text = e.KeyChar.ToString(); character1.Text = character1.Text.ToUpper(); }
            }
        }

        private void character2_Key(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                if (character2.Text.Length == 0) { form.character1.Focus(); character1.SelectionStart = character1.Text.Length; }
            }
            if (e.KeyChar != (char)Keys.Back)
            {
                if (character2.Text.Length == 1) { character2.Text = e.KeyChar.ToString(); character2.Text = character2.Text.ToUpper(); }
            }
        }

        private void character3_Key(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                if (character3.Text.Length == 0) { form.character2.Focus(); character2.SelectionStart = character2.Text.Length; }
            }
            if (e.KeyChar != (char)Keys.Back)
            {
                if (character3.Text.Length == 1) { character3.Text = e.KeyChar.ToString(); character3.Text = character3.Text.ToUpper(); }
            }
        }
        private void character4_Key(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                if (character4.Text.Length == 0) { form.character3.Focus(); character3.SelectionStart = character3.Text.Length; }
            }
            if (e.KeyChar != (char)Keys.Back)
            {
                if (character4.Text.Length == 1) { character4.Text = e.KeyChar.ToString(); character4.Text = character4.Text.ToUpper(); }
            }

        }
        private void character5_Key(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter) 
            {
                enterInput();
            }
            if(e.KeyChar == (char)Keys.Back) 
            {
                if (character5.ReadOnly) { return; }
                if (character5.Text.Length == 0) {form.character4.Focus(); character4.SelectionStart = character4.Text.Length; }
                else if (character5.Text.Length == 1) { character5.Text = ""; }
            }
            if (e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                if (character5.ReadOnly) { return; }
                if (character5.Text.Length == 1) { character5.Text = e.KeyChar.ToString(); character5.Text = character5.Text.ToUpper(); character5.SelectionStart = character5.Text.Length; }
            }
        }

        private void character1_TextChanged(object sender, EventArgs e)
        {
            character1.Text = character1.Text.ToUpper();
            if(character1.Text.Length == 1) { form.character2.Focus(); }
        }

        private void character2_TextChanged(object sender, EventArgs e)
        {
            character2.Text = character2.Text.ToUpper();
            if (character1.Text.Length == 1) { form.character3.Focus(); }
        }

        private void character3_TextChanged(object sender, EventArgs e)
        {
            character3.Text = character3.Text.ToUpper();
            if (character1.Text.Length == 1) { form.character4.Focus(); }
        }

        private void character4_TextChanged(object sender, EventArgs e)
        {
            character4.Text = character4.Text.ToUpper();
            if (character1.Text.Length == 1) { form.character5.Focus(); }
        }

        private void character5_TextChanged(object sender, EventArgs e)
        {
            character5.Text = character5.Text.ToUpper();
        }

        private void btnNewGameHard_Click(object sender, EventArgs e)
        {
            isEasy = false;
            newGame(isEasy);
        }
    }
}
