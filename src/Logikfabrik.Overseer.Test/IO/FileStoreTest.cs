// <copyright file="FileStoreTest.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Overseer.Test.IO
{
    using AutoFixture.Xunit2;
    using Moq;
    using Overseer.IO;
    using Shouldly;
    using Xunit;

    public class FileStoreTest
    {
        [Theory]
        [AutoData]
        public void CanReadWhenFileExists(string path, string text)
        {
            var fileSystemMock = new Mock<IFileSystem>();

            fileSystemMock.Setup(m => m.FileExists(path)).Returns(true);
            fileSystemMock.Setup(m => m.ReadFileText(path)).Returns(text);

            var fileStore = new FileStore(fileSystemMock.Object, path);

            fileStore.Read().ShouldBe(text);
        }

        [Theory]
        [AutoData]
        public void CanNotReadWhenFileDoesNotExist(string path)
        {
            var fileSystemMock = new Mock<IFileSystem>();

            fileSystemMock.Setup(m => m.FileExists(path)).Returns(false);

            var fileStore = new FileStore(fileSystemMock.Object, path);

            fileStore.Read().ShouldBeNull();
        }

        [Theory]
        [AutoData]
        public void CanWrite(string path, string text)
        {
            var fileSystemMock = new Mock<IFileSystem>();

            var fileStore = new FileStore(fileSystemMock.Object, path);

            fileStore.Write(text);

            fileSystemMock.Verify(m => m.WriteFileText(path, text), Times.Once);
        }
    }
}
