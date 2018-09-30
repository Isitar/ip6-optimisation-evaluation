using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using localsolver;

namespace LocalSolver.Console
{
    class VRP2
    {
        public static void Execute()
        {

            const int numberOfSantas = 2;
            const int numberOfVisits = 6;
            const int m = int.MaxValue;
            var distanceHome = new int[numberOfVisits]
            {
                2,4,4,2,4,3
            };
            var distance = new int[numberOfVisits][]
            {
                new int[numberOfVisits] { m, 2, 3, 4, 6, 5 },
                new int[numberOfVisits] { 2, m, 1, 6, 8, 7 },
                new int[numberOfVisits] { 3, 1, m, 6, 8, 7 },
                new int[numberOfVisits] { 4, 6, 6, m, 2, 4 },
                new int[numberOfVisits] { 6, 8, 8, 2, m, 4 },
                new int[numberOfVisits] { 5, 7, 7, 4, 4, m },
            };

            using (var localsolver = new localsolver.LocalSolver())
            {
                // Declares the optimization model.
                var model = localsolver.GetModel();

                var santaUsed = new LSExpression[numberOfSantas];
                var visitSequences = new LSExpression[numberOfSantas];
                var routeDistances = new LSExpression[numberOfSantas];
                

                // Sequence of customers visited by each truck.
                for (int k = 0; k < numberOfSantas; k++)
                    visitSequences[k] = model.List(numberOfVisits);

                model.Constraint(model.Partition(visitSequences));

                // Create demands and distances as arrays to be able to access it with an "at" operator


                var distanceArray = model.Array(distance);
                var distanceHomeArray = model.Array(distanceHome);

                for (int s = 0; s < numberOfSantas; s++)
                {
                    var sequence = visitSequences[s];
                    var c = model.Count(sequence);

                    santaUsed[s] = c > 0;


                    var distSelector = model.Function(i => distanceArray[sequence[i - 1], sequence[i]]);
                    routeDistances[s] = model.Sum(model.Range(1, c), distSelector)
                                        + model.If(c > 0, distanceHomeArray[sequence[0]] + distanceHomeArray[sequence[c - 1]], 0);
                }

                
                var totalDistance = model.Sum(routeDistances);

                // Objective: minimize the number of trucks used, then minimize the distance traveled
                //model.Minimize(nbTrucksUsed);
                model.Minimize(totalDistance);

                model.Close();

                // Parameterizes the solver.
                var phase = localsolver.CreatePhase();
                phase.SetTimeLimit(2);

                localsolver.Solve();

                // output
                for (int i = 0; i < numberOfSantas; i++)
                {
                    System.Console.WriteLine($"santa {i+1}: ");
                    System.Console.WriteLine(string.Join("->", visitSequences[i].GetCollectionValue()));

                }
            }
        }
    }
}
