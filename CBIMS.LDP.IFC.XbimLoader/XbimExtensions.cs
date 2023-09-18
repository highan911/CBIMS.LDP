// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Common;
using Xbim.Ifc2x3.Interfaces;

namespace CBIMS.LDP.IFC.XbimLoader
{
    internal static class XbimExtensions
    {
        public static IEnumerable<T> FindAll<T>(this IModel model)
        {
            return model.Instances.Where(t=>t is T).Cast<T>();
        }

        public static object UnWrap(this IExpressValueType value)
        {
            if(value==null) 
                return null;
            return value.Value;
        }

    }
}
