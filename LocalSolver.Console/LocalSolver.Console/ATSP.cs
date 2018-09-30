using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSolver.Console
{
    class ATSP
    {
        public static void Execute()
        {
            const int m = int.MaxValue;
            var distance = new int[6][]
            {
                new [] { m, 3, 6, 2, 8, 1 },
                new [] { 4, m, 3, 4, 4, 5 },
                new [] { 3, 2, m, 6, 3, 5 },
                new [] { 4, 2, 5, m, 4, 4 },
                new [] { 3, 3, 2, 6, m, 4 },
                new [] { 7, 4, 5, 7, 6, m },
            };

            using (var localsolver = new localsolver.LocalSolver())
            {
                // Declares the optimization model.
                var model = localsolver.GetModel();
                var nCities = distance.GetLength(0);
                var cities = model.List(nCities);
                model.AddConstraint(model.Count(cities) == nCities);
                var distArray = model.Array(distance);
                var distSelector = model.Function(i => distArray[cities[i - 1], cities[i]]);

                // sum of all used ways
                var obj = model.Sum(model.Range(1, nCities), distSelector) + distArray[cities[nCities - 1], cities[0]];
                model.Minimize(obj);
                model.Close();
                // Parameterizes the solver.
                var phase = localsolver.CreatePhase();
                phase.SetTimeLimit(10);
                //phase.SetIterationLimit(30);
                localsolver.Solve();

                // extract result
                System.Console.WriteLine($"Objective val: {obj.GetValue()}");
                System.Console.WriteLine($"City order: {string.Join("->", cities.GetCollectionValue())}");
            }
        }
    }
}
