using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.CrossCutting.PostalCode.Contracts.Arguments;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace _4oito6.Contact.Api.Configuration
{
    public static class EdmModelBuilder
    {
        public static IEdmModel BuildEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            //address
            builder.EntitySet<ViewAddress>("Address").EntityType.HasKey(x => x.Id);
            var getAddressFromUser = builder.Function("address");
            getAddressFromUser.ReturnsCollectionFromEntitySet<ViewAddress>("Address");

            var getAddressFromDistrictAndCity = builder.Function("address/{district}/{city}");
            getAddressFromDistrictAndCity.ReturnsCollectionFromEntitySet<ViewAddress>("Address");

            getAddressFromDistrictAndCity.Parameter<string>("district");
            getAddressFromDistrictAndCity.Parameter<string>("city");

            builder.EntitySet<AddressFromPostalCodeResponse>("Address").EntityType.HasKey(x => x.Street);
            var getAddressFromWsPostalCode = builder.Function("address/ws/{postalCode}");

            getAddressFromWsPostalCode.ReturnsCollectionFromEntitySet<AddressFromPostalCodeResponse>("Address");
            getAddressFromWsPostalCode.Parameter<string>("postalCode");

            //phone
            builder.EntitySet<ViewPhone>("Phone").EntityType.HasKey(x => x.Id);
            var getPhoneFromUser = builder.Function("phone");
            getPhoneFromUser.ReturnsCollectionFromEntitySet<ViewPhone>("Phone");

            var getPhoneFromLocalCode = builder.Function("phone/{localCode}");
            getPhoneFromLocalCode.ReturnsCollectionFromEntitySet<ViewPhone>("Phone");
            getPhoneFromLocalCode.Parameter<string>("localCode");

            return builder.GetEdmModel();
        }
    }
}