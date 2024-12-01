using System.Reflection;

namespace AdventOfCode.Core
{
    public static class SolverExtensions
    {
        public static string GetName(this ISolver solver)
        {
            return (solver.GetType().GetCustomAttribute(typeof(ProblemNameAttribute)) as ProblemNameAttribute)?.Name ?? "Unknown name";
        }
        
        public static string GetName(this Type solverType)
        {
            return (solverType.GetCustomAttribute(typeof(ProblemNameAttribute)) as ProblemNameAttribute)?.Name ?? "Unknown name";
        }

        public static string GetInput(this ISolver solver)
        {
            var filePath = Path.Combine(solver.WorkingDir(), "input.txt");
            var input = File.ReadAllText(filePath);
            return input;
        }
        
        public static string GetSampleInput(this ISolver solver)
        {
            var filePath = Path.Combine(solver.WorkingDir(), "sample.txt");
            var input = File.ReadAllText(filePath);
            return input;
        }

        public static int Year(this ISolver solver)
        {
            return Year(solver.GetType());
        }

        public static int Year(this Type solverType)
        {
            if (solverType.FullName == null) return 0;
            return int.Parse(solverType.FullName.Split('.')[1][1..]);
        }

        public static int Day(this ISolver solver)
        {
            return Day(solver.GetType());
        }

        public static int Day(this Type solverType)
        {
            if (solverType.FullName == null) return 0;
            return int.Parse(solverType.FullName.Split('.')[2][3..]);
        }

        public static string WorkingDir(int year)
        {
            return Path.Combine(year.ToString());
        }

        public static string WorkingDir(int year, int day)
        {
            return Path.Combine(WorkingDir(year), "Day" + day.ToString("00"));
        }

        public static string WorkingDir(this ISolver solver)
        {
            var solverAssembly = Assembly.GetAssembly(solver.GetType()) ?? throw new InvalidOperationException("Assembly cannot be loaded");
            var assemblyDir = Path.GetDirectoryName(solverAssembly.Location) ?? throw new InvalidOperationException("Assembly location not found");
            return Path.Combine(assemblyDir, WorkingDir(solver.Year(), solver.Day()));
        }

        public static ISolver Activate(this Type solverType)
        {
            return Activator.CreateInstance(solverType) as ISolver ?? throw new InvalidOperationException("Type is not a solver");
        }
    }
}
