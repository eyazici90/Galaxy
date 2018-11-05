using Galaxy.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Galaxy.Mapster.Tests
{
    public class MapsterMaps_Tests : GalaxyMapsterTestApplication
    {
        [Fact]
        public void Object_mapper_should_not_be_null()
        {
            var objectMapper = TheObject<IObjectMapper>();
            Assert.NotNull(objectMapper);
        }

        [Fact]
        public void Mapping_with_classic_way_when_only_Destination_provided_success()
        {
            var objectMapper = TheObject<IObjectMapper>();

            var fakeObj = new MyClass {
                Name = "Emre",
                Surname = "Yazıcı",
                Title = "Software Architect"
            };

            var mappedObj = objectMapper.MapTo<MyClassDto>(fakeObj);
            Assert.NotNull(mappedObj);
            Assert.Equal(fakeObj.Name, mappedObj.Name);
            Assert.Equal(fakeObj.Surname, mappedObj.Surname);
            Assert.Equal(fakeObj.Title, mappedObj.Title);
        }

        [Fact]
        public void Mapping_with_null_object_when_only_Destination_provided_not_throw()
        {
            var objectMapper = TheObject<IObjectMapper>();

            MyClass fakeObj = null;
            var mappedObj = objectMapper.MapTo<MyClassDto>(fakeObj);
            Assert.Null(mappedObj);
        }

        [Fact]
        public void Mapping_with_null_object_when_only_Destination_provided_fail()
        {
            var objectMapper = TheObject<IObjectMapper>();

            MyClass fakeObj = null;
            var mappedObj = objectMapper.MapTo<MyClassDto>(fakeObj);
            Assert.Null(mappedObj);
        }
    }
}
