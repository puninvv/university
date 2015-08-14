using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangulationWithAfineTransformation.Classes
{
    internal class TriangulationsMatcher
    {
        private List<Triangle> TrianglesFrom;
        private List<Triangle> TrianglesTo;

        public TriangulationsMatcher(List<Triangle> trianglesFrom, List<Triangle> trianglesTo)
        {
            TrianglesFrom = trianglesFrom;
            TrianglesTo = trianglesTo;
        }

        private List<OptimumConversionBuilder> CreatListWithDistances()
        {
            List<OptimumConversionBuilder> result = null;

            foreach (Triangle tFrom in TrianglesFrom)
            {
                OptimumConversionBuilder tmpToAdd = null;
                foreach (Triangle tTo in TrianglesTo)
                {
                    OptimumConversionBuilder tmpTo = new OptimumConversionBuilder(tFrom, tTo);
                    if (tmpToAdd == null || tmpTo.Distance < tmpToAdd.Distance)
                        tmpToAdd = tmpTo;
                }
                result.Add(tmpToAdd);
            }

            result.Sort();

            return result;
        }

        private double[] Match(double distance)
        {
            double[] result = new double[3];            //result[0] - полностью совпали
                                                        //result[1] - почти совпали (в пределах погрешности)
                                                        //result[2] - сумма расстояний

            List<OptimumConversionBuilder> builders = new List<OptimumConversionBuilder>();
            OptimumConversionBuilder resultBuilder = null;

            foreach (OptimumConversionBuilder builder in builders)
            {
                double[] tmpResult = new double[3];

                foreach (Triangle tFrom in TrianglesFrom)
                {
                    Triangle transformedTFrom = tFrom.GetTransformation(builder.Dx, builder.Dy, builder.Phi);

                    bool equalsAdded = false;

                    foreach (Triangle tTo in TrianglesTo)
                    {
                        if (transformedTFrom.Equals(tTo))
                        {
                            tmpResult[0]++;
                            tmpResult[2] = transformedTFrom.GetDistanceTo(tTo);
                            equalsAdded = true;
                            break;
                        }
                    }

                    if (!equalsAdded)
                    {
                        foreach (Triangle tTo in TrianglesTo)
                        {
                            if (transformedTFrom.Equals(tTo, distance))
                            {
                                tmpResult[1]++;
                                tmpResult[2] = transformedTFrom.GetDistanceTo(tTo);
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}