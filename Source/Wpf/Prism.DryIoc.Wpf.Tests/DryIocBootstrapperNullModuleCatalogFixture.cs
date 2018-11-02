﻿using System;
using System.Windows;
using Xunit;
using Prism.IocContainer.Wpf.Tests.Support;
using Prism.Modularity;

namespace Prism.DryIoc.Wpf.Tests
{
    
    public class DryIocBootstrapperNullModuleCatalogFixture : BootstrapperFixtureBase
    {
        [Fact]
        public void NullModuleCatalogThrowsOnDefaultModuleInitialization()
        {
            var bootstrapper = new NullModuleCatalogBootstrapper();

            AssertExceptionThrownOnRun(bootstrapper, typeof(InvalidOperationException), "IModuleCatalog");
        }

        private class NullModuleCatalogBootstrapper : DryIocBootstrapper
        {
            protected override IModuleCatalog CreateModuleCatalog()
            {
                return null;
            }

            protected override DependencyObject CreateShell()
            {
                throw new NotImplementedException();
            }

            protected override void InitializeShell()
            {
                throw new NotImplementedException();
            }
        }
    }
}
