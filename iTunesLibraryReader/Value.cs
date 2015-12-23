
namespace iTunesLibraryReader
{
    /// <summary>
    /// An immutable boxed XML value struct, implementing a generic type would prevent listing values of different types
    /// </summary>
    public struct Value
    {
        /// <summary>
        /// Type of the value stored
        /// </summary>
        public enum ValueType
        {
            STRING,
            BOOL,
            INTEGER
        }

        #region Properties
        private readonly object _value;
        /// <summary>
        /// Boxed value
        /// </summary>
        public object value
        {
            get { return this._value; }
        }

        private readonly ValueType _type;
        /// <summary>
        /// Type of the boxed value
        /// </summary>
        public ValueType type
        {
            get { return this._type; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new boxed value from the given value and type
        /// </summary>
        /// <param name="value">Boxed value</param>
        /// <param name="type">Type of the value</param>
        public Value(object value, ValueType type)
        {
            this._type = type;
            this._value = value;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the type and string representation of the value
        /// </summary>
        /// <returns>String representation of this value</returns>
        public override string ToString()
        {
            switch (this._type)
            {
                case ValueType.STRING:
                    return "String: " + this._value.ToString();

                case ValueType.BOOL:
                    return "Bool: " + this._value.ToString();

                case ValueType.INTEGER:
                    return "Integer: " + this._value.ToString();

                default:
                    return string.Empty;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tries to get a String out of the boxed value
        /// </summary>
        /// <param name="result">Variable to store the result into</param>
        /// <returns>If the conversion succeeded</returns>
        public bool TryGetString(ref string result)
        {
            if (this._type == ValueType.STRING)
            {
                result = (string)this._value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to get an Integer out of the boxed value
        /// </summary>
        /// <param name="result">Variable to store the result into</param>
        /// <returns>If the conversion succeeded</returns>
        public bool TryGetBool(ref bool result)
        {
            if (this._type == ValueType.BOOL)
            {
                result = (bool)this._value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to get a Bool out of the boxed value
        /// </summary>
        /// <param name="result">Variable to store the result into</param>
        /// <returns>If the conversion succeeded</returns>
        public bool TryGetInt(ref int result)
        {
            if (this._type == ValueType.INTEGER)
            {
                result = (int)this._value;
                return true;
            }
            return false;
        }
        #endregion
    }
}
