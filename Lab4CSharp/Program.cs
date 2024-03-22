
public class Money
{
    private int nominal;
    private int num;
    // Конструктор класу Money
    public Money(int nominal, int num)
    {
        this.nominal = nominal;
        this.num = num;
    }

    public void Print()
    {
        Console.WriteLine($"Nominal: {nominal}, Number: {num}");
    }

    public bool CanBuy(int price)
    {
        return price <= nominal * num;
    }

    public int CalculateItems(int price)
    {
        return (nominal * num) / price;
    }
    // Властивість для доступу до номіналу грошової одиниці
    public int Nominal
    {
        get { return nominal; }
        set { nominal = value; }
    }

    public int Num
    {
        get { return num; }
        set { num = value; }
    }

    public int TotalAmount
    {
        get { return nominal * num; }
    }
    // Індексатор для доступу до номіналу та кількості грошових одиниць
    public int this[int index]
    {
        get
        {
            if (index == 0)
                return nominal;
            else if (index == 1)
                return num;
            else
            {
                Console.WriteLine("Error: Invalid index.");
                return -1;
            }
        }
        set
        {
            if (index == 0)
                nominal = value;
            else if (index == 1)
                num = value;
            else
                Console.WriteLine("Error: Invalid index.");
        }
    }
    // Інкремент
    public static Money operator ++(Money money)
    {
        money.nominal++;
        money.num++;
        return money;
    }
    // Декремент
    public static Money operator --(Money money)
    {
        money.nominal--;
        money.num--;
        return money;
    }

    public static bool operator !(Money money)
    {
        return money.num != 0;
    }

    public static Money operator +(Money money, int scalar)
    {
        money.num += scalar;
        return money;
    }

    public static explicit operator string(Money money)
    {
        return $"Nominal: {money.nominal}, Number: {money.num}";
    }

    public static explicit operator Money(string moneyString)
    {
        string[] parts = moneyString.Split(new char[] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 4)
            throw new ArgumentException("Invalid string format for Money.");
        //"Nominal: {nominal}, Number: {num}"

        int nominal = int.Parse(parts[1]);
        int num = int.Parse(parts[3]);

        return new Money(nominal, num);
    }
}
/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
//Створити клас VectorLong (вектор цілих чисел).


public class Vector
{
    protected long[] IntArray;       // масив
    protected uint size;             // розмір вектора
    protected int codeError;         // код помилки
    protected static uint num_vl;    // кількість векторів

    public Vector()
    {
        size = 1;
        IntArray = new long[1];
        IntArray[0] = 0;
        num_vl++;
    }
    public Vector(uint size)
    {
        this.size = size;
        IntArray = new long[size];
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = 0;
        }
        num_vl++;
    }
    public Vector(uint size, long initialValue) : this(size)
    {
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = initialValue;
        }
    }

    ~Vector()
    {
        Console.WriteLine("Vector destructed.");
    }

    public void Input()
    {
        Console.WriteLine("Enter vector elements:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Element {i + 1}: ");
            IntArray[i] = long.Parse(Console.ReadLine());
        }
    }

    public void Output()
    {
        Console.WriteLine("Vector elements:");
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine($"Element {i + 1}: {IntArray[i]}");
        }
    }
    public void Assign(long value)
    {
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = value;
        }
    }
    public static uint CountVectors()
    {
        return num_vl;
    }
    public void AssignValues(params long[] values)
    {
        if (values.Length != size)
        {
            Console.WriteLine($"Error: Number of values should be {size}.");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            IntArray[i] = values[i];
        }
    }
    // Властивості
    public uint Size
    {
        get { return size; }
    }
    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    public long this[int index]
    {
        get
        {
            if (index < 0 || index >= size)
            {
                Console.WriteLine("Error: Invalid index.");
                return -1;
            }
            return IntArray[index];
        }
        set
        {
            if (index >= 0 && index < size)
            {
                IntArray[index] = value;
            }
            else
            {
                codeError = 1;
                Console.WriteLine("Error: Invalid index.");
            }
        }
    }

    public static Vector operator ++(Vector v)
    {
        for (int i = 0; i < v.Size; i++)
        {
            v.IntArray[i]++;
        }
        return v;
    }

    public static Vector operator --(Vector v)
    {
        for (int i = 0; i < v.Size; i++)
        {
            v.IntArray[i]--;
        }
        return v;
    }

    public static bool operator true(Vector v)
    {
        foreach (long element in v.IntArray)
        {
            if (element == 0)
                return false;
        }
        return true;
    }

    public static bool operator false(Vector v)
    {
        foreach (long element in v.IntArray)
        {
            if (element == 0)
                return true;
        }
        return false;
    }

    public static Vector operator !(Vector v)
    {
        for (int i = 0; i < v.Size; i++)
        {
            v.IntArray[i] = v.IntArray[i] != 0 ? 0 : 1;
        }
        return v;
    }

    public static Vector operator ~(Vector v)
    {
        for (int i = 0; i < v.Size; i++)
        {
            v.IntArray[i] = ~v.IntArray[i];
        }
        return v;
    }
    //арифметичних бінарні операції
    public static Vector operator +(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) + (i < v2.Size ? v2[i] : 0);
        }
        return result;
    }

    public static Vector operator +(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] + scalar;
        }
        return result;
    }
    public static Vector operator -(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) - (i < v2.Size ? v2[i] : 0);
        }
        return result;
    }

    public static Vector operator -(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] - scalar;
        }
        return result;
    }
    public static Vector operator *(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) * (i < v2.Size ? v2[i] : 0);
        }
        return result;
    }

    public static Vector operator *(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] * scalar;
        }
        return result;
    }
    public static Vector operator /(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) / (i < v2.Size ? v2[i] : 1); 
        }
        return result;
    }
    public static Vector operator /(Vector v, long scalar)
    {
        if (scalar == 0)
        {
            throw new ArgumentException("Division by zero.");
        }
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] / scalar;
        }
        return result;
    }
    public static Vector operator %(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) % (i < v2.Size ? v2[i] : 1); 
        }
        return result;
    }
    public static Vector operator %(Vector v, long scalar)
    {
        if (scalar == 0)
        {
            throw new ArgumentException("Division by zero.");
        }
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] % scalar;
        }
        return result;
    }
    public static Vector operator |(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) | (i < v2.Size ? v2[i] : 0);
        }
        return result;
    }
    public static Vector operator |(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] | scalar;
        }
        return result;
    }
    public static Vector operator ^(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) ^ (i < v2.Size ? v2[i] : 0);
        }
        return result;
    }
    public static Vector operator ^(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] ^ scalar;
        }
        return result;
    }
    public static Vector operator &(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) & (i < v2.Size ? v2[i] : 0);
        }
        return result;
    }
    public static Vector operator &(Vector v, int scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] & scalar;
        }
        return result;
    }
    public static Vector operator >>(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
result[i] = (i < v1.Size ? v1[i] : 0) / (1 << (i < v2.Size ? (int)v2[i] : 0));
        }
        return result;
    }
    public static Vector operator >>(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] >> (int)scalar;
        }
        return result;
    }
    public static Vector operator <<(Vector v1, Vector v2)
    {
        uint maxSize = Math.Max(v1.Size, v2.Size);
        Vector result = new Vector(maxSize);
        for (int i = 0; i < maxSize; i++)
        {
            result[i] = (i < v1.Size ? v1[i] : 0) << (i < v2.Size ? (int)v2[i] : 0);
        }
        return result;
    }
    public static Vector operator <<(Vector v, long scalar)
    {
        Vector result = new Vector(v.Size);
        for (int i = 0; i < v.Size; i++)
        {
            result[i] = v[i] << (int)scalar;
        }
        return result;
    }
    public static bool operator ==(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
            return false;

        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] != v2[i])
                return false;
        }
        return true;
    }

    public static bool operator !=(Vector v1, Vector v2)
    {
        return !(v1 == v2);
    }

    public static bool operator >(Vector v1, Vector v2)
    {
        return v1.Size > v2.Size;
    }

    public static bool operator <(Vector v1, Vector v2)
    {
        return v1.Size < v2.Size;
    }

    public static bool operator >=(Vector v1, Vector v2)
    {
        return v1.Size >= v2.Size;
    }

    public static bool operator <=(Vector v1, Vector v2)
    {
        return v1.Size <= v2.Size;
    }
}
//Створити клас MatrixLong (матриця цілих чисел)
public class MatrixLong
{
    // Поля
    protected long[,] LongArray; // масив
    protected uint n, m;         // розміри матриці
    protected int codeError;     // код помилки
    protected static int num_m;  // кількість матриць

    // Властивості
    public uint N => n;          // розмірність матриці n 
    public uint M => m;          // розмірність матриці m 
    public int CodeError         // код помилки 
    {
        get { return codeError; }
        set { codeError = value; }
    }
    public MatrixLong()
    {
        n = 1;
        m = 1;
        LongArray = new long[n, m];
        LongArray[0, 0] = 0;
        num_m++;
    }

    public MatrixLong(uint n, uint m)
    {
        this.n = n;
        this.m = m;
        LongArray = new long[n, m];
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                LongArray[i, j] = 0;
            }
        }
        num_m++;
    }
    public MatrixLong(uint n, uint m, long initValue)
    {
        this.n = n;
        this.m = m;
        LongArray = new long[n, m];
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                LongArray[i, j] = initValue;
            }
        }
        codeError = 0;
        num_m++;
    }
    ~MatrixLong()
    {
        Console.WriteLine("Destructor is called. Matrix destroyed.");
    }
    public void InputMatrix()
    {
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                Console.Write($"Enter element at position [{i},{j}]: ");
                LongArray[i, j] = Convert.ToInt64(Console.ReadLine());
            }
        }
    }

    public void PrintMatrix()
    {
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                Console.Write($"{LongArray[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    public void SetAllElements(long value)
    {
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                LongArray[i, j] = value;
            }
        }
    }
    public static int CountMatrices()
    {
        return num_m;
    }
    public void SetElement(uint i, uint j, long value)
    {
        if (i < n && j < m)
        {
            LongArray[i, j] = value;
        }
        else
        {
            Console.WriteLine("Invalid indices");
        }
    }
    public long this[uint i, uint j]
    {
        get
        {
            if (i < n && j < m)
            {
                return LongArray[i, j];
            }
            else
            {
                codeError = -1;
                return 0;
            }
        }
        set
        {
            if (i < n && j < m)
            {
                LongArray[i, j] = value;
            }
            else
            {
                codeError = -1;
            }
        }
    }
    public static MatrixLong operator ++(MatrixLong matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                matrix.LongArray[i, j]++;
            }
        }
        return matrix;
    }

    public static MatrixLong operator --(MatrixLong matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                matrix.LongArray[i, j]--;
            }
        }
        return matrix;
    }

    public static bool operator true(MatrixLong matrix)
    {
        if (matrix.n != 0 && matrix.m != 0)
        {
            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    if (matrix.LongArray[i, j] != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public static bool operator false(MatrixLong matrix)
    {
        if (matrix.n == 0 || matrix.m == 0)
        {
            return true;
        }
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                if (matrix.LongArray[i, j] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public static MatrixLong operator ~(MatrixLong matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                matrix.LongArray[i, j] = ~matrix.LongArray[i, j];
            }
        }
        return matrix;
    }
    public static MatrixLong operator +(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for addition.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result.LongArray[i, j] = matrix1.LongArray[i, j] + matrix2.LongArray[i, j];
            }
        }
        return result;
    }
    public static MatrixLong operator +(MatrixLong matrix, long scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] + scalar;
            }
        }
        return result;
    }
    public static MatrixLong operator -(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for subtraction.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result.LongArray[i, j] = matrix1.LongArray[i, j] - matrix2.LongArray[i, j];
            }
        }
        return result;
    }
    public static MatrixLong operator -(MatrixLong matrix, long scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] - scalar;
            }
        }
        return result;
    }
    public static MatrixLong operator *(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.m != matrix2.n)
        {
            Console.WriteLine("Number of columns in the first matrix must be equal to the number of rows in the second matrix for multiplication.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix2.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix2.m; j++)
            {
                result.LongArray[i, j] = 0;
                for (uint k = 0; k < matrix1.m; k++)
                {
                    result.LongArray[i, j] += matrix1.LongArray[i, k] * matrix2.LongArray[k, j];
                }
            }
        }
        return result;
    }
    public static MatrixLong operator *(MatrixLong matrix, long scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] * scalar;
            }
        }
        return result;
    }
    public static MatrixLong operator /(MatrixLong matrix, long scalar)
    {
        if (scalar == 0)
        {
            Console.WriteLine("Division by zero is not allowed.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] / scalar;
            }
        }
        return result;
    }
    public static MatrixLong operator /(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix2.n != matrix2.m || matrix1.m != matrix2.n)
        {
            Console.WriteLine("The number of rows in the second matrix must be equal to the number of columns, and the number of columns in the first matrix must be equal to the number of rows in the second matrix for division.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix2.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix2.m; j++)
            {
                result.LongArray[i, j] = 0;
                for (uint k = 0; k < matrix1.m; k++)
                {
                    result.LongArray[i, j] += matrix1.LongArray[i, k] / matrix2.LongArray[k, j];
                }
            }
        }
        return result;
    }
    public static MatrixLong operator %(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix2.n != matrix2.m || matrix1.m != matrix2.n)
        {
            Console.WriteLine("The number of rows in the second matrix must be equal to the number of columns, and the number of columns in the first matrix must be equal to the number of rows in the second matrix for modulo operation.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix2.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix2.m; j++)
            {
                result.LongArray[i, j] = 0;
                for (uint k = 0; k < matrix1.m; k++)
                {
                    result.LongArray[i, j] += matrix1.LongArray[i, k] % matrix2.LongArray[k, j];
                }
            }
        }
        return result;
    }

    public static MatrixLong operator %(MatrixLong matrix, long scalar)
    {
        if (scalar == 0)
        {
            Console.WriteLine("Division by zero is not allowed for modulo operation.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] % scalar;
            }
        }
        return result;
    }
   
    public static MatrixLong operator |(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for bitwise OR operation.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result.LongArray[i, j] = matrix1.LongArray[i, j] | matrix2.LongArray[i, j];
            }
        }
        return result;
    }

    public static MatrixLong operator |(MatrixLong matrix, ulong scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] | (long)scalar;
            }
        }
        return result;
    }

    public static MatrixLong operator ^(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for bitwise XOR operation.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result.LongArray[i, j] = matrix1.LongArray[i, j] ^ matrix2.LongArray[i, j];
            }
        }
        return result;
    }
    public static MatrixLong operator ^(MatrixLong matrix, ulong scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] ^ (long)scalar;
            }
        }
        return result;
    }
    
 
    public static MatrixLong operator >>(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for right bitwise shift operation.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result.LongArray[i, j] = matrix1.LongArray[i, j] >> (int)matrix2.LongArray[i, j];
            }
        }
        return result;
    }

 
    public static MatrixLong operator >>(MatrixLong matrix, uint scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] >> (int)scalar;
            }
        }
        return result;
    }

  
    public static MatrixLong operator <<(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for left bitwise shift operation.");
            return null;
        }

        MatrixLong result = new MatrixLong(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result.LongArray[i, j] = matrix1.LongArray[i, j] << (int)matrix2.LongArray[i, j];
            }
        }
        return result;
    }

   
    public static MatrixLong operator <<(MatrixLong matrix, uint scalar)
    {
        MatrixLong result = new MatrixLong(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result.LongArray[i, j] = matrix.LongArray[i, j] << (int)scalar;
            }
        }
        return result;
    }
    public static bool operator ==(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (ReferenceEquals(matrix1, matrix2))
        {
            return true;
        }

        if (matrix1 is null || matrix2 is null)
        {
            return false;
        }

        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            return false;
        }

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix1.LongArray[i, j] != matrix2.LongArray[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator !=(MatrixLong matrix1, MatrixLong matrix2)
    {
        return !(matrix1 == matrix2);
    }

    public static bool operator >(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for comparison.");
            return false;
        }

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix1.LongArray[i, j] <= matrix2.LongArray[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator >=(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for comparison.");
            return false;
        }

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix1.LongArray[i, j] < matrix2.LongArray[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator <(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for comparison.");
            return false;
        }

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix1.LongArray[i, j] >= matrix2.LongArray[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator <=(MatrixLong matrix1, MatrixLong matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
        {
            Console.WriteLine("Matrix dimensions must be the same for comparison.");
            return false;
        }

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix1.LongArray[i, j] > matrix2.LongArray[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("1. Task 1");
        Console.WriteLine("2. Task 2");
        Console.WriteLine("3. Task 3");
        Console.WriteLine("Exit");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Enter nominal: ");
                int nominal = int.Parse(Console.ReadLine());
                Console.Write("Enter number: ");
                int num = int.Parse(Console.ReadLine());
                Console.Write("Enter the price: ");
                int price = int.Parse(Console.ReadLine());

                Money money = new Money(nominal, num);
                money.Print();
                Console.WriteLine($"Can buy: {money.CanBuy(price)}");
                Console.WriteLine($"Calculate items: {money.CalculateItems(price)}");
                Console.WriteLine("Incrementing money:");

                // Використання індексатора
                money[0]++;
                money[1]++; ;
                money.Print();

                Console.WriteLine("Decrementing money:");
                money--;
                money.Print();

                Console.WriteLine($"Is second not zero: {!money}");

                Console.Write("Enter scalar value to add: ");
                int scalar = int.Parse(Console.ReadLine());
                money += scalar;
                Console.WriteLine($"Money after adding scalar: {money.Nominal}, {money.Num}");

                Console.WriteLine("Converting Money object to string...");
                string moneyString = (string)money;
                Console.WriteLine($"Money as string: {moneyString}");

                Console.WriteLine("Converting string to Money object...");
                Money newMoney = (Money)moneyString;
                newMoney.Print(); break;
            case "2":
                Vector v1 = new Vector(3, 2);
                Vector v2 = new Vector(3, 3);

                Console.WriteLine("Vector 1:");
                v1.Output();
                Console.WriteLine("Vector 2:");
                v2.Output();

                Console.WriteLine("Vector 1 + Vector 2:");
                (v1 + v2).Output();

                Console.WriteLine("Vector 1 + 5:");
                (v1 + 5).Output();
                Console.WriteLine("Vector 1 * 5:");
                (v1 * 5).Output();

                Console.WriteLine("Vector 1 % Vector 2:");
                (v1 % v2).Output();

                Console.WriteLine("Vector 1 % 3:");
                (v1 % 3).Output();
                Console.WriteLine("Is Vector 1 equal to Vector 2? " + (v1 == v2));
                Console.WriteLine("Is Vector 1 not equal to Vector 2? " + (v1 != v2));
                Console.WriteLine("Is Vector 1 greater than Vector 2? " + (v1 > v2));
                Console.WriteLine("Is Vector 1 less than Vector 2? " + (v1 < v2)); break;
            case "3":
                MatrixLong matrix1 = new MatrixLong(2, 2, 2);
                MatrixLong matrix2 = new MatrixLong(2, 2, 3);
                Console.WriteLine("Matrix1:");

                matrix1.PrintMatrix();
                Console.WriteLine("Matrix2:");

                matrix2.PrintMatrix();

                MatrixLong resultAddition = matrix1 + matrix2;
                MatrixLong resultSubtraction = matrix1 - matrix2;
                MatrixLong resultMultiplication = matrix1 * matrix2;
                MatrixLong resultDivision = matrix1 / matrix2;
                MatrixLong resultModulo = matrix1 % matrix2;
                MatrixLong resultBitwiseOR = matrix1 | matrix2;
                MatrixLong resultBitwiseXOR = matrix1 ^ matrix2;

                Console.WriteLine("Result of Addition:");
                resultAddition.PrintMatrix();

                Console.WriteLine("\nResult of Subtraction:");
                resultSubtraction.PrintMatrix();

                Console.WriteLine("\nResult of Multiplication:");
                resultMultiplication.PrintMatrix();

                Console.WriteLine("\nResult of Division:");
                resultDivision.PrintMatrix();

                Console.WriteLine("\nResult of Modulo:");
                resultModulo.PrintMatrix();

                Console.WriteLine("\nResult of Bitwise OR:");
                resultBitwiseOR.PrintMatrix();

                Console.WriteLine("\nResult of Bitwise XOR:");
                resultBitwiseXOR.PrintMatrix();

                Console.WriteLine("\nComparison (Equal): " + (matrix1 == matrix2));
                Console.WriteLine("Comparison (Not Equal): " + (matrix1 != matrix2));
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }

    }
    }