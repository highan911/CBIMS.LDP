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
    public abstract class AbstractIFCRdfEntity : RdfInstPersist
    {
        public object Entity { get; }
        public AbstractIFCRdfModel Host { get; }
        protected string PrefixNC_Schema => Host.NS_Schema.PrefixNC;


        protected AbstractIFCRdfEntity(AbstractIFCRdfModel host, object ent, Type instType, int entId) : base(host.NS, $"{instType.Name}_{entId}", null, null)
        {
            Entity = ent;
            Host = host;

            RdfURIClassDef classDef = host.GetClassDef(instType);
            this.AddType(classDef);

            host.AddEntity(this);
        }

    }
    public class DefaultIFCRdfEntity : AbstractIFCRdfEntity
    {
        public DefaultIFCRdfEntity(AbstractIFCRdfModel host, object ent, Type instType, int entId) : base(host, ent, instType, entId)
        {
        }
    }
}
