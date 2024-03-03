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

        private void DisplayWin()
        {
            if (turn)
                DisplayAlert("Koniec gry!", "wygrywa x!", "wybieram creepera");
            else
                DisplayAlert("Koniec gry!", "wygrywa o!", "wybieram creepera");

        }
    }
}
