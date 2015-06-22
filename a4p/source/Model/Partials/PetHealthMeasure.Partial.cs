using System;
using Model.Interfaces;

namespace Model
{
    public partial class PetHealthMeasure : IMedicalData
    {
        public HealthMeasureGroupEnum HealthMeasureGroupEnum
        {
            get
            {
                switch (HealthMeasureTypeId)
                {
                    case HealthMeasureTypeEnum.Height:
                        return HealthMeasureGroupEnum.Vitals;
                    case HealthMeasureTypeEnum.Weight:
                        return HealthMeasureGroupEnum.Vitals;
                    case HealthMeasureTypeEnum.Temperature:
                        return HealthMeasureGroupEnum.Vitals;
                    case HealthMeasureTypeEnum.Pulse:
                        return HealthMeasureGroupEnum.Vitals;
                    case HealthMeasureTypeEnum.CBG:
                        return HealthMeasureGroupEnum.CBG;
                    case HealthMeasureTypeEnum.Hemogram:
                        return HealthMeasureGroupEnum.Hemogram;
                    case HealthMeasureTypeEnum.Hemoglobin:
                        return HealthMeasureGroupEnum.Hemoglobin;
                    case HealthMeasureTypeEnum.SerumElectrolytes:
                        return HealthMeasureGroupEnum.SerumElectrolytes;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
