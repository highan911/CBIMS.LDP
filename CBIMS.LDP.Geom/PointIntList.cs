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
    public class PointIntList : PointIntCollectionBase, IList<PointInt>
    {
        protected List<PointInt> _Content = new List<PointInt>();
        protected override ICollection<PointInt> Content => _Content;

        public PointIntList() { }
        public PointIntList(IEnumerable<PointInt> items) 
        {
            _Content.AddRange(items);
        }

        public PointInt this[int index] { get => _Content[index]; set => _Content[index] = value; }

        public int IndexOf(PointInt item)
        {
            return _Content.IndexOf(item);
        }

        public virtual void Insert(int index, PointInt item)
        {
            _Content.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _Content.RemoveAt(index);
        }

        public override PointIntCollectionBase Clone()
        {
            PointIntList pointIntList = new PointIntList();
            pointIntList._Content.AddRange(this);
            return pointIntList;
        }
        public override PointIntCollectionBase ApplyTransform(Transform trans)
        {
            PointIntList pointIntList = new PointIntList();
            using (IEnumerator<PointInt> enumerator = GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    PointInt current = enumerator.Current;
                    pointIntList.Add(trans.ApplyTrans(current));
                }
            }
            return pointIntList;
        }
    }
}
