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
            /*
             * Для каждого билдера: 
             * 1) Преобразуем каждый треугольник из tForm в
             * 2) Находим для него совпадение или примерное совпадение
             * 3) Если совпадение не нашлось, ищем примерное совпадение 
             *      (каждая точка удалена не более чем на distance)
             * 4) Удаляем найденый треугольник из списка
             */

            foreach (OptimumConversionBuilder builder in builders)
            {
                double[] tmpResult = new double[3];
                List<Triangle> tmpTrianglesTo = new List<Triangle>(TrianglesTo);

                foreach (Triangle tFrom in TrianglesFrom)
                {
                    Triangle transformedTFrom = tFrom.GetTransformation(builder.Dx, builder.Dy, builder.Phi);
                    Triangle tFromEquals = FindEquals(transformedTFrom, tmpTrianglesTo);
                    if (tFromEquals != null) 
                    {
                        tmpResult[0]++;
                        tmpTrianglesTo.Remove(tFromEquals);
                        break;
                    }

                    Triangle tFromNear = FindNear(transformedTFrom, distance, tmpTrianglesTo);
                    if (tFromNear != null)
                    {
                        tmpResult[1]++;
                        tmpResult[2] += transformedTFrom.GetDistanceTo(tFromNear);

                        tmpTrianglesTo.Remove(tFromNear);
                        break;
                    }
                    tmpResult[2] += Point.GetMassCenter(tFrom.A, tFrom.B, tFrom.C).GetDistance(tFrom.A);
                }
                
                if (tmpResult[0] + tmpResult[1] > result[0] + result[1])
                {
                    result[0] = tmpResult[0];
                    result[1] = tmpResult[1];
                    resultBuilder = builder;
                }
            }
            return result;
        }


        private Triangle FindEquals(Triangle triangle, List<Triangle> list) {
            Triangle result = null;

            foreach (Triangle t in list) 
            {
                if (t.Equals(triangle))
                    return t;
            }

            return result;
        }

        private Triangle FindNear(Triangle triangle, double distance, List<Triangle> list) {
            Triangle result = null;
            double nearDistance = -1;
            foreach (Triangle t in list)
            {
                if (t.Equals(triangle, distance))
                { 
                    double tmpNearDistance = t.GetDistanceTo(triangle);
                    if (tmpNearDistance < nearDistance || nearDistance == -1)
                    {
                        result = t;
                        nearDistance = tmpNearDistance;
                    }
                }
            }
            return result;
        }
    }
}