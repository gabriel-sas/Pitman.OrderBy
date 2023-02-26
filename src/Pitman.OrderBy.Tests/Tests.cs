namespace Pitman.OrderBy.Tests
{
    public class OrderByQueryTests
    {
        [Fact]
        public void OrderByData_Setter_Should_Parse_And_Set_SortingProperties()
        {
            // Arrange
            var orderByQuery = new OrderByQuery("name:asc,id:desc");

            // Act

            // Assert
            var expected = new List<(string Property, string Direction)>
            {
                ("Name", "ASC"),
                ("Id", "DESC")
            };
            var actual = orderByQuery.SortingProperties;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ApplyOrder_Should_Return_Expected_Result()
        {
            // Arrange
            var data = new List<TestData>
            {
                new TestData { Id = 1, Name = "John" },
                new TestData { Id = 2, Name = "Alice" },
                new TestData { Id = 3, Name = "Bob" }
            }.AsQueryable();

            var orderByQuery = new OrderByQuery("name:asc");
            
            // Act
            var result = orderByQuery.ApplyOrder(data).ToList();

            // Assert
            Assert.Equal(2, result[0].Id);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal(3, result[1].Id);
            Assert.Equal("Bob", result[1].Name);
            Assert.Equal(1, result[2].Id);
            Assert.Equal("John", result[2].Name);

        }

        [Fact]
        public void GetOrderByDirection_Should_Return_Expected_Result()
        {
            // Arrange
            var orderByQuery = new OrderByQuery("name:asc");

            // Act & Assert
            Assert.Equal(Direction.ASC, orderByQuery.GetOrderByDirection("asc"));
            Assert.Equal(Direction.DESC, orderByQuery.GetOrderByDirection("desc"));
            Assert.Throws<ArgumentException>(() => orderByQuery.GetOrderByDirection("invalid"));
        }

        private class TestData
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}