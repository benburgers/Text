﻿/*
 * © 2022-2024 Ben Burgers and contributors.
 * This work is licensed by GNU General Public License version 3.
 */

using BenBurgers.Text.Csv.Mapping;
using System.Collections;
using static BenBurgers.Text.Csv.Tests.CsvReaders.CsvReaderTestGenericValues;

namespace BenBurgers.Text.Csv.Tests.CsvWriters;

public class CsvWriterTestGenericValues : IEnumerable<object?[]>
{
    private static readonly IEnumerable<object?[]> Values =
        new[]
        {
            new object?[]
            {
                new CsvOptions<MockCsvRecord>(new CsvConverterMapping<MockCsvRecord>(
                    record => new string[] { record.Value1, record.Value2.ToString(), record.Value3, record.Value4 },
                    rawValues => new(rawValues[0], int.Parse(rawValues[1]), rawValues[2], rawValues[3]))),
                new[]
                {
                    new MockCsvRecord("abc", 123, "ABC", "!@#"),
                    new MockCsvRecord("def", 456, "DEF", "$%^")
                },
@"abc,123,ABC,!@#
def,456,DEF,$%^
"
            },
            new object?[]
            {
                new CsvOptions<MockCsvRecord>(new CsvHeaderMapping<MockCsvRecord>(
                    new [] { "Value1", "Value2", "Value3", "Value4" }.ToHashSet(),
                    rawValues => new(
                        rawValues[nameof(MockCsvRecord.Value1)],
                        int.Parse(rawValues[nameof(MockCsvRecord.Value2)]),
                        rawValues[nameof(MockCsvRecord.Value3)],
                        rawValues[nameof(MockCsvRecord.Value4)]),
                    new Dictionary<string, Func<MockCsvRecord, string?>>
                    {
                        { nameof(MockCsvRecord.Value1), m => m.Value1 },
                        { nameof(MockCsvRecord.Value2), m => m.Value2.ToString() },
                        { nameof(MockCsvRecord.Value3), m => m.Value3 },
                        { nameof(MockCsvRecord.Value4), m => m.Value4 }
                    })
                ),
                new[]
                {
                    new MockCsvRecord("abc", 123, "ABC", "!@#"),
                    new MockCsvRecord("def", 456, "DEF", "$%^")
                },
@"Value1,Value2,Value3,Value4
abc,123,ABC,!@#
def,456,DEF,$%^
"
            },
            new object?[] {
                new CsvOptions<MockCsvRecord>(new CsvHeaderTypeMapping<MockCsvRecord>()),
                new[]
                {
                    new MockCsvRecord("abc", 123, "ABC", "!@#"),
                    new MockCsvRecord("def", 456, "DEF", "$%^")
                },
@"Value1,Value2,Value3,Value4
abc,123,ABC,!@#
def,456,DEF,$%^
"
            },
            new object?[]
            {
                new CsvOptions<MockCsvRecord>(new CsvConverterMapping<MockCsvRecord>(
                    record => new string[] { record.Value1, record.Value2.ToString(), record.Value3, record.Value4 },
                    rawValues => new(rawValues[0], int.Parse(rawValues[1]), rawValues[2], rawValues[3])),
                    Delimiter: ';'),
                new[]
                {
                    new MockCsvRecord("abc", 123, "ABC", "!@#"),
                    new MockCsvRecord("def", 456, "DEF", "$%^")
                },
@"abc;123;ABC;!@#
def;456;DEF;$%^
"
            },
            new object?[]
            {
                new CsvOptions<MockCsvRecordWithAttributes>(new CsvHeaderTypeMapping<MockCsvRecordWithAttributes>()),
                new[]
                {
                    new MockCsvRecordWithAttributes("cba", 321, "def", "abc"),
                    new MockCsvRecordWithAttributes("tre", 123, "zxy", "qwe")
                },
@"myValue,nextValue,Value3,Value4
cba,321,def,abc
tre,123,zxy,qwe
",
            }
        };

    public IEnumerator<object?[]> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
