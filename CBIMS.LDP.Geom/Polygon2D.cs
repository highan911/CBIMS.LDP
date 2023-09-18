// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.Geom
{
    public class Polygon2D : BBox
    {
        public List<PointIntList2D> Curves;
        public override PointInt Center => (0.5 * (Max + Min)).ToPointInt();

        protected Polygon2D()
        {
        }

        public Polygon2D(IList<PointIntList2D> Curves, string elementId)
        {
            ElementId = elementId;
            this.Curves = new List<PointIntList2D>();
            foreach (PointIntList2D Curf in Curves)
            {
                this.Curves.Add(Curf.Clone() as PointIntList2D);
            }
            _getAABB();
        }

        public Polygon2D(IList<PointInt> Points, string elementId)
        {
            ElementId = elementId;
            PointIntList2D pointIntList2D = new PointIntList2D(Points);
            Curves = new List<PointIntList2D> { pointIntList2D };
            _getAABB();
        }

        public override BBox ApplyTransform(Transform trans)
        {
            if (trans == null || trans.IsIdentity4())
            {
                return Clone();
            }
            List<PointIntList2D> list = new List<PointIntList2D>();
            foreach (PointIntList2D curf in Curves)
            {
                list.Add(curf.ApplyTransform(trans) as PointIntList2D);
            }
            return new Polygon2D(list, ElementId);
        }

        public override BBox Clone()
        {
            List<PointIntList2D> list = new List<PointIntList2D>();
            foreach (PointIntList2D curf in Curves)
            {
                list.Add(curf.Clone() as PointIntList2D);
            }
            return new Polygon2D(list, ElementId);
        }

        private void _getAABB()
        {
            int[] array = new int[6]
            {
            Curves[0][0].X,
            Curves[0][0].Y,
            0,
            Curves[0][0].X,
            Curves[0][0].Y,
            0
            };
            for (int i = 0; i < Curves.Count; i++)
            {
                foreach (PointInt item in Curves[i])
                {
                    array[0] = Math.Min(array[0], item.X);
                    array[1] = Math.Min(array[1], item.Y);
                    array[3] = Math.Max(array[3], item.X);
                    array[4] = Math.Max(array[4], item.Y);
                }
            }
            Min = new PointInt(array[0], array[1], -1000000);
            Max = new PointInt(array[3], array[4], 1000000);
        }
    }
}
