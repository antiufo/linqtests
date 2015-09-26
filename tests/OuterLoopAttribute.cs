namespace System.Linq.Tests
{
    internal class OuterLoopAttribute : Attribute
    {
    }
    internal class ActiveIssue : Attribute
    {
        public ActiveIssue(int id) { }
    }
}