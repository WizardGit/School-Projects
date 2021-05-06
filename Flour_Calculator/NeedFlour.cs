using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flour_Calculator
{
    public partial class NeedFlour : Form
    {
        public NeedFlour()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Display_Data(float[] amounts, string[] units)
        {
            SorghumAmountTextBox.Text = amounts[0].ToString();
            XanthumGumAmountTextBox.Text = amounts[1].ToString();
            SweetWhiteRiceAmountTextBox.Text = amounts[2].ToString();
            CreamOfTartarAmountTextBox.Text = amounts[3].ToString();
            WhiteRiceAmountTextBox.Text = amounts[4].ToString();
            BrownRiceAmountTextBox.Text = amounts[5].ToString();
            BakingSodaAmountTextBox.Text = amounts[6].ToString();
            BakingPowderAmountTextBox.Text = amounts[7].ToString();
            PotatoStarchAmountTextBox.Text = amounts[8].ToString();
            TapiocaStarchAmountTextBox.Text = amounts[9].ToString();
            GarbanzoAmountTextBox.Text = amounts[10].ToString();
            MilletAmountTextBox.Text = amounts[11].ToString();
            SaltAmountTextBox.Text = amounts[12].ToString();
            ButtermilkPowderAmountTextBox.Text = amounts[13].ToString();
            FlaxAmountTextBox.Text = amounts[14].ToString();

            SorghumUnitsTextBox.Text = units[0];
            XanthumGumUnitsTextBox.Text = units[1];
            SweetWhiteRiceUnitsTextBox.Text = units[2];
            CreamOfTartarUnitsTextBox.Text = units[3];
            WhiteRiceUnitsTextBox.Text = units[4];
            BrownRiceUnitsTextBox.Text = units[5];
            BakingSodaUnitsTextBox.Text = units[6];
            BakingPowderUnitsTextBox.Text = units[7];
            PotatoStarchUnitsTextBox.Text = units[8];
            TapiocaStarchUnitsTextBox.Text = units[9];
            GarbanzoUnitsTextBox.Text = units[10];
            MilletUnitsTextBox.Text = units[11];
            SaltUnitsTextBox.Text = units[12];
            ButtermilkPowderUnitsTextBox.Text = units[13];
            FlaxUnitsTextBox.Text = units[14];
        }

        private void NeedFlour_Load(object sender, EventArgs e)
        {
            // What we actually need is our two arrays from the main form 
            string[] units = new string[15];
            float[] amounts = new float[15];
            for (int i = 0; i < 15; i++)
            {                
                amounts[i] = 9.5f;
                units[i] = "grams";
            }

            Display_Data(amounts, units);
        }
    }
}
