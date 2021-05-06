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
    public partial class WantFlour : Form
    {

        // The amounts array holds the decimal number of the following ingredients in that order - with their units specified in the units array
        //Sorghum, Xanthum Gum, Sweet White Rice, Cream of Tartar, White Rice, Brown Rice, Baking Soda, Baking Powder,
        //Potato Starch, Tapioca Starch, Garbanzo, Millet, Salt, Buttermilk Powder, Flax
        private decimal[] amounts;
        private string[] units;
        private decimal[] OnHandGrams;

        public WantFlour()
        {
            InitializeComponent();
        }           

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult button = MessageBox.Show("Are you sure you want to close this application already?", "Terminate Program?", MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (button == DialogResult.Yes)
                this.Close();
        }

        private void InitializeData()
        {
            SorghumAmountTextBox.Text = "0";
            XanthumGumAmountTextBox.Text = "0";
            SweetWhiteRiceAmountTextBox.Text = "0";
            CreamOfTartarAmountTextBox.Text = "0";
            WhiteRiceAmountTextBox.Text = "0";
            BrownRiceAmountTextBox.Text = "0";
            BakingSodaAmountTextBox.Text = "0";
            BakingPowderAmountTextBox.Text = "0";
            PotatoStarchAmountTextBox.Text = "0";
            TapiocaStarchAmountTextBox.Text = "0";
            GarbanzoAmountTextBox.Text = "0";
            MilletAmountTextBox.Text = "0";
            SaltAmountTextBox.Text = "0";
            ButtermilkPowderAmountTextBox.Text = "0";
            FlaxAmountTextBox.Text = "0";

            SorghumComboBox.SelectedIndex = 0;
            XanthumGumComboBox.SelectedIndex = 0;
            SweetWhiteRiceComboBox.SelectedIndex = 0;
            CreamOfTartarComboBox.SelectedIndex = 0;
            WhiteRiceComboBox.SelectedIndex = 0;
            BrownRiceComboBox.SelectedIndex = 0;
            BakingSodaComboBox.SelectedIndex = 0;
            BakingPowderComboBox.SelectedIndex = 0;
            PotatoStarchComboBox.SelectedIndex = 0;
            TapiocaStarchComboBox.SelectedIndex = 0;
            GarbanzoComboBox.SelectedIndex = 0;
            MilletComboBox.SelectedIndex = 0;
            SaltComboBox.SelectedIndex = 0;
            ButtermilkPowderComboBox.SelectedIndex = 0;
            FlaxComboBox.SelectedIndex = 0;

            for (int i = 0; i < 15; i++)
            {
                units[i] = "grams";
                amounts[i] = -1;
            }
        }

        private void Convert()
        {
            //First turn everything into grams
            for (int i = 0; i < 15; i++)
            {
                if (units[i] == "kilogram")
                {
                    amounts[i] *= 1000;
                }
                if (units[i] == "pound")
                {
                    // Find the actual conversion!
                    amounts[i] *= 3.2m;
                }
            }

            decimal temp = 0.0m;
            for (int i = 0; i < 15; i++)
            {
                temp = amounts[i] - OnHandGrams[i];
                if (temp > 0)
                {
                    amounts[i] = Math.Ceiling(temp);
                }
                else
                {
                    amounts[i] = 0.0m;
                }
            }

            // Now Convert
            for (int i = 0; i < 15; i++)
            {
                if (units[i] == "kilogram")
                {
                    amounts[i] /= 1000;
                }
                if (units[i] == "pound")
                {
                    // Find the actual conversion!
                    amounts[i] /= 3.2m;
                }
            }
        }


        private void calculateButton_Click(object sender, EventArgs e)
        {
            ExitButton.Enabled = false;
            calculateButton.Enabled = false;
            FlourOnHandButton.Enabled = false;

            units[0] = SorghumComboBox.Text;
            units[1] = XanthumGumComboBox.Text;
            units[2] = SweetWhiteRiceComboBox.Text;
            units[3] = CreamOfTartarComboBox.Text;
            units[4] = WhiteRiceComboBox.Text;
            units[5] = BrownRiceComboBox.Text;
            units[6] = BakingSodaComboBox.Text;
            units[7] = BakingPowderComboBox.Text;
            units[8] = PotatoStarchComboBox.Text;
            units[9] = TapiocaStarchComboBox.Text;
            units[10] = GarbanzoComboBox.Text;
            units[11] = MilletComboBox.Text;
            units[12] = SaltComboBox.Text;
            units[13] = ButtermilkPowderComboBox.Text;
            units[14] = FlaxComboBox.Text;

            // Actually validate the data!
            if (
               (Decimal.TryParse(SorghumAmountTextBox.Text, out amounts[0]) == false)
            || (Decimal.TryParse(XanthumGumAmountTextBox.Text, out amounts[1]) == false)
            || (Decimal.TryParse(SweetWhiteRiceAmountTextBox.Text, out amounts[2]) == false)
            || (Decimal.TryParse(CreamOfTartarAmountTextBox.Text, out amounts[3]) == false)
            || (Decimal.TryParse(WhiteRiceAmountTextBox.Text, out amounts[4]) == false)
            || (Decimal.TryParse(BrownRiceAmountTextBox.Text, out amounts[5]) == false)
            || (Decimal.TryParse(BakingSodaAmountTextBox.Text, out amounts[6]) == false)
            || (Decimal.TryParse(BakingPowderAmountTextBox.Text, out amounts[7]) == false)
            || (Decimal.TryParse(PotatoStarchAmountTextBox.Text, out amounts[8]) == false)
            || (Decimal.TryParse(TapiocaStarchAmountTextBox.Text, out amounts[9]) == false)
            || (Decimal.TryParse(GarbanzoAmountTextBox.Text, out amounts[10]) == false)
            || (Decimal.TryParse(MilletAmountTextBox.Text, out amounts[11]) == false)
            || (Decimal.TryParse(SaltAmountTextBox.Text, out amounts[12]) == false)
            || (Decimal.TryParse(ButtermilkPowderAmountTextBox.Text, out amounts[13]) == false)
            || (Decimal.TryParse(FlaxAmountTextBox.Text, out amounts[14]) == false))
            {
                MessageBox.Show("One or more of your inputs is not a valid number!", "Invalid Input");
            }
            else
            {
                // If we are here, we have at least valid values!
                // Check for negative or too large numbers!
                for (int i = 0; i < 15; i++)
                {
                    if (amounts[i] < 0)
                    {
                        MessageBox.Show("One of your inputs is too small.  Please make sure to enter in no less than '0' into each input box!", "Invalid Input");
                        break;
                    }
                    else if (amounts[i] > 1000000)
                    {
                        MessageBox.Show("One of your inputs is too large.  Please do not enter anything larger than 1 million!", "Invalid Input");
                        break;
                    }
                }
            }

            Convert();

            //ExitButton.Enabled = true;
            //calculateButton.Enabled = true;
            //FlourOnHandButton.Enabled = true;

            //Form needFlour = new NeedFlour();

            //this.Close();
            //needFlour.ShowDialog();
            //
        }

        private void FlourOnHandButton_Click(object sender, EventArgs e)
        {
            /*Form addFlour = new HaveFlour();
            DialogResult selectedButton = addFlour.ShowDialog();
            if (selectedButton == DialogResult.OK)
            {
                OnHandGrams = (decimal[])addFlour.Tag;
            }*/


            using (HaveFlour frm = new HaveFlour())
            {
                frm.ShowDialog();
                
                OnHandGrams = frm.GetValue;
                
            }
            MessageBox.Show(OnHandGrams[1].ToString());
        }

        private void WantFlour_Load(object sender, EventArgs e)
        {
            ExitButton.Enabled = false;
            calculateButton.Enabled = false;
            FlourOnHandButton.Enabled = false;

            amounts = new decimal[15];
            units = new string[15];
            OnHandGrams = new decimal[15];
            InitializeData();

            ExitButton.Enabled = true;
            calculateButton.Enabled = true;
            FlourOnHandButton.Enabled = true;
        }
    }
}
