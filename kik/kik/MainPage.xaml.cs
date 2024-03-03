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
    }
}
