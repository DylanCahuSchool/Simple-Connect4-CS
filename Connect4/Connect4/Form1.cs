using System.Data.Common;
using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Connect4
{
    public partial class Form1 : Form
    {
        private string player = "red";
        private int largeur = 6;
        private int hauteur = 5;
        private PictureBox[,] grid;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            displaySpacesStart();
            grid = getGrid();
        }
        private void displaySpacesStart()
        {
            //create a grid with 7 columns et 6 rows
            PictureBox[,] emptyGrid = new PictureBox[7,6];

            //for each column
            for (int i = 0; i < 7; i++)
            {
                //for each row
                for (int j = 0; j < 6; j++)
                {
                    //create a new space
                    emptyGrid[i,j] = new PictureBox();
                    emptyGrid[i, j].Name = "space" + i + "-" + j;
                    emptyGrid[i, j].Size = new Size(100, 100);
                    emptyGrid[i, j].Location = new Point((i * 100),(j * 100));
                    emptyGrid[i, j].ImageLocation = "none.png";
                    emptyGrid[i, j].Image = Image.FromFile(emptyGrid[i,j].ImageLocation);
                    emptyGrid[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                    emptyGrid[i, j].Click += new EventHandler(col_Click);
                    this.Controls.Add(emptyGrid[i, j]);
                }
            }
        }
        private void displaySpaceColor(int col, int row)
        {
            PictureBox change = (PictureBox)this.Controls.Find("space" + col + "-" + row, true)[0];

            //if last change was red, change to yellow
            if (player == "red")
            {
                //change to red
                change.Image = Image.FromFile("red.png");
                change.ImageLocation = "red.png";
                player = "yellow";
            }
            else {
                //change to yellow
                change.Image = Image.FromFile("yellow.png");
                change.ImageLocation = "yellow.png";
                player = "red";
            }
        }
        private PictureBox[,] getGrid()
        {
            PictureBox[,] Array = new PictureBox[7,6];
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Array[i,j] = (PictureBox)this.Controls.Find("space" + i + "-" + j, true)[0];
                }
            }
            return Array;
        }
        private void IsWinning()
        {
            for (int row = 0; row < hauteur; row++)
            {
                for (int column = 0; column < largeur; column++)
                {
                    if (CheckVertically(row, column)) { win(); }
                    else if (CheckHorizontally(row, column)) { win(); }
                    else if (CheckDiagonallyDown(row, column)) { win();}
                    else if (CheckDiagonallyUp(row, column)) { win();}
                }
            }
            //return result;
        }
        private bool CheckHorizontally(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (column + 3 > largeur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[column + distance, row].ImageLocation != player + ".png") { return false; }
            }

            return true;
        }
        private bool CheckVertically(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (row + 3 > hauteur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[column, row + distance].ImageLocation != player +".png") { return false; }
            }

            return true;
        }
        private bool CheckDiagonallyDown(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (row + 3 >= hauteur) { return false; }
            if (column + 3 >= largeur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[column + distance, row + distance].ImageLocation != player + ".png") { return false; }
            }

            return true;
        }
        private bool CheckDiagonallyUp(int row, int column)
        {
            // If there aren't even four more spots before leaving the grid,
            // we know it can't be.
            if (row - 3 < 0) { return false; }
            if (column + 3 >= largeur) { return false; }

            for (int distance = 0; distance < 4; distance++)
            {
                if (grid[column + distance, row - distance].ImageLocation != player +".png") { return false; }
            }

            return true;
        }
        private void win()
        {
            if (MessageBox.Show("Player " + player + " won! Do you want to restart?", "Restart", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }   
        }
        private void col_Click(object sender, EventArgs e)
        {
            PictureBox col = (PictureBox)sender;
            string name = col.Name;
            int colNumber = Convert.ToInt32(name.Substring(5, 1));
            
            if (grid[colNumber,0].ImageLocation != "none.png")
            {
                return;
            }
            else if (grid[colNumber,hauteur].ImageLocation == "none.png")
            {
                displaySpaceColor(colNumber, hauteur);
                //if (IsWinning())win();
                IsWinning();
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                 if (grid[colNumber, i +1].ImageLocation != "none.png")
                 {
                    displaySpaceColor(colNumber, i);
                    //if (IsWinning())win();
                    IsWinning();
                    return;
                 }
            }
        }
    }
}