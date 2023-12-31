// <auto-generated> This file has been auto generated. </auto-generated>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
#if!GRAPHQL_GENERATOR_DISABLE_NEWTONSOFT_JSON
using Newtonsoft.Json;
#endif

namespace MTGViewer.Maui.Data.MtgJson
{
    public partial class LegalitiesQueryBuilder : GraphQlQueryBuilder<LegalitiesQueryBuilder>
    {
        private static readonly GraphQlFieldMetadata[] AllFieldMetadata =
        {
            new GraphQlFieldMetadata { Name = "format" },
            new GraphQlFieldMetadata { Name = "status" },
            new GraphQlFieldMetadata { Name = "uuid", IsComplex = true, QueryBuilderType = typeof(CardQueryBuilder) }
        };

        protected override string TypeName { get; } = "Legalities";

        public override IReadOnlyList<GraphQlFieldMetadata> AllFields { get; } = AllFieldMetadata;

        public LegalitiesQueryBuilder WithFormat(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("format", alias, new GraphQlDirective?[] { include, skip });

        public LegalitiesQueryBuilder ExceptFormat() => ExceptField("format");

        public LegalitiesQueryBuilder WithStatus(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("status", alias, new GraphQlDirective?[] { include, skip });

        public LegalitiesQueryBuilder ExceptStatus() => ExceptField("status");

        public LegalitiesQueryBuilder WithUuid(CardQueryBuilder cardQueryBuilder, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithObjectField("uuid", alias, cardQueryBuilder, new GraphQlDirective?[] { include, skip });

        public LegalitiesQueryBuilder ExceptUuid() => ExceptField("uuid");
    }
}
