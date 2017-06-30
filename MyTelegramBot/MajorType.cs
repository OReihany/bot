using System.ComponentModel;

namespace MyTelegramBot
{
    public enum MajorType
    {
        [Description("الکترونیک")]
        Electronic =1,
        [Description("سیستم های قدرت")]
        PowerSystems =2,
        [Description("ماشین های الکتریکی")]
        Machine =3,
        [Description("مخابرات میدان")]
        FieldComunication =4,
        [Description("مخابرات سیستم")]
        SystemComunication =5,
        [Description("کنترل")]
        Control =6,
        [Description("مهندسی پزشکی")]
        MedicalEngineering =7,
        [Description("مکاترونیک")]
        Mecatronic =8
    }
}