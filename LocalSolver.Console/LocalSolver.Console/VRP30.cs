using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using localsolver;

namespace LocalSolver.Console
{
    /// <summary>
    /// VRP with 30 visits, only optimizing distance.
    /// </summary>
    class VRP30
    {
        public static void Execute()
        {

            const int numberOfSantas = 6;
            const int numberOfVisits = 30;
            const int m = int.MaxValue;
            var distanceHome = new int[numberOfVisits]
            {
                106,  378,    305,    337,    483,    337,    316,    142,    306,    242,    130,    0,  381,    358,    255,    167,    34, 345,    972,    1116,   1160,   1113,   467,    534,    1073,   956,    937,    1020,   254,    389
            };
            var distance = new int[numberOfVisits][]
            {
                new int[numberOfVisits] { 0,    273,    274,    366,    383,    429,    408,    234,    398,    334,    222,    92, 302,    253,    150,    156,    63, 240,    1064,   1208,   1252,   1205,   496,    563,    1165,   1048,   1029,   1112,   264,    418},
                new int[numberOfVisits] { 246,  0,  203,    219,    181,    675,    654,    480,    644,    580,    468,    338,    100,    428,    107,    331,    309,    120,    1310,   1454,   1498,   1451,   416,    483,    1411,   1294,   1275,   1358,   439,    504},
                new int[numberOfVisits] { 268,  215,    0,  180,    207,    621,    600,    426,    590,    526,    340,    284,    105,    450,    184,    353,    255,    264,    1256,   1400,   1444,   1397,   377,    444,    1357,   1240,   1221,   1304,   461,    348},
                new int[numberOfVisits] { 414,  275,    224,    0,  267,    709,    688,    514,    678,    614,    426,    372,    165,    632,    311,    539,    343,    324,    1344,   1488,   1532,   1485,   197,    264,    1445,   1328,   1309,   1392,   626,    311},
                new int[numberOfVisits] { 345,  170,    184,    200,    0,  774,    753,    579,    743,    679,    495,    437,    81, 527,    206,    430,    408,    219,    1409,   1553,   1597,   1550,   293,    360,    1510,   1393,   1374,   1457,   538,    485},
                new int[numberOfVisits] { 480,  753,    680,    712,    858,    0,  186,    236,    176,    355,    443,    375,    756,    733,    630,    525,    409,    720,    1085,   1229,   977,    1008,   842,    909,    1094,   1069,   1050,   1041,   612,    743},
                new int[numberOfVisits] { 441,  714,    641,    673,    819,    161,    0,  224,    8,  262,    404,    336,    717,    694,    591,    486,    370,    681,    992,    1136,   1069,   1100,   803,    870,    1093,   976,    957,    1040,   573,    704},
                new int[numberOfVisits] { 244,  517,    444,    476,    622,    195,    208,    0,  198,    119,    207,    139,    520,    497,    394,    289,    173,    484,    849,    993,    1037,   990,    606,    673,    950,    833,    814,    897,    376,    507},
                new int[numberOfVisits] { 433,  706,    633,    665,    811,    153,    11, 216,    0,  254,    396,    328,    709,    686,    583,    478,    362,    673,    984,    1128,   1061,   1092,   795,    862,    1085,   968,    949,    1032,   565,    696},
                new int[numberOfVisits] { 329,  602,    529,    561,    707,    299,    224,    104,    214,    0,  292,    224,    605,    582,    479,    374,    258,    569,    746,    874,    918,    871,    691,    758,    831,    714,    695,    778,    338,    592},
                new int[numberOfVisits] { 253,  526,    379,    409,    557,    423,    402,    228,    392,    328,    0,  148,    455,    506,    403,    298,    182,    493,    1058,   1202,   1246,   1199,   539,    606,    1159,   1042,   1023,   1106,   385,    393},
                new int[numberOfVisits] { 106,  378,    305,    337,    483,    337,    316,    142,    306,    242,    130,    0,  381,    358,    255,    167,    34, 345,    972,    1116,   1160,   1113,   467,    534,    1073,   956,    937,    1020,   254,    389}, // depot
                new int[numberOfVisits] { 285,  110,    103,    119,    102,    695,    674,    500,    664,    600,    414,    358,    0,  467,    146,    370,    329,    159,    1330,   1474,   1518,   1471,   316,    383,    1431,   1314,   1295,   1378,   478,    404},
                new int[numberOfVisits] { 203,  405,    406,    569,    515,    632,    611,    437,    601,    537,    425,    295,    434,    0,  282,    207,    266,    372,    1267,   1411,   1455,   1408,   699,    766,    1368,   1251,   1232,   1315,   315,    621},
                new int[numberOfVisits] { 139,  123,    179,    271,    233,    568,    547,    373,    537,    473,    361,    231,    152,    321,    0,  224,    202,    90, 1203,   1347,   1391,   1344,   468,    535,    1304,   1187,   1168,   1251,   332,    421},
                new int[numberOfVisits] { 128,  330,    331,    465,    440,    448,    427,    253,    417,    353,    241,    128,    359,    229,    207,    0,  162,    297,    1083,   1227,   1271,   1224,   595,    662,    1184,   1067,   1048,   1131,   108,    517},
                new int[numberOfVisits] { 70,   343,    266,    298,    444,    365,    344,    170,    334,    270,    158,    28, 342,    323,    220,    195,    0,  310,    954,    1098,   1008,   1039,   711,    778,    1055,   938,    919,    1002,   481,    612},
                new int[numberOfVisits] { 217,  124,    256,    272,    234,    646,    625,    451,    615,    551,    439,    309,    153,    399,    78, 302,    280,    0,  1281,   1425,   1469,   1422,   469,    536,    1382,   1265,   1246,   1329,   410,    499},
                new int[numberOfVisits] { 1114, 1387,   1314,   1346,   1522,   1084,   1009,   889,    999,    794,    1077,   1009,   1420,   1367,   1264,   1159,   970,    1354,   0,  245,    316,    269,    1476,   1543,   229,    112,    276,    176,    1052,   1377},
                new int[numberOfVisits] { 1280, 1553,   1480,   1512,   1688,   1250,   1175,   1055,   1165,   951,    1243,   1175,   1586,   1533,   1430,   1325,   1136,   1520,   259,    0,  353,    306,    1642,   1709,   139,    252,    315,    184,    1289,   1543},
                new int[numberOfVisits] { 1355, 1628,   1555,   1587,   1763,   1034,   1145,   1130,   1135,   1026,   1318,   1250,   1661,   1608,   1505,   1400,   1082,   1595,   361,    391,    0,  149,    1532,   1599,   235,    250,    282,    182,    1364,   1540},
                new int[numberOfVisits] { 1311, 1584,   1511,   1543,   1719,   1068,   1179,   1086,   1169,   982,    1274,   1206,   1617,   1564,   1461,   1356,   1116,   1551,   317,    347,    152,    0,  1566,   1633,   191,    206,    238,    138,    1320,   1574},
                new int[numberOfVisits] { 577,  505,    454,    230,    387,    872,    851,    677,    841,    777,    589,    535,    395,    830,    541,    702,    758,    554,    1507,   1651,   1508,   1539,   0,  67, 1608,   1491,   1472,   1555,   789,    474},
                new int[numberOfVisits] { 646,  574,    523,    299,    456,    941,    920,    746,    910,    846,    658,    604,    464,    899,    610,    771,    827,    623,    1576,   1720,   1577,   1608,   69, 0,  1677,   1560,   1541,   1624,   858,    543},
                new int[numberOfVisits] { 1247, 1520,   1447,   1479,   1655,   1130,   1142,   1022,   1132,   918,    1210,   1142,   1553,   1500,   1397,   1292,   1103,   1487,   253,    156,    214,    167,    1609,   1676,   0,  142,    176,    45, 1256,   1510},
                new int[numberOfVisits] { 1125, 1398,   1325,   1357,   1533,   1095,   1020,   900,    1010,   796,    1088,   1020,   1431,   1378,   1275,   1170,   981,    1365,   131,    257,    224,    177,    1487,   1554,   137,    0,  184,    84, 1134,   1388},
                new int[numberOfVisits] { 1109, 1382,   1309,   1341,   1517,   1079,   1004,   884,    994,    780,    1072,   1004,   1415,   1362,   1259,   1154,   965,    1349,   290,    322,    251,    204,    1471,   1538,   166,    179,    0,  113,    1118,   1372},
                new int[numberOfVisits] { 1202, 1475,   1402,   1434,   1610,   1085,   1097,   977,    1087,   873,    1165,   1097,   1508,   1455,   1352,   1247,   1058,   1442,   208,    209,    169,    122,    1564,   1631,   54, 97, 131,    0,  1211,   1465},
                new int[numberOfVisits] { 241,  443,    444,    557,    553,    540,    519,    345,    509,    337,    333,    220,    472,    342,    320,    113,    426,    410,    996,    1211,   1255,   1208,   687,    754,    1168,   1051,   1032,   1115,   0,  609},
                new int[numberOfVisits] { 505,  606,    443,    350,    598,    775,    754,    580,    744,    680,    445,    463,    496,    758,    521,    630,    661,    611,    1410,   1554,   1525,   1551,   480,    547,    1511,   1394,   1375,   1458,   717,    0},
            };

            var visitDuration = new int[numberOfVisits]
            {
                2700,
                1500,
                2100,
                3600,
                1500,
                1200,
                1800,
                1500,
                1500,
                1800,
                2100,
                0,
                1800,
                1200,
                1500,
                1500,
                1800,
                1500,
                1500,
                1500,
                1500,
                1200,
                1200,
                1500,
                1800,
                1500,
                1500,
                1500,
                2400,
                1500
            };

            using (var localsolver = new localsolver.LocalSolver())
            {
                // Declares the optimization model.
                var model = localsolver.GetModel();

                var santaUsed = new LSExpression[numberOfSantas];
                var visitSequences = new LSExpression[numberOfSantas];
                var routeDistances = new LSExpression[numberOfSantas];
                var santaVisitDurations = new LSExpression[numberOfSantas];


                // Sequence of customers visited by each truck.
                for (int k = 0; k < numberOfSantas; k++)
                    visitSequences[k] = model.List(numberOfVisits);

                model.Constraint(model.Partition(visitSequences));


                var distanceArray = model.Array(distance);
                var distanceHomeArray = model.Array(distanceHome);
                var visitDurationArray = model.Array(visitDuration);

                for (int s = 0; s < numberOfSantas; s++)
                {
                    var sequence = visitSequences[s];
                    var c = model.Count(sequence);

                    santaUsed[s] = c > 0;

                    var distSelector = model.Function(i => distanceArray[sequence[i - 1], sequence[i]] + visitDurationArray[i]);
                    var visitSelector = model.Function(i => visitDurationArray[i]);
                    routeDistances[s] = model.Sum(model.Range(1, c), distSelector)
                                        + model.If(santaUsed[s], distanceHomeArray[sequence[0]] + distanceHomeArray[sequence[c - 1]], 0);
                    santaVisitDurations[s] = model.Sum(model.Range(1, c), visitSelector);

                    model.Constraint(routeDistances[s] <= (21 - 17) * 60 * 60); // working hours from 17:00 to 21:00
                }


                var totalDistance = model.Sum(routeDistances);


               // model.Minimize(totalDistance);

                // every santa same visit duration:

                var totalDuration = visitDuration.Sum();
                var numberOfSantasUsed = model.Sum(santaUsed);
                var avgDists = new LSExpression[numberOfSantas];
                for (int s = 0; s < numberOfSantas; s++)
                {

                    avgDists[s] = model.Abs(santaVisitDurations[s] -
                                            (totalDuration / numberOfSantasUsed));
                }

                var avgTime = model.Sum(avgDists);
                model.Minimize(avgTime * 5 + totalDistance);

                model.Close();

                // Parameterizes the solver.
                var phase = localsolver.CreatePhase();
                phase.SetTimeLimit(500);

                localsolver.Solve();

                // output
                for (int i = 0; i < numberOfSantas; i++)
                {
                    System.Console.WriteLine($"santa {i + 1}: ");
                    System.Console.WriteLine(string.Join("->", visitSequences[i].GetCollectionValue()));
                    System.Console.WriteLine($"route distance: {routeDistances[i].GetValue()}");

                }
            }
        }
    }
}
