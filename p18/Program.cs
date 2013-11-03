using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p18
{
    class Program
    {
        static List<List<int>> readFile(string fileName)
        {
            List<List<int>> result = new List<List<int>>(); 
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            while ( (line = file.ReadLine()) != null)
            {
                List<int> l = new List<int>();
                string[] tokens = line.Split(' ');
                for (int i = 0; i < tokens.Length; ++i)
                {
                    l.Add(Convert.ToInt32(tokens[i]));
                }
                result.Add(l);
            }
            return result;
        }
        static void Main(string[] args)
        {
            string fileName = args.Length == 0 ? "input.txt" : args[0];
            List<List<int>> data = readFile(fileName);
            int sum = ComputeSum(data, new Node { Value = data[0][0], Index = 0, Line = 0 }, 0);

            System.Console.WriteLine(sum);
        }

        static int ComputeSum(List<List<int>> data, Node node, int currentSum)
        {
            currentSum += node.Value;
            List<Node> children = node.getChildren(data);
            if (children == null)
                return currentSum;

            int s1 = ComputeSum(data, children[0], currentSum);
            int s2 = ComputeSum(data, children[1], currentSum);
            return s1 > s2 ? s1 : s2;
        }

        class Node
        {
            public int Value { set; get; }
            public int Index { get; set; }
            public int Line { get; set; }

            public List<Node> getChildren(List<List<int>> data)
            {
                if (Line == data.Count - 1)
                    return null;

                List<int> l = data[Line + 1];

                List<Node> result = new List<Node>();
                result.Add(new Node { Value = l[Index], Index = Index, Line = Line + 1 });
                result.Add(new Node { Value = l[Index + 1], Index = Index + 1, Line = Line + 1 });
                return result;
            }
        }
    }
}
