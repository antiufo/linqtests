// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace System.Linq.Tests
{
    public class ZipTests
    {
        [Fact]
        public void ImplicitTypeParameters()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = new int[] { 2, 5, 9 };
            IEnumerable<int> expected = new int[] { 3, 7, 12 };

            Assert.Equal(expected, first.Zip(second, (x, y) => x + y));
        }
        
        [Fact]
        public void ExplicitTypeParameters()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = new int[] { 2, 5, 9 };
            IEnumerable<int> expected = new int[] { 3, 7, 12 };

            Assert.Equal(expected, first.Zip<int, int, int>(second, (x, y) => x + y));
        }

        [Fact]
        public void FirstIsNull()
        {
            IEnumerable<int> first = null;
            IEnumerable<int> second = new int[] { 2, 5, 9 };
            
            Assert.Throws<ArgumentNullException>(() => first.Zip<int, int, int>(second, (x, y) => x + y));
        }

        [Fact]
        public void SecondIsNull()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = null;
            
            Assert.Throws<ArgumentNullException>(() => first.Zip<int, int, int>(second, (x, y) => x + y));
        }
        
        [Fact]
        public void FuncIsNull()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = new int[] { 2, 4, 6 };
            Func<int, int, int> func = null;
            
            Assert.Throws<ArgumentNullException>(() => first.Zip(second, func));
        }

        private class MyIEnum<T> : IEnumerable<T>
        {
            public IEnumerable<T> _data;

            public MyIEnum(IEnumerable<T> source)
            {
                _data = source;
            }

            public IEnumerator<T> GetEnumerator()
            {
                foreach (var datum in _data)
                {
                    if (datum.Equals(2)) throw new Exception();
                    yield return datum;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        [Fact]
        public void ExceptionThrownFromFirstsEnumerator()
        {
            MyIEnum<int> first = new MyIEnum<int>(new int[] { 1, 3, 3 });
            IEnumerable<int> second = new int[] { 2, 4, 6 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 7, 9 };
            
            Assert.Equal(expected, first.Zip(second, func));

            first = new MyIEnum<int>(new int[] { 1, 2, 3 });
            
            var zip = first.Zip(second, func);
            
            Assert.Throws<Exception>(() => zip.ToList());
        }

        [Fact]
        public void ExceptionThrownFromSecondsEnumerator()
        {
            MyIEnum<int> second = new MyIEnum<int>(new int[] { 1, 3, 3 });
            IEnumerable<int> first = new int[] { 2, 4, 6 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 7, 9 };

            Assert.Equal(expected, first.Zip(second, func));

            second = new MyIEnum<int>(new int[] { 1, 2, 3 });
            
            var zip = first.Zip(second, func);

            Assert.Throws<Exception>(() => zip.ToList());
        }

        [Fact]
        public void FirstAndSecondEmpty()
        {
            IEnumerable<int> first = new int[] { };
            IEnumerable<int> second = new int[] { };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { };

            Assert.Equal(expected, first.Zip(second, func));
        }


        [Fact]
        public void FirstEmptySecondSingle()
        {
            IEnumerable<int> first = new int[] { };
            IEnumerable<int> second = new int[] { 2 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstEmptySecondMany()
        {
            IEnumerable<int> first = new int[] { };
            IEnumerable<int> second = new int[] { 2, 4, 8 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { };

            Assert.Equal(expected, first.Zip(second, func));
        }


        [Fact]
        public void SecondEmptyFirstSingle()
        {
            IEnumerable<int> first = new int[] { 1 };
            IEnumerable<int> second = new int[] { };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondEmptyFirstMany()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = new int[] { };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstAndSecondSingle()
        {
            IEnumerable<int> first = new int[] { 1 };
            IEnumerable<int> second = new int[] { 2 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstAndSecondEqualSize()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = new int[] { 2, 3, 4 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 5, 7 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondOneMoreThanFirst()
        {
            IEnumerable<int> first = new int[] { 1, 2 };
            IEnumerable<int> second = new int[] { 2, 4, 8 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 6 };

            Assert.Equal(expected, first.Zip(second, func));
        }


        [Fact]
        public void SecondManyMoreThanFirst()
        {
            IEnumerable<int> first = new int[] { 1, 2 };
            IEnumerable<int> second = new int[] { 2, 4, 8, 16 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 6 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstOneMoreThanSecond()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3 };
            IEnumerable<int> second = new int[] { 2, 4 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 6 };

            Assert.Equal(expected, first.Zip(second, func));
        }


        [Fact]
        public void FirstManyMoreThanSecond()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int> second = new int[] { 2, 4 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 6 };

            Assert.Equal(expected, first.Zip(second, func));
        }


        [Fact]
        public void DelegateFuncChanged()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int> second = new int[] { 2, 4, 8 };
            Func<int, int, int> func = (x, y) => x + y;
            IEnumerable<int> expected = new int[] { 3, 6, 11 };

            Assert.Equal(expected, first.Zip(second, func));

            func = (x, y) => x - y;
            expected = new int[] { -1, -2, -5 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void LambdaFuncChanged()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int> second = new int[] { 2, 4, 8 };
            IEnumerable<int> expected = new int[] { 3, 6, 11 };

            Assert.Equal(expected, first.Zip(second, (x, y) => x + y));

            expected = new int[] { -1, -2, -5 };

            Assert.Equal(expected, first.Zip(second, (x, y) => x - y));
        }

        [Fact]
        public void FirstHasFirstElementNull()
        {
            IEnumerable<int?> first = new[] { (int?)null, 2, 3, 4 };
            IEnumerable<int> second = new int[] { 2, 4, 8 };
            Func<int?, int, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, 6, 11 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstHasLastElementNull()
        {
            IEnumerable<int?> first = new[] { 1, 2, (int?)null };
            IEnumerable<int> second = new int[] { 2, 4, 6, 8 };
            Func<int?, int, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { 3, 6, null };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstHasMiddleNullValue()
        {
            IEnumerable<int?> first = new[] { 1, (int?)null, 3 };
            IEnumerable<int> second = new int[] { 2, 4, 6, 8 };
            Func<int?, int, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { 3, null, 9 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstAllElementsNull()
        {
            IEnumerable<int?> first = new int?[] { null, null, null };
            IEnumerable<int> second = new int[] { 2, 4, 6, 8 };
            Func<int?, int, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, null, null };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondHasFirstElementNull()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int?> second = new int?[] { null, 4, 6 };
            Func<int, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, 6, 9 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondHasLastElementNull()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int?> second = new int?[] { 2, 4, null };
            Func<int, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { 3, 6, null };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondHasMiddleElementNull()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int?> second = new int?[] { 2, null, 6 };
            Func<int, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { 3, null, 9 };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondHasAllElementsNull()
        {
            IEnumerable<int> first = new int[] { 1, 2, 3, 4 };
            IEnumerable<int?> second = new int?[] { null, null, null };
            Func<int, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, null, null };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void SecondLargerFirstAllNull()
        {
            IEnumerable<int?> first = new int?[] { null, null, null, null };
            IEnumerable<int?> second = new int?[] { null, null, null };
            Func<int?, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, null, null };

            Assert.Equal(expected, first.Zip(second, func));
        }


        [Fact]
        public void FirstSameSizeSecondAllNull()
        {
            IEnumerable<int?> first = new int?[] { null, null, null };
            IEnumerable<int?> second = new int?[] { null, null, null };
            Func<int?, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, null, null };

            Assert.Equal(expected, first.Zip(second, func));
        }

        [Fact]
        public void FirstSmallerSecondAllNull()
        {
            IEnumerable<int?> first = new int?[] { null, null, null };
            IEnumerable<int?> second = new int?[] { null, null, null, null };
            Func<int?, int?, int?> func = (x, y) => x + y;
            IEnumerable<int?> expected = new int?[] { null, null, null };

            Assert.Equal(expected, first.Zip(second, func));
        }
    }
}