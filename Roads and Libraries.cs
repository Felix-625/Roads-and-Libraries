using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'roadsAndLibraries' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER c_lib
     *  3. INTEGER c_road
     *  4. 2D_INTEGER_ARRAY cities
     */

    public static long roadsAndLibraries(int n, int c_lib, int c_road, List<List<int>> cities)
    {
         if (c_lib <= c_road)
    {
        return (long)n * c_lib;
    }

    // Union-Find data structure
    int[] parent = new int[n + 1];
    int[] size = new int[n + 1];
    
    // Initialize Union-Find
    for (int i = 1; i <= n; i++)
    {
        parent[i] = i;
        size[i] = 1;
    }

    // Union cities that are connected by roads
    foreach (var city in cities)
    {
        int u = city[0];
        int v = city[1];
        Union(u, v, parent, size);
    }

    // Count component sizes
    Dictionary<int, int> componentSizes = new Dictionary<int, int>();
    for (int i = 1; i <= n; i++)
    {
        int root = Find(i, parent);
        if (componentSizes.ContainsKey(root))
        {
            componentSizes[root]++;
        }
        else
        {
            componentSizes[root] = 1;
        }
    }

    // Calculate total cost
    long totalCost = 0;
    foreach (var componentSize in componentSizes.Values)
    {
        totalCost += c_lib + (long)(componentSize - 1) * c_road;
    }

    return totalCost;
}

private static int Find(int x, int[] parent)
{
    if (parent[x] != x)
    {
        parent[x] = Find(parent[x], parent);
    }
    return parent[x];
}

private static void Union(int x, int y, int[] parent, int[] size)
{
    int rootX = Find(x, parent);
    int rootY = Find(y, parent);
    
    if (rootX != rootY)
    {
        if (size[rootX] < size[rootY])
        {
            parent[rootX] = rootY;
            size[rootY] += size[rootX];
        }
        else
        {
            parent[rootY] = rootX;
            size[rootX] += size[rootY];
        }
    }

    }
  }


class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            int c_lib = Convert.ToInt32(firstMultipleInput[2]);

            int c_road = Convert.ToInt32(firstMultipleInput[3]);

            List<List<int>> cities = new List<List<int>>();

            for (int i = 0; i < m; i++)
            {
                cities.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(citiesTemp => Convert.ToInt32(citiesTemp)).ToList());
            }

            long result = Result.roadsAndLibraries(n, c_lib, c_road, cities);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
