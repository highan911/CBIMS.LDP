// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using System;

namespace CBIMS.LDP.Geom
{
    public class ArrayDouble
    {
        public const double INT_CODE_PRECISION = 1000;

        public static ArrayDouble O = new ArrayDouble(0.0, 0.0, 0.0);

        public static ArrayDouble EX = new ArrayDouble(1.0, 0.0, 0.0);

        public static ArrayDouble EY = new ArrayDouble(0.0, 1.0, 0.0);

        public static ArrayDouble EZ = new ArrayDouble(0.0, 0.0, 1.0);

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double Length => Math.Sqrt(this * this);

        public ArrayDouble(ArrayDouble p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }

        public ArrayDouble(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int[] ToIntCode()
        {
            return (INT_CODE_PRECISION * Normalize()).ToPointInt().ToIntCode();
        }
        public double[] ToArray()
        {
            return new double[3] { X, Y, Z };
        }
        public override int GetHashCode()
        {
            int hashCode = X.GetHashCode();
            hashCode = Y.GetHashCode() ^ hashCode;
            return Z.GetHashCode() ^ hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is ArrayDouble)
            {
                return 0.0 == _CompareTo((ArrayDouble)obj);
            }
            return base.Equals(obj);
        }
        public double _CompareTo(ArrayDouble other)
        {
            double num = X - other.X;
            if (0.0 == num)
            {
                num = Y - other.Y;
                if (0.0 == num)
                {
                    num = Z - other.Z;
                }
            }
            return num;
        }

        public ArrayDouble Normalize()
        {
            double length = Length;
            if (length > 0.0)
            {
                return Scale(1.0 / length);
            }
            return new ArrayDouble(0.0, 0.0, 0.0);
        }
        public ArrayDouble Scale(double s)
        {
            double x = X * s;
            double y = Y * s;
            double z = Z * s;
            return new ArrayDouble(x, y, z);
        }

        public static ArrayDouble operator +(ArrayDouble v1, ArrayDouble v2)
        {
            return new ArrayDouble(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static ArrayDouble operator -(ArrayDouble v1, ArrayDouble v2)
        {
            return new ArrayDouble(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static ArrayDouble operator *(double t, ArrayDouble v)
        {
            return new ArrayDouble(t * v.X, t * v.Y, t * v.Z);
        }

        public static double operator *(ArrayDouble v1, ArrayDouble v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static ArrayDouble operator /(ArrayDouble v, double t)
        {
            return new ArrayDouble(v.X / t, v.Y / t, v.Z / t);
        }

        public static ArrayDouble operator ^(ArrayDouble v1, ArrayDouble v2)
        {
            return new ArrayDouble(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public PointInt ToPointInt()
        {
            return new PointInt((int)Math.Round(X), (int)Math.Round(Y), (int)Math.Round(Z));
        }
        public override string ToString()
        {
            return X + "," + Y + "," + Z;
        }
        public ArrayDouble GetDirectionUnit()
        {
            double length = Length;
            if (length == 0.0)
            {
                return new ArrayDouble(0.0, 0.0, 0.0);
            }
            return 1.0 / length * this;
        }
    }
}
