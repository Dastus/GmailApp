﻿using System;
using StructureMap;
using System.Web.Mvc;

namespace GmailListApp.IoC
{
    public class Bootstrapper
    {
        public static void ConfigureStructureMap(Action<ConfigurationExpression> configurationAction)
        {
            IContainer container = new Container(configurationAction);
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}