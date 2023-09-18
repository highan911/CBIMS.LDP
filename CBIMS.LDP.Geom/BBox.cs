// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using RTree;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.Geom
{
    public abstract class BBox
    {
        public PointInt Min;

        public PointInt Max;

        public string ElementId = "";

        public abstract PointInt Center { get; }

        public PointInt AABBSize => Max - Min;
        protected BBox()
        {
        }

        protected BBox(PointInt Min, PointInt Max, string elementId)
        {
            this.Min = Min;
            this.Max = Max;
            ElementId = elementId;
        }
        public abstract BBox ApplyTransform(Transform trans);

        public abstract BBox Clone();

        public virtual Rectangle GetRTreeRectangle()
        {
            return new Rectangle((float)Min.X, (float)Min.Y, (float)Max.X, (float)Max.Y, (float)Min.Z, (float)Max.Z);
        }
    }
}
