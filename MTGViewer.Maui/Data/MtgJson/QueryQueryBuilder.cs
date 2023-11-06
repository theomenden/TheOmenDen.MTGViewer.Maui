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
    public partial class QueryQueryBuilder : GraphQlQueryBuilder<QueryQueryBuilder>
    {
        private static readonly GraphQlFieldMetadata[] AllFieldMetadata =
        {
            new GraphQlFieldMetadata { Name = "status" },
            new GraphQlFieldMetadata { Name = "cards", RequiresParameters = true, IsComplex = true, QueryBuilderType = typeof(CardQueryBuilder) },
            new GraphQlFieldMetadata { Name = "cardImage", RequiresParameters = true, IsComplex = true, QueryBuilderType = typeof(CardImageQueryBuilder) },
            new GraphQlFieldMetadata { Name = "decks", RequiresParameters = true, IsComplex = true, QueryBuilderType = typeof(DeckQueryBuilder) },
            new GraphQlFieldMetadata { Name = "metadata", IsComplex = true, QueryBuilderType = typeof(MetaQueryBuilder) },
            new GraphQlFieldMetadata { Name = "prices", RequiresParameters = true, IsComplex = true, QueryBuilderType = typeof(CardPricesQueryBuilder) },
            new GraphQlFieldMetadata { Name = "sets", RequiresParameters = true, IsComplex = true, QueryBuilderType = typeof(SetQueryBuilder) }
        };

        protected override string TypeName { get; } = "Query";

        public override IReadOnlyList<GraphQlFieldMetadata> AllFields { get; } = AllFieldMetadata;

        public QueryQueryBuilder(string? operationName = null) : base("query", operationName)
        {
        }

        public QueryQueryBuilder WithParameter<T>(GraphQlQueryParameter<T> parameter) => WithParameterInternal(parameter);

        public QueryQueryBuilder WithStatus(string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithScalarField("status", alias, new GraphQlDirective?[] { include, skip });

        public QueryQueryBuilder ExceptStatus() => ExceptField("status");

        public QueryQueryBuilder WithCards(CardQueryBuilder cardQueryBuilder, QueryBuilderParameter<ListOrderInput> order, QueryBuilderParameter<PaginationInput> page, QueryBuilderParameter<CardEntityFilterInput> filter, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null)
        {
            var args = new List<QueryBuilderArgumentInfo>();
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "order", ArgumentValue = order} );
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "page", ArgumentValue = page} );
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "filter", ArgumentValue = filter} );
            return WithObjectField("cards", alias, cardQueryBuilder, new GraphQlDirective?[] { include, skip }, args);
        }

        public QueryQueryBuilder ExceptCards() => ExceptField("cards");

        public QueryQueryBuilder WithCardImage(CardImageQueryBuilder cardImageQueryBuilder, QueryBuilderParameter<CardImageGetInput> input, QueryBuilderParameter<CardImageFaceInput?>? face = null, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null)
        {
            var args = new List<QueryBuilderArgumentInfo>();
            if (face != null)
                args.Add(new QueryBuilderArgumentInfo { ArgumentName = "face", ArgumentValue = face} );

            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "input", ArgumentValue = input} );
            return WithObjectField("cardImage", alias, cardImageQueryBuilder, new GraphQlDirective?[] { include, skip }, args);
        }

        public QueryQueryBuilder ExceptCardImage() => ExceptField("cardImage");

        public QueryQueryBuilder WithDecks(DeckQueryBuilder deckQueryBuilder, QueryBuilderParameter<DeckGetInput> input, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null)
        {
            var args = new List<QueryBuilderArgumentInfo>();
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "input", ArgumentValue = input} );
            return WithObjectField("decks", alias, deckQueryBuilder, new GraphQlDirective?[] { include, skip }, args);
        }

        public QueryQueryBuilder ExceptDecks() => ExceptField("decks");

        public QueryQueryBuilder WithMetadata(MetaQueryBuilder metaQueryBuilder, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null) => WithObjectField("metadata", alias, metaQueryBuilder, new GraphQlDirective?[] { include, skip });

        public QueryQueryBuilder ExceptMetadata() => ExceptField("metadata");

        public QueryQueryBuilder WithPrices(CardPricesQueryBuilder cardPricesQueryBuilder, QueryBuilderParameter<ListOrderInput> order, QueryBuilderParameter<PaginationInput> page, QueryBuilderParameter<PriceGetInput> input, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null)
        {
            var args = new List<QueryBuilderArgumentInfo>();
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "order", ArgumentValue = order} );
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "page", ArgumentValue = page} );
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "input", ArgumentValue = input} );
            return WithObjectField("prices", alias, cardPricesQueryBuilder, new GraphQlDirective?[] { include, skip }, args);
        }

        public QueryQueryBuilder ExceptPrices() => ExceptField("prices");

        public QueryQueryBuilder WithSets(SetQueryBuilder setQueryBuilder, QueryBuilderParameter<PaginationInput> page, QueryBuilderParameter<ListOrderInput> order, QueryBuilderParameter<SetGetInput> input, string? alias = null, IncludeDirective? include = null, SkipDirective? skip = null)
        {
            var args = new List<QueryBuilderArgumentInfo>();
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "page", ArgumentValue = page} );
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "order", ArgumentValue = order} );
            args.Add(new QueryBuilderArgumentInfo { ArgumentName = "input", ArgumentValue = input} );
            return WithObjectField("sets", alias, setQueryBuilder, new GraphQlDirective?[] { include, skip }, args);
        }

        public QueryQueryBuilder ExceptSets() => ExceptField("sets");
    }
}
