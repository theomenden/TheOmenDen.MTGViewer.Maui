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
    public partial class DeckQueryBuilder : GraphQlQueryBuilder<DeckQueryBuilder>
    {
        private static readonly GraphQlFieldMetadata[] AllFieldMetadata =
        {
            new GraphQlFieldMetadata { Name = "id" },
            new GraphQlFieldMetadata { Name = "code" },
            new GraphQlFieldMetadata { Name = "mainBoard", IsComplex = true, QueryBuilderType = typeof(CardQueryBuilder) },
            new GraphQlFieldMetadata { Name = "name" },
            new GraphQlFieldMetadata { Name = "sideBoard", IsComplex = true, QueryBuilderType = typeof(CardQueryBuilder) },
            new GraphQlFieldMetadata { Name = "releaseDate" },
            new GraphQlFieldMetadata { Name = "type" }
        };

        protected override string TypeName { get; } = "Deck";

        public override IReadOnlyList<GraphQlFieldMetadata> AllFields { get; } = AllFieldMetadata;

        public DeckQueryBuilder WithId(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("id", alias, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptId() => ExceptField("id");

        public DeckQueryBuilder WithCode(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("code", alias, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptCode() => ExceptField("code");

        public DeckQueryBuilder WithMainBoard(CardQueryBuilder cardQueryBuilder, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithObjectField("mainBoard", alias, cardQueryBuilder, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptMainBoard() => ExceptField("mainBoard");

        public DeckQueryBuilder WithName(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("name", alias, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptName() => ExceptField("name");

        public DeckQueryBuilder WithSideBoard(CardQueryBuilder cardQueryBuilder, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithObjectField("sideBoard", alias, cardQueryBuilder, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptSideBoard() => ExceptField("sideBoard");

        public DeckQueryBuilder WithReleaseDate(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("releaseDate", alias, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptReleaseDate() => ExceptField("releaseDate");

        public DeckQueryBuilder WithType(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("type", alias, new GraphQlDirective?[] { include, skip });

        public DeckQueryBuilder ExceptType() => ExceptField("type");
    }
}