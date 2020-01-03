using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Blobucket.Builders;
using Blobucket.Formatters;
using FluentAssertions;
using Xunit;

namespace Blobucket
{
    public class BlobEntityOptionsBuilderExtensionsTests
    {
        [Fact]
        public void CanUseAutoGeneratedContainerNameFromTypeName()
        {
            var builder = new BlobEntityContainerOptionsBuilder();
            builder.UseContainerName<BlobEntityContainerOptions>();
            var options = builder.Build();
            options.ContainerName.Should().Be("blob-entity-container-options");
        }

        [Fact]
        public void CanCustomizeTheContainerName()
        {
            var builder = new BlobEntityContainerOptionsBuilder();
            builder.UseContainerName<BlobEntityContainerOptions>();
            builder.UseContainerName("custom-name");
            var options = builder.Build();
            options.ContainerName.Should().Be("custom-name");
        }

        [Fact]
        public void CanUseCustomFormatter()
        {
            var builder = new BlobEntityOptionsBuilder();
            builder.UseFormatter(new CustomBlobEntityFormatter());
            var options = builder.Build();
            options.Formatter.Should().BeAssignableTo<CustomBlobEntityFormatter>();
        }

        class CustomBlobEntityFormatter : BlobEntityFormatter
        {
            public override Task<T> DeserializeAsync<T>(Stream stream, IReadOnlyDictionary<string, string> metadata, CancellationToken cancellationToken = default)
            {
                throw new System.NotImplementedException();
            }

            public override Task<Stream> SerializeAsync<T>(T entity, IDictionary<string, string> metadata, CancellationToken cancellationToken = default)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}