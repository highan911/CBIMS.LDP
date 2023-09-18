// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.Geom
{
    public class AABB : BBox
    {
        public override PointInt Center => (0.5 * (Max + Min)).ToPointInt();

        public AABB(PointInt Min, PointInt Max, string elementId)
            : base(Min, Max, elementId)
        {
        }
        public override BBox ApplyTransform(Transform trans)
        {
            if (trans == null || trans.IsIdentity4())
            {
                return Clone();
            }
            PointInt o = trans.ApplyTrans(Center);
            PointInt pointInt = (0.5 * base.AABBSize).ToPointInt();
            ArrayDouble nA = trans.ApplyTrans_Vector(ArrayDouble.EX);
            ArrayDouble nB = trans.ApplyTrans_Vector(ArrayDouble.EY);
            ArrayDouble nC = trans.ApplyTrans_Vector(ArrayDouble.EZ);
            int x = pointInt.X;
            int y = pointInt.Y;
            int z = pointInt.Z;
            return new OBB(o, nA, nB, nC, x, y, z, ElementId);
        }

        public override BBox Clone()
        {
            return new AABB(Min, Max, ElementId);
        }
    }
}
