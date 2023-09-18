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

    public class OBB : BBox
    {
        public PointInt O;

        public ArrayDouble nA;

        public ArrayDouble nB;

        public ArrayDouble nC;

        public double dA;

        public double dB;

        public double dC;
        public override PointInt Center => O;

        protected OBB()
        {
        }

        public OBB(PointInt O, ArrayDouble nA, ArrayDouble nB, ArrayDouble nC, 
            double dA, double dB, double dC, string elementId)
        {
            ElementId = elementId;
            this.O = O;
            this.nA = nA;
            this.nB = nB;
            this.nC = nC;
            this.dA = dA;
            this.dB = dB;
            this.dC = dC;
            double num = double.MaxValue;
            double num2 = double.MaxValue;
            double num3 = double.MaxValue;
            double num4 = double.MinValue;
            double num5 = double.MinValue;
            double num6 = double.MinValue;
            for (int i = 0; i < 8; i++)
            {
                PointInt vertex = GetVertex(i);
                num = Math.Min(num, vertex.X);
                num2 = Math.Min(num2, vertex.Y);
                num3 = Math.Min(num3, vertex.Z);
                num4 = Math.Max(num4, vertex.X);
                num5 = Math.Max(num5, vertex.Y);
                num6 = Math.Max(num6, vertex.Z);
            }
            Min = new PointInt(num, num2, num3);
            Max = new PointInt(num4, num5, num6);
        }

        public OBB(BBox AABB, string elementId)
        {
            ElementId = elementId;
            Min = AABB.Min;
            Max = AABB.Max;
            O = (0.5 * (AABB.Max + AABB.Min)).ToPointInt();
            PointInt pointInt = Max - O;
            nA = ArrayDouble.EX;
            nB = ArrayDouble.EY;
            nC = ArrayDouble.EZ;
            dA = pointInt.X;
            dB = pointInt.Y;
            dC = pointInt.Z;
        }


        public override BBox Clone()
        {
            return new OBB(O, nA, nB, nC, dA, dB, dC, ElementId);
        }
        public override BBox ApplyTransform(Transform trans)
        {
            if (trans == null || trans.IsIdentity4())
            {
                return Clone();
            }
            return new OBB(trans.ApplyTrans(O), trans.ApplyTrans_Vector(nA), trans.ApplyTrans_Vector(nB), trans.ApplyTrans_Vector(nC), dA, dB, dC, ElementId);
        }
        public PointInt GetVertex(int i)
        {
            i = i % 8;

            PointInt pointInt = (dA * nA.Normalize()).ToPointInt();
            PointInt pointInt2 = (dB * nB.Normalize()).ToPointInt();
            PointInt pointInt3 = (dC * nC.Normalize()).ToPointInt();
            return i switch
            {
                0 => O - pointInt - pointInt2 - pointInt3,
                1 => O - pointInt - pointInt2 + pointInt3,
                2 => O - pointInt + pointInt2 - pointInt3,
                3 => O - pointInt + pointInt2 + pointInt3,
                4 => O + pointInt - pointInt2 - pointInt3,
                5 => O + pointInt - pointInt2 + pointInt3,
                6 => O + pointInt + pointInt2 - pointInt3,
                7 => O + pointInt + pointInt2 + pointInt3,
                _ => throw new InvalidOperationException(),
            };
        }

    }
}
