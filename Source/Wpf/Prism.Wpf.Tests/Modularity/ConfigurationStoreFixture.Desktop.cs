

using Xunit;
using Prism.Modularity;

namespace Prism.Wpf.Tests.Modularity
{
    
    public class ConfigurationStoreFixture
    {
        [Fact]
        public void ShouldRetrieveModuleConfiguration()
        {
            ConfigurationStore store = new ConfigurationStore();
            var section = store.RetrieveModuleConfigurationSection();

            Assert.NotNull(section);
            Assert.NotNull(section.Modules);
            Assert.Equal(1, section.Modules.Count);
            Assert.NotNull(section.Modules[0].AssemblyFile);
            Assert.Equal("MockModuleA", section.Modules[0].ModuleName);
            Assert.NotNull(section.Modules[0].AssemblyFile);
            Assert.True(section.Modules[0].AssemblyFile.Contains(@"MocksModules\MockModuleA.dll"));
            Assert.NotNull(section.Modules[0].ModuleType);
            Assert.True(section.Modules[0].StartupLoaded);
            Assert.Equal("Prism.Wpf.Tests.Mocks.Modules.MockModuleA", section.Modules[0].ModuleType);
        }
    }
}