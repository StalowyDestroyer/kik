using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kik
{
    public partial class MainPage : ContentPage
    {
        bool turn = true;
        public MainPage()
        {
            InitializeComponent();
        }

        private void Restart(object sender, EventArgs e)
        {

        }

        private bool CheckForWin(Grid grid)
        {
            List<Button> buttons = new List<Button>();
            foreach (Button button in grid.Children)
                buttons.Add(button);

            for(int i = 0; i < 3; i++)
            {
                if (buttons[i].Text == buttons[i + 3].Text && buttons[i + 3].Text == buttons[i + 6].Text && !string.IsNullOrEmpty(buttons[i].Text)) 
                {
                    DisplayWin();
                    return true;
                }
                if (buttons[i*3].Text == buttons[i * 3+1].Text && buttons[i * 3+1].Text == buttons[i *3+2].Text && !string.IsNullOrEmpty(buttons[i*3].Text))
                {
                    DisplayWin();
                    return true;
                }
            }
            if (buttons[0].Text == buttons[4].Text && buttons[4].Text == buttons[8].Text && !string.IsNullOrEmpty(buttons[4].Text))
            {
                DisplayWin();
                return true;
            }
            if (buttons[2].Text == buttons[4].Text && buttons[4].Text == buttons[6].Text && !string.IsNullOrEmpty(buttons[4].Text))
            {
                DisplayWin();
                return true;
            }
            return false;
        }
        private void DisplayWin()
        {
            if (turn)
                DisplayAlert("Koniec gry!", "wygrywa x!", "wybieram creepera");
            else
                DisplayAlert("Koniec gry!", "wygrywa o!", "wybieram creepera");

        }

    }
}
