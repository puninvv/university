using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Kursach
{
    class myPolygon
    {
        private List<myVertex> vertexes;
        private myPolygon() 
        {
            vertexes = new List<myVertex>();
        }
        public myVertex GetLastVertex() {
            return new myVertex(vertexes[vertexes.Count - 1]);
        }
        public bool Intersect(mySection section)
        { 
            for (int i = 1; i < vertexes.Count; i++)
            {
                myVertex intersection = new mySection(vertexes[i - 1], vertexes[i]).Intersect(section);
                if ( intersection != null)
                { 
                    if (!intersection.Equals(section.Left))
                        return true;
                    else
                        continue;
                }
            }
            return false;
        }

        public myPolygon(myVertex head)
        {
            vertexes = new List<myVertex>();
            vertexes.Add(head);
        }
        public myPolygon(myVertex head, Graphics graphics, Pen pen) 
        {
            vertexes = new List<myVertex>();
            vertexes.Add(head);
            head.Paint(graphics, pen);
        }

        public myPolygon(myPolygon polygon)
        {
            this.vertexes = new List<myVertex>();
            for (int i = 0; i < polygon.vertexes.Count; i++)
                this.vertexes.Add(new myVertex(polygon.vertexes[i]));
        }

        public bool AddVertex(myVertex vertex) 
        {
            bool result = true;
            if (vertexes[0].DistanceTo(vertex) < 4)
            {
                result = false;
                vertexes.Add(vertexes[0]);
            }
            else
                vertexes.Add(vertex);
            
            return result;
        }
        public bool AddVertex(myVertex vertex, Graphics graphics, Pen pen)
        {
            bool result = this.AddVertex(vertex);
            if (result == true)
            {
                vertex.Paint(graphics, pen);
                new mySection(vertexes[vertexes.Count - 2], vertexes[vertexes.Count - 1]).Paint(graphics, pen);
            }
            else
                new mySection(vertexes[vertexes.Count - 2], vertexes[0]).Paint(graphics, pen);
            
            return result;
        }

        public void Paint(Graphics graphics, Pen pen)
        {
            for (int i = 1; i < vertexes.Count; i++)
            {
                vertexes[i].Paint(graphics, pen);
                vertexes[i - 1].Paint(graphics, pen);
                new mySection(vertexes[i - 1], vertexes[i]).Paint(graphics, pen);
            }
        }

        public int ContainVertex(myVertex vertex, int start_pos) 
        {
            for (int i = start_pos; i < vertexes.Count; i++)
                if (vertex.Equals(vertexes[i]))
                    return i;
            return -1;
        }
        public bool ContainInArea(myVertex vertex)
        {
            if (ContainVertex(vertex,0) != -1)
                return true;

            double x_max = vertexes[0].X;
            for (int i = 1; i < vertexes.Count - 1; i++)
                if (vertexes[i].X > x_max)
                    x_max = vertexes[i].X;
            
            x_max += 10;
            mySection luch = new mySection(vertex, new myVertex(x_max,vertex.Y));
            int k = 0;
            for (int i = 0; i < vertexes.Count - 1; i++ )
            {
                mySection section = new mySection(vertexes[i], vertexes[i + 1]);
                myVertex intersection = luch.Intersect(section);
                if (intersection != null)
                {
                    k++;
                    if (this.ContainVertex(intersection,0) != -1)
                        i++;
                }
            }

            if (k>0 && k % 2 == 1)
                return true;
            else
                return false;
        }
        public bool ContainInAreaONLY(myVertex vertex) 
        {
            double x_max = vertexes[0].X;

            for (int i = 1; i < vertexes.Count; i++)
            {
                if (new mySection(vertexes[i - 1], vertexes[i]).Contain(vertex))
                    return false;
                if (vertexes[i].X > x_max)
                    x_max = vertexes[i].X;
            }

            x_max += 10;
            mySection luch = new mySection(vertex, new myVertex(x_max, vertex.Y));
            int k = 0;
            for (int i = 0; i < vertexes.Count - 1; i++)
            {
                mySection section = new mySection(vertexes[i], vertexes[i + 1]);
                myVertex intersection = luch.Intersect(section);
                if (intersection != null)
                {
                    k++;
                    if (this.ContainVertex(intersection, 0) != -1)
                        i++;
                }
            }

            if (k > 0 && k % 2 == 1)
                return true;
            else
                return false;

        }

        private void SetOrinentation()
        { 
            //вычислим количество "поворотов наружу" (скалярное произведение меньше нуля (1)
            //и количество "поворотов внутрь" (скалярное произведение больше нуля) (2)
            //если (1) > (2) - тогда нужно сменить ориентацию
            int n_1 = 0;
            int n_2 = 0;
            double angle;

            angle = vertexes[0].CountAngleBetween(vertexes[vertexes.Count - 2], vertexes[1]);
            if (angle < 0)
                n_1++;
            else
                n_2++;            

            for (int i = 1; i < vertexes.Count - 2; i++)
            {
                angle = vertexes[i].CountAngleBetween(vertexes[i - 1], vertexes[i + 1]);
                if (angle < 0)
                    n_1++;
                else
                    n_2++;
            }

            angle = vertexes[vertexes.Count - 2].CountAngleBetween(vertexes[vertexes.Count - 3], vertexes[0]);
            if (angle < 0)
                n_1++;
            else
                n_2++;

            //связано с особенностью системы координат формы windows
            if (n_1 > n_2)
            {
                int count = vertexes.Count - 1;
                for (int i = 0; i < count; i++)
                    vertexes.Add(vertexes[count-1-i]);
                for (int i = 0; i < count; i++)
                    vertexes.RemoveAt(0);
            }
        }

        private myPolygon GetIntersectionWith(myPolygon polygon)
        {
            //создадим результирующий многоугольник
            myPolygon result = new myPolygon();
            
            for (int i = 0; i < vertexes.Count - 1; i++)
            {
                //добавим в него текущую вершину
                result.vertexes.Add(vertexes[i]);
                //создадим ребро, из текущей и следующей вершины
                mySection i_section = new mySection(vertexes[i], vertexes[i + 1]);

                //а так же список добавляемых вершин
                List<myVertex> vertexes_to_add = new List<myVertex>();

                //будем искать пересечения
                for (int j = 0; j < polygon.vertexes.Count - 1; j++)
                {
                    //текущее ребро во втором многоугольнике
                    mySection j_section = new mySection(polygon.vertexes[j], polygon.vertexes[j + 1]);
                    
                    //если есть пересечение - добавим в список
                    myVertex intersection = i_section.Intersect(j_section);
                    if (intersection != null && result.ContainVertex(intersection,0) == -1 && !vertexes_to_add.Contains(intersection))
                        vertexes_to_add.Add(intersection);
                }

                //теперь нужно отсортировать список по удалению от начала отрезка
                //параллельно добавляя их в многоугольник-результат
                for (int j = 0; j < vertexes_to_add.Count; j++)
                { 
                    double min_distance = result.vertexes[result.vertexes.Count - 1].DistanceTo(vertexes_to_add[j]);
                    for (int k = j + 1; k < vertexes_to_add.Count; k++)
                    {
                        double distance = result.vertexes[result.vertexes.Count - 1].DistanceTo(vertexes_to_add[k]);
                        if (distance < min_distance)
                        {
                            myVertex temp = vertexes_to_add[j];
                            vertexes_to_add[j] = vertexes_to_add[k];
                            vertexes_to_add[k] = temp;
                            min_distance = distance;
                        }
                    }
                    result.vertexes.Add(vertexes_to_add[j]);
                }
            }
            
            //не забудем добавить самую первую точку
            result.vertexes.Add(result.vertexes[0]);

            return result;
        }
        public myPolygon GetIntersectionWith(myPolygon polygon, Graphics graphics, Pen pen)
        {
            myPolygon result = GetIntersectionWith(polygon);
            for (int i = 0; i < result.vertexes.Count - 1; i++)
            {
                result.vertexes[i].Paint(graphics, pen);
                result.vertexes[i + 1].Paint(graphics, pen);
                new mySection(result.vertexes[i], result.vertexes[i + 1]).Paint(graphics, pen);
                Thread.Sleep(100);
            }
            
            return result;
        }

        private class Polygons_Iterator 
        {
            public myPolygon polygon;
            public int pos;
            public Polygons_Iterator(myPolygon polygon, int start_pos)
            {
                this.polygon = polygon;
                pos = start_pos;
            }
        }
        private int HasVertexesInside(myPolygon polygon)
        {
            //выбираем полигон с которого начать обход
            //"0" - текущий
            //"1" - передаваемый
            //"2" - они совпадают
            //"3" - они не пересекаются


            int polygon_vertexes_count_which_is_not_in_first = 0;
            for (int i = 0; i < polygon.vertexes.Count; i++)
                if (!this.ContainInArea(polygon.vertexes[i]))
                    polygon_vertexes_count_which_is_not_in_first++;

            int this_vertexes_count_which_is_not_in_second = 0;
            for (int i = 0; i < this.vertexes.Count; i++)
                if (!polygon.ContainInArea(this.vertexes[i]))
                    this_vertexes_count_which_is_not_in_second++;

            if (polygon_vertexes_count_which_is_not_in_first == polygon.vertexes.Count && this_vertexes_count_which_is_not_in_second == this.vertexes.Count)
            {
                Boolean there_is_no_intersection = true;
                for (int i = 1; i < this.vertexes.Count; i++)
                    for (int j = 1; j < polygon.vertexes.Count; j++)
                    {
                        if (new mySection(this.vertexes[i - 1], this.vertexes[i]).Intersect(new mySection(polygon.vertexes[j - 1], polygon.vertexes[j])) != null)
                        {
                            there_is_no_intersection = false;
                            break;
                        }
                    }

                        if (there_is_no_intersection)
                            return 3;
            } 


            //проверяем, является ли хотя бы одна точка вершина первого вне второго
            int pos_of_vertex_in_first_which_is_not_in_second = -1;

            for (int i = 0; i < this.vertexes.Count; i++)
                if (!polygon.ContainInArea(this.vertexes[i]))
                {
                    pos_of_vertex_in_first_which_is_not_in_second = i;
                    break;
                }


            //если да - возвращаем 0
            if (pos_of_vertex_in_first_which_is_not_in_second != -1)
            {
                this.vertexes.RemoveAt(this.vertexes.Count - 1);
                for (int i = 0; i < pos_of_vertex_in_first_which_is_not_in_second; i++)
                {
                    this.vertexes.Add(this.vertexes[0]);
                    this.vertexes.RemoveAt(0);
                }
                this.vertexes.Add(this.vertexes[0]);
                return 0;
            }


            //если нет - проворачиваем всё то же самое со вторым полигоном
            int pos_of_vertex_in_second_which_is_not_in_first = -1;
            for (int i = 0; i < polygon.vertexes.Count; i++)
                if (!this.ContainInArea(polygon.vertexes[i]))
                {
                    pos_of_vertex_in_second_which_is_not_in_first = i;
                    break;
                }

            //если да - возвращаем 1
            if (pos_of_vertex_in_second_which_is_not_in_first != -1)
            {
                polygon.vertexes.RemoveAt(polygon.vertexes.Count - 1);
                for (int i = 0; i < pos_of_vertex_in_second_which_is_not_in_first; i++)
                {
                    polygon.vertexes.Add(polygon.vertexes[0]);
                    polygon.vertexes.RemoveAt(0);
                }
                polygon.vertexes.Add(polygon.vertexes[0]);
                return 1;
            }

            //смотрим, совпадает ли количество вершин "вне" с количеством вершин вообще
            //если да - они не имеют пересечения
            return 2;
        }


        //меняю подход к задаче:
        public myPolygon GetUnionWith(myPolygon polygon, Graphics graphics, Pen pen, bool paint_or_not)
        {
            this.SetOrinentation();
            polygon.SetOrinentation();

            int arrangment_of_polygons = this.HasVertexesInside(polygon);

            if (arrangment_of_polygons == 0)
            {
                return this.GetUnionWithIntersectedPolygons(polygon, graphics, pen, paint_or_not);
            }
            else
                if (arrangment_of_polygons == 1)
                {
                    return polygon.GetUnionWithIntersectedPolygons(this, graphics, pen, paint_or_not);
                }
                else
                    if (arrangment_of_polygons == 3)
                    {
                        //this.Paint(graphics, pen);
                        this.Fill(graphics, Color.Brown, Color.White, null);
                        polygon.Fill(graphics, Color.Brown, Color.White, null);
                        //polygon.Paint(graphics, pen);
                        return null;
                    }
                    else
                    {
                        //this.Paint(graphics, pen);
                        this.Fill(graphics, Color.Brown, Color.White, null);
                        
                        return this;
                    }
        }

        private myPolygon GetUnionWithIntersectedPolygons(myPolygon polygon, Graphics graphics, Pen pen, bool paint_or_not)
        {
            myPolygon copy_of_this_with_intersected_points = this.GetIntersectionWith(polygon);
            myPolygon copy_of_second_with_intersected_points = polygon.GetIntersectionWith(this);

            Boolean[] points_of_this_which_are_in_result = new Boolean[copy_of_this_with_intersected_points.vertexes.Count];
            for (int i = 0; i < copy_of_this_with_intersected_points.vertexes.Count; i++ )
                if(copy_of_second_with_intersected_points.ContainInAreaONLY(copy_of_this_with_intersected_points.vertexes[i]))
                    points_of_this_which_are_in_result[i] = true;

            int count_vertexes_in_second = copy_of_second_with_intersected_points.vertexes.Count;
            for (int i = 1; i < count_vertexes_in_second; i++)
                copy_of_second_with_intersected_points.vertexes.Add(copy_of_second_with_intersected_points.vertexes[i]);
            copy_of_second_with_intersected_points.vertexes.Add(copy_of_second_with_intersected_points.vertexes[0]);

            

            //основной обход
            myPolygon result = copy_of_this_with_intersected_points.Round_ver(copy_of_second_with_intersected_points, 0, graphics, pen);
            for (int i = 0; i < copy_of_this_with_intersected_points.vertexes.Count; i++)
                for (int j = 0; j < result.vertexes.Count; j++)
                    if (copy_of_this_with_intersected_points.vertexes[i].Equals(result.vertexes[j]))
                        points_of_this_which_are_in_result[i] = true;


            pen = new Pen(Color.BlueViolet, 4);
            //выкалываем дыры
            List<myPolygon> holes = new List<myPolygon>();
            Boolean i_can_go_away = false;
            while (!i_can_go_away)
            {
                for (int i = 0; i < copy_of_this_with_intersected_points.vertexes.Count; i++)
                    if (points_of_this_which_are_in_result[i] == false)
                    {
                        myPolygon Hole = copy_of_this_with_intersected_points.Round_ver(copy_of_second_with_intersected_points, i, graphics, pen);
                        holes.Add(new myPolygon(Hole));
                        for (int k = 0; k < copy_of_this_with_intersected_points.vertexes.Count; k++)
                            for (int j = 0; j < Hole.vertexes.Count; j++)
                                if (copy_of_this_with_intersected_points.vertexes[k].Equals(Hole.vertexes[j]))
                                    points_of_this_which_are_in_result[k] = true;
                        i_can_go_away = false;
                    }
                    else
                        i_can_go_away = true;
            }

            if (paint_or_not)
                result.Fill(graphics, Color.Brown, Color.White, holes);
            return result;
        }

        private myPolygon Round_ver(myPolygon polygon, int start_position, Graphics graphics, Pen pen)
        {
            myPolygon copy_of_this = new myPolygon(this);
            myPolygon copy_of_poly = new myPolygon(polygon);
            copy_of_this.vertexes.RemoveAt(copy_of_this.vertexes.Count - 1);
            for (int i = 0; i < start_position; i++)
            {
                copy_of_this.vertexes.Add(copy_of_this.vertexes[0]);
                copy_of_this.vertexes.RemoveAt(0);
            }
            copy_of_this.vertexes.Add(copy_of_this.vertexes[0]);

            myVertex Start_Vertex = copy_of_this.vertexes[0]; Start_Vertex.Paint(graphics, pen);
            myPolygon result = new myPolygon(Start_Vertex);
            
            Polygons_Iterator pthis = new Polygons_Iterator(copy_of_this,1);
            Polygons_Iterator ppoly = new Polygons_Iterator(copy_of_poly,0);

            Boolean I_REALLY_SHOULD_GO_AWAY = false;

            while (!I_REALLY_SHOULD_GO_AWAY && !pthis.polygon.vertexes[pthis.pos].Equals(Start_Vertex))
            {
                result.AddVertex(pthis.polygon.vertexes[pthis.pos]);

                new mySection(result.vertexes[result.vertexes.Count - 2], result.vertexes[result.vertexes.Count - 1]).Paint(graphics, pen);
                result.vertexes[result.vertexes.Count - 1].Paint(graphics, pen);

                pthis.pos++;
                if (pthis.pos >= pthis.polygon.vertexes.Count)
                    break;

                //такая же точка есть и в другом списке
                int pos_in_poly = ppoly.polygon.ContainVertex(pthis.polygon.vertexes[pthis.pos-1], ppoly.pos);
                if (pos_in_poly != -1)
                {
                    ppoly.pos = pos_in_poly + 1;

                    myVector vector_in_first = new myVector(pthis.polygon.vertexes[pthis.pos-1], pthis.polygon.vertexes[pthis.pos]);
                    myVector vector_in_second = new myVector(pthis.polygon.vertexes[pthis.pos-1], ppoly.polygon.vertexes[ppoly.pos]);
                    //проверяем, нужен ли переход
                    if (vector_in_first.getVectorProduct(vector_in_second) < 0)
                    { 
                        //нужен переход! поэтому дальше идём по второму списку
                        Boolean i_can_repeat = true;
                        while (!ppoly.polygon.vertexes[ppoly.pos].Equals(Start_Vertex) && i_can_repeat) 
                        {
                            result.AddVertex(ppoly.polygon.vertexes[ppoly.pos]);
                            
                            new mySection(result.vertexes[result.vertexes.Count - 2], result.vertexes[result.vertexes.Count - 1]).Paint(graphics, pen);
                            result.vertexes[result.vertexes.Count - 1].Paint(graphics, pen);
                            
                            ppoly.pos++;
                            if (ppoly.pos >= ppoly.polygon.vertexes.Count)
                                break;
                            
                            int pos_in_this = pthis.polygon.ContainVertex(ppoly.polygon.vertexes[ppoly.pos-1], pthis.pos);
                            if (pos_in_this != -1)
                            {
                                pthis.pos = pos_in_this + 1;

                                vector_in_first = new myVector(ppoly.polygon.vertexes[ppoly.pos-1], ppoly.polygon.vertexes[ppoly.pos]);
                                vector_in_second = new myVector(ppoly.polygon.vertexes[ppoly.pos-1], pthis.polygon.vertexes[pthis.pos]);

                                if (vector_in_first.getVectorProduct(vector_in_second) < 0)
                                    i_can_repeat = false;
                            }
                        }
                        if (ppoly.polygon.vertexes[ppoly.pos].Equals(Start_Vertex))
                            I_REALLY_SHOULD_GO_AWAY = true;
                    }
                }

            }

            result.vertexes.Add(result.vertexes[0]);
            result.Paint(graphics, pen);
            return result;
        }

        private void Fill(Graphics graphics, Color MainColor, Color Holes, List<myPolygon> holes) 
        {
            Point[] point_of_main = new Point[vertexes.Count];
            for (int i = 0; i < vertexes.Count; i++)
                point_of_main[i] = vertexes[i].ToPoint();
            SolidBrush brush = new SolidBrush(MainColor);
            graphics.FillPolygon(brush, point_of_main);
            
            if (holes != null)
            {
                brush = new SolidBrush(Holes);
                for (int i = 0; i < holes.Count; i++)
                {
                    Point[] points = new Point[holes[i].vertexes.Count];
                    for (int j = 0; j < holes[i].vertexes.Count; j++)
                        points[j] = holes[i].vertexes[j].ToPoint();
                    graphics.FillPolygon(brush, points);
                }
            }
        }
    }
}