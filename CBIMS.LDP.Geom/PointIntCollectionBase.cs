// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Double;

namespace CBIMS.LDP.Geom
{
    public abstract class PointIntCollectionBase : ICollection<PointInt>
    {
        protected abstract ICollection<PointInt> Content { get; }

        public virtual int Count => Content.Count;

        public virtual bool IsReadOnly => Content.IsReadOnly;

        public abstract PointIntCollectionBase Clone();

        public abstract PointIntCollectionBase ApplyTransform(Transform trans);

        public virtual void Add(PointInt item)
        {
            Content.Add(item);
        }

        public virtual void Clear()
        {
            Content.Clear();
        }

        public virtual bool Contains(PointInt item)
        {
            return Content.Contains(item);
        }

        public virtual void CopyTo(PointInt[] array, int arrayIndex)
        {
            Content.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<PointInt> GetEnumerator()
        {
            return Content.GetEnumerator();
        }

        public virtual bool Remove(PointInt item)
        {
            return Content.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Content).GetEnumerator();
        }






        public AABB GetAABB(string elementId)
        {
            double num = double.MaxValue;
            double num2 = double.MaxValue;
            double num3 = double.MaxValue;
            double num4 = double.MinValue;
            double num5 = double.MinValue;
            double num6 = double.MinValue;
            using (IEnumerator<PointInt> enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PointInt current = enumerator.Current;
                    num = Math.Min(num, current.X);
                    num2 = Math.Min(num2, current.Y);
                    num3 = Math.Min(num3, current.Z);
                    num4 = Math.Max(num4, current.X);
                    num5 = Math.Max(num5, current.Y);
                    num6 = Math.Max(num6, current.Z);
                }
            }
            return new AABB(new PointInt(num, num2, num3), new PointInt(num4, num5, num6), elementId);
        }

        public OBB GetOBB(string elementId)
        {
            if (Count == 0)
            {
                return null;
            }
            ArrayDouble pCADirection = GetPCADirection();
            return GetOBB(pCADirection, elementId);
        }

        public OBB GetOBB(ArrayDouble AxisA, string elementId)
        {
            if (Count == 0)
            {
                return null;
            }
            if (AxisA.X == 0.0 && AxisA.Y == 0.0)
            {
                ArrayDouble eX = ArrayDouble.EX;
                ArrayDouble eY = ArrayDouble.EY;
                double[] origin = new double[3];
                Transform transform = new Transform(AxisA.GetDirectionUnit().ToArray(), eX.GetDirectionUnit().ToArray(), eY.GetDirectionUnit().ToArray(), origin, 1.0);
                return GetOBB(transform, elementId);
            }
            ArrayDouble arrayDouble = ArrayDouble.EZ ^ AxisA;
            ArrayDouble arrayDouble2 = AxisA ^ arrayDouble;
            double[] origin2 = new double[3];
            Transform transform2 = new Transform(AxisA.GetDirectionUnit().ToArray(), arrayDouble.GetDirectionUnit().ToArray(), arrayDouble2.GetDirectionUnit().ToArray(), origin2, 1.0);
            return GetOBB(transform2, elementId);
        }

        private OBB GetOBB(Transform transform, string elementId)
        {
            Transform trans = transform.Inv();
            BBox bBox = ApplyTransform(trans).GetAABB(elementId).ApplyTransform(transform);
            if (bBox is OBB result)
            {
                return result;
            }
            if (bBox is AABB aABB)
            {
                return new OBB(aABB, elementId);
            }
            throw new NotImplementedException();
        }

        public ArrayDouble GetPCADirection()
        {
            DenseMatrix dataMat = getDataMat();
            DenseMatrix meanMat = getMeanMat(dataMat);
            dataMat -= meanMat;
            Matrix<double> obj = dataMat * dataMat.Transpose();
            Evd<double> obj2 = obj.Evd((Symmetricity)1);
            int num = obj2.EigenValues.AbsoluteMaximumIndex();
            Vector<double> val = obj2.EigenVectors.Column(num);
            return new ArrayDouble(val[0], val[1], val[2]);
        }
        private static DenseMatrix getMeanMat(DenseMatrix mat)
        {
            DenseMatrix val = new DenseMatrix(mat.RowCount, mat.ColumnCount);
            for (int i = 0; i < mat.RowCount; i++)
            {
                double num = 0.0;
                for (int j = 0; j < mat.ColumnCount; j++)
                {
                    num += mat[i, j];
                }
                double num2 = num / (double)mat.ColumnCount;
                for (int k = 0; k < mat.ColumnCount; k++)
                {
                    ((Matrix<double>)(object)val)[i, k] = num2;
                }
            }
            return val;
        }

        protected DenseMatrix getDataMat()
        {
            double[] array = new double[3 * Count];
            int num = 0;
            foreach (PointInt item in Content)
            {
                array[3 * num] = item.X;
                array[3 * num + 1] = item.Y;
                array[3 * num + 2] = item.Z;
                num++;
            }
            return new DenseMatrix(3, Count, array);
        }
    }
}
