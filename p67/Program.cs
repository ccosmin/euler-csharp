using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p67
{
    class Program
    {
        static List<List<int>> readFile(string fileName)
        {
            List<List<int>> result = new List<List<int>>();
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
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
            IDictionary<Node, int> cache = new Dictionary<Node, int>();
            string fileName = args.Length == 0 ? "input.txt" : args[0];
            List<List<int>> data = readFile(fileName);
            int sum = ComputeSum(data, new Node { Value = data[0][0], Index = 0, Line = 0 }, 0, cache);

            System.Console.WriteLine(sum);
        }

        private static int ComputeSum(List<List<int>> data, Node node, int currentSum, IDictionary<Node, int> cache)
        {
            List<Node> children = node.getChildren(data);
            if (children == null)
            {
                cache[node] = node.Value;
                return node.Value;
            }

            //currentSum += node.Value;

            Node c1 = children[0];
            int s1 = 0;
            if (cache.ContainsKey(c1))
                s1 = cache[c1];
            else
                s1 = ComputeSum(data, c1, currentSum, cache);
            Node c2 = children[1];
            int s2 = 0;
            if (cache.ContainsKey(c2))
                s2 = cache[c2];
            else
                s2 = ComputeSum(data, c2, currentSum, cache);
            int max = s1 > s2 ? s1 : s2;
            cache[node] = max + node.Value;
            return max + node.Value;
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

            public override int GetHashCode()
            {
                return Value ^ Index ^ Line;
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                Node n = obj as Node;
                if (n == null)
                    return false;
                return Value == n.Value && Index == n.Index && Line == n.Line;
            }
        }
    }
}
