public static class Day2
{
    private static int _maxNumberLength = 0;
    public static void Run(string[] input)
    {
        Part1(input);
    }

    private static void Part1(string[] input)
    {
        long sum1 = 0;
        long sum2 = 0;

        var numbers = input[0]
            .Split(",")
            .Select(line =>
            {
                var dash = line.IndexOf('-');
                var start = long.Parse(line[..dash]);
                var end = long.Parse(line[(dash+1)..]);
                return (start, end);
            });

        foreach (var (start, end) in numbers)
        {
            for (var i = start; i <= end; i++)
            {
                var num = i.ToString();

                if (num.Length == 1)
                {
                    continue;
                } 
                
                var half = num.Length / 2;
            
                if (Part1Check(num, half))
                {
                    sum1 += i;
                    sum2 += i;
                }
                else if (Part2Check(num, num.Length))
                {
                    sum2 += i;
                }

            }
        }

        Console.WriteLine(sum1);
        Console.WriteLine(sum2);
    }

    private static bool Part1Check(string num, int half) => num[..half] == num[half..];
    private static bool Part2Check(string num, int length)
    {
        var three = length % 3 == 0;
        if (three)
        {
            var third = length / 3;
            var ok = true;
            for (int i = 0; i < third; i++)
            {
                if (num[i] != num[i + third] || 
                    num[i + third] != num[i + third * 2])
                {
                    ok = false;
                    break;
                }
            }

            if (ok) return true;
        }

        var four = length % 4 == 0;
        if (four)
        {
            var fourth = length / 4;
            var ok = true;

            for (int i = 0; i < fourth; i++)
            {
                if (num[i] != num[i + fourth] ||
                    num[i + fourth] != num[i + fourth * 2] ||
                    num[i + fourth * 2] != num[i + fourth * 3])
                {
                    ok = false;
                    break;
                }
            }

            if (ok) return true;
        }

        var five = length % 5 == 0;
        if (five)
        {
            var fifth = length / 5;
            var ok = true;

            for (int i = 0; i < fifth; i++)
            {
                if (num[i] != num[i + fifth] ||
                    num[i + fifth] != num[i + fifth * 2] ||
                    num[i + fifth * 2] != num[i + fifth * 3] ||
                    num[i + fifth * 3] != num[i + fifth * 4])
                {
                    ok = false;
                    break;
                }
            }

            if (ok) return true;
        }

        var lastChance = true;
        char c = num[0];
        for (int i = 1; i < num.Length; i++)
        {
            if (num[i] != c)
            {
                lastChance = false;
            } 
        }

        return lastChance;
    }
}