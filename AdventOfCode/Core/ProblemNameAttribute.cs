namespace AdventOfCode.Core
{
    public class ProblemNameAttribute : Attribute
    {
        public readonly string Name;
        public ProblemNameAttribute(string name)
        {
            Name = name;
        }
    }
}
