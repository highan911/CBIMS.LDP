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
    public class PointIntSet : PointIntCollectionBase, ISet<PointInt>
    {
        protected HashSet<PointInt> _Content = new HashSet<PointInt>();
        protected override ICollection<PointInt> Content => _Content;

        public PointIntSet() { }
        public PointIntSet(IEnumerable<PointInt> items)
        {
            _Content.UnionWith(items);
        }

        public void ExceptWith(IEnumerable<PointInt> other)
        {
            _Content.ExceptWith(other);
        }

        public void IntersectWith(IEnumerable<PointInt> other)
        {
            _Content.IntersectWith(other);
        }

        public bool IsProperSubsetOf(IEnumerable<PointInt> other)
        {
            return _Content.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<PointInt> other)
        {
            return _Content.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<PointInt> other)
        {
            return _Content.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<PointInt> other)
        {
            return _Content.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<PointInt> other)
        {
            return _Content.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<PointInt> other)
        {
            return _Content.SetEquals(other);
        }

        public virtual void SymmetricExceptWith(IEnumerable<PointInt> other)
        {
            _Content.SymmetricExceptWith(other);
        }

        public virtual void UnionWith(IEnumerable<PointInt> other)
        {
            _Content.UnionWith(other);
        }

        bool ISet<PointInt>.Add(PointInt item)
        {
            return _Content.Add(item);
        }


        public override PointIntCollectionBase Clone()
        {
            PointIntSet pointIntSet = new PointIntSet();
            pointIntSet._Content.UnionWith(this);
            return pointIntSet;
        }

        public override PointIntCollectionBase ApplyTransform(Transform trans)
        {
            PointIntSet pointIntSet = new PointIntSet();
            using(IEnumerator<PointInt> enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PointInt current = enumerator.Current;
                    pointIntSet.Add(trans.ApplyTrans(current));
                }
            }
            return pointIntSet;
        }


    }
}
