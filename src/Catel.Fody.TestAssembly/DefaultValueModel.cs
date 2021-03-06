﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultValueModel.cs" company="Catel development team">
//   Copyright (c) 2008 - 2013 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Catel.Fody.TestAssembly
{
    using System.ComponentModel;
    using Catel.Data;

    public enum ExampleEnum
    {
        A,

        B,

        C
    }

    public class DefaultValueModel : ModelBase
    {
        [DefaultValue("Geert")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DefaultValue(true)]
        public bool BoolValue { get; set; }

        [DefaultValue(null)]
        public bool? NullableBoolDefaultNullValue { get; set; }

        [DefaultValue(true)]
        public bool? NullableBoolDefaultTrueValue { get; set; }

        [DefaultValue(false)]
        public bool? NullableBoolDefaultFalseValue { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool BoolValueCatel
        {
            get { return GetValue<bool>(BoolValueCatelProperty); }
            set { SetValue(BoolValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the BoolValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BoolValueCatelProperty = RegisterProperty("BoolValueCatel", typeof(bool), true);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool? NullableBoolDefaultNullValueCatel
        {
            get { return GetValue<bool?>(NullableBoolDefaultNullValueCatelProperty); }
            set { SetValue(NullableBoolDefaultNullValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the BoolValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData NullableBoolDefaultNullValueCatelProperty = RegisterProperty("NullableBoolDefaultNullValueCatel", typeof(bool?), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool? NullableBoolDefaultTrueValueCatel
        {
            get { return GetValue<bool?>(NullableBoolDefaultTrueValueCatelProperty); }
            set { SetValue(NullableBoolDefaultTrueValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the BoolValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData NullableBoolDefaultTrueValueCatelProperty = RegisterProperty("NullableBoolDefaultTrueValueCatel", typeof(bool?), true);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool? NullableBoolDefaultFalseValueCatel
        {
            get { return GetValue<bool?>(NullableBoolDefaultFalseValueCatelProperty); }
            set { SetValue(NullableBoolDefaultFalseValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the BoolValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData NullableBoolDefaultFalseValueCatelProperty = RegisterProperty("NullableBoolDefaultFalseValueCatel", typeof(bool?), false);

        [DefaultValue(42)]
        public int IntValue { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int IntValueCatel
        {
            get { return GetValue<int>(IntValueCatelProperty); }
            set { SetValue(IntValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the IntValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IntValueCatelProperty = RegisterProperty("IntValueCatel", typeof(int), 42);

        [DefaultValue(42L)]
        public long LongValue { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public long LongValueCatel
        {
            get { return GetValue<long>(LongValueCatelProperty); }
            set { SetValue(LongValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the LongValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData LongValueCatelProperty = RegisterProperty("LongValueCatel", typeof(long), 42L);

        [DefaultValue(42d)]
        public double DoubleValue { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public double DoubleValueCatel
        {
            get { return GetValue<double>(DoubleValueCatelProperty); }
            set { SetValue(DoubleValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the DoubleValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DoubleValueCatelProperty = RegisterProperty("DoubleValueCatel", typeof(double));

        [DefaultValue(42f)]
        public float FloatValue { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public float FloatValueCatel
        {
            get { return GetValue<float>(FloatValueCatelProperty); }
            set { SetValue(FloatValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the FloatValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FloatValueCatelProperty = RegisterProperty("FloatValueCatel", typeof(float), 42f);

        [DefaultValue(ExampleEnum.B)]
        public ExampleEnum EnumValue { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ExampleEnum EnumValueCatel
        {
            get { return GetValue<ExampleEnum>(EnumValueCatelProperty); }
            set { SetValue(EnumValueCatelProperty, value); }
        }

        /// <summary>
        /// Register the EnumValueCatel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData EnumValueCatelProperty = RegisterProperty("EnumValueCatel", typeof(ExampleEnum), ExampleEnum.B);
    }
}