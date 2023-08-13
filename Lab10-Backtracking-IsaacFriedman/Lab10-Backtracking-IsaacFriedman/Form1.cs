using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab10_Backtracking_IsaacFriedman
{
    public partial class Form1 : Form
    {
        private Sudoku board;
        private TextBox[,] sudokuTextBoxes = new TextBox[9, 9];

        public Form1()
        {
            InitializeComponent();
            InitializeSudokuGrid();
        }

        private void InitializeSudokuGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuTextBoxes[i, j] = new TextBox
                    {
                        Width = 20,
                        Height = 20,
                        Location = new System.Drawing.Point(j * 22, i * 22),
                        MaxLength = 1,
                        TextAlign = HorizontalAlignment.Center
                    };
                    this.Controls.Add(sudokuTextBoxes[i, j]);
                }
            }

            Button solveButton = new Button
            {
                Text = "Resolver",
                Location = new System.Drawing.Point(10, 210)
            };
            solveButton.Click += SolveButton_Click;
            this.Controls.Add(solveButton);



            Button clearButton = new Button
            {
                Text = "Borrar",
                Location = new System.Drawing.Point(90, 210)
            };
            clearButton.Click += ClearButton_Click;
            this.Controls.Add(clearButton);

            Button botonGenerar = new Button
            {
                Text = "Generar Nuevo Juego",
                Location = new System.Drawing.Point(10, 250)
            };
            botonGenerar.Click += BotonGenerar_Click;
            this.Controls.Add(botonGenerar);

            Button botonComprobar = new Button
            {
                Text = "Comprobar Solución Actual",
                Location = new System.Drawing.Point(150, 250)
            };
            botonComprobar.Click += BotonComprobar_Click;
            this.Controls.Add(botonComprobar);

        }
        private void BotonGenerar_Click(object sender, EventArgs e)
        {
            int[,] arr = new int[9, 9];
            board = new Sudoku(arr);

            board.GenerarNuevoJuego();
            DisplaySudoku();

            
        }

        private void BotonComprobar_Click(object sender, EventArgs e)
        {
            try
            {
                if (board != null)
                {
                    if (board.ComprobarSolucion())
                    {
                        MessageBox.Show("¡Solución correcta!");
                    }
                    else
                    {
                        MessageBox.Show("Solución incorrecta. Por favor, revisa tu tablero.");
                    }
                }
                else
                {
                    MessageBox.Show("Primero genera un tablero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Ocurrió un error inesperado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SolveButton_Click(object sender, EventArgs e)
        {
            int[,] arr = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (int.TryParse(sudokuTextBoxes[i, j].Text, out int value))
                    {
                        arr[i, j] = value;
                    }
                }
            }

            board = new Sudoku(arr);
            board.Resolver(0, 0);
            DisplaySudoku();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuTextBoxes[i, j].Text = "";
                }
            }
        }

        private void DisplaySudoku()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuTextBoxes[i, j].Text = board.ObtenerValor(i, j).ToString();
                }
            }
        }
    }
}

