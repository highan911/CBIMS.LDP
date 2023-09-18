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
using VDS.RDF;
using static System.Net.Mime.MediaTypeNames;

namespace CBIMS.LDP.Def
{
    public class RdfReadOnlyList : RdfInstPersist
    {
        public RdfReadOnlyList(IEnumerable<object> contents, RdfNSDef ns, string name, IUriNode node = null) : base(ns, name, RDFSCommonDef.List, node)
        {
            RdfInstPersist current_list = this;

            string prefix = name;
            if (prefix.EndsWith("_0"))
            {
                prefix = prefix.Substring(0, prefix.Length - 2);
            }

            var _contents = contents.ToList();

            for (int i = 0; i < _contents.Count; i++)
            {
                object content = _contents[i];
                current_list.AddProp(RDFSCommonDef.first.QName, content);
                if(i < _contents.Count - 1)
                {
                    RdfInstPersist next = new RdfInstPersist(ns, prefix + $"_{i + 1}", null);
                    current_list.AddProp(RDFSCommonDef.rest.QName, next);
                    current_list = next;
                }
                else
                {
                    current_list.AddProp(RDFSCommonDef.rest.QName, RDFSCommonDef.nil);
                }
            }
        }
    }


}
