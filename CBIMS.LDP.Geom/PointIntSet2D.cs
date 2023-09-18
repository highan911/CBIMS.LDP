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
    internal class PointIntSet2D : PointIntSet
    {
        public override void Add(PointInt p)
        {
            base.Add(p.GetZOffset(0));
        }

        public PointIntSet2D() { }
        public PointIntSet2D(IEnumerable<PointInt> items)
        {
            foreach (var item in items)
            {
                _Content.Add(item.GetZOffset(0));
            }
        }

        public override void SymmetricExceptWith(IEnumerable<PointInt> other)
        {
            PointIntSet2D pointIntSet2D = new PointIntSet2D(other);
            _Content.SymmetricExceptWith(pointIntSet2D);
        }

        public override void UnionWith(IEnumerable<PointInt> other)
        {
            PointIntSet2D pointIntSet2D = new PointIntSet2D(other);
            _Content.UnionWith(pointIntSet2D);
        }

        public override PointIntCollectionBase Clone()
        {
            PointIntSet2D pointIntSet2D = new PointIntSet2D();
            pointIntSet2D._Content.UnionWith(this);
            return pointIntSet2D;
        }

        public override PointIntCollectionBase ApplyTransform(Transform trans)
        {
            PointIntSet2D pointIntSet2D = new PointIntSet2D();
            using(IEnumerator<PointInt> enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PointInt current = enumerator.Current;
                    pointIntSet2D.Add(trans.ApplyTrans(current));
                }
            }
            return pointIntSet2D;
        }
    }
}
