using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10_Backtracking_IsaacFriedman
{
    class Sudoku
    {
        private int[,] matrizSudoku = new int[9, 9];

        public Sudoku(int[,] matriz)
        {
            matrizSudoku = matriz;
        }

        public int ObtenerValor(int fila, int columna)
        {
            return matrizSudoku[fila, columna];
        }

        private bool VerificarColumna(int fila, int columna, int numero)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i != fila)
                {
                    if (numero == matrizSudoku[i, columna]) return true;
                }
            }
            return false;
        }

        private bool VerificarFila(int fila, int columna, int numero)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i != columna)
                {
                    if (numero == matrizSudoku[fila, i]) return true;
                }
            }
            return false;
        }

        private bool VerificarCuadro(int fila, int columna, int numero)
        {
            int filaTemporal = (fila / 3) * 3;
            int columnaTemporal = (columna / 3) * 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (((filaTemporal + i) != fila) || ((columnaTemporal + j) != columna))
                    {
                        if (numero == matrizSudoku[filaTemporal + i, columnaTemporal + j]) return true;
                    }
                }
            }
            return false;
        }

        private bool PuedeColocar(int fila, int columna, int numero)
        {
            if (VerificarCuadro(fila, columna, numero)) return false;
            if (VerificarFila(fila, columna, numero)) return false;
            if (VerificarColumna(fila, columna, numero)) return false;
            return true;
        }

        public bool Resolver(int fila, int columna)
        {
            if (columna == 9)
            {
                columna = 0;
                fila++;
                if (fila == 9)
                {
                    return true;
                }
            }
            if (matrizSudoku[fila, columna] != 0)
            {
                return Resolver(fila, columna + 1);
            }

            for (int i = 1; i <= 9; i++)
            {
                if (PuedeColocar(fila, columna, i))
                {
                    matrizSudoku[fila, columna] = i;
                    if (Resolver(fila, columna + 1))
                    {
                        return true;
                    }
                    matrizSudoku[fila, columna] = 0;
                }
            }
            return false;
        }
        public void GenerarNuevoJuego()
        {
            Resolver(0, 0);

            Random rand = new Random();
            for (int i = 0; i < 40; i++)
            {
                int fila = rand.Next(9);
                int columna = rand.Next(9);
                matrizSudoku[fila, columna] = 0;
            }
        }

        public bool ComprobarSolucion()
        {
            int[,] copiaMatriz = (int[,])matrizSudoku.Clone();
            Sudoku sudokuCopia = new Sudoku(copiaMatriz);
            return sudokuCopia.Resolver(0, 0);
        }

        public void ResolverJuegoActual()
        {
            if (!Resolver(0, 0))
            {
                MessageBox.Show("No hay solución para este rompecabezas.");
            }
        }
    }
}
