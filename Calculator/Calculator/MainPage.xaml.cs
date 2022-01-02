using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private decimal firstNum;
        private decimal secondNum;
        private string selectedOperator;
        private bool isOperatorClicked;
        private bool isFinalResult;

        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnCommon_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (LblResult.Text.StartsWith("="))
            {
                isFinalResult = false;
                LblResult.Text = LblResult.Text.Remove(0, 1);
                firstNum = Convert.ToDecimal(LblResult.Text);
                LblResult.Text = "0";
            }

            if(btn.Text == "." && LblResult.Text.Contains("."))
            {
                return;
            }

            if (LblResult.Text == "0" && btn.Text != "." || isOperatorClicked)
            {
                isOperatorClicked = false;
                LblResult.Text = btn.Text;
            }
            else
            {
                LblResult.Text += btn.Text;
            }
        }

        private void BtnCommonOperation_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            isOperatorClicked = true;
            selectedOperator = btn.Text;

            if (LblResult.Text.StartsWith("="))
            {
                try
                {
                    isFinalResult = false;
                    LblResult.Text = LblResult.Text.Remove(0, 1);
                    firstNum = Convert.ToDecimal(LblResult.Text);
                    LblResult.Text = "0";
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "Ok");
                }
                return;
            }

            if(firstNum != 0 && selectedOperator != "")
            {
                RunEqual();
                return;
            }
            
            firstNum = Convert.ToDecimal(LblResult.Text);
            LblResult.Text = "0";
        }

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            LblResult.Text = "0";
            isOperatorClicked = false;
            selectedOperator = "";
            firstNum = 0;
            isFinalResult = false;
        }

        private void BtnX_Clicked(object sender, EventArgs e)
        {
            string number = LblResult.Text;
            if (number != "0")
            {
                number = number.Remove(number.Length - 1, 1);
                if (string.IsNullOrEmpty(number))
                {
                    LblResult.Text = "0";
                }
                else
                {
                    LblResult.Text = number;
                }
            }
        }

        private async void BtnPercentage_Clicked(object sender, EventArgs e)
        {
            try
            {
                string number = LblResult.Text;
                if(number != "0")
                {
                    decimal percentageVal = Convert.ToDecimal(number);
                    string result = (percentageVal / 100).ToString("0.##");
                    LblResult.Text = result;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void BtnEqual_Clicked(object sender, EventArgs e)
        {
            if (LblResult.Text.StartsWith("="))
            {
                return;
            }

                if (firstNum == 0)
            {
                firstNum = Convert.ToDecimal(LblResult.Text);
                LblResult.Text = "=" + LblResult.Text;
                return;
            }

            RunEqual();
            isOperatorClicked = false;
            selectedOperator = "";
        }

        public void RunEqual()
        {
            try
            {
                decimal secondNum = Convert.ToDecimal(LblResult.Text);
                decimal result = Calculate(firstNum, secondNum);

                LblResult.Text = "=" + result.ToString("0.##");
                isFinalResult = true;
                firstNum = 0;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        public decimal Calculate(decimal firstNun, decimal secondNum)
        {
            decimal result = 0;

            switch (selectedOperator)
            {
                case "+":
                    result = firstNun + secondNum;
                    break;
                case "-":
                    result = firstNun - secondNum;
                    break;
                case "*":
                    result = firstNun * secondNum;
                    break;
                case "/":
                    result = firstNun / secondNum;
                    break;
            }
            selectedOperator = "";
            isOperatorClicked = false;
            return result;
        }
    }
}
