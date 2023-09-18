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
    public class BBoxs : BBox
    {
        public List<BBox> Children;

        public override PointInt Center => (0.5 * (Min + Max)).ToPointInt();

        public BBoxs(List<BBox> Children, string elementId)
        {
            ElementId = elementId;
            this.Children = Children;
            double num = double.MaxValue;
            double num2 = double.MaxValue;
            double num3 = double.MaxValue;
            double num4 = double.MinValue;
            double num5 = double.MinValue;
            double num6 = double.MinValue;
            foreach (BBox Child in Children)
            {
                num = Math.Min(num, Child.Min.X);
                num2 = Math.Min(num2, Child.Min.Y);
                num3 = Math.Min(num3, Child.Min.Z);
                num4 = Math.Max(num4, Child.Max.X);
                num5 = Math.Max(num5, Child.Max.Y);
                num6 = Math.Max(num6, Child.Max.Z);
            }
            Min = new PointInt(num, num2, num3);
            Max = new PointInt(num4, num5, num6);
        }


        public override BBox ApplyTransform(Transform trans)
        {
            if (trans == null || trans.IsIdentity4())
            {
                return Clone();
            }
            List<BBox> list = new List<BBox>();
            foreach (BBox child in Children)
            {
                list.Add(child.ApplyTransform(trans));
            }
            return new BBoxs(list, ElementId);
        }

        public override BBox Clone()
        {
            List<BBox> list = new List<BBox>();
            foreach (BBox child in Children)
            {
                list.Add(child.Clone());
            }
            return new BBoxs(list, ElementId);
        }
    }
}
