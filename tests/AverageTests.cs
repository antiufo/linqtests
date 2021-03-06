// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Xunit;

namespace System.Linq.Tests
{
    public class AverageTests
    {
        private struct NameNum<TNumeric>
        {
            public string name;
            public TNumeric num;
        }

        [Fact]
        public void SameResultsRepeatCallsIntQuery()
        {
            var q = from x in new[] { 9999, 0, 888, -1, 66, -777, 1, 2, -12345 }
                    where x > Int32.MinValue
                    select x;

            Assert.Equal(q.Average(), q.Average());
        }

        [Fact]
        public void SameResultsRepeatCallsNullableLongQuery()
        {
            var q = from x in new long?[] { Int32.MaxValue, 0, 255, 127, 128, 1, 33, 99, null, Int32.MinValue }
                    select x;

            Assert.Equal(q.Average(), q.Average());
        }

        [Fact]
        public void EmptyNullableFloatSource()
        {
            float?[] source = { };
            float? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void SingleNullableFloatSource()
        {
            float?[] source = { float.MinValue };
            float? expected = float.MinValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableFloatAllZeroSource()
        {
            float?[] source = { 0f, 0f, 0f, 0f, 0f };
            float? expected = 0f;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableFloatSource()
        {
            float?[] source = { 5.5f, 0, null, null, null, 15.5f, 40.5f, null, null, -23.5f };
            float? expected = 7.6f;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableFloatSourceOnlyOneNotNull()
        {
            float?[] source = { null, null, null, null, 45f };
            float? expected = 45f;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableFloatSourceAllNull()
        {
            float?[] source = { null, null, null, null, null };
            float? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void NullableFloatFromSelector()
        {
            NameNum<float?>[] source = new NameNum<float?>[]
            {
                new NameNum<float?>{name="Tim", num=5.5f},
                new NameNum<float?>{name="John", num=15.5f},
                new NameNum<float?>{name="Bob", num=null}
            };
            float? expected = 10.5f;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyIntSource()
        {
            int[] source = { };
            
            Assert.Throws<InvalidOperationException>(() => source.Average());
        }

        [Fact]
        public void SingleElementIntSource()
        {
            int[] source = { 5 };
            double expected = 5;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleIntValuesAllZero()
        {
            int[] source = { 0, 0, 0, 0, 0 };
            double expected = 0;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleIntSouce()
        {
            int[] source = { 5, -10, 15, 40, 28 };
            double expected = 15.6;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleIntFromSelector()
        {
            NameNum<int>[] source = new NameNum<int>[]
            {
                new NameNum<int>{name="Tim", num=10},
                new NameNum<int>{name="John", num=-10},
                new NameNum<int>{name="Bob", num=15}
            };
            double expected = 5;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyNullableIntSource()
        {
            int?[] source = { };
            double? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void SingleNullableIntSource()
        {
            int?[] source = { -5 };
            double? expected = -5;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableIntAllZeroSource()
        {
            int?[] source = { 0, 0, 0, 0, 0 };
            double? expected = 0;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableIntSource()
        {
            int?[] source = { 5, -10, null, null, null, 15, 40, 28, null, null };
            double? expected = 15.6;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableIntSourceOnlyOneNotNull()
        {
            int?[] source = { null, null, null, null, 50 };
            double? expected = 50;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableIntSourceAllNull()
        {
            int?[] source = { null, null, null, null, null };
            double? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void NullableIntFromSelector()
        {
            NameNum<int?>[] source = new NameNum<int?>[]
            {
                new NameNum<int?>{name="Tim", num=10},
                new NameNum<int?>{name="John", num=null},
                new NameNum<int?>{name="Bob", num=10}
            };
            double? expected = 10;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyLongSource()
        {
            long[] source = { };
            
            Assert.Throws<InvalidOperationException>(() => source.Average());
        }

        [Fact]
        public void SingleElementLongSource()
        {
            long[] source = { Int64.MaxValue };
            double expected = Int64.MaxValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleLongValuesAllZero()
        {
            long[] source = { 0, 0, 0, 0, 0 };
            double expected = 0;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleLongValues()
        {
            long[] source = { 5, -10, 15, 40, 28 };
            double expected = 15.6;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleLongFromSelector()
        {
            NameNum<long>[] source = new NameNum<long>[]
            {
                new NameNum<long>{name="Tim", num=40L},
                new NameNum<long>{name="John", num=50L},
                new NameNum<long>{name="Bob", num=60L}
            };
            double expected = 50;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void OverflowFromSummingLong()
        {
            long[] source = { Int64.MaxValue, Int64.MaxValue };

            Assert.Throws<OverflowException>(() => source.Average());
        }

        [Fact]
        public void EmptyNullableLongSource()
        {
            long?[] source = { };
            double? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void SingleNullableLongSource()
        {
            long?[] source = { Int64.MinValue };
            double? expected = Int64.MinValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableLongAllZeroSource()
        {
            long?[] source = { 0, 0, 0, 0, 0 };
            double? expected = 0;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableLongSource()
        {
            long?[] source = { 5, -10, null, null, null, 15, 40, 28, null, null };
            double? expected = 15.6;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableLongSourceOnlyOneNotNull()
        {
            long?[] source = { null, null, null, null, 50 };
            double? expected = 50;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableLongSourceAllNull()
        {
            long?[] source = { null, null, null, null, null };
            double? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void NullableLongFromSelector()
        {
            NameNum<long?>[] source = new NameNum<long?>[]
            {
                new NameNum<long?>{name="Tim", num=40L},
                new NameNum<long?>{name="John", num=null},
                new NameNum<long?>{name="Bob", num=30L}
            };
            double? expected = 35;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyDoubleSource()
        {
            double[] source = { };
            
            Assert.Throws<InvalidOperationException>(() => source.Average());
        }

        [Fact]
        public void SingleElementDoubleSource()
        {
            double[] source = { Double.MaxValue };
            double expected = Double.MaxValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDoubleValuesAllZero()
        {
            double[] source = { 0.0, 0.0, 0.0, 0.0, 0.0 };
            double expected = 0;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDoubleValues()
        {
            double[] source = { 5.5, -10, 15.5, 40.5, 28.5 };
            double expected = 16;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDoubleValuesIncludingNaN()
        {
            double[] source = { 5.58, Double.NaN, 30, 4.55, 19.38 };
            double expected = Double.NaN;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDoubleFromSelector()
        {
            NameNum<double>[] source = new NameNum<double>[]
            {
                new NameNum<double>{name="Tim", num=5.5},
                new NameNum<double>{name="John", num=15.5},
                new NameNum<double>{name="Bob", num=3.0}
            };
            double expected = 8.0;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyNullableDoubleSource()
        {
            double?[] source = { };
            double? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void SingleNullableDoubleSource()
        {
            double?[] source = { Double.MinValue };
            double? expected = Double.MinValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDoubleAllZeroSource()
        {
            double?[] source = { 0, 0, 0, 0, 0 };
            double? expected = 0;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDoubleSource()
        {
            double?[] source = { 5.5, 0, null, null, null, 15.5, 40.5, null, null, -23.5 };
            double? expected = 7.6;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDoubleSourceOnlyOneNotNull()
        {
            double?[] source = { null, null, null, null, 45 };
            double? expected = 45;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDoubleSourceAllNull()
        {
            double?[] source = { null, null, null, null, null };
            double? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDoubleSourceIncludingNaN()
        {
            double?[] source = { -23.5, 0, Double.NaN, 54.3, 0.56 };
            double? expected = Double.NaN;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void NullableDoubleFromSelector()
        {
            NameNum<double?>[] source = new NameNum<double?>[]
            {
                new NameNum<double?>{name="Tim", num=5.5},
                new NameNum<double?>{name="John", num=15.5},
                new NameNum<double?>{name="Bob", num=null}
            };
            double? expected = 10.5;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyDecimalSource()
        {
            decimal[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.Average());
        }

        [Fact]
        public void SingleElementDecimalSource()
        {
            decimal[] source = { Decimal.MaxValue };
            decimal expected = Decimal.MaxValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDecimalValuesAllZero()
        {
            decimal[] source = { 0.0m, 0.0m, 0.0m, 0.0m, 0.0m };
            decimal expected = 0m;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDecimalValues()
        {
            decimal[] source = { 5.5m, -10m, 15.5m, 40.5m, 28.5m };
            decimal expected = 16m;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleDecimalFromSelector()
        {
            NameNum<decimal>[] source = new NameNum<decimal>[]
            {
                new NameNum<decimal>{name="Tim", num=5.5m},
                new NameNum<decimal>{name="John", num=15.5m},
                new NameNum<decimal>{name="Bob", num=3.0m}
            };
            decimal expected = 8.0m;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void EmptyNullableDecimalSource()
        {
            decimal?[] source = { };
            decimal? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void SingleNullableDecimalSource()
        {
            decimal?[] source = { Decimal.MinValue };
            decimal? expected = Decimal.MinValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableeDecimalAllZeroSource()
        {
            decimal?[] source = { 0m, 0m, 0m, 0m, 0m };
            decimal? expected = 0m;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableeDecimalSource()
        {
            decimal?[] source = { 5.5m, 0, null, null, null, 15.5m, 40.5m, null, null, -23.5m };
            decimal? expected = 7.6m;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDecimalSourceOnlyOneNotNull()
        {
            decimal?[] source = { null, null, null, null, 45m };
            decimal? expected = 45m;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleNullableDecimalSourceAllNull()
        {
            decimal?[] source = { null, null, null, null, null };
            decimal? expected = null;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void NullableDecimalFromSelector()
        {
            NameNum<decimal?>[] source = new NameNum<decimal?>[]
            {
                new NameNum<decimal?>{name="Tim", num=5.5m},
                new NameNum<decimal?>{name="John", num=15.5m},
                new NameNum<decimal?>{name="Bob", num=null}
            };
            decimal? expected = 10.5m;

            Assert.Equal(expected, source.Average(e => e.num));
        }

        [Fact]
        public void OverflowFromSummingDecimal()
        {
            decimal?[] source = { decimal.MaxValue, decimal.MaxValue };

            Assert.Throws<OverflowException>(() => source.Average());
        }

        [Fact]
        public void EmptyFloatSource()
        {
            float[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.Average());
        }

        [Fact]
        public void SingleElementFloatSource()
        {
            float[] source = { float.MaxValue };
            float expected = float.MaxValue;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleFloatValuesAllZero()
        {
            float[] source = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
            float expected = 0f;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleFloatValues()
        {
            float[] source = { 5.5f, -10f, 15.5f, 40.5f, 28.5f };
            float expected = 16f;

            Assert.Equal(expected, source.Average());
        }

        [Fact]
        public void MultipleFloatFromSelector()
        {
            NameNum<float>[] source = new NameNum<float>[]
            {
                new NameNum<float>{name="Tim", num=5.5f},
                new NameNum<float>{name="John", num=15.5f},
                new NameNum<float>{name="Bob", num=3.0f}
            };
            float expected = 8.0f;

            Assert.Equal(expected, source.Average(e => e.num));
        }
    }
}
