using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using LottoSimulatorService.DataObjects;
using LottoSimulatorService.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace LottoSimulatorService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<Lotto> todoItems = new List<Lotto>
            {
                new Lotto { Id = Guid.NewGuid().ToString(), WinningRow = "First item", playerName = "testi1", Cost = 1, latitude = 1.0 , longitude = 1.0 },
                new Lotto { Id = Guid.NewGuid().ToString(), WinningRow = "Second item", playerName = "test2", Cost = 2, latitude = 1.0 , longitude = 1.0 },
            };

            foreach (Lotto todoItem in todoItems)
            {
                context.Set<Lotto>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

