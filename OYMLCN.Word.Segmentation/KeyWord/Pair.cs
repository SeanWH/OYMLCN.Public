#pragma warning disable
namespace OYMLCN.Word.KeyWord
{
    public class Pair
    {
        public string Word { get; set; }
        public string Flag { get; set; }
        public Pair(string word, string flag)
        {
            Word = word;
            Flag = flag;
        }

        public override string ToString() =>
            string.Format("{0}/{1}", Word, Flag);
    }
}
