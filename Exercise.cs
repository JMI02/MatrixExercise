using System;

namespace ConsoleApp5;

public sealed class Exercise
{
    private readonly int[][] _matrix;
    private readonly int _size;
    private int _minimumFirst = int.MaxValue - 1;
    private int _minimumSecond = int.MaxValue;

    public Exercise(int size)
    {
        _matrix = new int[size][];
        _size = size;

        GenerateRandom();
    }

    private void GenerateRandom()
    {
        for (int i = 0; i < _size; i++)
        {
            var row = new int[_size];
        
            for (int m = 0; m < _size; m++)
            {
                row[m] = Random.Shared.Next(10, 50);
            }
        
            _matrix[i] = row;
        }
    }

    public Exercise OutputFullMatrix()
    {
        Console.WriteLine();

        foreach (var row in _matrix)
        {
            foreach (var i in row)
            {
                Console.Write(i);
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();

        return this;
    }

    public Exercise Solve()
    {
        IterateMiddleDiagonal();
        IteratePart(Part.Right);
        IteratePart(Part.Left);
        
        return this;
    }

    public void OutputResult()
    {
        Console.WriteLine();
        Console.WriteLine($"First minimum number is: {_minimumFirst}");
        Console.WriteLine($"Second minimum number is: {_minimumSecond}");
    }

    private void IterateMiddleDiagonal()
    {
        for (int i = 0; i < _size; i++)
        {
            OperationWithCurrentValue(i, i);
        }
    }

    private void IteratePart(Part part)
    {
        var currentRow = part == Part.Right ? _size - 2 : 1;
        var dif = (int)part;
        var direction = (int)part * -1;

        for (int i = 1; i < _size; i++)
        {
            for (int j = i; j < _size; j++)
            {
                var currentCol = currentRow + dif;

                OperationWithCurrentValue(currentRow, currentCol);

                currentRow += direction;
            }

            direction *= -1;
            currentRow += direction * (2 - i % 2);
            dif = Math.Sign(dif) * (Math.Abs(dif) + 1);
        }
    }

    private void OperationWithCurrentValue(int currentRow, int currentCol)
    {
        var value = _matrix[currentRow][currentCol];
        Console.WriteLine($"Row: {currentRow}; Column: {currentCol}; Value: {value}");
        Compare(value);
    }

    private void Compare(int value)
    {
        if (value < _minimumFirst)
        {
            _minimumSecond = _minimumFirst;
            _minimumFirst = value;
        }
        else if (value < _minimumSecond && value != _minimumFirst)
        {
            _minimumSecond = value;
        }
    }

    private enum Part
    {
        Left = -1,
        Right = 1
    }
}
