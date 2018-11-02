using CommonServiceLocator;
using Xunit;
using Ninject;
using Prism.Ninject.Wpf.Tests.Mocks;
using Prism.Regions;

namespace Prism.Ninject.Wpf.Tests
{
    
    public class NinjectBootstrapperRunMethodFixture
    {
        [Fact]
        public void CanRunBootstrapper()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
        }

        [Fact]
        public void RunShouldNotFailIfReturnedNullShell()
        {
            var bootstrapper = new DefaultNinjectBootstrapper {ShellObject = null};
            bootstrapper.Run();
        }

        [Fact]
        public void RunConfiguresServiceLocatorProvider()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();

            Assert.True(ServiceLocator.Current is NinjectServiceLocatorAdapter);
        }

        [Fact]
        public void RunShouldInitializeKernel()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();
            var kernel = bootstrapper.Kernel;

            Assert.Null(kernel);

            bootstrapper.Run();

            kernel = bootstrapper.Kernel;

            Assert.NotNull(kernel);
            Assert.IsAssignableFrom<IKernel>(kernel);
        }

        [Fact]
        public void RunAddsCompositionKernelToKernel()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            var createdKernel = bootstrapper.CallCreateKernel();
            var returnedKernel = createdKernel.Get<IKernel>();
            Assert.NotNull(returnedKernel);
            Assert.Equal(typeof(StandardKernel), returnedKernel.GetType());
        }

        [Fact]
        public void RunShouldCallInitializeModules()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();

            Assert.True(bootstrapper.InitializeModulesCalled);
        }

        [Fact]
        public void RunShouldCallConfigureDefaultRegionBehaviors()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.ConfigureDefaultRegionBehaviorsCalled);
        }

        [Fact]
        public void RunShouldCallConfigureRegionAdapterMappings()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.ConfigureRegionAdapterMappingsCalled);
        }

        [Fact]
        public void RunShouldAssignRegionManagerToReturnedShell()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.NotNull(RegionManager.GetRegionManager(bootstrapper.BaseShell));
        }

        [Fact]
        public void RunShouldCallCreateLogger()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.CreateLoggerCalled);
        }

        [Fact]
        public void RunShouldCallCreateModuleCatalog()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.CreateModuleCatalogCalled);
        }

        [Fact]
        public void RunShouldCallConfigureModuleCatalog()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.ConfigureModuleCatalogCalled);
        }

        [Fact]
        public void RunShouldCallCreateKernel()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.CreateKernelCalled);
        }

        [Fact]
        public void RunShouldCallCreateShell()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.CreateShellCalled);
        }

        [Fact]
        public void RunShouldCallConfigureKernel()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.ConfigureKernelCalled);
        }

        [Fact]
        public void RunShouldCallConfigureServiceLocator()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.ConfigureServiceLocatorCalled);
        }

        [Fact]
        public void RunShouldCallConfigureViewModelLocator()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();

            Assert.True(bootstrapper.ConfigureViewModelLocatorCalled);
        }

        [Fact]
        public void RunShouldCallTheMethodsInOrder()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();

            Assert.Equal("CreateLogger", bootstrapper.MethodCalls[0]);
            Assert.Equal("CreateModuleCatalog", bootstrapper.MethodCalls[1]);
            Assert.Equal("ConfigureModuleCatalog", bootstrapper.MethodCalls[2]);
            Assert.Equal("CreateKernel", bootstrapper.MethodCalls[3]);
            Assert.Equal("ConfigureKernel", bootstrapper.MethodCalls[4]);
            Assert.Equal("ConfigureServiceLocator", bootstrapper.MethodCalls[5]);
            Assert.Equal("ConfigureViewModelLocator", bootstrapper.MethodCalls[6]);
            Assert.Equal("ConfigureRegionAdapterMappings", bootstrapper.MethodCalls[7]);
            Assert.Equal("ConfigureDefaultRegionBehaviors", bootstrapper.MethodCalls[8]);
            Assert.Equal("RegisterFrameworkExceptionTypes", bootstrapper.MethodCalls[9]);
            Assert.Equal("CreateShell", bootstrapper.MethodCalls[10]);
            Assert.Equal("InitializeShell", bootstrapper.MethodCalls[11]);
            Assert.Equal("InitializeModules", bootstrapper.MethodCalls[12]);
        }

        [Fact]
        public void RunShouldLogBootstrapperSteps()
        {
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.Contains("Logger was created successfully.", messages[0]);
            Assert.Contains("Creating module catalog.", messages[1]);
            Assert.Contains("Configuring module catalog.", messages[2]);
            Assert.Contains("Creating the Ninject kernel.", messages[3]);
            Assert.Contains("Configuring the Ninject kernel.", messages[4]);
            Assert.Contains("Configuring ServiceLocator singleton.", messages[5]);
            Assert.Contains("Configuring the ViewModelLocator to use Ninject.", messages[6]);
            Assert.Contains("Configuring region adapters.", messages[7]);
            Assert.Contains("Configuring default region behaviors.", messages[8]);
            Assert.Contains("Registering Framework Exception Types.", messages[9]);
            Assert.Contains("Creating the shell.", messages[10]);
            Assert.Contains("Setting the RegionManager.", messages[11]);
            Assert.Contains("Updating Regions.", messages[12]);
            Assert.Contains("Initializing the shell.", messages[13]);
            Assert.Contains("Initializing modules.", messages[14]);
            Assert.Contains("Bootstrapper sequence completed.", messages[15]);
        }

        [Fact]
        public void RunShouldLogLoggerCreationSuccess()
        {
            const string expectedMessageText = "Logger was created successfully.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutModuleCatalogCreation()
        {
            const string expectedMessageText = "Creating module catalog.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutConfiguringModuleCatalog()
        {
            const string expectedMessageText = "Configuring module catalog.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutCreatingTheKernel()
        {
            const string expectedMessageText = "Creating the Ninject kernel.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutConfiguringKernel()
        {
            const string expectedMessageText = "Configuring the Ninject kernel.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutConfiguringViewModelLocator()
        {
            const string expectedMessageText = "Configuring the ViewModelLocator to use Ninject.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutConfiguringRegionAdapters()
        {
            const string expectedMessageText = "Configuring region adapters.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutConfiguringRegionBehaviors()
        {
            const string expectedMessageText = "Configuring default region behaviors.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutRegisteringFrameworkExceptionTypes()
        {
            const string expectedMessageText = "Registering Framework Exception Types.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutCreatingTheShell()
        {
            const string expectedMessageText = "Creating the shell.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutInitializingTheShellIfShellCreated()
        {
            const string expectedMessageText = "Initializing the shell.";
            var bootstrapper = new DefaultNinjectBootstrapper();

            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldNotLogAboutInitializingTheShellIfShellIsNotCreated()
        {
            const string expectedMessageText = "Initializing shell";
            var bootstrapper = new DefaultNinjectBootstrapper {ShellObject = null};

            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.False(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutInitializingModules()
        {
            const string expectedMessageText = "Initializing modules.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }

        [Fact]
        public void RunShouldLogAboutRunCompleting()
        {
            const string expectedMessageText = "Bootstrapper sequence completed.";
            var bootstrapper = new DefaultNinjectBootstrapper();
            bootstrapper.Run();
            var messages = bootstrapper.BaseLogger.Messages;

            Assert.True(messages.Contains(expectedMessageText));
        }
    }
}