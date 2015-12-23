using System;
using System.Collections.Generic;

namespace iTunesLibraryReader
{
    /// <summary>
    /// An immutable structure reprsenting a file size in bytes
    /// </summary>
    public struct FileSize : IEquatable<FileSize>, IComparable<FileSize>
    {
        /// <summary>
        /// Enum representing a postfix for size magnitudes
        /// </summary>
        public enum FilePostfix
        {
            BYTE = 0,
            KILOBYTE = 1,
            MEGABYTE = 2,
            GIGABYTE = 3,
            TERABYTE = 4
        }

        #region Constants
        /// <summary>
        /// The amount of bytes per orders of magnitude
        /// </summary>
        private const double bytesPerPrefix = 1024d;
        #endregion

        #region Static Fields
        /// <summary>
        /// Dictionary of FileSize -> string postfix conversions
        /// </summary>
        private static readonly Dictionary<FilePostfix, string> sizes;
        /// <summary>
        /// Empty filesize
        /// </summary>
        public static readonly FileSize zero;
        #endregion

        #region Fields
        /// <summary>
        /// Amount of bytes to the file
        /// </summary>
        private readonly ulong bytes;
        /// <summary>
        /// Decimal size of the file
        /// </summary>
        public readonly double size;
        /// <summary>
        /// Postfix of the file
        /// </summary>
        public readonly FilePostfix postfix;
        #endregion

        #region Constructors
        /// <summary>
        /// Initiates the static values
        /// </summary>
        static FileSize()
        {
            sizes = new Dictionary<FilePostfix, string>(5)
            {
                { FilePostfix.BYTE, " bytes" },
                { FilePostfix.KILOBYTE, "KB" },
                { FilePostfix.MEGABYTE, "MB" },
                { FilePostfix.GIGABYTE, "GB" },
                { FilePostfix.TERABYTE, "TB" }
            };
            zero = new FileSize();
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        private FileSize(ulong bytes)
        {
            this.bytes = bytes;
            double temp = bytes;
            this.size = temp;
            for (int i = 0; i < 4; i++)
            {
                temp /= bytesPerPrefix;
                if (temp < 1d)
                {
                    this.postfix = (FilePostfix)i;
                    if (i > 0) { this.size = Math.Round(this.size, 2, MidpointRounding.AwayFromZero); }
                    return;
                }
                this.size = temp;
            }
            this.postfix = FilePostfix.TERABYTE;
            this.size = Math.Round(this.size, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Cloning constructor creating a new FileSize from the given size
        /// </summary>
        /// <param name="size"></param>
        private FileSize(FileSize size)
        {
            this.bytes = size.bytes;
            this.size = size.size;
            this.postfix = size.postfix;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the amount of bytes of this size
        /// </summary>
        /// <returns>Size in bytes</returns>
        public ulong ToBytes()
        {
            return this.bytes;
        }

        /// <summary>
        /// Adds the two sizes and returns a new object of the cummulated sizes
        /// </summary>
        /// <param name="size">Size to add to this instance</param>
        /// <returns>New cummulative size</returns>
        public FileSize Add(FileSize size)
        {
            return Add(this, size);
        }

        /// <summary>
        /// Prints a string representation of this size with it's postfix
        /// </summary>
        /// <returns>String of this size</returns>
        public override string ToString()
        {
            return size + sizes[this.postfix];
        }

        /// <summary>
        /// If the passed value is equal to this one
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>If the objects are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is FileSize)
            {
                return Equals((FileSize)obj);
            }
            return false;
        }

        /// <summary>
        /// If the passed FileSize is equal to this one
        /// </summary>
        /// <param name="size">FileSize to compare</param>
        /// <returns>If the sizes are equal</returns>
        public bool Equals(FileSize size)
        {
            return this.bytes.Equals(size.bytes);
        }

        /// <summary>
        /// Compares the two sizes and determines which is largest
        /// </summary>
        /// <param name="size">Size to compare to</param>
        /// <returns>1 if this instance is larger, -1 if it's smaller, 0 if both are equal</returns>
        public int CompareTo(FileSize size)
        {
            if (this.bytes == size.bytes) { return 0; }
            return this.bytes > size.bytes ? 1 : -1;
        }

        /// <summary>
        /// Hashcode of this size (implemented through the underlying UInt64 hashcode)
        /// </summary>
        /// <returns>Hash of this object</returns>
        public override int GetHashCode()
        {
            return this.bytes.GetHashCode();
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Adds the two provided FileSizes and returns the result in a new object
        /// </summary>
        /// <param name="a">First size</param>
        /// <param name="b">Second size</param>
        /// <returns>Result of the addition</returns>
        public static FileSize Add(FileSize a, FileSize b)
        {
            if (a == zero) { return new FileSize(b); }
            if (b == zero) { return new FileSize(a); }

            return new FileSize(checked(a.bytes + b.bytes));
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes as an UInt64
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        /// <returns>New FileSize for the bytes</returns>
        public static FileSize FromUInt64(ulong bytes)
        {
            return new FileSize(bytes);
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes as an Int64
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        /// <returns>New FileSize for the bytes</returns>
        public static FileSize FromInt64(long bytes)
        {
            if (bytes < 0) { throw new ArgumentOutOfRangeException("bytes", "File size cannot be negative"); }
            return new FileSize((ulong)bytes);
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes as an UNint32
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        /// <returns>New FileSize for the bytes</returns>
        public static FileSize FromUInt32(uint bytes)
        {
            return new FileSize(bytes);
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes as an Int32
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        /// <returns>New FileSize for the bytes</returns>
        public static FileSize FromInt32(int bytes)
        {
            if (bytes < 0) { throw new ArgumentOutOfRangeException("bytes", "File size cannot be negative"); }
            return new FileSize((uint)bytes);
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes as an UInt16
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        /// <returns>New FileSize for the bytes</returns>
        public static FileSize FromUInt16(ushort bytes)
        {
            return new FileSize(bytes);
        }

        /// <summary>
        /// Creates a new FileSize from the given amount of bytes as an Int16
        /// </summary>
        /// <param name="bytes">Amount of bytes in the size</param>
        /// <returns>New FileSize for the bytes</returns>
        public static FileSize FromInt16(short bytes)
        {
            if (bytes < 0) { throw new ArgumentOutOfRangeException("bytes", "File size cannot be negative"); }
            return new FileSize((ushort)bytes);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds two FileSizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>Result of the addition</returns>
        public static FileSize operator +(FileSize left, FileSize right)
        {
            return Add(left, right);
        }

        /// <summary>
        /// Tests the equality of two sizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>If the sizes are equal</returns>
        public static bool operator ==(FileSize left, FileSize right)
        {
            return left.bytes == right.bytes;
        }

        /// <summary>
        /// Tests the inequality of two sizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>If the sizes are inequal</returns>
        public static bool operator !=(FileSize left, FileSize right)
        {
            return left.bytes != right.bytes;
        }

        /// <summary>
        /// Compares the two sizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>If the left size is larger than the right</returns>
        public static bool operator <(FileSize left, FileSize right)
        {
            return left.bytes < right.bytes;
        }

        /// <summary>
        /// Compares the two sizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>If the left size is smaller than the right</returns>
        public static bool operator >(FileSize left, FileSize right)
        {
            return left.bytes > right.bytes;
        }

        /// <summary>
        /// Compares the two sizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>If the left size is larger or equal to the right</returns>
        public static bool operator <=(FileSize left, FileSize right)
        {
            return left.bytes <= right.bytes;
        }

        /// <summary>
        /// Compares the two sizes
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>If the left size is smaller or equal to the right</returns>
        public static bool operator >=(FileSize left, FileSize right)
        {
            return left.bytes >= right.bytes;
        }
        #endregion
    }
}
