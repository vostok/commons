﻿using System;
using Vostok.Commons.Binary;
using Vostok.Commons.Parsers;

namespace Vostok.Commons
{
    [Serializable]
    public struct DataSize : IEquatable<DataSize>, IComparable<DataSize>, IBinarySerializable
    {
        public static DataSize FromBytes(long bytes) => 
            new DataSize(bytes);

        public static DataSize FromKilobytes(double kilobytes) => 
            new DataSize((long)(kilobytes * DataSizeConstants.Kilobyte));

        public static DataSize FromMegabytes(double megabytes) => 
            new DataSize((long)(megabytes * DataSizeConstants.Megabyte));

        public static DataSize FromGigabytes(double gigabytes) => 
            new DataSize((long)(gigabytes * DataSizeConstants.Gigabyte));

        public static DataSize FromTerabytes(double terabytes) => 
            new DataSize((long)(terabytes * DataSizeConstants.Terabyte));

        public static DataSize FromPetabytes(double petabytes) => 
            new DataSize((long)(petabytes * DataSizeConstants.Petabyte));
        public static DataSize Parse(string input) => 
            DataSizeParser.Parse(input);

        public static bool TryParse(string input, out DataSize result)
        {
            try
            {
                result = Parse(input);
                return true;
            }
            catch
            {
                result = default(DataSize);
                return false;
            }
        }

        public static DataSize DeserializeBinary(IBinaryDeserializer deserializer) => 
            new DataSize(deserializer.ReadInt64());

        private readonly long bytes;

        public DataSize(long bytes)
        {
            this.bytes = bytes;
        }

        public long Bytes => bytes;

        public double TotalKilobytes => bytes / (double)DataSizeConstants.Kilobyte;

        public double TotalMegabytes => bytes / (double)DataSizeConstants.Megabyte;

        public double TotalGigabytes => bytes / (double)DataSizeConstants.Gigabyte;

        public double TotalTerabytes => bytes / (double)DataSizeConstants.Terabyte;

        public double TotalPetabytes => bytes / (double)DataSizeConstants.Petabyte;

        public static explicit operator long(DataSize size)
        {
            return size.bytes;
        }

        public override string ToString() => ToString(true);

        public string ToString(bool shortFormat)
        {
            if (Math.Abs(TotalPetabytes) >= 1)
                return TotalPetabytes.ToString("0.##") + ' ' + (shortFormat ? "PB" : "petabytes");

            if (Math.Abs(TotalTerabytes) >= 1)
                return TotalTerabytes.ToString("0.##") + ' ' + (shortFormat ? "TB" : "terabytes");

            if (Math.Abs(TotalGigabytes) >= 1)
                return TotalGigabytes.ToString("0.##") + ' ' + (shortFormat ? "GB" : "gigabytes");

            if (Math.Abs(TotalMegabytes) >= 1)
                return TotalMegabytes.ToString("0.##") + ' ' + (shortFormat ? "MB" : "megabytes");

            if (Math.Abs(TotalKilobytes) >= 1)
                return TotalKilobytes.ToString("0.##") + ' ' + (shortFormat ? "KB" : "kilobytes");

            return bytes.ToString() + ' ' + (shortFormat ? "B" : "bytes");
        }

        public static DataSize operator +(DataSize size1, DataSize size2) => 
            new DataSize(size1.bytes + size2.bytes);

        public static DataSize operator -(DataSize size1, DataSize size2) => 
            new DataSize(size1.bytes - size2.bytes);

        public static DataSize operator *(DataSize size, int multiplier) => 
            new DataSize(size.bytes * multiplier);

        public static DataSize operator *(int multiplier, DataSize size) => 
            size * multiplier;

        public static DataSize operator *(DataSize size, long multiplier) => 
            new DataSize(size.bytes * multiplier);

        public static DataSize operator *(long multiplier, DataSize size) => 
            size * multiplier;

        public static DataSize operator *(DataSize size, double multiplier) => 
            new DataSize((long)(size.bytes * multiplier));

        public static DataSize operator *(double multiplier, DataSize size) => 
            size * multiplier;

        public static DataSize operator /(DataSize size, int multiplier) => 
            new DataSize(size.bytes / multiplier);

        public static DataSize operator /(DataSize size, long multiplier) => 
            new DataSize(size.bytes / multiplier);

        public static DataSize operator /(DataSize size, double multiplier) => 
            new DataSize((long)(size.bytes / multiplier));

        public static DataRate operator /(DataSize size, TimeSpan time) => 
            new DataRate((size / time.TotalSeconds).Bytes);

        public static TimeSpan operator /(DataSize size, DataRate speed) => 
            TimeSpan.FromSeconds(size.Bytes / (double)speed.BytesPerSecond);

        public static DataSize operator -(DataSize size) => 
            new DataSize(-size.bytes);

        public bool Equals(DataSize other) => 
            bytes == other.bytes;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is DataSize size && Equals(size);
        }

        public override int GetHashCode() => 
            bytes.GetHashCode();

        public static bool operator ==(DataSize left, DataSize right) => 
            left.Equals(right);

        public static bool operator !=(DataSize left, DataSize right) => 
            !left.Equals(right);

        public int CompareTo(DataSize other) => 
            bytes.CompareTo(other.bytes);

        public static bool operator >(DataSize size1, DataSize size2) => 
            size1.bytes > size2.bytes;

        public static bool operator >=(DataSize size1, DataSize size2) => 
            size1.bytes >= size2.bytes;

        public static bool operator <(DataSize size1, DataSize size2) => 
            size1.bytes < size2.bytes;

        public static bool operator <=(DataSize size1, DataSize size2) => 
            size1.bytes <= size2.bytes;

        public void SerializeBinary(IBinarySerializer serializer) => 
            serializer.Write(bytes);
    }
}