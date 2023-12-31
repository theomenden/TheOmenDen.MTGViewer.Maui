// <auto-generated> This file has been auto generated. </auto-generated>

#if!GRAPHQL_GENERATOR_DISABLE_NEWTONSOFT_JSON
using Newtonsoft.Json;
#endif

namespace MTGViewer.Maui.Data.MtgJson
{
    public partial class ListOrderInput : IGraphQlInputObject
    {
        private InputPropertyInfo _order;

#if !GRAPHQL_GENERATOR_DISABLE_NEWTONSOFT_JSON
        [JsonConverter(typeof(QueryBuilderParameterConverter<ListOrder?>))]
#endif
        public QueryBuilderParameter<ListOrder?>? Order
        {
            get => (QueryBuilderParameter<ListOrder?>?)_order.Value;
            set => _order = new InputPropertyInfo { Name = "order", Value = value };
        }

        IEnumerable<InputPropertyInfo> IGraphQlInputObject.GetPropertyValues()
        {
            if (_order.Name != null) yield return _order;
        }
    }
}
