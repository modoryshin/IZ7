while (true)
{
    Console.Write("Введите Число экспертов: ");
    int experts = Convert.ToInt32(Console.ReadLine());
    Console.Clear();

    Console.Write("Введите число целей: ");
    int aims = Convert.ToInt32(Console.ReadLine());
    Console.Clear();

    List<double[,]> matrixList = new List<double[,]>();
    for(int i = 0; i < experts; i++)
    {
        double[,] matrix = new double[aims, aims];
        for(int j = 0; j < aims; j++)
        {
            for(int c = 0; c < aims; c++)
            {
                if (c != j)
                {
                    Console.Write("Для матрицы эксперта " + (i + 1).ToString() + " введите значение " + (j + 1).ToString() + " " + (c + 1).ToString() + ":");
                    string temp = Console.ReadLine();
                    if (temp.Contains('/'))
                    {
                        string[] nums = temp.Split('/');
                        matrix[j, c] = Math.Round(Convert.ToDouble(nums[0]) / Convert.ToDouble(nums[1]), 3);
                    }
                    else
                    {
                        matrix[j, c] = Convert.ToDouble(temp);
                    }
                }
                else
                {
                    matrix[j, c] = 0;
                }
            }
            Console.Clear();
        }
        matrixList.Add(matrix);
    }
    Console.Clear();

    List<double[,]> allweights = new List<double[,]>();
    for(int c = 0; c < matrixList.Count();  c++)
    {
        double[,] weights = new double[2, aims];
        for(int i = 0; i < aims; i++)
        {
            for(int j = 0; j < aims; j++)
            {
                weights[0, i] = weights[0, i] + matrixList[c][i,j];
            }
            weights[1, i] = i + 1;
        }
        allweights.Add(weights);
    }

    double N = aims * (aims - 1);
    foreach(double[,] item in allweights)
    {
        for(int i = 0; i < aims; i++)
        {
            item[0, i] = Math.Round((item[0,i]/N), 3);
        }
    }

    double[,] results = new double[2, aims];
    for(int i = 0; i < aims; i++)
    {
        for(int j = 0; j < experts; j++)
        {
            results[0, i] = results[0, i] + allweights[j][0, i]; 
        }
        results[1, i] = (i + 1);
    }

    for (int i = 0; i < aims; i++)
    {
        for (int j = 0; j < aims - 1; j++)
        {
            if (results[0, j] < results[0, j + 1])
            {
                double t = results[0, j + 1];
                results[0, j + 1] = results[0, j];
                results[0, j] = t;
                t = results[1, j + 1];
                results[1, j + 1] = results[1, j];
                results[1, j] = t;
            }
        }
    }
    Console.WriteLine("Наиболее выгодная цель: " + results[1, 0]);
    Console.WriteLine("Завершить работу? +/-");
    string ans = Console.ReadLine();
    if (ans == "-")
    {
        Console.Clear();
    }
    else { break; }
}