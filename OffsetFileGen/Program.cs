
namespace OffsetFileGen
{
    class Program
    {
        public static readonly string[] RegionTags =
        {
            "jp_ja",
            "us_en",
            "us_fr",
            "us_es",
            "eu_en",
            "eu_fr",
            "eu_es",
            "eu_de",
            "eu_nl",
            "eu_it",
            "eu_ru",
            "kr_ko",
            "zh_cn",
            "zh_tw"
        };
        static void Main(string[] args)
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            if (args.Length == 0)
            {
                System.Console.WriteLine("Must give a path to the data.arc as an argument\nOptionally give a region as a second argument");
                return;
            }
            string regionName = "us_en";
            int region = 0;
            if (args.Length > 1)
            {
                regionName = args[1];
            }
            for (int i = 0; i < RegionTags.Length; i++)
            {
                if (regionName == RegionTags[i]) region = i;
            }
            string[] lines = System.IO.File.ReadAllLines("Hashes.txt");
            string arcPath = System.Environment.ExpandEnvironmentVariables(args[0]);
            if(!System.IO.File.Exists(arcPath))
            {
                System.Console.WriteLine("Arc not found");
                return;
            }
            ArcCross.Arc dataArc = new ArcCross.Arc(arcPath);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Offsets.txt"))
            {
                foreach (string line in lines)
                {
                    dataArc.GetFileInformation(line, out long offset, out uint compSize, out uint decompSize, out bool regional, region);
                    if (offset != 0)
                    {
                        file.WriteLine(line + ',' + offset.ToString("X"));
                    }
                   
                }
            }
        }
    }
}
