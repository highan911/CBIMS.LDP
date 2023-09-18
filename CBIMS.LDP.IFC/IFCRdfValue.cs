// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using CBIMS.LDP.Def;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBIMS.LDP.IFC
{
    public class IFCRdfValue : RdfInstPersist
    {
        public object Value { get; }
        public IFCRdfValue(AbstractIFCRdfModel host, string typeName, int id, object value) : base(host.NS, $"{typeName}_{id}", null, null)
        {
            Value = value;
            AddType(host.NS_Schema, typeName);

            if (ExpressValType != null)
                AddProp(ExpressValType, Value);
        }

        public string ExpressValType
        {
            get
            {
                if (Value == null) return null;
                if (Value is string) return "express:hasString";
                if (Value is double) return "express:hasDouble";
                if (Value is int) return "express:hasInteger";
                if (Value is bool) return "express:hasBoolean";

                throw new NotImplementedException();
            }
        }
    }
}
