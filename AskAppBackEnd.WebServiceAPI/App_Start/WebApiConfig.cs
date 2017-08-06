using Autofac.Integration.WebApi;
using AskAppBackEnd.Autofac.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.OData.Extensions;
using Microsoft.OData;
using System.Web.OData.Builder;
using AskAppBackEnd.Data.Entities;
using Microsoft.OData.Edm;

namespace AskAppBackEnd
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            config.Formatters.XmlFormatter.UseXmlSerializer = false;

            var builder = AutofacRegister.Setup();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute("odata", "odata", GetEdmModel());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static IEdmModel GetEdmModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            var entitySet = builder.EntitySet<AnswerOption>("answeroptions");
            entitySet.EntityType.HasKey(entity => entity.Id);
            var entitySet1 = builder.EntitySet<Question>("questions");
            entitySet1.EntityType.HasKey(entity => entity.Id);
            var entitySet2 = builder.EntitySet<QuestionMedia>("questionmedias");
            entitySet2.EntityType.HasKey(entity => entity.Id);
            var entitySet3 = builder.EntitySet<UserAnswer>("useranswers");
            entitySet3.EntityType.HasKey(entity => entity.Id);
            var entitySet4 = builder.EntitySet<UserFriend>("uerfriends");
            entitySet4.EntityType.HasKey(entity => entity.Id);
            var entitySet5 = builder.EntitySet<UserLocation>("userlocations");
            entitySet5.EntityType.HasKey(entity => entity.Id);
            var entitySet6 = builder.EntitySet<UserNotification>("usernotifications");
            entitySet6.EntityType.HasKey(entity => entity.Id);
            var entitySet7 = builder.EntitySet<UserPollingAnswer>("userpollinganswers");
            entitySet7.EntityType.HasKey(entity => entity.Id);
            var entitySet8 = builder.EntitySet<UserPollingAnswer>("userpollinganswers");
            entitySet8.EntityType.HasKey(entity => entity.Id);
            var entitySet9 = builder.EntitySet<UserResponse>("userresponses");
            entitySet9.EntityType.HasKey(entity => entity.Id);
            var entitySet10 = builder.EntitySet<User>("users");
            entitySet10.EntityType.HasKey(entity => entity.Id);
            var entitySet11 = builder.EntitySet<UserPreference>("userpreferences");
            entitySet11.EntityType.HasKey(entity => entity.Id);

            return builder.GetEdmModel();
        }
    }
}
