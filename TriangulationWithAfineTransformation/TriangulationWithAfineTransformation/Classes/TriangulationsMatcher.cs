using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangulationWithAfineTransformation.Classes
{
    class TriangulationsMatcher
    {
        private List<Triangle> TrianglesFrom;
        private List<Triangle> TrianglesTo;

        public TriangulationsMatcher(List<Triangle> trianglesFrom, List<Triangle> trianglesTo)
        {
            TrianglesFrom = trianglesFrom;
            TrianglesTo = trianglesTo;
        }

        private OptimumConversionBuilder FindNearestTriangle(Triangle triangle, List<Triangle> list){
            OptimumConversionBuilder result = null;

            foreach (Triangle t in list)
            {
                OptimumConversionBuilder tmpResult = new OptimumConversionBuilder(triangle, t);
                if (result == null || tmpResult.Distance < result.Distance)
                    result = tmpResult;
            }

            return result;
        }
    }
}
