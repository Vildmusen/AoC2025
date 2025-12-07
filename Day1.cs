public static class Day1
{
    public static void Run(string[] input)
    {
        Part1(input);
    }

    private static void Part1(string[] input)
    {
        var count1 = 0;
        var count2 = 0;
        var current = 50;

        foreach (var line in input)
        {
            current = line[0] switch
            {
                'L' => current - int.Parse(line[1..]),
                'R' => current + int.Parse(line[1..]),
                _ => throw new NotImplementedException()
            };

            while (current < 0)
            {
                count2++;
                current += 100;
            }

            while (current >= 100)
            {
                count2++;
                current -= 100;
            }

            if (current == 0)
            {
                count1++;
            }
        }

        Console.WriteLine(count1);
        Console.WriteLine(count2);
    }
}