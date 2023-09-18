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

namespace CBIMS.LDP.Def
{

    public interface IRdfInst : IRdfNode
    {
        IGraph Graph { get; }

        IEnumerable<IRdfClassDef> Types { get; }
        IEnumerable<T> GetProp<T>(string qname);
        IEnumerable<object> GetProp(string qname);
        T GetPropSingle<T>(string qname);
        object GetPropSingle(string qname);

        bool HasProp(string qname);
        bool HasPropVal(string qname, object v);
    }
    public interface IRdfInstEditable : IRdfInst
    {
        IRdfInstEditable AddType(IRdfClassDef type);
        IRdfInstEditable AddProp(string key, object val);
        IRdfInstEditable SetProp(string key, object val);
        IRdfInstEditable SetProps(string key, IEnumerable<object> vals);
        IRdfInstEditable SetProps<T>(string key, IEnumerable<T> vals);

        IRdfInstEditable RemovePropAll(string key);
        IRdfInstEditable RemoveProp (string key, object val);
        
    }

    public interface IRdfInstWithTransaction : IRdfInstEditable
    {
        IRdfInstEditable Commit();
        IRdfInstEditable Cancel();
    }

}
