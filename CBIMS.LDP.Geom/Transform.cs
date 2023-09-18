// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.Geom
{
    public class Transform
    {
        public double[] BasisX = new double[3] { 1.0, 0.0, 0.0 };

        public double[] BasisY = new double[3] { 0.0, 1.0, 0.0 };

        public double[] BasisZ = new double[3] { 0.0, 0.0, 1.0 };

        public double[] Origin = new double[3];

        public double Scale = 1.0;

        private DenseMatrix Mat = DenseMatrix.CreateIdentity(4);

        public Transform()
        {
        }

        public Transform(double[] BasisX, double[] BasisY, double[] BasisZ, double[] Origin, double Scale)
        {
            this.BasisX = BasisX;
            this.BasisY = BasisY;
            this.BasisZ = BasisZ;
            this.Origin = Origin;
            this.Scale = Scale;
            _toMatrix();
        }

        public Transform(DenseMatrix Mat)
        {
            this.Mat = Mat;
            _fromMatrix();
        }

        private void _toMatrix()
        {
            double[] obj = new double[16]
            {
            1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0,
            1.0, 0.0, 0.0, 0.0, 0.0, 1.0
            };
            obj[12] = Origin[0];
            obj[13] = Origin[1];
            obj[14] = Origin[2];
            double[] array = obj;
            double[] array2 = new double[16]
            {
            BasisX[0],
            BasisX[1],
            BasisX[2],
            0.0,
            BasisY[0],
            BasisY[1],
            BasisY[2],
            0.0,
            BasisZ[0],
            BasisZ[1],
            BasisZ[2],
            0.0,
            0.0,
            0.0,
            0.0,
            1.0
            };
            double[] array3 = new double[16]
            {
            Scale, 0.0, 0.0, 0.0, 0.0, Scale, 0.0, 0.0, 0.0, 0.0,
            Scale, 0.0, 0.0, 0.0, 0.0, 1.0
            };
            DenseMatrix val = new DenseMatrix(4, 4, array);
            DenseMatrix val2 = new DenseMatrix(4, 4, array2);
            DenseMatrix val3 = new DenseMatrix(4, 4, array3);
            Mat = val * val2 * val3;
        }

        private void _fromMatrix()
        {
            DenseMatrix val = Mat.Clone() as DenseMatrix;
            Origin[0] = val[0, 3];
            Origin[1] = val[1, 3];
            Origin[2] = val[2, 3];
            double[] obj2 = new double[16]
            {
            1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0,
            1.0, 0.0, 0.0, 0.0, 0.0, 1.0
            };
            obj2[12] = Origin[0];
            obj2[13] = Origin[1];
            obj2[14] = Origin[2];
            double[] array = obj2;
            Matrix<double> obj3 = new DenseMatrix(4, 4, array).Inverse();
            val = (obj3 * val) as DenseMatrix;
            Scale = Math.Sqrt(val[0, 0] * val[0, 0] + val[1, 0] * val[1, 0] + val[2, 0] * val[2, 0]);
            double[] array2 = new double[16]
            {
            Scale, 0.0, 0.0, 0.0, 0.0, Scale, 0.0, 0.0, 0.0, 0.0,
            Scale, 0.0, 0.0, 0.0, 0.0, 1.0
            };
            Matrix<double> obj4 = new DenseMatrix(4, 4, array2).Inverse();
            val = (val * obj4) as DenseMatrix;
            BasisX[0] = val[0, 0];
            BasisX[1] = val[1, 0];
            BasisX[2] = val[2, 0];
            BasisY[0] = val[0, 1];
            BasisY[1] = val[1, 1];
            BasisY[2] = val[2, 1];
            BasisZ[0] = val[0, 2];
            BasisZ[1] = val[1, 2];
            BasisZ[2] = val[2, 2];
        }

        public static Transform operator *(Transform m1, Transform m2)
        {
            return new Transform(m1.Mat * m2.Mat);
        }

        public PointInt ApplyTrans(PointInt p)
        {
            DenseMatrix val = new DenseMatrix(4, 1);
            val[0, 0] = p.X;
            val[1, 0] = p.Y;
            val[2, 0] = p.Z;
            val[3, 0] = 1.0;
            DenseMatrix val2 = Mat * val;
            return new PointInt(val2[0, 0], val2[1, 0], val2[2, 0]);
        }

        public ArrayDouble ApplyTrans(ArrayDouble p)
        {
            DenseMatrix val = new DenseMatrix(4, 1);
            val[0, 0] = p.X;
            val[1, 0] = p.Y;
            val[2, 0] = p.Z;
            val[3, 0] = 1.0;
            val = Mat * val;
            return new ArrayDouble(val[0, 0], val[1, 0], val[2, 0]);
        }
        public void ApplyTrans_Vector(double x, double y, double z, out double xp, out double yp, out double zp)
        {
            DenseMatrix val = new DenseMatrix(4, 1);
            val[0, 0] = x;
            val[1, 0] = y;
            val[2, 0] = z;
            val[3, 0] = 0.0;
            val = Mat * val;
            xp = val[0, 0];
            yp = val[1, 0];
            zp = val[2, 0];
        }

        public PointInt ApplyTrans_Vector(PointInt p)
        {
            DenseMatrix val = new DenseMatrix(4, 1);
            val[0, 0] = p.X;
            val[1, 0] = p.Y;
            val[2, 0] = p.Z;
            val[3, 0] = 0.0;
            val = Mat * val;
            return new PointInt(val[0, 0], val[1, 0], val[2, 0]);
        }

        public ArrayDouble ApplyTrans_Vector(ArrayDouble p)
        {
            DenseMatrix val = new DenseMatrix(4, 1);
            val[0, 0] = p.X;
            val[1, 0] = p.Y;
            val[2, 0] = p.Z;
            val[3, 0] = 0.0;
            val = Mat * val;
            return new ArrayDouble(val[0, 0], val[1, 0], val[2, 0]);
        }

        public DenseMatrix GetMatrix()
        {
            return Mat.Clone() as DenseMatrix;
        }

        public bool IsIdentity4()
        {
            for(int i=0; i < 4; i++)
            {
                for(int j=0; j < 4; j++)
                {
                    if (i == j)
                    {
                        var item = Mat[i, j] - 1;
                        if (item > 1e-6 || item<-1e-6)
                            return false;
                    }
                    else
                    {
                        var item = Mat[i, j];
                        if (item > 1e-6 || item < -1e-6)
                            return false;
                    }
                }
            }
            return true;
        }

        public Transform Inv()
        {
            DenseMatrix obj = Mat.Inverse() as DenseMatrix;
            return new Transform(obj);
        }
    }
}
