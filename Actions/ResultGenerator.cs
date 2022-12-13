namespace Lotto
{
    public class ResultGenerator
    {
        public static string Randomizing()
        {
            Random r = new Random();
            var list = new List<int>();
            while (list.Count != 6)
            {
                int i = r.Next(1, 49);
                if (list.Contains(i))
                    continue;
                else
                    list.Add(i);
            }
            list.Sort();
            foreach (var number in list)
            {
                number.ToString();
            }

            return String.Join(" ", list.ToArray());
        }
    }
}
