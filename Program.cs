

new Exercise(6)
    .OutputFullMatrix()
    .Solve()
    .OutputResult();

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

// var size = 5;
// var matrix = new List<List<int>>();
// var count = 11;
//
// for (int i = 0; i < size; i++)
// {
//     var row = new List<int>();
//         
//     for (int m = 0; m < size; m++)
//     {
//         // row.Add(Random.Shared.Next(10, 50));
//         row.Add(count++);
//     }
//         
//     matrix.Add(row);
// }
//
// Console.WriteLine();
//
// foreach (var row in matrix)
// {
//     foreach (var i in row)
//     {
//         Console.Write(i);
//         Console.Write(" ");
//     }
//     Console.WriteLine();
// }
//
// Console.WriteLine();
//
// for (int i = 0; i < size; i++)
// {
//     Console.WriteLine(matrix[i][i]);
// }
//
// int cr, ccDif, step;
//
// cr = size - 2;
// ccDif = 1;
// step = -1;
// for (int i = size - 1; i > 0; i--)
// {
//     for (int j = i; j > 0; j--)
//     {
//         Console.WriteLine(matrix[cr][cr + ccDif]);
//         if (j == 1)
//         {
//             if (step == 1) cr -= 1;
//         }
//         else cr += step;
//     }
//
//     step *= -1;
//     ccDif++;
// }
//
// cr = 1;
// ccDif = 1;
// step = 1;
// for (int i = size - 1; i > 0; i--)
// {
//     for (int j = i; j > 0; j--)
//     {
//         Console.WriteLine(matrix[cr][cr - ccDif]);
//         if (j == 1)
//         {
//             if (step == -1) cr += 1;
//         }
//         else cr += step;
//     }
//
//     step *= -1;
//     ccDif++;
// }

/*

012345432100123210010
012345543212345543455

543210123455432345545
543210012343210012100

[012345678 76543210 0123456 543210 01234 3210 012 10 0] [876543210 12345678 8765432 345678 87654 5678 876 78 8]
[012345678 87654321 2345678 876543 45678 8765 678 87 8] [876543210 01234567 6543210 012345 43210 0123 210 01 0]

[76543210 0123456 543210 01234 3210 012 10 0] 
[12345678 8765432 345678 87654 5678 876 78 8]

876543210
 876543210
 
  012345678
012345678

876543210
   876543210
 
    012345678
012345678

876543210
     876543210
 
      012345678
012345678

876543210
       876543210
 
        012345678
012345678

876543210012345676543210012345654321001234543210012343210012321001210010


0123432100121001234432344

int Get(int value)

Get(1) => 1
Get(2) => 2
Get(3) => 1
Get(4) => 2
Get(5) => 1
Get(6) => 2
Get(7) => 1
Get(8) => 2
 
11   19   20   24   25

26   12   18   21   23

32   27   13   17   22

33   31   28   14   16

35   34   30   29   15

11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35

[[11, 19, 20, 24, 25],
[26, 12, 18, 21, 23],
[32, 27, 13, 17, 22],
[33, 31, 28, 14, 16],
[35, 34, 30, 29, 15]]


size = 5
i   =  0  1  2  3  4    5  6  7  8    9 10 11   12 13    14         15 16 17 18 19 20 21 22 23 24

row =  0  1  2  3  4    3  2  1  0    0  1  2    1  0    0          1  2  3  4  4  3  2  3  4  4
col =  0  1  2  3  4    4  3  2  1    2  3  4    4  3    4          0  1  2  3  2  1  0  0  1  0

row = 0123432100121001234432344
col = 0123443212344340123210010


GetRow(5,  0) => 0;  |  GetCol(5,  0) => 0;
GetRow(5,  1) => 1;  |  GetCol(5,  1) => 1;
GetRow(5,  2) => 2;  |  GetCol(5,  2) => 2;
GetRow(5,  3) => 3;  |  GetCol(5,  3) => 3;
GetRow(5,  4) => 4;  |  GetCol(5,  4) => 4;
GetRow(5,  5) => 3;  |  GetCol(5,  5) => 4;
GetRow(5,  6) => 2;  |  GetCol(5,  6) => 3;
GetRow(5,  7) => 1;  |  GetCol(5,  7) => 2;
GetRow(5,  8) => 0;  |  GetCol(5,  8) => 1;
GetRow(5,  9) => 0;  |  GetCol(5,  9) => 2;
GetRow(5, 10) => 1;  |  GetCol(5, 10) => 3;
GetRow(5, 11) => 2;  |  GetCol(5, 11) => 4;
GetRow(5, 12) => 1;  |  GetCol(5, 12) => 4;
GetRow(5, 13) => 0;  |  GetCol(5, 13) => 3;
GetRow(5, 14) => 0;  |  GetCol(5, 14) => 4;
GetRow(5, 15) => 1;  |  GetCol(5, 15) => 0;
GetRow(5, 16) => 2;  |  GetCol(5, 16) => 1;
GetRow(5, 17) => 3;  |  GetCol(5, 17) => 2;
GetRow(5, 18) => 4;  |  GetCol(5, 18) => 3;
GetRow(5, 19) => 4;  |  GetCol(5, 19) => 2;
GetRow(5, 20) => 3;  |  GetCol(5, 20) => 1;
GetRow(5, 21) => 2;  |  GetCol(5, 21) => 0;
GetRow(5, 22) => 3;  |  GetCol(5, 22) => 0;
GetRow(5, 23) => 4;  |  GetCol(5, 23) => 1;
GetRow(5, 24) => 4;  |  GetCol(5, 24) => 0;

*/