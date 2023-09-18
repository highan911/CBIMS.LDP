// Copyright (C) 2023  Liu, Han; School of Software, Tsinghua University
//
// This file is part of CBIMS.LDP.
// CBIMS.LDP is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
// CBIMS.LDP is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License along with CBIMS.LDP. If not, see <https://www.gnu.org/licenses/>.

using CBIMS.LDP.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Query.Expressions;
using VDS.RDF.Query.PropertyFunctions;
using VDS.RDF.Writing.Formatting;

namespace CBIMS.LDP.Repo
{

    public interface IRepository
    {
        ITripleStore Store { get; }
        INamespaceMapper NamespaceMap { get; }
        TurtleFormatter Formatter { get; }

        IEnumerable<IRdfModel> Models { get; }
        void AddModel(IRdfModel model);
        void RemoveModel(IRdfModel model);
    }
    public interface IQueryableRepository : IRepository
    {
        new IInMemoryQueryableStore Store { get; }
        ISparqlDataset Dataset { get; }
    }


    public class Repository : IQueryableRepository
    {
        Dictionary<string, IRdfModel> _Models = new Dictionary<string, IRdfModel>(); //qname, model
        public IEnumerable<IRdfModel> Models => _Models.Values;

        public IInMemoryQueryableStore Store { get; }
        ITripleStore IRepository.Store => Store;

        public ISparqlDataset Dataset { get; }

        public INamespaceMapper NamespaceMap { get; }
        public TurtleFormatter Formatter { get; }


        public Repository()
        {
            Store = new TripleStore();
            NamespaceMap = new NamespaceMapper();
            Formatter = new TurtleFormatter(NamespaceMap);
            Dataset = new InMemoryDataset(Store, true);
        }

        public void AddModel(IRdfModel model)
        {
            _Models[model.QName] = model;
            Store.Add(model.Graph);
            NamespaceMap.Import(model.Graph.NamespaceMap);
        }

        public void RemoveModel(IRdfModel model)
        {
            if (_Models.ContainsKey(model.QName))
            {
                _Models.Remove(model.QName);
                Store.Remove(model.Graph.BaseUri);
                NamespaceMap.RemoveNamespace(model.NS.PrefixNC);
            }
        }
    }
}
