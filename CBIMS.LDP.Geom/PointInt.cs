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
    public class PointInt
    {
        public static PointInt O = new PointInt(0, 0, 0);

        public static PointInt EX = new PointInt(1, 0, 0);

        public static PointInt EY = new PointInt(0, 1, 0);

        public static PointInt EZ = new PointInt(0, 0, 1);

        private const double _feet_to_mm = 304.79999999999995;


        public PointInt(PointInt p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public PointInt(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public PointInt(double x, double y, double z)
        {
            X = (int)Math.Round(x);
            Y = (int)Math.Round(y);
            Z = (int)Math.Round(z);
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Z { get; private set; }

        public double Length => Math.Sqrt(this * this);

        public int[] ToIntCode()
        {
            return new int[3] { X, Y, Z };
        }

        public override int GetHashCode()
        {
            int hashCode = X.GetHashCode();
            hashCode = Y.GetHashCode() ^ hashCode;
            return Z.GetHashCode() ^ hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is PointInt)
            {
                return _CompareTo((PointInt)obj) == 0;
            }
            return base.Equals(obj);
        }

        public int _CompareTo(PointInt other)
        {
            int num = X - other.X;
            if (num == 0)
            {
                num = Y - other.Y;
                if (num == 0)
                {
                    num = Z - other.Z;
                }
            }
            return num;
        }

        public static PointInt operator +(PointInt v1, PointInt v2)
        {
            return new PointInt(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static PointInt operator -(PointInt v1, PointInt v2)
        {
            return new PointInt(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static ArrayDouble operator *(double t, PointInt v)
        {
            return new ArrayDouble(t * (double)v.X, t * (double)v.Y, t * (double)v.Z);
        }

        public static double operator *(PointInt v1, PointInt v2)
        {
            return (double)v1.X * (double)v2.X + (double)v1.Y * (double)v2.Y + (double)v1.Z * (double)v2.Z;
        }

        public static ArrayDouble operator ^(PointInt v1, PointInt v2)
        {
            return new ArrayDouble((double)v1.Y * (double)v2.Z - (double)v1.Z * (double)v2.Y, (double)v1.Z * (double)v2.X - (double)v1.X * (double)v2.Z, (double)v1.X * (double)v2.Y - (double)v1.Y * (double)v2.X);
        }

        public override string ToString()
        {
            return X + "," + Y + "," + Z;
        }

        public ArrayDouble ToArrayDouble()
        {
            return new ArrayDouble(X, Y, Z);
        }

        public PointInt GetZOffset(int z)
        {
            return new PointInt(X, Y, z);
        }
    }
}
