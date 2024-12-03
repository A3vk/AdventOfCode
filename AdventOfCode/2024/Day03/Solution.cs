using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode._2024.Day03;

[ProblemName("Mull It Over")]
public partial class Solution : ISolver
{
    public string PartOne(string input)
    {
        var expressions = GetMultiplyExpressions(input);
        return expressions.Sum(exp => exp.X * exp.Y).ToString();
    }

    public string PartTwo(string input)
    {
        var expressions = GetExpressions(input);

        int result = 0;
        bool isEnabled = true;
        foreach (var expression in expressions)
        {
            if (isEnabled && expression.Token == Token.Multiply)
            {
                result += expression.X * expression.Y;
                continue;
            }
            
            isEnabled = expression.Token switch
            {
                Token.Do => true,
                Token.DoNot => false,
                _ => isEnabled
            };
        }
        
        return result.ToString();
    }

    private List<Expression> GetMultiplyExpressions(string input)
    {
        var expressions = GetExpressions(input);
        return expressions.Where(x => x.Token == Token.Multiply).ToList();
    }

    private List<Expression> GetExpressions(string input)
    {
        List<Expression> expressions = [];
        
        var matches = ExpressionRegex().Matches(input);
        foreach (Match match in matches)
        {
            var token = ToToken(match.Groups[1].Value);
            var expression = new Expression() { Token = token };
            if (token == Token.Multiply)
            {
                expression = expression with
                {
                    X = int.Parse(match.Groups[3].Value), 
                    Y = int.Parse(match.Groups[4].Value)
                };
            }
            
            expressions.Add(expression);
        }
        
        return expressions;
    }

    private Token ToToken(string tokenString) => tokenString switch
    {
        "mul" => Token.Multiply,
        "do" => Token.Do,
        "don't" => Token.DoNot,
        _ => throw new ArgumentOutOfRangeException(nameof(tokenString), tokenString, "The given token string is not supported!")
    };

    private struct Expression
    {
        public Token Token { get; init; }
        public int X { get; init; }
        public int Y { get; init; }
    }

    private enum Token
    {
        Multiply,
        Do,
        DoNot
    }

    [GeneratedRegex(@"(mul|do|don't)\(((\d+),(\d+))?\)")]
    private static partial Regex ExpressionRegex();
}