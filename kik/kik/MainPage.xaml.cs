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
            CreateBoard();
        }
        
        private void CreateBoard()
        {
            MainGrid.Children.Clear();
            MainGrid.RowSpacing = 0;
            MainGrid.ColumnSpacing = 0;
            turn = true;
            TurnLabel.Text = "Kolejka: x";

            for (int i = 0; i < 3; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                for(int j = 0; j < 3; j++)
                {
                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
                    Grid grid = new Grid()
                    {
                        Margin = new Thickness(3),
                        RowSpacing = 0,
                        ColumnSpacing = 0,
                    };
                    Grid.SetRow(grid, i);
                    Grid.SetColumn(grid, j);
                    MainGrid.Children.Add(grid);
                    for(int k = 0; k < 3; k++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });
                        for(int l = 0; l < 3; l++)
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto) });
                            Button button = new Button()
                            {
                                WidthRequest = 38,
                                HeightRequest = 38,
                                Margin = new Thickness(1),
                                ClassId = "enabled",
                                BorderColor = Color.White,
                            };
                            Grid.SetRow(button, k);
                            Grid.SetColumn(button, l);
                            grid.Children.Add(button);
                            button.Clicked += Move;
                        }
                    }
                }
            }
        }

        private void Restart(object sender, EventArgs e)
        {
            CreateBoard();
        }

        private void Move(object sender, EventArgs e)
        {
            Button clickedField = sender as Button;

            if(clickedField.ClassId == "enabled")
            {
                if (turn)
                    clickedField.Text = "x";
                else
                    clickedField.Text = "o";

                DisableAll();

                Grid parent = clickedField.Parent as Grid;
                parent.BackgroundColor = Color.Transparent;

                if (CheckForWin(parent))
                    return;

                int gridToEnable = parent.Children.IndexOf(clickedField);
                EnableGrid(gridToEnable);

                ChangeTurn();
            }
        }

        private void EnableGrid(int gridToEnable)
        {
            ((Grid)MainGrid.Children[gridToEnable]).BackgroundColor = Color.LightGray;
            foreach(Button button in ((Grid)MainGrid.Children[gridToEnable]).Children)
            {
                if (string.IsNullOrEmpty(button.Text))
                    button.ClassId = "enabled";
            }
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

        private void DisableAll()
        {
            foreach (Grid grid in MainGrid.Children)
                foreach (Button button in grid.Children)
                    button.ClassId = "disabled";
        }

        private void ChangeTurn()
        {
            turn = !turn;
            if (turn)
                TurnLabel.Text = "Kolejka: x";
            else
                TurnLabel.Text = "Kolejka: o";

        }
    }
}
